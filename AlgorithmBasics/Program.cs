using System;
using System.Collections.Generic;
using AlgorithmBasics.Algorithms.Graphs;

namespace AlgorithmBasics
{
    class Program
    {
        public static void Main(string[] args)
        {
            for (int i = 0; i < 1; i++)
            {
                Dictionary<int, List<int>> dict = MinimumCuts.ParseGraph("CourseTasks\\kargerMinCut.txt");

                Console.WriteLine($"Started! {DateTime.Now}");
                int result = MinimumCuts.FindWithAdjacencyList(dict);
                Console.WriteLine($"Finished! {DateTime.Now}");
                Console.WriteLine($"MinCut = {result}");
            }
            Console.ReadLine();
        }
        
    }
}
