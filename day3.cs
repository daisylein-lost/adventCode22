using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace advent
{
    public class day3
    {
        public void part1(string[] input)
        {
            var priority = new List<int>();
            foreach (var rucksack in input)
            {
                var firstCompartment = rucksack.Substring(0, rucksack.Length/2).ToList();
                var secondCompartment = rucksack.Substring(rucksack.Length/2, rucksack.Length/2).ToList();
                priority.Add(convertToPriority(firstCompartment.Intersect(secondCompartment).First()));
            }

            Console.WriteLine($"Sum: {priority.Sum()}");

        }

        public void part2(string[] input)
        {
            var priority = new List<int>();

            for (int group = 0; group+3 <= input.Length; group = group+3)
            {
                var firstElve = input[group].ToList();
                var secondElve = input[group+1].ToList();
                var thirdElve = input[group+2].ToList();

                priority.Add(convertToPriority(firstElve.Intersect(secondElve).Intersect(thirdElve).First()));
            }

            Console.WriteLine($"Sum: {priority.Sum()}");
        }

        private int convertToPriority(char letter)
        {
            if (char.IsUpper(letter))
            {
                return letter-38;
            }
            else
            {
                return letter-96;
            }
        }
    }
}