namespace TSPVisualizer.Core; 
public class GraphStateDifference {
    public HashSet<Edge> InBoth { get; set; }
    public List<Edge> InPrevious { get; set; }
    public List<Edge> InCurrent { get; set; }

    public GraphStateDifference(HashSet<Edge> inBoth, List<Edge> inPrev, List<Edge> inCurr) {
        InBoth = inBoth;
        InPrevious = inPrev;
        InCurrent = inCurr;
    }
}
