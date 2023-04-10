using Excubo.Blazor.Canvas.Contexts;
using Excubo.Blazor.Canvas;
using System.Numerics;
using TSPVisualizer.Util;

namespace TSPVisualizer.Extensions; 
public static class Context2DExtension {
    public static async Task DrawLine(this Context2D context, CanvasBrush brush, Vector2 pos1, Vector2 pos2) {
        if (brush.Color == "red") {
            await context.SetLineDashAsync(new double[] { 5 });
        } else {
            await context.SetLineDashAsync(Array.Empty<double>());
        }

        Console.WriteLine($"Color: {brush.Color}; Width: {brush.Width}; Pos1: ({pos1.X},{pos1.Y}); Pos2: ({pos2.X},{pos2.Y})");

        await context.StrokeStyleAsync(brush.Color);
        await context.LineWidthAsync(brush.Width);
        await context.BeginPathAsync();
        await context.MoveToAsync(pos1.X, pos1.Y);
        await context.LineToAsync(pos2.X, pos2.Y);
        await context.StrokeAsync();
    }

    public static async Task DrawCircle(this Context2D context, CanvasBrush brush, double radius, Vector2 pos) {
        await context.FillStyleAsync(brush.Color);
        await context.LineWidthAsync(brush.Width);
        await context.BeginPathAsync();
        await context.MoveToAsync(pos.X, pos.Y);
        await context.EllipseAsync(pos.X, pos.Y, radius, radius, 0, 0, 360);
        await context.FillAsync(FillRule.EvenOdd);
    }

    public static async Task DrawTextBox(this Context2D context, CanvasTextStyle textStyle, Vector2 pos, string text) {
        var metrics = await context.MeasureTextAsync(text);
        var width = metrics.Width * 1.2;
        var height = (metrics.FontBoundingBoxAscent + metrics.FontBoundingBoxDescent) * 1.2;

        //calculate bottom left corner
        var x = (float)(pos.X - (width / 2));
        var y = (float)(pos.Y - (width / 2));

        //create vectors for rectangle corners
        var bottomLeft = new Vector2(x, y);
        var topLeft = new Vector2(x, y + (float)height);
        var bottomRight = new Vector2(x + (float)width, y);
        var topRight = new Vector2(x + (float)width, y + (float)height);

        //draw rectangle
        await context.BeginPathAsync();
        await context.MoveToAsync(bottomLeft.X, bottomLeft.Y);
        await context.ArcToAsync(topLeft.X, topLeft.Y, topRight.X, topRight.Y, 10);
        await context.ArcToAsync(topRight.X, topLeft.Y, bottomRight.X, bottomRight.Y, 10);
        await context.ArcToAsync(bottomRight.X, bottomRight.Y, bottomLeft.X, bottomLeft.Y, 10);
        await context.ArcToAsync(bottomLeft.X, bottomLeft.Y, topLeft.X, topLeft.Y, 10);
        await context.FillStyleAsync("#3f74a8");
        await context.FillAsync(FillRule.EvenOdd);

        //put text into rectangle
        await WriteText(context, textStyle, text, pos);
    }

    public static async Task FillTextAsyncVector(this Context2D context, string text, Vector2 pos) {
        await context.FillTextAsync(text, pos.X, pos.Y);
    }

    public static async Task WriteText(this Context2D context, CanvasTextStyle textStyle, string text, Vector2 v2) {
        await context.FontAsync(textStyle.Font);
        await context.FillStyleAsync(textStyle.Style);
        await context.TextBaseLineAsync(TextBaseLine.Middle);
        await context.TextAlignAsync(TextAlign.Center);
        await context.FillTextAsyncVector(text, v2);
    }
}
