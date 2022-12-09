using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace advent
{
    public class day4
    {
        public void part1(string[] input)
        {
            var fullyContained = 0;
            foreach (var pair in input)
            {
                var assigments = pair.Split(",");
                var firstElve = getSections(assigments[0]);
                var secondElve = getSections(assigments[1]);

                var overlap = firstElve.Intersect(secondElve);

                if (overlap.Count() == firstElve.Count() || overlap.Count() == secondElve.Count())
                {
                    fullyContained++;
                    Console.WriteLine($"Fully Contained: {pair}");
                }
            }

            Console.WriteLine($"Number of fully contained pairs: {fullyContained}");
        }

        public void part2(string[] input)
        {
            var overlapped= 0;
            foreach (var pair in input)
            {
                var assigments = pair.Split(",");
                var firstElve = getSections(assigments[0]);
                var secondElve = getSections(assigments[1]);

                var overlap = firstElve.Intersect(secondElve);

                if (overlap.Count() != 0)
                {
                    overlapped++;
                    Console.WriteLine($"Overlap: {pair}");
                }
            }

            Console.WriteLine($"Number of overlapped pairs: {overlapped}");
        }

        private List<int> getSections(string assigment)
        {
            var sections = assigment.Split("-");
            var startSection = int.Parse(sections[0]);
            var endSection = int.Parse(sections[1]);
            return Enumerable.Range(startSection, endSection - startSection+1).ToList();
        }
        
    }
}