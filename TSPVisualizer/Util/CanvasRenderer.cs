using Excubo.Blazor.Canvas.Contexts;
using Microsoft.AspNetCore.Components.RenderTree;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;
using TSPVisualizer.Core;
using TSPVisualizer.Core.Util;
using TSPVisualizer.Extensions;

namespace TSPVisualizer.Util; 
public static class CanvasRenderer {

    public static readonly CanvasBrush NodeBrush = new("#4e5072", 2, FillStyle.Fill);
    public static readonly CanvasTextStyle NodeTextStyle = new("35px serif bold", "white");
    public static readonly CanvasBrush EdgeBrush = new("black", 2, FillStyle.Stroke);
    public static readonly CanvasBrush EdgeAddedBrush = new("green", 2, FillStyle.Stroke);
    public static readonly CanvasBrush EdgeDeletedBrush = new("red", 2, FillStyle.Stroke);
    public static readonly CanvasTextStyle EdgeTextStyle = new("35px serif", "black");
    public static readonly CanvasBrush GridBrush = new("#999999", 1, FillStyle.Stroke);

    public static async Task DrawGrid(Context2D context, CanvasSettings settings) {
        for (int i = (int)settings.MinPos.Y; i <= settings.MaxPos.Y; i++) {
            await context.DrawLine(GridBrush,
                Manipulate(new Vector2((int)settings.MinPos.X, i), settings),
                Manipulate(new Vector2(settings.MaxPos.X, i), settings));
        }
        for (int i = (int)settings.MinPos.X; i <= settings.MaxPos.X; i++) {
            await context.DrawLine(GridBrush,
                Manipulate(new Vector2(i, (int)settings.MinPos.Y), settings),
                Manipulate(new Vector2(i, settings.MaxPos.Y), settings));
        }
    }

    public static async Task DrawNodes(Context2D context, CanvasSettings settings, GraphState state) {
        var nodes = state.Graph.Nodes;

        Console.WriteLine("DrawNodes");

        foreach (var node in nodes) {
            await context.DrawCircle(NodeBrush, settings.NodeRadius,
                Manipulate(node.Position, settings));

            await context.WriteText(NodeTextStyle, node.Index.ToString(),
                Manipulate(node.Position, settings));
        }
    }

    public static async Task DrawEdges(Context2D context, CanvasSettings settings, List<Edge> edges) {
        foreach (var edge in edges) {
            await context.DrawLine(EdgeBrush,
                Manipulate(edge.Node1.Position, settings),
                Manipulate(edge.Node2.Position, settings));
        }
    }

    public static async Task DrawEdges(Context2D context, CanvasSettings settings, HashSet<Edge> inBoth, List<Edge> inEitherOr, CanvasBrush brush) {
        Console.WriteLine("DrawEdges");
        
        foreach (var edge in inBoth) {
            await context.DrawLine(EdgeBrush,
                Manipulate(edge.Node1.Position, settings),
                Manipulate(edge.Node2.Position, settings));
        }

        foreach (var edge in inEitherOr) {
            await context.DrawLine(brush,
                Manipulate(edge.Node1.Position, settings),
                Manipulate(edge.Node2.Position, settings));
        }
    }

    private static Vector2 Manipulate(Vector2 vector, CanvasSettings settings) {
        return ((vector - settings.MinPos) * settings.Zoom + settings.Offset).InverseY(settings.Height);
    }
}
