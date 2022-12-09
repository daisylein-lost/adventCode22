using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace advent
{
    public class day2
    {
        private Dictionary<string, int> scores = new Dictionary<string, int>
        {
            {"L", 0},
            {"D", 3},
            {"W", 6},
            {"X", 1},
            {"Y", 2},
            {"Z", 3}
        };

        private Dictionary<string, string> translate = new Dictionary<string, string>
        {
            {"X", "L"},
            {"Y", "D"},
            {"Z", "W"}
        };

        public void part1(string[] input)
        {
            var rounds = new List<(string result, string hand)>();
            foreach (var line in input)
            {
                var hands = line.Split(" ");
                rounds.Add((determineWinner(hands[0], hands[1]),hands[1]));
            }

            var score = rounds.Select(round => scores[round.result] + scores[round.hand]);

            Console.WriteLine($"Total Score: {score.Sum()}");
        }

        public void part2(string[] input)
        {
            var rounds = new List<(string result, string hand)>();

            foreach (var line in input)
            {
                var hands = line.Split(" ");
                rounds.Add((translate[hands[1]], determineHand(hands[0], hands[1])));
            }

            var score = rounds.Select(round => scores[round.result] + scores[round.hand]);

            Console.WriteLine($"Total Score: {score.Sum()}");
        }

        private string determineHand(string opponent, string result)
        {
            if ((opponent.Equals("A") && result.Equals("X")) ||
                (opponent.Equals("B") && result.Equals("Z")) ||
                (opponent.Equals("C") && result.Equals("Y")))
            {
                return("Z");
            }
            if ((opponent.Equals("A") && result.Equals("Z")) ||
                (opponent.Equals("B") && result.Equals("Y")) ||
                (opponent.Equals("C") && result.Equals("X")))
            {
                return("Y");
            }
            else
            {
                return("X");
            }
        }

        private string determineWinner(string opponent, string player)
        {
            if ((opponent.Equals("A") && player.Equals("Z")) ||
                (opponent.Equals("B") && player.Equals("X"))|| 
                 (opponent.Equals("C") && player.Equals("Y")))
            {
                return("L");
            }
            if ((opponent.Equals("A") && player.Equals("X")) ||
                (opponent.Equals("B") && player.Equals("Y"))|| 
                 (opponent.Equals("C") && player.Equals("Z")))
            {
                return("D");
            }
            else
            {
                return("W");
            }
        }

    }
}