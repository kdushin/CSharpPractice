using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgorithmBasics.DataStructures.Graphs
{
    public class AdjacencyList
    {
        public Dictionary<int, List<int>> Nodes { get; }

        private AdjacencyList(int nodesCount)
        {
            Nodes = new Dictionary<int, List<int>>(nodesCount);
        }
        
        public static AdjacencyList Init(Dictionary<int, List<int>> graph)
        {
            AdjacencyList adjList = new AdjacencyList(graph.Count);
            foreach (KeyValuePair<int,List<int>> kvp in graph)
            {
                var adjacentNodes = new List<int>(kvp.Value.Count);
                kvp.Value.ForEach(v => adjacentNodes.Add(v));
                adjList.Nodes.Add(kvp.Key, adjacentNodes);
            }
            
            return adjList;
        }

        /// <summary>
        /// Merge node V with node U. Node U will be deleted 
        /// </summary>
        public void MergeNodes(int v, int u)
        {
            Nodes[v].RemoveAll(node => node == u);
            Nodes[u].RemoveAll(node => node == v);
            Nodes[v].AddRange(Nodes[u]);
            
            foreach (int uAdjNode in Nodes[u])
            {
                for (int i = 0; i < Nodes[uAdjNode].Count; i++)
                {
                    if (Nodes[uAdjNode][i] == u)
                    {
                        Nodes[uAdjNode][i] = v;
                    }
                }
            }
        }

        public override string ToString()
        {
            var strBuilder = new StringBuilder();
            strBuilder.AppendLine("GRAPH:");
            foreach (KeyValuePair<int,List<int>> kvp in Nodes)
            {
                var str = $"Id: {kvp.Key}. Adjs: {string.Join(",", kvp.Value.Select(node => node))} Count: {kvp.Value.Count}";
                strBuilder.AppendLine(str);
            }

            return strBuilder.ToString();
        }
    }
}