using System.Numerics;
using System.Runtime.Intrinsics.X86;
using TSPVisualizer.Core;
using TSPVisualizer.Core.Util;

namespace TSPVisualizer.Tests.Core;

[TestClass]
public class GraphUtilTest {
    private List<Node> CreateNodes() {
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
                Position = new Vector2(2, 2)
            },
            new Node {
                Index = 3,
                Position = new Vector2(0, 2)
            }
        };

        return nodes;
    }

    [TestMethod]
    public void ConnectsNodesCorrectly() {
        //Arrange
        var nodes = CreateNodes();

        //Act
        var edges = GraphUtil.ConnectAllNodes(nodes);

        //Assert
        Assert.AreEqual(12, edges.Count);
    }

    [TestMethod]
    public void AddsEdgesCorrectly() {
        //Arrange
        var nodes = CreateNodes();

        //Act
        _ = GraphUtil.ConnectAllNodes(nodes);

        //Assert
        Assert.AreEqual(6, nodes[0].Edges.Count);
        Assert.AreEqual(6, nodes[1].Edges.Count);
        Assert.AreEqual(6, nodes[2].Edges.Count);
        Assert.AreEqual(6, nodes[3].Edges.Count);
    }

    [TestMethod]
    public void FindsClosestNodeCorrectly() {
        //Arrange
        var nodes = CreateNodes();

        //Act
        var closest = GraphUtil.GetClosestNode(nodes, nodes[0].Position);

        //Assert
        Assert.AreEqual((nodes[0], 0), closest);
    }

    [TestMethod]
    public void CreatesRandomGraphCorrectly() { 
        //Arrange + Act
        var random = new Random(8516);
        var graph = GraphUtil.RandomGraph(20, 20, 10, random);

        //Assert
        Assert.AreEqual(10, graph.Nodes.Count);
    }
}
