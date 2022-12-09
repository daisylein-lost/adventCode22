using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace advent
{
    public class day9
    {
        public void part1(string[] input)
        {
            var startPoint = new Location(0, 0);
            startPoint.isHead = true;
            startPoint.isTail = true;
            var allPoints = new List<Location>();
            allPoints.Add(startPoint);

            foreach (var move in input)
            {
                moveHead(move, allPoints);
            }

            Console.WriteLine($"Total count: {allPoints.Where(p => p.wasTail || p.isTail).Count()}");
            
        }

        private void moveHead(string rawCommand, List<Location> allPoints)
        {
            var commands = rawCommand.Split(" ");
            var times = int.Parse(commands[1]);
            var direction = commands[0][0];

            for (int i = 0; i < times; i++)
            {
                moveOnce(allPoints, direction);
            }
        }

        private void moveOnce(List<Location> allPoints, char direction)
        {
            moveHeadOnce(allPoints, direction);

            if (!AreHeadAndTailTouching(allPoints))
            {
                moveTailToHead(allPoints);
            }
        }

        private void moveTailToHead(List<Location> allPoints)
        {
            var head = allPoints.First(p => p.isHead);
            var tail = allPoints.First(p => p.isTail);
            var move = 'N';

            if(head.yCoordinate == tail.yCoordinate)
            {
                move = (head.xCoordinate - tail.xCoordinate) >  0 ? 'R' : 'L';

            }
            else if(head.xCoordinate == tail.xCoordinate)
            {
                move = (head.yCoordinate - tail.yCoordinate) > 0 ? 'U' : 'D';
            }
            else if((head.xCoordinate - tail.xCoordinate > 0)) // need to move dioganly right
            {
                move = (head.yCoordinate - tail.yCoordinate) > 0 ? 'W' : 'X';
            }
            else // need to move dioganly left
            {
                move = (head.yCoordinate - tail.yCoordinate) > 0 ? 'Y' : 'Z';
            }

            moveTailOnce(allPoints, move);
        }

        private bool AreHeadAndTailTouching(List<Location> allPoints)
        {
            var head = allPoints.First(p => p.isHead);
            var tail = allPoints.First(p => p.isTail);

            if (Math.Abs(head.yCoordinate-tail.yCoordinate)<=1 && Math.Abs(head.xCoordinate-tail.xCoordinate)<=1)
            {
                return true;
            }

            return false;
        }

        private void moveHeadOnce(List<Location> allPoints, char direction)
        {
            var positionHeadOld = allPoints.First(p => p.isHead);

            var newPoint = movePoint(allPoints, direction, p => p.isHead);

            positionHeadOld.isHead = false;
            allPoints.First(p => p.Equals(newPoint)).isHead = true;
        }

        private void moveTailOnce(List<Location> allPoints, char direction)
        {
            var positionTail = allPoints.First(p => p.isTail);

            var newPoint = movePoint(allPoints, direction, p => p.isTail);

            positionTail.isTail = false;
            positionTail.wasTail = true;
            allPoints.First(p => p.Equals(newPoint)).isTail = true;
        }

        private Location movePoint(List<Location> allPoints, char direction, Func<Location, bool> predicate)
        {
            var point = allPoints.First(predicate);
            int newXCoor = point.xCoordinate;
            int newYCoor = point.yCoordinate;

            switch (direction)
            {
                case 'R': 
                    newXCoor++;
                    break;
                case 'L': 
                    newXCoor--;
                    break;
                case 'U': 
                    newYCoor++;
                    break;
                case 'D': 
                    newYCoor--;
                    break;
                case 'W' ://diagonally right-up
                    newXCoor++;
                    newYCoor++;
                    break;
                case 'X' ://diagonally right-down
                    newXCoor++;
                    newYCoor--;
                    break;
                case 'Y' ://diagonally left-up
                    newXCoor--;
                    newYCoor++;
                    break;
                case 'Z' ://diagonally left-down
                    newXCoor--;
                    newYCoor--;
                    break;
                default:
                    throw new ArgumentException();
            }

            if(!allPoints.Any(p => p.xCoordinate == newXCoor && p.yCoordinate == newYCoor))
            {   
                point = new Location(newXCoor, newYCoor);
                allPoints.Add(point);
            }

            return allPoints.First(p => p.xCoordinate == newXCoor && p.yCoordinate == newYCoor);
        }

        public void part2(string[] input)
        {
            var allPoints = new List<Location>();
            var allKnots = initializeKnots(allPoints);

            foreach (var move in input)
            {
                moveKnot(move, allPoints, allKnots);
            }

            Console.WriteLine($"Total count: {allPoints.Where(p => p.wasTail || p.isTail).Count()}");
        }

        private void moveKnot(string rawCommand, List<Location> allPoints, List<Knot> allKnots)
        {
            var commands = rawCommand.Split(" ");
            var times = int.Parse(commands[1]);
            var direction = commands[0][0];

            for (int i = 0; i < times; i++)
            {
                moveKnotOnce(allPoints, direction, allKnots);
            }
        }

        private void moveKnotOnce(List<Location> allPoints, char direction, List<Knot> allKnots)
        {
            moveKnot(allPoints, direction, allKnots.First(k => k.linePosition == 0));

            for (int i = 0; i < 9; i++)
            { 
                var firstKnot = allKnots.First(p => p.linePosition == i);
                var secondKnot = allKnots.First(p => p.linePosition == i+1);
                if (!AreKnotsTouching(firstKnot, secondKnot))
                {
                    moveKnotToHead(allPoints, firstKnot, secondKnot);
                }
            }

            allKnots.First(k => k.linePosition == 9).Location.wasTail = true;
        }

        private void moveKnot(List<Location> allPoints, char direction, Knot knot)
        {
            knot.Location  = movePoint(allPoints, direction, p => p.Equals(knot.Location));
        }

        private void moveKnotToHead(List<Location> allPoints, Knot firstKnot, Knot secondKnot)
        {
            var head = firstKnot.Location;
            var tail = secondKnot.Location;
            var move = 'N';

            if(secondKnot.linePosition == 9)
            {
                secondKnot.Location.wasTail = true;
            }

            if(head.yCoordinate == tail.yCoordinate)
            {
                move = (head.xCoordinate - tail.xCoordinate) >  0 ? 'R' : 'L';

            }
            else if(head.xCoordinate == tail.xCoordinate)
            {
                move = (head.yCoordinate - tail.yCoordinate) > 0 ? 'U' : 'D';
            }
            else if((head.xCoordinate - tail.xCoordinate > 0)) // need to move dioganly right
            {
                move = (head.yCoordinate - tail.yCoordinate) > 0 ? 'W' : 'X';
            }
            else // need to move dioganly left
            {
                move = (head.yCoordinate - tail.yCoordinate) > 0 ? 'Y' : 'Z';
            }

            moveKnot(allPoints, move, secondKnot);
        }


        private bool AreKnotsTouching(Knot firstKnot, Knot secondKnot)
        {
            var headLocation = firstKnot.Location;
            var tailLocation = secondKnot.Location;

            if (Math.Abs(headLocation.yCoordinate-tailLocation.yCoordinate)<=1 && Math.Abs(headLocation.xCoordinate-tailLocation.xCoordinate)<=1)
            {
                return true;
            }

            return false;
        }

        private List<Knot> initializeKnots(List<Location> allPoints)
        {
            var allKnots = new List<Knot>();
            var point = new Location(0, 0);
            allPoints.Add(point);

            for (int i = 0; i <= 9; i++)
            {
                allKnots.Add(new Knot(point, i));
            }

            allKnots.First(k => k.linePosition == 9).Location.wasTail = true;

            return allKnots;
        }

        public class Location : Point
        {
            public Location(int xCoordinate, int yCoordinate): base(xCoordinate, yCoordinate) {}

            public bool isHead = false;

            public bool isTail = false;

            public bool wasTail = false;

        }

        public class Knot
        {
            public Knot(Location location, int position)
            {
                this.Location = location;
                this.linePosition = position;
            }
            public Location Location;

            public int linePosition;
        }

    }
}