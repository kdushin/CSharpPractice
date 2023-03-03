namespace AlgorithmBasics.DataStructures.Graph
{
    public interface IDirectedWeightedGraph<TVertex>
    {
        /// <summary>
        /// Returns the number of vertices in this directed graph.
        /// </summary>
        int VerticesCount { get; }
        
        // Returns the number of edges in this directed graph.
        int EdgesCount { get; }

        /// <summary>
        /// Returns all edges of current graph.
        /// </summary>
        IEnumerable<TVertex> GetVertices();
        
        /// <summary>
        /// Returns all edges of current graph.
        /// </summary>
        IEnumerable<(TVertex v, TVertex w, int weight)> GetEdges();
        
        void AddVertex(TVertex v);

        /// <summary>
        /// Adds the directed edge v -> w to this graph.
        /// </summary>
        /// <param name="v">the tail vertex</param>
        /// <param name="w">the head vertex</param>
        /// <param name="weight"></param>
        void AddEdge(TVertex v, TVertex w, int weight);

        /// <summary>
        /// Returns the vertices adjacent from vertex v in this directed graph. 
        /// </summary>
        /// <param name="v">vertex v</param>
        IEnumerable<(TVertex w, int weight)> GetAdjacentVertices(TVertex v);
    }
}