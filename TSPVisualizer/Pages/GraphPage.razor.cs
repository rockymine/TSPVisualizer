using Microsoft.AspNetCore.Components;
using TSPVisualizer.Algorithms.NearestNeighbor;
using TSPVisualizer.Components;
using TSPVisualizer.Core;
using TSPVisualizer.Core.Util;
using TSPVisualizer.Util;

namespace TSPVisualizer.Pages; 
public partial class GraphPage : ComponentBase {
    [Inject]
    public required AppState AppState { get; init; }
    private ControlClusterComponent? ClusterComponent;
    private GraphDisplayComponent? DisplayComponent;

    protected override Task OnAfterRenderAsync(bool firstRender) {
        if (firstRender) {
            ClusterComponent.GraphDisplayComponent = DisplayComponent;

            Task.Run(async () => {
                while (true) {
                    await Task.Delay(AppState.VisualSettings.AnimationDelay);

                    if (AppState.VisualSettings.AutoAdvance)
                        await ClusterComponent!.AdvanceHistory(true);
                }
            });

            //AppState.FileInput.LoadedFileChanged += StateHasChanged;
        }

        return base.OnAfterRenderAsync(firstRender);
    }
    
    public async Task InitiateParameters() {
        var random = new Random();
        AppState.GraphState = new GraphState { 
            Graph = GraphUtil.RandomGraph(AppState.GraphSettings.MaxX,
            AppState.GraphSettings.MaxY, AppState.GraphSettings.NodeCount,
            random)
        };

        var minPos = GraphUtil.FindMin(AppState.GraphState.Graph.Nodes);
        var maxPos = GraphUtil.FindMax(AppState.GraphState.Graph.Nodes);

        AppState.CanvasSettings.MinPos = minPos;
        AppState.CanvasSettings.MaxPos = maxPos;
        AppState.CanvasSettings.Zoom = (float)(120 * Math.Pow(Math.E, -0.06 * minPos.X)) + 12;

        await UpdateEnumerator();
    }
    
    private async Task UpdateEnumerator() {
        //await DisplayComponent!.ResetCanvases();

        AppState.Algorithm = new NearestNeigborAlgorithm();
        AppState.AlgorithmParameter = new NearestNeighborAlgorithmParameter {
            Start = AppState.GraphState!.Graph.Nodes[
                AppState.GraphSettings.StartNode]
        };

        AppState.GraphState = AppState.Algorithm.Solve(
            AppState.GraphState, AppState.AlgorithmParameter);

        Console.WriteLine($"Previous: {(AppState.GraphState.Previous == null ? "null" : "exists")}; Next: {(AppState.GraphState.Next == null ? "null" : "exists")}");

        //StateHasChanged();
        await DisplayComponent!.Redraw();
        await DisplayComponent!.Redraw();
    }
}
