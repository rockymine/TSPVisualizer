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
}
