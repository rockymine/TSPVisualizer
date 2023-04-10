namespace TSPVisualizer.Core;

public class GraphPath {
    private List<Node> _path = new();
    public IReadOnlyList<Node> Path => _path;
    public bool IsClosed { get; set; }

    public void AddNodeToPath(Node node) => _path.Add(node);
}
