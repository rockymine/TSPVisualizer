namespace TSPVisualizer.Core; 
public class GraphState {
    public required Graph Graph { get; init; }
    public GraphPath Path { get; private set; } = new();
    public GraphState? Previous { get; set; }
    public GraphState? Next { get; set; }

    public GraphState CloneAsNext() {
        var nextState = new GraphState {
            Graph = Graph,
            Previous = this,
            Path = Path
        };

        Next = nextState;
        return nextState;
    }
}
