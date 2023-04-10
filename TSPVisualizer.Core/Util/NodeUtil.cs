using System.Xml.Linq;

namespace TSPVisualizer.Core.Util;

public static class NodeUtil {
    public static Node? FindShortest(Node node, HashSet<Node> visited) {
        Node? closest = null;
        var closestDistance = double.MaxValue;

        foreach (var edge in node.Edges) {
            var opposite = edge.Opposite(node);

            if (visited.Contains(opposite))
                continue;

            if (edge.Distance < closestDistance) {
                closestDistance = edge.Distance;
                closest = opposite;
            }
        }

        return closest;
    }

    public static IEnumerable<Edge> FindPathEdges(GraphPath path) {
        for (var i = 0; i < (path.IsClosed ? path.Path.Count : path.Path.Count - 1); i++)
            yield return path.Path[i].EdgeTo(path.Path[(i + 1) % path.Path.Count]);
    }
}
