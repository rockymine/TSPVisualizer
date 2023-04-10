using System.Numerics;
using System.Xml.Linq;

namespace TSPVisualizer.Core.Util;

public static class GraphUtil {
    public static Graph RandomGraph(int maxX, int maxY, int maxNodes, Random random) {
        maxNodes = Math.Min(maxNodes, maxX * maxY);
        var nodes = new List<Node>();

        // Create list of possible points
        var candidates = PoissonDiskSampling.Sample2D(2f, 30, maxX, maxY, random);
        var index = 0;

        // Choose specified amount of nodes at random
        while (maxNodes > 0 && candidates.Count > 0) {
            var randomIndex = random.Next(candidates.Count);
            var randomPosition = candidates[randomIndex];
            candidates.RemoveAt(randomIndex);
            
            var newNode = new Node {
                Index = index,
                Position = randomPosition
            };
            
            index++;
            maxNodes--;
            nodes.Add(newNode);
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

    public static Vector2 FindMin(IReadOnlyList<Node> nodes) {
        var x = nodes.Min(n => n.Position.X);
        var y = nodes.Min(n => n.Position.Y);
        return new Vector2(x, y);
    }

    public static Vector2 FindMax(IReadOnlyList<Node> nodes) {
        var x = nodes.Max(n => n.Position.X);
        var y = nodes.Max(n => n.Position.Y);
        return new Vector2(x, y);
    }

    public static GraphStateDifference ComputeStateDifference(GraphState? oldState, GraphState newState) {
        if (oldState == null) {
            return new GraphStateDifference(new HashSet<Edge>(NodeUtil.FindPathEdges(newState.Path)),
                new List<Edge>(), new List<Edge>());
        }

        var inBoth = new HashSet<Edge>();
        var onlyInOld = new List<Edge>();
        var onlyInNew = new List<Edge>();

        var oldStateEdges = NodeUtil.FindPathEdges(oldState.Path);
        var newStateEdges = NodeUtil.FindPathEdges(newState.Path);

        foreach (var edge in oldStateEdges) {
            if (newStateEdges.Contains(edge)) {
                inBoth.Add(edge);
            } else {
                onlyInOld.Add(edge);
            }
        }

        foreach (var edge in newStateEdges) {
            if (!inBoth.Contains(edge))
                onlyInNew.Add(edge);
        }

        return new GraphStateDifference(inBoth, onlyInOld, onlyInNew);
    }
}
