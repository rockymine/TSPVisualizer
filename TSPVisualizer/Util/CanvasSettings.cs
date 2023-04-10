using System.Numerics;

namespace TSPVisualizer.Util;

public class CanvasSettings {
    public Vector2 MinPos { get; set; }
    public Vector2 MaxPos { get; set; }
    public Vector2 Offset => new(Zoom / 2, Zoom / 2);

    public float Zoom { get; set; }
    public float NodeRadius { get; set; }

    public int Height => (int)(Zoom * (MaxPos.Y - MinPos.Y + 1));
    public int Width => (int)(Zoom * (MaxPos.X - MinPos.X + 1));

    public bool Colorize { get; set; }
    public bool Annotate { get; set; }
    public bool ShowGrid { get; set; } = true;

    public string BackgroundColor = "#000";
}
