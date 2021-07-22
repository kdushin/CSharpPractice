﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using AlgorithmBasics.Algorithms.Graphs;
using AlgorithmBasics.Algorithms.GreedyAlgorithms;
using AlgorithmBasics.DataStructures.Graph;
using AlgorithmBasics.DataStructures.Graph.GraphImplementations;
using AlgorithmBasics.TestAssignments;

namespace AlgorithmBasics
{
    class Program
    {
        public static void Main(string[] args)
        {
            ConcurrencySandbox.DisplayPrimeCounts();
            
            
//             var path =
// @"C:\Projects\CSharpPractice\AlgorithmBasics\CourseTasks\GreedyAlgorithms\MinimumSpanningTree\TestCase1.txt";
//             // call method for specific algorithm here
//             int minimumCost = MinimumSpanningTree.ComputeMinSpanningTreeCost(path);
//             
//             // print out result here
//             Console.WriteLine("Program finished");
//             Console.ReadLine();
            
        }

        #region Assignments

        private static void CheckJobsCompletionTime(string path)
        {
            BigInteger weightedSum = SchedulingJobs.ComputeJobsCompletionTimeDifference(path);   // 69119377652
            BigInteger weightedSumRatio = SchedulingJobs.ComputeJobsCompletionTimeRatio(path);   // 67311454237
            Console.WriteLine(weightedSum);
            Console.WriteLine(weightedSumRatio);
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

        private static void FindMinCutWithAdjLists(string filePath)
        {
            for (int i = 0; i < 1; i++)
            {
                Dictionary<int, List<int>> dict = MinimumCuts.ParseGraph(filePath);

                Console.WriteLine($"Started! {DateTime.Now}");
                int result = MinimumCuts.FindWithAdjacencyList(dict);
                Console.WriteLine($"Finished! {DateTime.Now}");
                Console.WriteLine($"MinCut = {result}");
            }
        }

        private static void TestDirectedGraph(string path)
        {
            using (var reader = new StreamReader(path))
            {
                IDirectedGraph<int> graph = GraphHelper.CreateWithCapacity(8);
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (line == null) { continue; }
                    graph.AddEdge(int.Parse(line.Split()[0]), int.Parse(line.Split()[1]));
                }

                Console.WriteLine(graph);
                //Console.WriteLine(graph.ReverseGraph());
            }
        }

        private static void TestBfsDirectedSmallGraph(string path)
        {
            IDirectedGraph<int> graph = null;
            using (var reader = new StreamReader(path))
            {
                graph = GraphHelper.ParseFromTextDirectedGraph(reader, saveReversedVersion: false);
                Console.WriteLine("Graph initiated!");
                Console.WriteLine(graph);
            }

            foreach (int exploredNode in GraphSearch.BreadthFirst(graph, 1))
            {
                Console.WriteLine(exploredNode);
            }
        }

        private static void TestShortestPath(string path)
        {
            IDirectedGraph<int> graph = null;
            using (var reader = new StreamReader(path))
            {
                graph = GraphHelper.ParseFromTextDirectedGraph(reader, saveReversedVersion: false);
                Console.WriteLine("Graph initiated!");
                Console.WriteLine(graph);
            }

            int startVertex = 1, endVertex = 9;
            var shortestPath = GraphSearch.ComputeShortestPath(graph, startVertex, endVertex);
            Console.WriteLine($"Shortest path in graph from vertex {startVertex} to {endVertex} is {shortestPath}");
        }

        private static void TestDfs(string path)
        {
            IDirectedGraph<int> graph = null;
            using (var reader = new StreamReader(path))
            {
                graph = GraphHelper.ParseFromTextDirectedGraph(reader, saveReversedVersion: false);
                Console.WriteLine("Graph initiated!");
                Console.WriteLine(graph);
            }

            int startVertex = 1;
            Console.WriteLine($"Dfs result. Start vertex - {startVertex}");
            var result = GraphSearch.DepthFirst(graph, 1);
            foreach (int node in result)
            {
                Console.WriteLine(node);
            }
        }

        private static void TestFindStronglyConnectedComponents(string path)
        {
            IDirectedGraphWithReversed<int> graph;
            using (var reader = new StreamReader(path))
            {
                graph = GraphHelper.ParseFromTextDirectedGraph(reader, saveReversedVersion: true);
            }

            var results = GraphSearch.FindStronglyConnectedComponents(graph);
            var biggestSccs = results.OrderByDescending(r => r.Count)
                .Take(6)
                .ToList();
            
            Console.WriteLine($"Strongly connected components number - {biggestSccs.Count}!");
            for (var i = 0; i < biggestSccs.Count; i++)
            {
                var sccVertices = biggestSccs[i];
                Console.WriteLine($"SCC #{i+1}: {sccVertices.Count}");
            }
        }
        
        private static List<int> TestDijkstraSearch(string path)
        {
            IDirectedWeightedGraph<int> graph;
            using (var reader = new StreamReader(path))
            {
                graph = GraphHelper.ParseFromTextDirectedWeightedGraph(reader);
            }

            int[] result = GraphSearch.DijkstraMinHeap(graph, 1);

            int[] requiredPaths = {7,37,59,82,99,115,133,165,188,197};
            
            return requiredPaths.Select(i => result[i])
                .ToList();
        }

        #endregion
    }
}
