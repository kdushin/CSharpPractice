#define DEBUGX

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AlgorithmBasics.Algorithms.Graphs;

namespace AlgorithmBasics
{
    class Program
    {
        public static void Main(string[] args)
        {
            Dictionary<int, List<int>> dict = MinimumCuts.ParseGraph("CourseTasks\\kargerMinCut.txt");
         
            Console.WriteLine($"{DateTime.UtcNow} - Execute algorithm!");
            var result = MinimumCuts.Find(dict);
            Console.WriteLine($"Final minCut = {result} found!");
            Console.WriteLine($"{DateTime.UtcNow} - Finish!");
        }
        
    }
}
