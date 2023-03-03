namespace AlgorithmBasics.DataStructures.Graph
{
    public interface IDirectedGraph<TVertex>
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
        IEnumerable<(TVertex v1, TVertex v2)> GetEdges();
        
        void AddVertex(TVertex v);

        /// <summary>
        /// Adds the directed edge v -> w to this graph.
        /// </summary>
        /// <param name="v">the tail vertex</param>
        /// <param name="w">the head vertex</param>
        void AddEdge(TVertex v, TVertex w);

        /// <summary>
        /// Returns the vertices adjacent from vertex v in this directed graph. 
        /// </summary>
        /// <param name="v">vertex v</param>
        IEnumerable<TVertex> GetAdjacentVertices(TVertex v);

        // /// <summary>
        // /// Returns the degree of vertex v.
        // /// </summary>
        // /// <param name="v">vertex</param>
        // /// <returns></returns>
        // int GetVertexDegree(int v);
    }

    public interface IDirectedGraphWithReversed<TVertex> : IDirectedGraph<TVertex>
    {
        /// <summary>
        /// Returns outgoing vertices for specified vertex.
        /// <para>There is an edge E with tail vertex v and head vertex w (v -> w).</para>
        /// <para>Then GetIncomingVertices(w) returns vertex v</para>
        /// </summary>
        IEnumerable<TVertex> GetIncomingVertices(TVertex v);
    }
}