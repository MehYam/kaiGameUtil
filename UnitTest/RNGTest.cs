using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using kaiGameUtil;

namespace UnitTest
{
    [TestClass]
    public class RNGTest
    {
        [TestMethod]
        public void RNGShouldProducePredictableNumbersFromSeed()
        {
            void tester(int seed, int[] expected)
            {
                var rng = new RNG(seed);
                for (var i = 0; i < expected.Length; ++i)
                {
                    var random = rng.Next(0, int.MaxValue - 1);
                    Console.WriteLine(random);

                    Assert.AreEqual(random, expected[i]);
                }
            }

            tester(2112, new int[] { 370193976,788263724,1007215672,394361444,1047117664 });
            tester(0, new int[] { 1080676122,393483866,1976650138,1951072556,263568040 });
            tester(int.MaxValue, new int[] { 314688590,435359628,755013162,2108956428,357484728 });
        }
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [TestMethod]
        public void NextShouldThrowOnBadMax()
        {
            new RNG(0).Next(0, int.MaxValue);
        }
        private void TestDistributionUniformity(int[] hits, double tolerance)
        {
            for (int i = 0; i < hits.Length - 1; ++i)
            {
                double deviation = Math.Abs((double)(hits[i + 1] - hits[i])/(double)hits[i]);
                Assert.IsTrue(deviation < tolerance);
            }
        }
        [TestMethod]
        public void NextShouldChooseRandomICollectionItem_AndTestForEvenDistribution()
        {
            var collection = new List<int> { 1, 2, 3, 4, 5, 6};
            var hits = new int[6];
            var rng = new RNG(2112);

            for (int i = 0; i < 1000000; ++i)
            {
                ++hits[rng.NextIndex(collection)];
            }
            // ensure we're within a half pct
            TestDistributionUniformity(hits, 0.005);
        }
    }
}
