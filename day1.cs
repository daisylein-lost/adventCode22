namespace advent
{
    public class day1
    {
        public void part1(string[] input)
        {
            var elves = new List<int>();
            elves.Add(0);
            
            for (int i = 0; i < input.Count(); i++)
            {
                if(input[i].Equals(""))
                {
                    elves.Add(0);
                }
                else
                {
                  elves[elves.Count-1] += Int32.Parse(input[i]);
                }
            }

            var highestCalories = elves.Max();

            Console.WriteLine(highestCalories);
        }

        public void part2(string[] input)
        {
            
            var elves = new List<int>();
            elves.Add(0);
            
            for (int i = 0; i < input.Count(); i++)
            {
                if(input[i].Equals(""))
                {
                    elves.Add(0);
                }
                else
                {
                  elves[elves.Count-1] += Int32.Parse(input[i]);
                }
            }

            elves.Sort();

            var highest3 = elves.Skip(elves.Count-3);

            var total = highest3.Sum();
            Console.WriteLine(total);
        }
    }
}