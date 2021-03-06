using System;

using KaiGameUtil;

class Test
{
    static void Main(string[] args)
    {
        TestPoint();
        TestPerlinLayer();
        TestLayerForEach();

        Console.ReadKey(false);
    }
    static void TestPoint()
    {
        var a = new Point<int>(0, 0);
        var b = new Point<int>(2, 2);

        var c = new Point<float>(2.5f, 3.5f);
        var d = new Point<float>(1, 2);

        Console.WriteLine("IEquatable (should be true): " + (new Point<int>(2, 2) == b));
        Console.WriteLine("IEquatable (should be false): " + (a == b));
        Console.WriteLine("Copy (should be true): " + (new Point<int>(b) == b));
        Console.WriteLine("Addition ({0}) ({1})", PointUtil.Add(a, b), PointUtil.Add(c, d));
        Console.WriteLine("Addition ({0}) ({1})", PointUtil.Add(a, 3), PointUtil.Add(c, 3.5f));
        Console.WriteLine("Subtraction ({0}) ({1})", PointUtil.Subtract(a, b), PointUtil.Subtract(c, d));
        Console.WriteLine("Subtraction ({0}) ({1})", PointUtil.Subtract(a, 3), PointUtil.Subtract(c, 3.5f));
        Console.WriteLine("Multiplication ({0}) ({1})", PointUtil.Multiply(a, b), PointUtil.Multiply(c, d));
        Console.WriteLine("Multiplication ({0}) ({1})", PointUtil.Multiply(b, 3), PointUtil.Multiply(c, 3));
        Console.WriteLine("Division ({0}) ({1})", PointUtil.Divide(b, b), PointUtil.Divide(c, d));
        Console.WriteLine("Division ({0}) ({1})", PointUtil.Divide(b, 2), PointUtil.Divide(c, 10));
    }
    struct TestTile
    {
        public static char[] types = { ' ', '.', 'o', 'O', '8', '#' };
        public static bool IsPassable(char type) { return type == ' ' || type == '.'; }

        public readonly int layer;
        public readonly Point<int> pos;
        public readonly char type;
        public TestTile(char type)
        {
            this.layer = 0;
            this.pos = new Point<int>(0, 0);
            this.type = type;
        }
        public TestTile(int layer, int x, int y, char type)
        {
            this.layer = layer;
            this.pos = new Point<int>(x, y);
            this.type = type;
        }
        public override string ToString()
        {
            return type.ToString();
        }
        public string ToStringLong()
        {
            return string.Format("L:{0}, {1} T:{2:00} |", layer, pos, type);
        }
    }
    static void TestPerlinLayer()
    {
        var noise = new Perlin();
        var layer = new Layer<TestTile>(120,30);
        const float zoom = 3;
        layer.Fill((x, y, oldTile) =>
        {
            double perlin = noise.perlin((float)x * zoom/layer.size.x, (float)y * zoom/layer.size.y, 0.3);

            char tile = TestTile.types[(int)(perlin * TestTile.types.Length)];
            return new TestTile(0, x, y, tile);
        });

        Console.WriteLine(layer);
    }
    static void TestLayerForEach()
    {
        var layer = new Layer<float>(2, 3);
        layer.ForEach((x, y, f) =>
        {
            Console.WriteLine("ForEach callback at {0}, {1}", x, y);
        });
        Console.WriteLine("-------------");
        layer.ForEachFromBottom((x, y, f) =>
        {
            Console.WriteLine("ForEachFromBottom callback at {0}, {1}", x, y);
        });
        Console.WriteLine("-------------");

        void boundsCheck(Layer<float> testLayer, Point<int> pos)
        {
            Console.WriteLine("InBounds: {0} -> {1}", pos, testLayer.Within(pos));
        };
        boundsCheck(layer, new Point<int>(-1, 0));
        boundsCheck(layer, new Point<int>(0, -1));
        boundsCheck(layer, new Point<int>(1, 3));
        boundsCheck(layer, new Point<int>(2, 2));
        boundsCheck(layer, new Point<int>(0, 0));
        boundsCheck(layer, new Point<int>(1, 2));
    }
}

