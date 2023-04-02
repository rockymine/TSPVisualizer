using TSPVisualizer.Core;
using TSPVisualizer.Core.Util;

namespace TSPVisualizer.Algorithms.NearestNeighbor;

public class NearestNeigborAlgorithm : Algorithm {
    public override GraphState Solve(GraphState state, AlgorithmParameter parameter) {
        if (parameter is not NearestNeighborAlgorithmParameter nnparameter)
            throw new ArgumentException("Parameter must be of type NearestNeighborAlgorithmParameter", nameof(parameter));

        var current = nnparameter.Start;
        var visitedNodes = new HashSet<Node> { current };
        state = state.CloneAsNext();
        state.AddNodeToPath(current);

        while (true) {
            var node = NodeUtil.FindShortest(current, visitedNodes);

            if (node == null) {
                state.IsClosed = true;
                break;
            }

            current = node;
            visitedNodes.Add(current);
            state.AddNodeToPath(current);
            state = state.CloneAsNext();
        }

        return state;
    }
}