using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace advent
{
    public class day7
    {
        public void part1(string[] input)
        {
            var inputList = input.ToList();
            var rootDir = new Folder("/");
            
            fillFolder(inputList, rootDir);

            rootDir.totalSize = calculateSizes(rootDir);

            var allDirs = new List<Folder>();

            findAllSubfolder(rootDir, allDirs);

            allDirs = allDirs.Where(x => x.totalSize <=100000).ToList();

            Console.WriteLine($"Sum of Size: {allDirs.Sum(x => x.totalSize)}");
        }

        private void findAllSubfolder(Folder rootDir, List<Folder> allDirs)
        {
            allDirs.Add(rootDir);
            foreach(var subFolder  in rootDir.Subfolder)
            {
                findAllSubfolder(subFolder, allDirs);
            }
        }

        private long calculateSizes(Folder folder)
        {
            foreach (var subFolder in folder.Subfolder)
            {
                subFolder.totalSize = calculateSizes(subFolder);
            }

            return folder.Files.Sum(x => x.Size) + folder.Subfolder.Sum(x => x.totalSize); 
        }

        private void fillFolder(List<string> input, Folder folder)
        {
            var startIndex = input.FindIndex(x => x.Equals($"$ cd {folder.Name}"));
            var endIndex = input.FindIndex(startIndex+1, x => x.StartsWith("$ cd"));
            
            if(endIndex == -1)
            {
                endIndex = input.Count();
            }

            var folderInfo = input.GetRange(startIndex, endIndex-startIndex);
            folder.Subfolder = folderInfo.FindAll(x => x.StartsWith("dir")).Select(x => new Folder(x.Split(" ")[1])).ToList();
            folder.Files = folderInfo.FindAll(x => !x.StartsWith("$") && !x.StartsWith("dir")).Select(x => new File(x.Split(" ")[1], x.Split(" ")[0])).ToList();
            input[startIndex] = "$ cd done";

            foreach (var subFolder in folder.Subfolder)
            {
                fillFolder(input, subFolder);
            }
        }

        public void part2(string[] input)
        {
            var inputList = input.ToList();
            var rootDir = new Folder("/");
            
            fillFolder(inputList, rootDir);

            rootDir.totalSize = calculateSizes(rootDir);

            var neededSpace = 30000000 - (70000000 - rootDir.totalSize);

            var allDirs = new List<Folder>();

            findAllSubfolder(rootDir, allDirs);

            var smallestDir = allDirs.Where(x => x.totalSize >= neededSpace).OrderBy(x => x.totalSize).First();

            Console.WriteLine($"Total Size: {smallestDir.totalSize}");

        }

        public class Folder
        {

            public string Name;
            public List<Folder> Subfolder = new List<Folder>();

            public List<File> Files = new List<File>();

            public long totalSize = 0;
            
            public Folder(string name)
            {
                this.Name = name;
            }

        }

        public class File
        {
            public string Name;
            public long Size;

            public File(string name, string size)
            {
                this.Name = name;

                this.Size = long.Parse(size);
            }
        }
    }
}