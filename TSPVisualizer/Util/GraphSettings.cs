using Microsoft.AspNetCore.Components;
using TSPVisualizer.Core;

namespace TSPVisualizer.Util;

public class GraphSettings {
    public AppState AppState { get; set; }

    private int _maxX = 30;
    public int MaxX {
        get => _maxX;
        set {
            if (value == _maxX)
                return;
            if (value < 5 || value > 90)
                return;

            _maxX = value;
        }
    }

    private int _maxY = 30;
    public int MaxY {
        get => _maxY;
        set {
            if (value == _maxY)
                return;
            if (value < 5 || value > 90)
                return;

            _maxY = value;
        }
    }

    private int _nodeCount = 30;
    public int NodeCount {
        get => _nodeCount;
        set {
            if (value == _nodeCount)
                return;
            if (value < 3 || value > 90)
                return;

            _nodeCount = value;
        }
    }

    private int _startNode;
    public int StartNode {
        get => _startNode;
        set {
            if (value == _startNode)
                return;
            if (AppState.GraphState == null)
                return;
            if (value < 0 || value > AppState.GraphState.Graph.Nodes.Count - 1)
                return;

            _startNode = value;
        }
    }

    public GraphSettings(AppState appState) {
        AppState = appState;
    }
}
