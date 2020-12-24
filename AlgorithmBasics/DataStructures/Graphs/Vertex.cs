using System.Collections.Generic;
using System.Linq;

namespace AlgorithmBasics.DataStructures.Graphs
{
    public class Vertex
    {
        public readonly List<Vertex> AdjacentVertices;
        public readonly int Id;
        
        public static List<Vertex> Init(Dictionary<int, List<int>> dictionary)
        {
            var vertices = new Dictionary<int, Vertex>();

            foreach (KeyValuePair<int,List<int>> pair in dictionary)
            {
                if (!vertices.ContainsKey(pair.Key))
                {
                    vertices.Add(pair.Key, new Vertex(pair.Key));
                }
                
                Vertex vertex = vertices[pair.Key];
                
                foreach (int adjacentVertexId in pair.Value)
                {
                    if (vertex.Id > adjacentVertexId) continue;
                    
                    if (!vertices.ContainsKey(adjacentVertexId))
                    {
                        vertices.Add(adjacentVertexId, new Vertex(adjacentVertexId));
                    }

                    Vertex adjVertex = vertices[adjacentVertexId];
                    vertex.AddAdjs(adjVertex);
                }
            }

// #if DEBUGX
//             Console.WriteLine("INIT::: GRAPH:");
//             foreach (var vx in vertices.Values)
//             {
//                 Console.WriteLine(vx.ToString());
//             }
//             Console.WriteLine();
// #endif
            
            return vertices.Select(kvp => kvp.Value).ToList();
        }

        public void AddAdjs(params Vertex[] adjs)
        {
            foreach (var adj in adjs)
            {
                AdjacentVertices.Add(adj);
                adj.AdjacentVertices.Add(this);
                
//                     stringBuilder.AppendLine($"Added vx{adj.Id} to vx{Id}");
//                     stringBuilder.AppendLine(this.ToString());
//                     stringBuilder.AppendLine(adj.ToString());
//                     stringBuilder.AppendLine();
            }
        }

        public override string ToString()
        {
            return $"{Id}: {string.Join(",", AdjacentVertices.Select(v => v.Id))} ({AdjacentVertices.Count})";
        }

        public void RemoveVertex(Vertex otherVertex)
        {
            while (otherVertex.AdjacentVertices.Remove(this)) { }

            while (AdjacentVertices.Remove(otherVertex)) { }
            
//                 stringBuilder.AppendLine($"Removed {otherVertex.Id} from {Id}");
//                 stringBuilder.AppendLine(this.ToString());
//                 stringBuilder.AppendLine(otherVertex.ToString());
//                 stringBuilder.AppendLine();
        }

        public Vertex(int id)
        {
            Id = id;
            AdjacentVertices = new List<Vertex>();
        }

        public void MergeWith(Vertex other)
        {
//                 stringBuilder.AppendLine($"Going to merge {other.Id} to {Id}");
//                 stringBuilder.AppendLine();

            var vxToRemove = new List<Vertex>();

            foreach (var adjVertex in other.AdjacentVertices)
            {
                if (adjVertex != this)
                {
                    vxToRemove.Add(adjVertex);
                    AddAdjs(adjVertex);
                }
            }

            foreach (var vx in vxToRemove)
            {
                other.RemoveVertex(vx);
            }

            RemoveVertex(other);

//                 stringBuilder.AppendLine($"Merged vx{other.Id} to vx{Id}");
//                 stringBuilder.AppendLine(this.ToString());
//                 stringBuilder.AppendLine(other.ToString());
//                 stringBuilder.AppendLine();
        }
    }
}