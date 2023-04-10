using Excubo.Blazor.Canvas;
using System.Diagnostics;
using TSPVisualizer.Algorithms.NearestNeighbor;
using TSPVisualizer.Components;
using TSPVisualizer.Core;

namespace TSPVisualizer.Util;

public class AppState {
    // Graph
    public GraphState? GraphState { get; set; }
    
    // Algorithm
    public Algorithm? Algorithm { get; set; }
    public AlgorithmParameter? AlgorithmParameter { get; set; }

    // Settings
    public VisualSettings VisualSettings = new();
    public GraphSettings GraphSettings { get; } 
    public CanvasSettings CanvasSettings = new();

    // Canvas
    public Canvas? PreviousCanvas { get; set; }
    public Canvas? CurrentCanvas { get; set; }

    //public Stopwatch Stopwatch = new();
    //public TimeSpan TimeSpan = new();

    public AppState() {
        GraphSettings = new(this);
    }
}
