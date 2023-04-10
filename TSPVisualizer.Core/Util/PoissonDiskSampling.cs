using System.Numerics;

namespace TSPVisualizer.Core.Util;

public static class PoissonDiskSampling {
    public static List<Vector2> Sample2D(float radius, int k, int width, int height, Random random) {
        var points = new List<Vector2>();
        var active = new List<Vector2>();
        var p0 = new Vector2(random.Next(width), random.Next(height));
        var cellSize = (float)Math.Floor(radius / Math.Sqrt(2));
        var nCellsWidth = (int)Math.Ceiling(width / cellSize) + 1;
        var nCellsHeigth = (int)Math.Ceiling(height / cellSize) + 1;

        var grid = new Vector2[nCellsWidth, nCellsHeigth];

        // Insert Points
        InsertPoint(grid, p0, cellSize);
        points.Add(p0);
        active.Add(p0);

        while(active.Count > 0) {
            // Pick a point from the active list
            var randomIndex = random.Next(active.Count);
            var point = active[randomIndex];

            // Try up to k times to find a satisfactory point
            bool found = false;
            for (int i = 0; i < k; i++) {
                // create a new point
                var theta = random.NextSingle() * MathF.Tau;
                var newRadius = (random.NextSingle() * radius) + radius;
                var pointNewX = point.X + newRadius * MathF.Cos(theta);
                var pointNewY = point.Y + newRadius * MathF.Sin(theta);
                var pointNew = new Vector2(pointNewX, pointNewY);

                // check if it is a valid point
                if (IsValidPoint(grid, pointNew, width, height, cellSize, radius)) {
                    // add point and set it to found if valid
                    points.Add(pointNew);
                    InsertPoint(grid, pointNew, cellSize);
                    found = true;
                    break;
                }
            }

            // If no point was found after k tries, remove point
            if (!found)
                active.RemoveAt(randomIndex);
        }
        
        return points;
    }

    private static void InsertPoint(Vector2[,] grid, Vector2 point, float cellSize) {
        var xindex = (int)Math.Floor(point.X / cellSize);
        var yindex = (int)Math.Floor(point.Y / cellSize);
        grid[xindex,yindex] = point;
    }

    private static bool IsValidPoint(Vector2[,] grid, Vector2 point, int width, int height, float cellSize, float radius) {
        // Make sure the point is on the grid
        if (point.X < 0 || point.Y < 0 || point.X >= width || point.Y >= height)
            return false;

        // Check neighboring eight cells
        var xIndex = (int)Math.Floor(point.X / cellSize);
        var yIndex = (int)Math.Floor(point.Y / cellSize);
        var i0 = Math.Max(xIndex - 1, 0);
        var i1 = Math.Min(xIndex + 1, width - 1);
        var j0 = Math.Max(yIndex - 1, 0);
        var j1 = Math.Min(yIndex + 1, height - 1);

        for (var i = i0; i <= i1; i++) {
            for (var j = j0; j <= j1; j++) {
                if (grid[i, j] == Vector2.Zero)
                    continue;

                if (Vector2.Distance(grid[i, j], point) < radius)
                    return false;
            }
        }

        return true;
    }
}
