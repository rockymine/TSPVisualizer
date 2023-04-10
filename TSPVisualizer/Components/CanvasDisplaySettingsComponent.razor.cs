using Microsoft.AspNetCore.Components;

namespace TSPVisualizer.Components; 
public partial class CanvasDisplaySettingsComponent : ComponentBase {
    private float _nodeRadius;
    [Parameter]
    public float NodeRadius {
        get => _nodeRadius;
        set {
            if (value == _nodeRadius)
                return;

            _nodeRadius = value;
            NodeRadiusChanged.InvokeAsync(value);
        }
    }
    [Parameter]
    public EventCallback<float> NodeRadiusChanged { get; set; }

    private bool _showGrid;
    [Parameter]
    public bool ShowGrid {
        get => _showGrid;
        set {
            if (value == _showGrid)
                return;

            _showGrid = value;
            ShowGridChanged.InvokeAsync(value);
        }
    }
    [Parameter]
    public EventCallback<bool> ShowGridChanged { get; set; }

    private bool _annotateEdges;
    [Parameter]
    public bool AnnotateEdges {
        get => _annotateEdges;
        set {
            if (value == _annotateEdges)
                return;

            _annotateEdges = value;
            AnnotateEdgesChanged.InvokeAsync(value);
        }
    }
    [Parameter]
    public EventCallback<bool> AnnotateEdgesChanged { get; set; }

    private bool _colorizeChanges;
    [Parameter]
    public bool ColorizeChanges {
        get => _colorizeChanges;
        set {
            if (value == _colorizeChanges)
                return;

            _colorizeChanges = value;
            ColorizeChangesChanged.InvokeAsync(value);
        }
    }
    [Parameter]
    public EventCallback<bool> ColorizeChangesChanged { get; set; }

    private int _animationDelay;
    [Parameter]
    public int AnimationDelay {
        get => _animationDelay;
        set {
            if (value == _animationDelay)
                return;

            _animationDelay = value;
            AnimationDelayChanged.InvokeAsync(value);
        }
    }
    [Parameter]
    public EventCallback<int> AnimationDelayChanged { get; set; }
}
