namespace TSPVisualizer.Core;

public class Graph {
    private readonly List<Node> _nodes;
    private readonly List<Edge> _edges;

    public IReadOnlyList<Node> Nodes => _nodes;
    public IReadOnlyList<Edge> Edges => _edges;

    public Graph(List<Node> nodes, List<Edge> edges) { 
        _nodes = nodes;
        _edges = edges;
    }
}
