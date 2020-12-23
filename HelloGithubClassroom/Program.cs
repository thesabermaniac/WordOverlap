using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace HelloGithubClassroom
{
    public class Program
    {
        private static void TruncateFile(String f)
        {
            var Lines = new List<string>();
            using (var stream = File.OpenRead(f))
            {
                using (var reader = new StreamReader(stream))
                {
                    string line = reader.ReadLine();
                    while(!line.ToUpper().Contains("START OF THE PROJECT GUTENBERG EBOOK"))
                    {
                        line = reader.ReadLine();
                    }
                    reader.ReadLine();
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.ToUpper().Contains("END OF THE PROJECT GUTENBERG EBOOK"))
                            break;
                        Lines.Add(line);
                    }
                }
                File.WriteAllLines(f, Lines);
            }
        }
        public static int Sum3or6(List<int> NumList)
        {
            var nums = NumList.Where(i => i % 3 == 0 || i % 5 == 0);
            var solution = 0;
            foreach(var n in nums)
            {
                solution += n;
            }
            return solution;
        }

        public static IEnumerable<(string, int)> Top20WordsInFile(String f)
        {
            IEnumerable<string> LineList = File.ReadAllLines(f);
            List<string> WordList = new List<string>();
            foreach(var Line in LineList)
            {
                foreach(var Word in Line.Trim().Split())
                {
                    Regex rgx = new Regex("[^a-zA-Z0-9 -]");
                    var str = rgx.Replace(Word, "").Replace("\n", "");
                    if (str != "")
                    {
                        WordList.Add(str.ToLower());
                    }
                }
            }
            var Top20Words = WordList.GroupBy(w => w).OrderByDescending(w => w.Count()).Take(20);
            List<(string, int)> Histogram = new List<(string, int)>();
            var FileName = Path.GetFileNameWithoutExtension(f);
            Histogram.Add((FileName, -1));
            foreach(var Word in Top20Words)
            {
                Histogram.Add((Word.Key, Word.Count()));
            }
            return Histogram;
        }

        public static IEnumerable<IEnumerable<string>> CreateTable(string dir)
        {
            List<IEnumerable<string>> Table = new List<IEnumerable<string>>();
            List<IEnumerable<(string, int)>> HistogramList = new List<IEnumerable<(string, int)>>();
            List<string> FileNames = new List<string>();

            //Add padding to cell 1-1 for aesthetic purposes
            FileNames.Add("".PadRight(16, ' '));

            foreach (var file in Directory.EnumerateFiles(dir, "*.txt"))
            {
                FileNames.Add(Path.GetFileNameWithoutExtension(file));
                HistogramList.Add(Top20WordsInFile(file));
            }
            Table.Add(FileNames);

            for(int i = 0; i < HistogramList.Count; i++)
            {
                var overlap = new List<string>();
                overlap.Add(HistogramList[i].First().Item1.PadRight(16, ' '));
                for (int j = 0; j < HistogramList.Count; j++)
                {
                    var OverlappingWords = HistogramList[i].Select(key => key.Item1).Where(key => key != HistogramList[j].First().Item1)
                        .Intersect(HistogramList[j].Select(key => key.Item1));
                    double OverlapPercentage = (double)OverlappingWords.Count() / (HistogramList[j].Count() - 1) * 100.0;
                    overlap.Add(Math.Round(OverlapPercentage).ToString().PadRight(HistogramList[j].First().Item1.Length, ' '));
                }
                Table.Add(overlap);
            }

            return Table;
        }
        static void Main(string[] args)
        {
            string directory = @"C:\Users\shalo\Documents\School\ObjectOrientedProg\6-linq1-thesabermaniac\HelloGithubClassroom\Books";
            foreach (var i in CreateTable(directory))
            {
                foreach (var j in i)
                {
                    Console.Write(j + " | ");
                }
                Console.WriteLine();
            }
        }
    }
}
