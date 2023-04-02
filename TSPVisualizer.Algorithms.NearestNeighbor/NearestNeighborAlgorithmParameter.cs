using TSPVisualizer.Core;

namespace TSPVisualizer.Algorithms.NearestNeighbor;

public class NearestNeighborAlgorithmParameter : AlgorithmParameter {
    public required Node Start { get; init; }
}
