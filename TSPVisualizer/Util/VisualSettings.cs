namespace TSPVisualizer.Util; 

public class VisualSettings {
    private float _nodeRadius = 15;
    public float NodeRadius {
        get => _nodeRadius;
        set {
            if (value == _nodeRadius)
                return;
            if (value < 5 || value > 50)
                return;

            _nodeRadius = value;
        }
    }

    private int _animationDelay = 500;
    public int AnimationDelay {
        get => _animationDelay;
        set {
            if (value == _animationDelay)
                return;
            if (value < 1 || value > 10_000)
                return;

            _animationDelay = value;
        }
    }

    public bool AutoAdvance;
    public bool AnnotateEdges;
    public bool ColorizeChanges = true;
    public bool ShowGrid = true;

    public string BackgroundColor = "#000";
}
