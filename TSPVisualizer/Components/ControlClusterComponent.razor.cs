using Microsoft.AspNetCore.Components;
using TSPVisualizer.Util;

namespace TSPVisualizer.Components; 
public partial class ControlClusterComponent : ComponentBase {
    [Inject]
    public required AppState AppState { get; init; }
    public GraphDisplayComponent GraphDisplayComponent { get; set; }

    public async Task JumpFirst() {
        if (AppState.GraphState == null)
            return;

        AppState.VisualSettings.AutoAdvance = false;

        while (AppState.GraphState.Previous != null)
            AppState.GraphState = AppState.GraphState.Previous;

        await GraphDisplayComponent.Redraw();
    }

    public async Task JumpLast() {
        if (AppState.GraphState == null)
            return;

        AppState.VisualSettings.AutoAdvance = false;

        while (AppState.GraphState.Next != null)
            AppState.GraphState = AppState.GraphState.Next;

        await GraphDisplayComponent.Redraw();
    }

    public async Task AdvanceHistory(bool auto = false) {
        if (AppState.GraphState == null)
            return;

        if (!auto)
            AppState.VisualSettings.AutoAdvance = false;

        if (AppState.GraphState.Next == null) {
            AppState.VisualSettings.AutoAdvance = false;
            return;
        }

        AppState.GraphState = AppState.GraphState.Next;
        await GraphDisplayComponent.Redraw();

    }

    public async Task ReverseHistory() {
        if (AppState.GraphState == null)
            return;

        AppState.VisualSettings.AutoAdvance = false;

        if (AppState.GraphState.Previous == null) {
            AppState.VisualSettings.AutoAdvance = false;
            return;
        }

        AppState.GraphState = AppState.GraphState.Previous;
        await GraphDisplayComponent.Redraw();
    }
}
