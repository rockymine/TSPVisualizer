namespace TSPVisualizer.Core; 
public abstract class Algorithm {
    public abstract string Name { get; }
    public abstract GraphState Solve(GraphState state, AlgorithmParameter parameter);
}
