using Excubo.Blazor.Canvas;
using Excubo.Blazor.Canvas.Contexts;
using Microsoft.AspNetCore.Components;
using TSPVisualizer.Core;
using TSPVisualizer.Core.Util;
using TSPVisualizer.Util;

namespace TSPVisualizer.Components; 
public partial class GraphDisplayComponent : ComponentBase {
    [Inject]
    public required AppState AppState { get; init; }

    public async Task Redraw() {        
        await FillCanvases();
        StateHasChanged();
    }

    // drawedges(a, o/n, style in dem o bzw n gerendert werden sollen)
    // alter graph a, o, rot aufrufen
    // neuer graph a, n, grün aufrufen
    // a immer in schwarz rendern, andere liste in grün

    public async Task FillCanvases() {
        if (AppState.CurrentCanvas == null || AppState.PreviousCanvas == null)
            return;
        if (AppState.GraphState == null)
            return;

        Console.WriteLine("FillCanvases");
        Console.WriteLine(AppState.CurrentCanvas.GetHashCode());

        var curr = AppState.GraphState;
        var prev = curr.Previous;

        var stateDifference = GraphUtil.ComputeStateDifference(prev, curr);

        await using var prevContext = await AppState.PreviousCanvas.GetContext2DAsync();
        await using var currContext = await AppState.CurrentCanvas.GetContext2DAsync();

        await ResetCanvas(currContext);
        await ResetCanvas(prevContext);

        await FillCanvas(prevContext, stateDifference.InBoth,
            stateDifference.InPrevious, CanvasRenderer.EdgeDeletedBrush);
        await FillCanvas(currContext, stateDifference.InBoth,
            stateDifference.InCurrent, CanvasRenderer.EdgeAddedBrush);
    }

    public async Task PleaseDrawALine() {
        if (AppState.CurrentCanvas == null)
            return;

        await using var currContext = await AppState.CurrentCanvas.GetContext2DAsync();

        await currContext.StrokeStyleAsync("red");
        await currContext.LineWidthAsync(2);
        await currContext.MoveToAsync(0, 0);
        await currContext.LineToAsync(500, 500);
        await currContext.StrokeAsync();
    }

    private async Task FillCanvas(Context2D context, HashSet<Edge> inBoth, List<Edge> inEitherOr, CanvasBrush brush) {
        var settings = AppState.CanvasSettings;

        Console.WriteLine("FillCanvas");
        
        await CanvasRenderer.DrawEdges(context, settings, inBoth, inEitherOr, brush);
        await CanvasRenderer.DrawNodes(context, settings, AppState.GraphState!);
    }

    public async Task ResetCanvas(Context2D context) {
        var settings = AppState.CanvasSettings;

        Console.WriteLine("ResetCanvas");
        Console.WriteLine($"MinPosX: {(int)settings.MinPos.X}; MinPosY: {(int)settings.MinPos.Y}; Width: {settings.Width}; Height: {settings.Height}");

        await context.ClearRectAsync((int)settings.MinPos.X, (int)settings.MinPos.Y, settings.Width, settings.Height);
        await context.FillStyleAsync("blue");
        await context.FillRectAsync((int)settings.MinPos.X, (int)settings.MinPos.Y, settings.Width, settings.Height);

        if (settings.ShowGrid)
            await CanvasRenderer.DrawGrid(context, settings);
    }

    public async Task ResetCanvases() {
        Console.WriteLine("ResetCanvases");

        if (AppState.PreviousCanvas != null)
            await ResetCanvas(await AppState.PreviousCanvas.GetContext2DAsync());

        if (AppState.CurrentCanvas != null)
            await ResetCanvas(await AppState.CurrentCanvas.GetContext2DAsync());
    }
}
