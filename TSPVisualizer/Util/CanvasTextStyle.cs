using System.Drawing;

namespace TSPVisualizer.Util; 
public class CanvasTextStyle {
    public string Font { get; }
    public string Style { get; }

    public CanvasTextStyle(string textFont, string textStyle) {
        Font = textFont;
        Style = textStyle;
    }
}
