using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AlgorithmBasics.DataStructures.Graph.GraphImplementations
{
    public class DirectedGraphAdjList<TVertex> : IDirectedGraphWithReversed<TVertex>
    {
        public int VerticesCount => _adjacencyList.Keys.Count;

        public int EdgesCount { get; private set; }

        private readonly Dictionary<TVertex, List<TVertex>> _adjacencyList;

        private readonly Dictionary<TVertex, List<TVertex>> _reversedList;

        private readonly bool _saveReversedVersion;

        public DirectedGraphAdjList(bool saveReversedVersion = false, int size = int.MinValue)
        {
            _adjacencyList = size == int.MinValue ? new Dictionary<TVertex, List<TVertex>>()
                                                  : new Dictionary<TVertex, List<TVertex>>(size);
            EdgesCount = 0;
            _saveReversedVersion = saveReversedVersion;
            if (_saveReversedVersion)
            {
                _reversedList = size == int.MinValue ? new Dictionary<TVertex, List<TVertex>>() 
                                                     : new Dictionary<TVertex, List<TVertex>>(size);
            }
        }

        public DirectedGraphAdjList(bool saveReversedVersion = false, params TVertex[] vertices)
            : this(saveReversedVersion, vertices.Length)
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

        public IEnumerable<(TVertex v1, TVertex v2)> GetEdges()
        {
            throw new NotImplementedException();
        }

        public void AddVertex(TVertex v)
        {
            if (!ContainsVertex(v))
            {
                _adjacencyList.Add(v, new List<TVertex>());
            }
            if (_saveReversedVersion && !_reversedList.ContainsKey(v))
            {
                _reversedList.Add(v, new List<TVertex>());
            }
        }

        public void AddEdge(TVertex v, TVertex w)
        {
            if (!ContainsVertex(v))
            {
                throw new InvalidOperationException($"Graph doesn't contain vertex {v}");
            }
            if (!ContainsVertex(w))
            {
                throw new InvalidOperationException($"Graph doesn't contain vertex {w}");
            }
            _adjacencyList[v].Add(w);
            EdgesCount++;
            
            if (_saveReversedVersion)
            {
                _reversedList[w].Add(v);
            }
        }

        public IEnumerable<TVertex> GetAdjacentVertices(TVertex v)
        {
            _adjacencyList.TryGetValue(v, out List<TVertex> value);
            if (value == null)
            {
                throw new ArgumentOutOfRangeException(nameof(v), "Vertex doesn't exist in graph");
            }
            return value;
        }

        public IEnumerable<TVertex> GetIncomingVertices(TVertex v)
        {
            _reversedList.TryGetValue(v, out var value);
            if (value == null)
            {
                throw new ArgumentOutOfRangeException(nameof(v), "Vertex doesn't exist in reversed graph");
            }
            return value;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine($"Vertices: {VerticesCount}, Edges: {EdgesCount}");

            foreach (var v in GetVertices())
            {
                builder.Append($"{v}: ");

                foreach (var w in GetAdjacentVertices(v))
                {
                    builder.Append($"{w} ");
                }
                builder.AppendLine();
            }

            return builder.ToString();
        }

        private bool ContainsVertex(TVertex vertex)
        {
            return _adjacencyList.ContainsKey(vertex);
        }
    }

    public static class GraphHelper 
    { 
        public static IDirectedGraphWithReversed<int> ParseFromText(StreamReader reader, bool saveReversedVersion)
        {
            var result = new DirectedGraphAdjList<int>(saveReversedVersion);

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (line == null) { continue; }

                var items = line.Split(' ');
                var v = int.Parse(items[0]);
                var w = int.Parse(items[1]);
                result.AddVertex(v);
                result.AddVertex(w);
                result.AddEdge(v, w);
            }

            return result;
        }

        public static IDirectedGraph<int> CreateWithCapacity(int count)
        {
            var result = new DirectedGraphAdjList<int>();

            for (int i = 1; i <= count; i++)
            {
                result.AddVertex(i);
            }

            return result;
        }
        
        public static IDirectedGraph<int> ReverseGraph(IDirectedGraph<int> graph)
        {
            var reversedGraph = new DirectedGraphAdjList<int>(saveReversedVersion: false,
                                                           vertices: graph.GetVertices().ToArray());

            foreach (int v in graph.GetVertices())
            {
                foreach (int w in graph.GetAdjacentVertices(v))
                {
                    reversedGraph.AddEdge(w, v);
                }
            }
            
            return reversedGraph;
        }
    }
}