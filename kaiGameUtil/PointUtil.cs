namespace KaiGameUtil
{
    public static class PointUtil
    {
        // Because C# can't support math operators on generics...
        //KAI: could use extensions and C# version of specialization?
        public static Point<int> Add(Point<int> a, Point<int> b)
        {
            return new Point<int>(a.x + b.x, a.y + b.y);
        }
        public static Point<int> Subtract(Point<int> a, Point<int> b)
        {
            return new Point<int>(a.x - b.x, a.y - b.y);
        }
        public static Point<int> Multiply(Point<int> a, Point<int> b)
        {
            return new Point<int>(a.x * b.x, a.y * b.y);
        }
        public static Point<int> Divide(Point<int> a, Point<int> b)
        {
            return new Point<int>(a.x / b.x, a.y / b.y);
        }
        public static Point<int> Add(Point<int> a, int b)
        {
            return new Point<int>(a.x + b, a.y + b);
        }
        public static Point<int> Subtract(Point<int> a, int b)
        {
            return new Point<int>(a.x - b, a.y - b);
        }
        public static Point<int> Multiply(Point<int> a, int b)
        {
            return new Point<int>(a.x * b, a.y * b);
        }
        public static Point<int> Divide(Point<int> a, int b)
        {
            return new Point<int>(a.x / b, a.y / b);
        }
        public static Point<float> Add(Point<float> a, Point<float> b)
        {
            return new Point<float>(a.x + b.x, a.y + b.y);
        }
        public static Point<float> Subtract(Point<float> a, Point<float> b)
        {
            return new Point<float>(a.x - b.x, a.y - b.y);
        }
        public static Point<float> Multiply(Point<float> a, Point<float> b)
        {
            return new Point<float>(a.x * b.x, a.y * b.y);
        }
        public static Point<float> Divide(Point<float> a, Point<float> b)
        {
            return new Point<float>(a.x / b.x, a.y / b.y);
        }
        public static Point<float> Add(Point<float> a, float b)
        {
            return new Point<float>(a.x + b, a.y + b);
        }
        public static Point<float> Subtract(Point<float> a, float b)
        {
            return new Point<float>(a.x - b, a.y - b);
        }
        public static Point<float> Multiply(Point<float> a, float b)
        {
            return new Point<float>(a.x * b, a.y * b);
        }
        public static Point<float> Divide(Point<float> a, float b)
        {
            return new Point<float>(a.x / b, a.y / b);
        }
        public static readonly Point<int> zero = new Point<int>(0, 0);
        public static readonly Point<int> up = new Point<int>(0, 1);
        public static readonly Point<int> right = new Point<int>(1, 0);
        public static readonly Point<int> down = new Point<int>(0, -1);
        public static readonly Point<int> left = new Point<int>(-1, 0);
        public static readonly Point<int> min = new Point<int>(int.MinValue, int.MinValue);
        public static readonly Point<int> max = new Point<int>(int.MaxValue, int.MaxValue);
        public static readonly Point<int>[] cardinalDirections = new Point<int>[] { up, right, down, left };
    }
}
