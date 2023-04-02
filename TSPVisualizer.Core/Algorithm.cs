namespace TSPVisualizer.Core; 
public abstract class Algorithm {
    public abstract GraphState Solve(GraphState state, AlgorithmParameter parameter);
}
