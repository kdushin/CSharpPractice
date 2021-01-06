using System;
using System.Collections.Generic;
using System.Linq;
using AlgorithmBasics.DataStructures.Graph;

namespace AlgorithmBasics.Algorithms.Graphs
{
    public static class GraphSearch
    {
        public static IEnumerable<int> DepthFirst(IDirectedGraph<int> graph, int startVertex)
        {
            var exploredVertices = new HashSet<int>{startVertex};
            var stack = new Stack<int>();
            stack.Push(startVertex);

            while (stack.Any())
            {
                int v = stack.Pop();
                yield return v;
                foreach (int w in graph.GetAdjacentVertices(v))
                {
                    if (!exploredVertices.Contains(w))
                    {
                        exploredVertices.Add(w);
                        stack.Push(w);
                    }
                }
            }
        }
        
        public static IEnumerable<int> BreadthFirst(IDirectedGraph<int> graph, int startVertex)
        {
            var exploredVertices = new HashSet<int>{startVertex};
            var queue = new Queue<int>();
            queue.Enqueue(startVertex);

            while (queue.Any())
            {
                int v = queue.Dequeue();
                yield return v;
                foreach (int w in graph.GetAdjacentVertices(v))
                {
                    if (!exploredVertices.Contains(w))
                    {
                        exploredVertices.Add(w);
                        queue.Enqueue(w);
                    }
                }
            }
        }

        public static int ComputeShortestPath(IDirectedGraph<int> graph, int startVertex, int endVertex)
        {
            if (startVertex == endVertex) { return 0; }
            // Key - Node, Value - node distance from startVertex
            var exploredVertices = new Dictionary<int, int>{{startVertex, 0}};
            var queue = new Queue<int>();
            queue.Enqueue(startVertex);

            while (queue.Any())
            {
                int v = queue.Dequeue();
                foreach (int w in graph.GetAdjacentVertices(v))
                {
                    if (!exploredVertices.ContainsKey(w))
                    {
                        int dist = exploredVertices[v] + 1;
                        exploredVertices.Add(w, dist);
                        queue.Enqueue(w);
                    }
                }
            }
            return exploredVertices.ContainsKey(endVertex) ? exploredVertices[endVertex] : int.MinValue;
        }

        // 1. Let Grev = G with all arcs reversed     
        // 2. Run DFS-Loop on Grev                     // goal: compute "magical ordering" of nodes
        // Let f(v) = "finishing time" of each vertex v 
        // 3. Run DFS-Loop on G                        // goal: discover the SCCs
        // Processing nodes in decreasing order of finishing times.
        /// <summary>
        /// </summary>
        public static Dictionary<int, int> FindStronglyConnectedComponents(IDirectedGraphWithReversed<int> graph)
        {
            Dictionary<int, int> finishingTimes = CalculateFinishingTimes(graph, graph.GetVertices().ToList());
            return ComputeStronglyConnectedComponents(graph, finishingTimes);
        }

        //   DFS-Loop(graph G)
        //       - global variable t = 0                     // for finishing times in 1st pass
        //       [# of nodes processed so far]
        //       // assume nodes labeled from 1 to n.
        //       - For i = n down to 1:
        //           - if i not yet explored
        //               - s:= i
        //               - DFS(G, i)
        private static Dictionary<int, int> CalculateFinishingTimes(IDirectedGraphWithReversed<int> graph, System.Collections.Generic.List<int> vertices)
        {
            var finishingTimes = new Dictionary<int, int>();
            int finishingTime = 0;
            var exploredVertices = new HashSet<int>();
            
            // Sort vertices in descending order.
            vertices.Sort((x, y) => y.CompareTo(x));
            
            foreach (int i in vertices)
            {
                if (!exploredVertices.Contains(i))
                {
                    DepthFirstLocal(i);
                }
            }

            return finishingTimes;
            
            // DFS(graph G, node i)
            //    - mark i as explored                            // for rest of DFS-Loop
            //    - for each arc (i, j) in G:
            //        - if j not yet explored:
            //            - DFS(G, i)                             // change to iterative dfs not recursive
            //    - t++
            //    - set f(i) := t                                 // it's finishing time
            void DepthFirstLocal(int startVertex)
            {
                exploredVertices.Add(startVertex);
                foreach (int j in graph.GetIncomingVertices(startVertex))
                {
                    if (!exploredVertices.Contains(j))
                    {
                        // TODO: change to iterative approach
                        DepthFirstLocal(j);
                    }
                }
                finishingTime++;
                finishingTimes.Add(finishingTime, startVertex);
                if (finishingTime > vertices.Count)
                {
                    Console.WriteLine($"Finishing time in graph is bigger than vertices count in graph. F - {finishingTime} V - {vertices.Count}");
                }
            }
        }
        
        //   DFS-Loop(graph G)
        //       - global variable s = null                  // for leaders in 2nd pass
        //       [current source vertex]
        //       // assume nodes labeled from 1 to n.
        //       - For i = n down to 1:
        //           - if i not yet explored
        //               - s:= i
        //               - DFS(G, i)
        private static Dictionary<int, int> ComputeStronglyConnectedComponents(IDirectedGraph<int> graph, Dictionary<int, int> finishingTimes)
        {
            var leaders = new Dictionary<int, int>();
            var exploredVertices = new HashSet<int>();
            int leaderVertex;
            
            for (int i = finishingTimes.Count; i >= 1; i--)
            {
                int vertex = finishingTimes[i];
                if (!exploredVertices.Contains(vertex))
                {
                    leaderVertex = vertex;
                    DepthFirstLocal(vertex);
                }
            }

            return leaders;
            
            // DFS(graph G, node i)
            //    - mark i as explored                            // for rest of DFS-Loop
            //    - set leader(i) := node s
            //    - for each arc (i, j) in G:
            //        - if j not yet explored:
            //            - DFS(G, i)                             // change to iterative dfs not recursive
            //    - t++
            //    - set f(i) := t                                 // it's finishing time
            void DepthFirstLocal(int startVertex)
            {
                exploredVertices.Add(startVertex);
                if (leaders.ContainsKey(leaderVertex))
                {
                    leaders[leaderVertex]++;
                }
                else
                {
                    leaders.Add(leaderVertex, 1);
                }
                foreach (int j in graph.GetAdjacentVertices(startVertex))
                {
                    if (!exploredVertices.Contains(j))
                    {
                        // TODO: change to iterative approach
                        DepthFirstLocal(j);
                    }
                }
            }
        }
    }
}