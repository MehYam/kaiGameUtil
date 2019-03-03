using System;
using System.Collections;
using System.Collections.Generic;

namespace KaiGameUtil
{
    // Adapted from http://rosettacode.org/wiki/Subtractive_generator#C.23
    public class RNG
    {
        public const int MAX = System.Int32.MaxValue >> 1;
        public readonly int seed;
        private readonly int[] state;
        private int pos;

        private int Mod(int n)
        {
            return ((n % MAX) + MAX) % MAX;
        }
        public RNG(int seed)
        {
            this.seed = seed;

            state = new int[55];

            int[] temp = new int[55];
            temp[0] = Mod(seed);
            temp[1] = 1;
            for (int i = 2; i < 55; ++i)
                temp[i] = Mod(temp[i - 2] - temp[i - 1]);

            for (int i = 0; i < 55; ++i)
                state[i] = temp[(34 * (i + 1)) % 55];

            pos = 54;
            for (int i = 55; i < 220; ++i)
                Next();
        }
        public int Next()
        {
            int temp = Mod(state[(pos + 1) % 55] - state[(pos + 32) % 55]);
            pos = (pos + 1) % 55;
            state[pos] = temp;
            return temp;
        }
        public double NextDouble()
        {
            return (double)Next() / (double) MAX;
        }
        public int Next(int min, int max)
        {
            // KAI: this is not completely accurate, I'm not sure what the actual range is.  For now, it's
            // good enough.  This stands for the second implementation below.
            if (max == int.MaxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(max), "Next must be less than int.MaxValue");
            }
            double r = NextDouble();
            return min + (int)(r * (double)(max - min + 1));
        }
        public int NextIndex(ICollection coll)
        {
            return Next(0, coll.Count - 1);
        }
    }
    // Alternative should you ever need it, 
    // adapted from https://msdn.microsoft.com/en-us/magazine/mt767700.aspx
    //public class RNG
    //{
    //    private const long a = 25214903917;
    //    private const long c = 11;
    //    private long seed;
    //    public RNG(long seed)
    //    {
    //        if (seed < 0)
    //            throw new Exception("Bad seed");
    //        this.seed = seed;
    //    }
    //    private int next(int bits) // helper
    //    {
    //        seed = (seed * a + c) & ((1L << 48) - 1);
    //        return (int)(seed >> (48 - bits));
    //    }
    //    public double Next()
    //    {
    //        return (((long)next(26) << 27) + next(27)) / (double)(1L << 53);
    //    }
    //    public int Next(int min, int max)
    //    {
    //        double r = Next();
    //        return min + (int)(r * (double)(max - min + 1));
    //    }
    //    public int NextIndex(ICollection coll)
    //    {
    //        return Next(0, coll.Count - 1);
    //    }
    //}
}
