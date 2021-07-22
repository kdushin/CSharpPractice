using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AlgorithmBasics.DataStructures.Graph;
using AlgorithmBasics.DataStructures.Graph.GraphImplementations;

namespace AlgorithmBasics.Algorithms.GreedyAlgorithms
{
    public class MinimumSpanningTree
    {
        public static int ComputeMinSpanningTreeCost(string path)
        {
            IUndirectedWeightedGraph<int> graph = ParseGraph(path);
            var result = Foo(graph);
            return 42;
        }

        private static int Foo(IUndirectedWeightedGraph<int> graph)
        {
            List<int> vertices = graph.GetVertices().ToList();
            var x = new List<int> {vertices.First()};
            
            // consider data structure for t
            var t = new List<int>(); // Invariant: X = vertices spanned by tree-so-far T
            var result = 0;

            while (x.Count != vertices.Count)
            {
                // let e = (u, v) be the cheapest edge of G with u belongs X, v do not belongs X
                var cheapestScores = new List<(int vertex, int score)>();
                foreach (int u in x)
                {
                    (int v, int weight) = graph.GetAdjacentVertices(u)
                                                .Where(i => !x.Contains(i.w))   // expensive contains
                                                .OrderBy(edge => edge.weight)                   
                                                .First();
                    cheapestScores.Add((v, weight));                            // maybe better solution?
                }
                // var zzz = graph.GetAdjacentVertices(u)
                //     .Where(i => !x.Contains(i.w)).ToList()
                //     .OrderBy(edge => edge.weight)
                //     .First();

                var cheapestPath = cheapestScores.OrderBy(tuple => tuple.score)
                                                 .First();                      // min instead of OrderBy?
                // add e to T
                t.Add(cheapestPath.score);
                // add v to X
                x.Add(cheapestPath.vertex);
                
                result += cheapestPath.score;
            }

            return result;
        }

        private static IUndirectedWeightedGraph<int> ParseGraph(string path)
        {
            var counts = File.ReadLines(path)
                             .First()
                             .Split(new[] {'\t', ' '}, StringSplitOptions.RemoveEmptyEntries)
                             .Select(int.Parse)
                             .ToArray();
            
            var graph = new UndirectedWeightedGraph<int>(verticesCount: counts[0], edgesCount: counts[1]);
            foreach (string line in File.ReadLines(path).Skip(1))
            {
                string[] args = line.Split(new[] {'\t', ' '}, StringSplitOptions.RemoveEmptyEntries);
                if (args.Any())
                {
                    int[] arr = args.Select(int.Parse).ToArray();
                    graph.AddVertex(arr[0]);
                    graph.AddVertex(arr[1]);
                    graph.AddEdge(arr[0], arr[1], arr[2]);
                }
            }

            return graph;
        }
    }
}