using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AlgorithmBasics.Algorithms.Graphs
{
    public class MinimumCuts
    {
        public static Dictionary<int, List<int>> ParseGraph(string fileName)
        {
            var result = new Dictionary<int, List<int>>();
            
            foreach (string line in File.ReadLines(fileName))
            {
                string[] x = line.Split(new[] {'\t', ' '}, StringSplitOptions.RemoveEmptyEntries);
                if (x.Any())
                {
                    int[] arr = x.Select(int.Parse).ToArray();
                    var adjs = arr.Skip(1).ToList();
                    if (result.ContainsKey(arr[0]))
                    {
                        result[arr[0]].AddRange(adjs);
                    }
                    else
                    {
                        result.Add(arr[0], adjs);
                    }
                    
                }
            }

            return result;
        }

        public static int FindWithAdjacencyList(Dictionary<int, List<int>> graph)
        {
            var totalMinCut = int.MaxValue;
            
            var iterationsCount = (int) Math.Ceiling(Math.Pow(graph.Keys.Count, 2) * Math.Log(graph.Keys.Count));
            Console.WriteLine($"Iterations count - {iterationsCount}");

            var sync = new object();

            Parallel.For(0, iterationsCount, i =>
            {
                var r = new Random();
                if (i == 100_000)
                {
                    Console.WriteLine($"{DateTime.Now}. 100 000 iterations!");
                }

                var adjList = AdjacencyList.Init(graph);

                List<int> nodesActiveLst = Enumerable.Range(1, adjList.Nodes.Count).ToList();
                
                // TODO: add queue of random vertices
                // Shuffle(nodesActiveLst);
                // var nodesActive = new Queue<int>();
                // nodesActiveLst.ForEach(p => nodesActive.Enqueue(p));

                while (nodesActiveLst.Count > 2)
                {
                    var deleteInt = r.Next(nodesActiveLst.Count);
                    var nodeToDelete = nodesActiveLst[deleteInt];

                    List<int> nodeWithEdges = adjList.Nodes[nodeToDelete];

                    // Console.WriteLine($"Get delete node index - {deleteNodeIndex}");
                    int targetNode = nodeWithEdges[r.Next(nodeWithEdges.Count)];

                    nodesActiveLst.RemoveAt(deleteInt);

                    adjList.MergeNodes(targetNode, nodeToDelete);
                }

                int minCut = adjList.Nodes[nodesActiveLst[0]].Count;
                lock (sync)
                {
                    if (minCut < totalMinCut)
                    {
                        totalMinCut = minCut;
                    }
                }
            });
            
            return totalMinCut;
        }

        // private static Random rng = new Random();
        //
        // public static void Shuffle<T>(IList<T> list)
        // {
        //     int n = list.Count;
        //     while (n > 1)
        //     {
        //         n--;
        //         int k = rng.Next(n + 1);
        //         T value = list[k];
        //         list[k] = list[n];
        //         list[n] = value;
        //     }
        // }
    }
    
}