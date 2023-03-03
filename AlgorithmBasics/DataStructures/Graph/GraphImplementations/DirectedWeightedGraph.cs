namespace AlgorithmBasics.DataStructures.Graph.GraphImplementations
{
    public class DirectedWeightedGraph<TVertex> : IDirectedWeightedGraph<TVertex>
    {
        public int VerticesCount => _adjacencyList.Keys.Count;

        public int EdgesCount => _edgeList.Count;

        private Dictionary<TVertex, List<(TVertex, int)>> _adjacencyList;

        private Dictionary<TVertex, List<(TVertex, int)>> _edgeList;

        public DirectedWeightedGraph(int size = int.MinValue)
        {
            _adjacencyList = size == int.MinValue ? new Dictionary<TVertex, List<(TVertex, int)>>() 
                                                  : new Dictionary<TVertex, List<(TVertex, int)>>(size);
            
            _edgeList = new Dictionary<TVertex, List<(TVertex, int)>>();
        }

        public DirectedWeightedGraph(params TVertex[] vertices)
            : this(vertices.Length)
        {
            foreach (var v in vertices)
            {
                AddVertex(v);
            }
        }
        
        public IEnumerable<TVertex> GetVertices()
        {
            return _adjacencyList.Keys;
        }

        public IEnumerable<(TVertex v, TVertex w, int weight)> GetEdges()
        {
            foreach (KeyValuePair<TVertex, List<(TVertex, int)>> edge in _edgeList)
            {
                foreach (var (vertex, weight) in edge.Value)
                {
                    yield return (edge.Key, vertex, weight);
                }
            }
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
            
            if (_edgeList.ContainsKey(v))
            {
                _edgeList[v].Add((w, weight));
            }
            else
            {
                _edgeList.Add(v, new List<(TVertex, int)> {(w, weight)});
            }
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