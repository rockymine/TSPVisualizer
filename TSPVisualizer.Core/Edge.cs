using System.Numerics;
using System.Xml.Linq;

namespace TSPVisualizer.Core;

public class Edge {
    public Node Node1 { get; }
    public Node Node2 { get; }
    public float Distance { get; }

    public Edge(Node node1, Node node2, float distance) { 
        Node1 = node1;
        Node2 = node2;
        Distance = distance;

        node1.AddEdge(this);
        node2.AddEdge(this);
    }

    public Node Opposite(Node node) {
        if (node == Node1)
            return Node2;
        if (node == Node2)
            return Node1;

        throw new ArgumentException("No opposite node.");
    }

    public bool IsEqual(Edge edge) {
        if (Node1 == edge.Node1 && Node2 == edge.Node2)
            return true;
        if (Node1 == edge.Node2 && Node2 == edge.Node1)
            return true;

        return false;
    }
}
