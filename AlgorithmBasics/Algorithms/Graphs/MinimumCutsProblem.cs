using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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

                    if (!result.ContainsKey(arr[0]))
                    {
                        result.Add(arr[0], new List<int>());
                    }
                    for (int i = 1; i < arr.Length; i++)
                    {
                        if (arr[0] > arr[i]) continue;
                    
                        result[arr[0]].Add(arr[i]);
                    }
                }
            }

            return result;
        }
        
        public static int Find(Dictionary<int, List<int>> graph)
        {
            var totalMinCut = int.MaxValue;
            var iterationsCount = (int) Math.Ceiling(Math.Pow(graph.Keys.Count, 2) * Math.Log(graph.Keys.Count));

            var sync = new object();
            
            var parallelOptions = new ParallelOptions {MaxDegreeOfParallelism = 8};

            Parallel.For(0, iterationsCount, parallelOptions, i =>
            {
                var rng = new RNGCryptoServiceProvider();
                var localDict = Vertex.Init(graph);
                for (int count = localDict.Count; count > 2; count--)
                {
                    var mainId = NextInt(rng, 0, count);
                    var mainVx = localDict[mainId];
                    var vxToDeleteId = NextInt(rng, 0, mainVx.AdjacentVertices.Count);

                    var vxToDelete = mainVx.AdjacentVertices[vxToDeleteId];

                    mainVx.MergeWith(vxToDelete);
                    localDict.Remove(vxToDelete);
                }

                var minCut = localDict[0].AdjacentVertices.Count;
                if (minCut < totalMinCut)
                {
                    lock (sync)
                    {
                        totalMinCut = minCut;
                    }
                }
            });
            
            return totalMinCut;
        }
        
        private static int NextInt(RNGCryptoServiceProvider rng, int min, int max)
        {
            byte[] buffer = new byte[4];
            rng.GetBytes(buffer);
            return new Random(BitConverter.ToInt32(buffer, 0)).Next(min, max);
        }
    }
    
}