using Microsoft.AspNetCore.Components;

namespace TSPVisualizer.Components; 
public partial class NearestNeighborSettings : ComponentBase {
    private int startNode;
    [Parameter]
    public int StartNode {
        get => startNode;
        set {
            if (value == startNode)
                return;

            startNode = value;
            StartNodeChanged.InvokeAsync(value);
        }
    }
    [Parameter]
    public EventCallback<int> StartNodeChanged { get; set; }
}
