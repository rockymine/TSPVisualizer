using System.Numerics;
using TSPVisualizer.Algorithms.NearestNeighbor;
using TSPVisualizer.Core;

namespace TSPVisualizer.Tests.Algorithms.NearestNeighbor;

[TestClass]
public class NearestNeighborTest {
    private Graph CreateGraph() {
        var nodes = new List<Node> {
            new Node {
                Index = 0,
                Position = new Vector2(0, 0)
            },
            new Node {
                Index = 1,
                Position = new Vector2(1, 0)
            },
            new Node {
                Index = 2,
                Position = new Vector2(1, 1)
            },
            new Node {
                Index = 3,
                Position = new Vector2(0, 1)
            }
        };

        var edges = new List<Edge> {
            new Edge(nodes[0], nodes[1], 1),
            new Edge(nodes[1], nodes[2], 1),
            new Edge(nodes[2], nodes[3], 1),
            new Edge(nodes[3], nodes[0], 1),
        };

        return new Graph(nodes, edges);
    }

    [TestMethod]
    public void ClosesPathCorrectly() {
        //Arrange: Create what you want to test.
        var initialState = new GraphState { Graph = CreateGraph() };
        var algorithm = new NearestNeigborAlgorithm();
        var algorithmParameters = new NearestNeighborAlgorithmParameter { Start = initialState.Graph.Nodes[0] };

        //Act
        var lastState = algorithm.Solve(initialState, algorithmParameters);

        //Assert
        Assert.AreEqual(true, lastState.IsClosed);
    }

    [TestMethod]
    public void SolvesPathCorrectly() {
        //Arrange: Create what you want to test.
        var initialState = new GraphState { Graph = CreateGraph() };
        var algorithm = new NearestNeigborAlgorithm();
        var algorithmParameters = new NearestNeighborAlgorithmParameter { Start = initialState.Graph.Nodes[0] };

        //Act
        var lastState = algorithm.Solve(initialState, algorithmParameters);

        //Assert
        Assert.AreEqual(0, lastState.Path[0].Index);
        Assert.AreEqual(1, lastState.Path[1].Index);
        Assert.AreEqual(2, lastState.Path[2].Index);
        Assert.AreEqual(3, lastState.Path[3].Index);
    }
}
