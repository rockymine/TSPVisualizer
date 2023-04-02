namespace TSPVisualizer.Core; 
public class GraphState {
    private List<Node> _path = new();

    public required Graph Graph { get; init; }
    // Path and IsClosed can be put into a GraphPath class.
    public IReadOnlyList<Node> Path => _path;
    public bool IsClosed { get; set; }

    public GraphState? Previous { get; set; }
    public GraphState? Next { get; set; }

    public void AddNodeToPath(Node node) => _path.Add(node);

    public GraphState CloneAsNext() {
        var nextState = new GraphState {
            Graph = Graph,
            Previous = this,
            _path = _path
        };

        Next = nextState;
        return nextState;
    }
}
