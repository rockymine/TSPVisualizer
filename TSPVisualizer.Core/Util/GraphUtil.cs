using System.Numerics;
using System.Xml.Linq;

namespace TSPVisualizer.Core.Util;

public static class GraphUtil {
    public static Graph RandomGraph(int maxX, int maxY, int maxNodes, Random random) {
        maxNodes = Math.Min(maxNodes, maxX * maxY);
        var nodes = new List<Node>();

        for (int i = 0; i < maxNodes; i++) {
            Node node;
            do {
                node = RandomNode(i, maxX, maxY, random);
            } while (GetClosestNode(nodes, node.Position).distance > 0.9);

            nodes.Add(node);
        }

        return new Graph(nodes, ConnectAllNodes(nodes));
    }

    private static Node RandomNode(int index, int maxX, int maxY, Random random) {
        return new Node {
            Index = index,
            Position = new Vector2(
                (int)(random.NextDouble() * maxX),
                (int)(random.NextDouble() * maxY))
        };
    }

    public static (Node? node, double distance) GetClosestNode(List<Node> nodes, Vector2 position) {
        if (nodes.Count == 0)
            return (null, double.PositiveInfinity);
        if (nodes.Count == 1)
            return (nodes[0], Vector2.Distance(nodes[0].Position, position));

        Node? closest = null;
        var closestDistanceSqr = double.MaxValue;

        foreach (var node in nodes) {
            var distanceSqr = (node.Position - position).LengthSquared();

            if (distanceSqr < closestDistanceSqr) {
                closest = node;
                closestDistanceSqr = distanceSqr;
            }
        }

        return (closest, Math.Sqrt(closestDistanceSqr));
    }

    public static List<Edge> ConnectAllNodes(List<Node> nodes) {
        var edges = new List<Edge>();

        foreach (var node1 in nodes) {
            foreach (var node2 in nodes) {
                if (node1 == node2)
                    continue;

                edges.Add(new Edge(node1, node2, Vector2.Distance(node1.Position, node2.Position)));
            }
        }

        return edges;
    }
}
