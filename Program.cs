using System.Runtime.CompilerServices;

namespace HelloWorld
{
    class Program
    {
        public static bool test = false;

        static void Main(string[] args)
        {
            var input = GetInput();

            part1(input);
            Console.WriteLine("///////////////////");
            part2(input);
        }

        static void part1(string[] input)
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

        static void part2(string[] input)
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

        static string[] GetInput([CallerFilePath] string filePath = "")
        {
            var fileNameEnding = test ? "_test.txt" : ".txt";
            return System.IO.File.ReadAllLines(System.IO.Path.Combine(System.IO.Directory.GetParent(filePath).FullName, "Input", String.Concat("inputDay1", fileNameEnding)));
        }
    }
}