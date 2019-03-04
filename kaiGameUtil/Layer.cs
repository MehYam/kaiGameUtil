using System;
using System.Text;

namespace KaiGameUtil
{
    public sealed class Layer<T>
    {
        readonly T[,] tiles;

        public readonly Point<int> min;
        public readonly Point<int> size;
        public Point<int> max { get { return PointUtil.Subtract(PointUtil.Add(min, size), 1); } }
        public Layer(int width, int height)
        {
            tiles = new T[width, height];
            this.min = new Point<int>(0, 0);
            this.size = new Point<int>(width, height);
        }
        public Layer(Point<int> min, Point<int> max)
        {
            var tmpMin = new Point<int>(Math.Min(min.x, max.x), Math.Min(min.y, max.y));
            var tmpMax = new Point<int>(Math.Max(min.x, max.x), Math.Max(min.y, max.y));

            this.min = tmpMin;
            this.size = PointUtil.Add(PointUtil.Subtract(tmpMax, tmpMin), 1);

            tiles = new T[size.x, size.y];
        }
        public Layer(Layer<T> rhs)
        {
            tiles = new T[rhs.size.x, rhs.size.y];
            this.min = rhs.min;
            this.size = rhs.size;

            ForEachFromBottom((x, y, tile) =>
            {
                //KAI: this is a shallow copy.  Not sure how to represent this fact
                Set(x, y, rhs.Get(x, y));
            });
        }
        public T Get(int x, int y)
        {
            return tiles[x - min.x, y - min.y];
        }
        public void Set(int x, int y, T t)
        {
            tiles[x - min.x, y - min.y] = t;
        }
        public T Get(Point<int> pos)
        {
            return Get(pos.x, pos.y);
        }
        public void Set(Point<int> pos, T t)
        {
            Set(pos.x, pos.y, t);
        }
        public bool Within(Point<int> pos)
        {
            return Util.Within(pos, min, max);
        }
        public Point<int> Find(T item)
        {
            var retval = PointUtil.Subtract(min, -1);
            for (var y = size.y - 1; y >= 0; --y)
            {
                for (var x = 0; x < size.x; ++x)
                {
                    if (tiles[x, y].Equals(item))
                    {
                        return PointUtil.Add(new Point<int>(x, y), min);
                    }
                }
            }
            return retval;
        }
        /// <summary>
        /// Iterate the layer, invoking the callback with (x, y, item) arguments
        /// </summary>
        /// <param name="callback"></param>
        public void ForEach(Action<int, int, T> callback)
        {
            for (var y = 0; y < size.y; ++y)
            {
                for (var x = 0; x < size.x; ++x)
                {
                    callback(x + min.x, y + min.y, tiles[x, y]);
                }
            }
        }
        /// <summary>
        /// Iterate the layer, invoking the callback with (x, y, item) arguments, starting at max y and progressing to the top (y == 0)
        /// </summary>
        /// <param name="callback"></param>
        public void ForEachFromBottom(Action<int, int, T> callback)
        {
            for (var y = size.y - 1; y >= 0; --y)
            {
                for (var x = 0; x < size.x; ++x)
                {
                    callback(x + min.x, y + min.y, tiles[x, y]);
                }
            }
        }
        /// <summary>
        /// Iterate every other tile in the layer, invoking the callback with (x, y, item) arguments
        /// </summary>
        /// <param name="callback"></param>
        public void ForEveryOther(Action<int, int, T> callback)
        {
            for (var y = 0; y < size.y; ++y)
            {
                for (var x = y % 2; x < size.x; x += 2)
                {
                    callback(x + min.x, y + min.y, tiles[x, y]);
                }
            }
        }
        public void Fill(Func<int, int, T, T> callback)
        {
            for (var y = 0; y < size.y; ++y)
            {
                for (var x = 0; x < size.x; ++x)
                {
                    tiles[x, y] = callback(x + min.x, y + min.y, tiles[x, y]);
                }
            }
        }
        public void Fill(Func<int, int, T> callback)
        {
            for (var y = 0; y < size.y; ++y)
            {
                for (var x = 0; x < size.x; ++x)
                {
                    tiles[x, y] = callback(x + min.x, y + min.y);
                }
            }
        }
        public override string ToString()
        {
            var sb = new StringBuilder();
            int currentY = size.y - 1;
            ForEachFromBottom((int x, int y, T t) =>
            {
                if (currentY != y)
                {
                    currentY = y;
                    sb.AppendLine();
                }
                sb.Append(t == null ? " " : t.ToString());
            });
            return sb.ToString();
        }
    }
}
