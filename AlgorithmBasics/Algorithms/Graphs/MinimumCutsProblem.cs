using AlgorithmBasics.DataStructures.Graph;

namespace AlgorithmBasics.Algorithms.Graphs
{
    public class MinimumCuts
    {
        public static Dictionary<int, List<int>> ParseGraph(string filePath)
        {
            var result = new Dictionary<int, List<int>>();
            
            foreach (string line in File.ReadLines(filePath))
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
            
            var sync = new object();
            Parallel.For(0, iterationsCount, i =>
            {
                var r = new Random();

                var adjList = AdjacencyList.Init(graph);

                List<int> nodesActiveLst = Enumerable.Range(1, adjList.Nodes.Count).ToList();
                
                while (nodesActiveLst.Count > 2)
                {
                    var deleteInt = r.Next(nodesActiveLst.Count);
                    var nodeToDelete = nodesActiveLst[deleteInt];

                    List<int> nodeWithEdges = adjList.Nodes[nodeToDelete];

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
    }
    
}