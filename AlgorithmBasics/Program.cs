using System;
using System.Collections.Generic;
using System.Diagnostics;
using AlgorithmBasics.Algorithms.Graphs;
using AlgorithmBasics.TestAssignments;

namespace AlgorithmBasics
{
    class Program
    {
        public static void Main(string[] args)
        {
            CheckExternalSortAssignment();
        }

        
        private static void CheckExternalSortAssignment()
        {
            var sw = new Stopwatch();
            var rawFilePath = @"C:\SomePath\GeneratedUnsortedFile.txt";
            var chunksDirectory = $"C:\\output\\chunks\\{Guid.NewGuid():N}";
            var sortedFilePath = $"{chunksDirectory}\\Sorted_{DateTime.UtcNow:yyyy-dd-M-HH-mm-ss.fff}.txt";
            
            ExternalSortAssignment.GenerateFile(path: rawFilePath, stringsCount: 30_000_000, maxStringSize: 20, maxIntNumber: 100);
            
            Console.WriteLine($"{DateTime.UtcNow:MM/dd/yyyy hh:mm:ss.fff tt}. Starting to sort file!");
            sw.Start();
            
            ExternalSortAssignment.Run(rawFilePath, chunksDirectory, 100, sortedFilePath);
            
            sw.Stop();
            Console.WriteLine($"{DateTime.UtcNow:MM/dd/yyyy hh:mm:ss.fff tt}. Finished to sort file! Elapsed milliseconds: {sw.ElapsedMilliseconds}");
            Console.WriteLine($"Sorted file path: {sortedFilePath}");
            
            ExternalSortAssignment.TestResults(sortedFilePath);
        }

        private void FindMinCutWithAdjLists()
        {
            for (int i = 0; i < 1; i++)
            {
                Dictionary<int, List<int>> dict = MinimumCuts.ParseGraph("CourseTasks\\kargerMinCut.txt");

                Console.WriteLine($"Started! {DateTime.Now}");
                int result = MinimumCuts.FindWithAdjacencyList(dict);
                Console.WriteLine($"Finished! {DateTime.Now}");
                Console.WriteLine($"MinCut = {result}");
            }
        }
    }
}
