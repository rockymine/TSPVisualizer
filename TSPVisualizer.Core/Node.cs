using System.Numerics;

namespace TSPVisualizer.Core;

public class Node {
    private readonly List<Edge> _edges = new();

    public required int Index { get; init; }
    public required Vector2 Position { get; init; }
    public IReadOnlyList<Edge> Edges => _edges;

    public void AddEdge(Edge edge) => _edges.Add(edge);
    public Edge EdgeTo(Node node) => _edges.Find(e => e.Node2 == node) 
        ?? throw new InvalidOperationException();
}
