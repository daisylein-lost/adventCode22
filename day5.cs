using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace advent
{
    public class day5
    {
        public void part1(string[] input)
        {
            var lineBreak = input.ToList().IndexOf("");
            var rawStacks = input.ToList().GetRange(0, lineBreak);
            var movements = input.ToList().GetRange(lineBreak+1, input.Count()-lineBreak-1);

            var stacks = genereateStacks(rawStacks);

            moveCrates9000(stacks, movements);

            var topCrates = stacks.Select(s => s.Last()).ToList();

            Console.WriteLine($"Top crates: {String.Join(" ", topCrates)}");
        }

        public void part2(string[] input)
        {
             var lineBreak = input.ToList().IndexOf("");
            var rawStacks = input.ToList().GetRange(0, lineBreak);
            var movements = input.ToList().GetRange(lineBreak+1, input.Count()-lineBreak-1);

            var stacks = genereateStacks(rawStacks);

            moveCrates9001(stacks, movements);

            var topCrates = stacks.Select(s => s.Last()).ToList();

            Console.WriteLine($"Top crates: {String.Join(" ", topCrates)}");
        }

        private void moveCrates9001 (List<string>[]stacks, List<string> movements)
        {
            var pattern = @"move (?<amount>\d+) from (?<start>\d+) to (?<dest>\d+)";
            var regex = new Regex(pattern);

            foreach (var move in movements)
            {
                var matches = regex.Match(move);
                var start = int.Parse(matches.Groups["start"].Value)-1;
                var amount = int.Parse(matches.Groups["amount"].Value);
                var movingCrates = stacks[start].GetRange(stacks[start].Count()-amount, amount);
                stacks[start].RemoveRange(stacks[start].Count()-amount, amount);
                stacks[int.Parse(matches.Groups["dest"].Value)-1].AddRange(movingCrates);
                
            }
        }

        private void moveCrates9000 (List<string>[]stacks, List<string> movements)
        {
            var pattern = @"move (?<amount>\d+) from (?<start>\d+) to (?<dest>\d+)";
            var regex = new Regex(pattern);

            foreach (var move in movements)
            {
                var matches = regex.Match(move);
                for (int i = 0; i < int.Parse(matches.Groups["amount"].Value); i++)
                {
                    var start = int.Parse(matches.Groups["start"].Value)-1;
                    var movingCrate = stacks[start].Last();
                    stacks[start].RemoveAt(stacks[start].Count()-1);
                    stacks[int.Parse(matches.Groups["dest"].Value)-1].Add(movingCrate);
                }
            }
        }

        private List<string>[] genereateStacks(List<string> rawStacks)
        {
            var ammountOfStacks = int.Parse(rawStacks[rawStacks.Count-1].Split("  ").Last().Trim());

            var stacks = new List<string>[ammountOfStacks];

            for (int i = 0; i < ammountOfStacks; i++)
            {
                stacks[i] = new List<string>();
            }

            for (int y = rawStacks.Count-2; y >= 0; y--)
            {
                var level = rawStacks[y];
            
                for (int i = 0; i <= level.Length-3; i=i+4)
                {
                    var crate = level.Substring(i,3);
                    if(!crate.Trim().Equals(""))
                        stacks[i/4].Add(level.Substring(i,3));
                }   
            }

            return stacks;
        }
    }
}