using System;
using System.Collections.Generic;

namespace KaiGameUtil
{
    public static class Util
    {
        public static float Clamp(float value, float min, float max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }
        public static float Lerp(float a, float b, float pct)
        {
            return a + (b - a) * Clamp(pct, 0, 1);
        }
        public static float Magnitude(Point<float> a)
        {
            return (float)Math.Sqrt(Math.Pow(a.x, 2) + Math.Pow(a.y, 2));
        }
        public static bool NearlyEqual(float a, float b, float tolerance = 0.0001f)
        {
            return Math.Abs(a - b) <= tolerance;
        }
        public static bool NearlyEqual(Point<float> a, Point<float> b)
        {
            return NearlyEqual(a.x, b.x) && NearlyEqual(a.y, b.y);
        }
        public static bool Within(Point<int> testPoint, Point<int> min, Point<int> max)
        {
            return min.x <= testPoint.x && testPoint.x <= max.x && min.y <= testPoint.y && testPoint.y <= max.y;
        }
        public static int LayerFloodFill(Layer<int> layer, Point<int> start, int fillColor)
        {
            return LayerFloodFill(layer, start, layer.Get(start), fillColor);
        }
        public static int LayerFloodFill(Layer<int> layer, Point<int> start, int targetColor, int fillColor)
        {
            return LayerFloodFill(
                layer,
                start,
                point => layer.Get(point) == targetColor, // fillCondition
                point => layer.Set(point, fillColor) // fillAction
                );
        }
        public static int LayerFloodFill(Layer<int> layer, Point<int> point, Func<Point<int>, bool> fillCondition, Action<Point<int>> fillAction)
        {
            // adapted from https://simpledevcode.wordpress.com/2015/12/29/flood-fill-algorithm-using-c-net/
            if (!fillCondition(point))
            {
                return 0;
            }
            Stack<Point<int>> points = new Stack<Point<int>>();
            points.Push(point);
            int pointsColored = 0;
            while (points.Count > 0)
            {
                Point<int> temp = points.Pop();
                int y1 = temp.y;
                while (y1 >= 0 && fillCondition(new Point<int>(temp.x, y1)))
                {
                    --y1;
                }
                ++y1;

                bool spanLeft = false;
                bool spanRight = false;
                while (y1 < layer.size.y && fillCondition(new Point<int>(temp.x, y1)))
                {
                    fillAction(new Point<int>(temp.x, y1));
                    ++pointsColored;

                    if (!spanLeft && temp.x > 0 && fillCondition(new Point<int>(temp.x - 1, y1)))
                    {
                        points.Push(new Point<int>(temp.x - 1, y1));
                        spanLeft = true;
                    }
                    else if (spanLeft && temp.x - 1 == 0 && fillCondition(new Point<int>(temp.x - 1, y1)))
                    {
                        spanLeft = false;
                    }
                    if (!spanRight && temp.x < layer.size.x - 1 && fillCondition(new Point<int>(temp.x + 1, y1)))
                    {
                        points.Push(new Point<int>(temp.x + 1, y1));
                        spanRight = true;
                    }
                    else if (spanRight && temp.x < layer.size.x - 1 && fillCondition(new Point<int>(temp.x + 1, y1)))
                    {
                        spanRight = false;
                    }
                    ++y1;
                }
            }
            return pointsColored;
        }
    }
}
