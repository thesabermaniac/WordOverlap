using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using HelloGithubClassroom;
using System.Linq;

namespace LINQTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test3or5Under10()
        {
            List<int> Nums = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Assert.AreEqual(Program.Sum3or6(Nums), 23);
        }
        [TestMethod]
        public void Test3or5Under1000()
        {
            List<int> nums = new List<int>();
            for(int i = 0; i < 1000; i++)
            {
                nums.Add(i);
            }
            Assert.AreEqual(Program.Sum3or6(nums), 233_168);
        }

        [TestMethod]
        public void Top20Words()
        {
            string file = @"C:\Users\shalo\Documents\School\ObjectOrientedProg\6-linq1-thesabermaniac\HelloGithubClassroom\TestFiles\TestText.txt";
            IEnumerable<(string, int)> hist = Program.Top20WordsInFile(file);
            Assert.AreEqual(hist.Skip(1).First().Item1, "harper");
            Assert.AreEqual(hist.Skip(1).First().Item2, 4);
            Assert.AreEqual(hist.Skip(2).First().Item1, "lucas");
            Assert.AreEqual(hist.Skip(2).First().Item2, 3);
        }

        [TestMethod]
        public void PercentageTable()
        {
            string directory = @"C:\Users\shalo\Documents\School\ObjectOrientedProg\6-linq1-thesabermaniac\HelloGithubClassroom\TestFiles\";
            IEnumerable<IEnumerable<string>> Table = Program.CreateTable(directory);
            Assert.AreEqual(Table.Skip(1).First().Skip(1).First(), "100".PadRight(8, ' '));
            Assert.AreEqual(Table.Skip(2).First().Skip(2).First(), "100".PadRight(9, ' '));
            Assert.AreEqual(Table.Skip(3).First().Skip(3).First(), "100".PadRight(9, ' '));
        }
    }
}
