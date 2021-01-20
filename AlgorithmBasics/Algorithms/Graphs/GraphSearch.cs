using System.Collections.Generic;
using System.Linq;
using AlgorithmBasics.DataStructures.Graph;
using AlgorithmBasics.DataStructures.Heap;

namespace AlgorithmBasics.Algorithms.Graphs
{
    public static class GraphSearch
    {
        #region Trivial Depth-first search and Breadth-first search implementations.

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

        #endregion

        #region Find strongly connected components in graph. Kosaraju's two path algorithm implementation.

        public static List<List<int>> FindStronglyConnectedComponents(IDirectedGraphWithReversed<int> graph)
        {
            List<int> finishingTimesVertices = CalculateFinishingTimes(graph);
            return ComputeStronglyConnectedComponents(graph, finishingTimesVertices);
        }

        private static List<int> CalculateFinishingTimes(IDirectedGraphWithReversed<int> graph)
        {
            var finishedVertices = new List<int>();
            var exploredVertices = new HashSet<int>();
            foreach (int v in graph.GetVertices())
            {
                if (!exploredVertices.Contains(v))
                {
                    DfsCalculateFinishingTimes(graph, v, exploredVertices, finishedVertices);
                }
            }

            return finishedVertices;
        }

        private static void DfsCalculateFinishingTimes(IDirectedGraphWithReversed<int> graph, 
                                                       int startVertex, 
                                                       HashSet<int> exploredVertices,
                                                       List<int> finishedVertices)
        {
            var stack = new Stack<int>();
            stack.Push(startVertex);

            while (stack.Any())
            {
                int v = stack.Peek();
                if (!exploredVertices.Contains(v))
                {
                    exploredVertices.Add(v);
                }

                bool hasUnexploredNeighbours = false;
                
                
                
                
                foreach (int w in graph.GetIncomingVertices(v))
                {
                    if (!exploredVertices.Contains(w))
                    {
                        hasUnexploredNeighbours = true;
                        stack.Push(w);
                        break;
                    }
                }
                if (hasUnexploredNeighbours) continue;
                finishedVertices.Add(stack.Pop());
            }
        }
        
        private static List<List<int>> ComputeStronglyConnectedComponents(IDirectedGraph<int> graph, 
                                                                          List<int> finishingTimesVertices)
        {
            var sccList = new List<List<int>>();
            var exploredVertices = new HashSet<int>();

            for (int i = finishingTimesVertices.Count - 1; i >= 0; i--)
            {
                if (!exploredVertices.Contains(finishingTimesVertices[i]))
                {
                    var foundSccs = new List<int>();
                    DfsFindSccs(graph,
                                startVertex: finishingTimesVertices[i],
                                exploredVertices: exploredVertices,
                                sccs: foundSccs);
                    sccList.Add(foundSccs);
                }
            }

            return sccList;
        }
        
        private static void DfsFindSccs(IDirectedGraph<int> graph, 
                                        int startVertex,
                                        HashSet<int> exploredVertices,
                                        List<int> sccs)
        {
            var stack = new Stack<int>();
            stack.Push(startVertex);

            while (stack.Any())
            {
                int v = stack.Peek();
                if (!exploredVertices.Contains(v))
                {
                    exploredVertices.Add(v);
                }
                
                bool hasUnexploredNeighbours = false;
                foreach (int w in graph.GetAdjacentVertices(v))
                {
                    if (!exploredVertices.Contains(w))
                    {
                        hasUnexploredNeighbours = true;
                        stack.Push(w);
                        break;
                    }
                }
                if (!hasUnexploredNeighbours)
                {
                    sccs.Add(stack.Pop());
                }
            }
        }

        #endregion

        #region Dijkstra algorithm implementation. Compute shortest path

        // DijkstraSearchNaive
        // X - set of explored vertices of graph G
        // V-X - set of unexplored vertices of graph G
        //
        // DijkstraSearchNaive(graph G, vertex s):
        //     // Initialization
        //     X = { s }
        //     len(s) = 0, len(v) = +infinity for every v != s
        //     // Main loop
        //     while there is an edge (v, w) with v ∈ X, w ∈ X do
        //         (v*, w*) = such an edge minimizing len(v) + l_vw
        //         X.Add(w*)
        //         len(w*) = len(v*) + l_v*w*
        public static void DijkstraSearchNaive(IDirectedGraph<int> graph, int startVertex)
        {
            var x = new Dictionary<int, int> {{startVertex, 0}};
            foreach (var vertex in graph.GetVertices().Where(i => i != startVertex))
            {
                x.Add(vertex, int.MaxValue);
            }

            foreach (KeyValuePair<int,int> pair in x)
            {
                
            }
        }
        
        // X - set of explored vertices of graph G
        // V-X - set of unexplored vertices of graph G
        //
        // Invariant.
        //     The key of a vertex w belongs to V-X is the min Dijkstra score of an edge with tail v belongs to X 
        //     and head w, or +infinity if no such edge exists.
        //
        //     key(w) = min len(v) + l(vw)
        //
        // DijkstraSearch(graph G, vertex s):
        //     // Initialization
        //     X = empty set
        //     H = empty min heap
        //     key(s) = 0
        //     for each vertex V in G.vertices do: key(V) = +infinity
        //     for each vertex V in G.vertices do: H.Insert(V)
        //     // Main loop
        //     while H is not empty do:
        //         w* = H.ExtractMin()
        //         X.Add(w*)
        //         len(w*) = key(w*)
        //         // Update Heap to maintain Invariant
        //         for every edge(w*, y) do:
        //             H.Delete(y)     // Delete: given a heap H and a pointer to an object x in H, delete x from H.
        //             key(y) = min {key(y), len(w*) + l(w*y)}
        //             H.Insert(y)
        public static void DijkstraSearchMinHeap(IDirectedGraph<int> graph, int startVertex)
        {
            // key - vertex, value - len(vertex)
            var discoveredVerticesWithLength = new Dictionary<int, int>();
            // key - vertex id, value - Dijkstra score
            var minHeap = new MinHeap<(int, int)>();
            minHeap.Insert((startVertex, 0));
            foreach (int v in graph.GetVertices())
            {
                if (v != startVertex)
                {
                    minHeap.Insert((v, int.MaxValue));
                }
            }
            // Main loop
            while (!minHeap.IsEmpty())
            {
                // key - vertexId, value - DijkstraScore
                (int, int) extractedW = minHeap.ExtractMin();
                discoveredVerticesWithLength.Add(extractedW.Item1, extractedW.Item2);
                // Update heap to maintain Invariant
                foreach (int y in graph.GetAdjacentVertices(extractedW.Item1))
                {
                    var x = discoveredVerticesWithLength[y];
                    minHeap.Delete((x, y));
                    //(int, int) yWithKey = minHeap.Peek(y);
                    //var newTuple = (y, Math.Min(yWithKey.Item2, extractedW.Item2 + len(extractedW)));
                }
            }
        }
        
        #endregion
    }
}