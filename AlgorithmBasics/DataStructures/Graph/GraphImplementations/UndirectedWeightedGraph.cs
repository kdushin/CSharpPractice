namespace AlgorithmBasics.DataStructures.Graph.GraphImplementations
{
    public class UndirectedWeightedGraph<TVertex> : IUndirectedWeightedGraph<TVertex>
    {
        public int VerticesCount { get; }

        public int EdgesCount { get; }
        
        private readonly Dictionary<TVertex, List<(TVertex, int)>> _adjacencyList;

        private readonly List<(TVertex, TVertex, int)> _edgeList;

        public UndirectedWeightedGraph(int verticesCount, int edgesCount)
        {
            _adjacencyList = new Dictionary<TVertex, List<(TVertex, int)>>();

            _edgeList = new List<(TVertex, TVertex, int)>();

            VerticesCount = verticesCount;
            EdgesCount = edgesCount;
        }
        
        public IEnumerable<TVertex> GetVertices()
        {
            return _adjacencyList.Keys;
        }

        public IEnumerable<(TVertex v, TVertex w, int weight)> GetEdges()
        {
            return _edgeList;
        }

        public void AddVertex(TVertex v)
        {
            if (!ContainsVertex(v))
            {
                _adjacencyList.Add(v, new List<(TVertex, int)>());
            }
        }

        public void AddEdge(TVertex v, TVertex w, int weight)
        {
            if (!ContainsVertex(v))
            {
                throw new InvalidOperationException($"Graph doesn't contain vertex {v}");
            }
            if (!ContainsVertex(w))
            {
                throw new InvalidOperationException($"Graph doesn't contain vertex {w}");
            }
            _adjacencyList[v].Add((w, weight));
            _adjacencyList[w].Add((v, weight));
            _edgeList.Add((v, w, weight));
        }

        public IEnumerable<(TVertex w, int weight)> GetAdjacentVertices(TVertex v)
        {
            _adjacencyList.TryGetValue(v, out List<(TVertex, int)> value);
            if (value == null)
            {
                throw new ArgumentOutOfRangeException(nameof(v), "Vertex doesn't exist in graph");
            }
            return value;
        }
        
        private bool ContainsVertex(TVertex vertex)
        {
            return _adjacencyList.ContainsKey(vertex);
        }
    }
}