using System.Runtime.CompilerServices;

namespace advent
{
    class Program
    {
        public static bool test = false;

        static void Main(string[] args)
        {
            var day = new day6();
            var input = GetInput();

            Console.WriteLine("Part 1:");
            //day.part1(input);
            Console.WriteLine("Part 2:");
            day.part2(input);
        }

        static string[] GetInput([CallerFilePath] string filePath = "")
        {
            var fileNameEnding = test ? "_test.txt" : ".txt";
            return System.IO.File.ReadAllLines(System.IO.Path.Combine(System.IO.Directory.GetParent(filePath).FullName, "Input", String.Concat("inputDay6", fileNameEnding)));
        }

    }
}