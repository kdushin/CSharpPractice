
namespace AlgorithmBasics.DataStructures.Graphs
{
    // TODO: Add iterator and IGraph implementations
    public interface IGraph 
    {
        int Count { get; }

        INode GetNode(int id);

        INode[] GetConnectedNodes(int id);

        IEdge[] GetEdges();
    }

    public interface INode 
    {
        
    }

    public interface IEdge
    {
        
    }
}