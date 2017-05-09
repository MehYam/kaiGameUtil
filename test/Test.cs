using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using kaiGameUtil;

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
        Console.WriteLine("Addition ({0}) ({1})", Util.Add(a, b), Util.Add(c, d));
        Console.WriteLine("Addition ({0}) ({1})", Util.Add(a, 3), Util.Add(c, 3.5f));
        Console.WriteLine("Subtraction ({0}) ({1})", Util.Subtract(a, b), Util.Subtract(c, d));
        Console.WriteLine("Subtraction ({0}) ({1})", Util.Subtract(a, 3), Util.Subtract(c, 3.5f));
        Console.WriteLine("Multiplication ({0}) ({1})", Util.Multiply(a, b), Util.Multiply(c, d));
        Console.WriteLine("Multiplication ({0}) ({1})", Util.Multiply(b, 3), Util.Multiply(c, 3));
        Console.WriteLine("Division ({0}) ({1})", Util.Divide(b, b), Util.Divide(c, d));
        Console.WriteLine("Division ({0}) ({1})", Util.Divide(b, 2), Util.Divide(c, 10));
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
        var layer = new Layer<float>(3, 2);
        layer.ForEachFromBottom((x, y, f) =>
        {
            Console.WriteLine("ForEach callback at {0}, {1}", x, y);
        });
    }
}

