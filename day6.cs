using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace advent
{
    public class day6
    {
        public void part1(string[] input)
        {
            
            using (var reader = new StringReader(input[0]))
            {
                var position = 4;
                var buffer = new char[4];
                var nextLetter = new char[1];
                reader.Read(buffer, 0, 4);
                var packet = buffer.ToList();
                while(!AreAllCharactersDifferent(packet))
                {
                    reader.Read(nextLetter, 0, 1);
                    packet.RemoveAt(0);
                    packet.Add(nextLetter[0]);
                    position++;
                }

                Console.WriteLine($"First marker after character {position}");
            }
        }

        private bool AreAllCharactersDifferent(List<char> chars)
        {
            return chars.Distinct().Count() == chars.Count();
        }


        public void part2(string[] input)
        {
            using (var reader = new StringReader(input[0]))
            {
                var position = 14;
                var buffer = new char[14];
                var nextLetter = new char[1];
                reader.Read(buffer, 0, 14);
                var packet = buffer.ToList();
                while(!AreAllCharactersDifferent(packet))
                {
                    reader.Read(nextLetter, 0, 1);
                    packet.RemoveAt(0);
                    packet.Add(nextLetter[0]);
                    position++;
                }

                Console.WriteLine($"First message marker after character {position}");
            }
        }
    }
}