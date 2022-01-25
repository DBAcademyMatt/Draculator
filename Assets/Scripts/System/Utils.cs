using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static Vector2Int[] PointsOnLine(Vector2Int start, Vector2Int end)
    {
        //same point? no need to run, return start point.
        if (start == end)
            return new Vector2Int[] { start };

        List<Vector2Int> Points = new List<Vector2Int>();

        //formula for a line.
        int mX1 = start.x;
        int mY1 = start.y;
        int mX2 = end.x;
        int mY2 = end.y;

        int xDiff = mX1 - mX2;
        int yDiff = mY1 - mY2;
        bool xDiffLargest = Mathf.Abs(xDiff) > Mathf.Abs(yDiff);

        int xMod = xDiff < 0 ? 1 : -1;
        int yMod = yDiff < 0 ? 1 : -1;

        int shortSideLength = Mathf.Min(Mathf.Abs(xDiff), Mathf.Abs(yDiff));
        int longSideLength = Mathf.Max(Mathf.Abs(xDiff), Mathf.Abs(yDiff));
        
        float gradient = (shortSideLength == 0 || longSideLength == 0) ? 0 
            : ((float)(shortSideLength + 1) / (longSideLength + 1));

        int shortSideIncrease;
        for (int i = 0; i <= longSideLength; i++)
        {
            shortSideIncrease = Mathf.RoundToInt(i * gradient);

            int yIncrease, xIncrease;
            if (xDiffLargest)
            {
                xIncrease = i;
                yIncrease = shortSideIncrease;
            }
            else
            {
                xIncrease = shortSideIncrease;
                yIncrease = i;
            }

            int x = mX1 + (xIncrease * xMod);
            int y = mY1 + (yIncrease * yMod);

            Points.Add(new Vector2Int(x, y));
        }

        return Points.ToArray();
    }
}
