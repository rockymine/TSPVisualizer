namespace TSPVisualizer.Util; 
public class CanvasBrush {
    public string Color { get; }
    public double Width { get; }
    public FillStyle Style { get; }

    public CanvasBrush(string color, double width, FillStyle style) {
        Color = color;
        Width = width;
        Style = style;
    }
}

public enum FillStyle {
    Stroke,
    Fill
}
