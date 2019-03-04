using Microsoft.VisualStudio.TestTools.UnitTesting;

using KaiGameUtil;

namespace UnitTest
{
    [TestClass]
    public class PointTest
    {
        [TestMethod]
        public void EqualsMethodShouldWorkForInts()
        {
            var a = new Point<int>(1, 2);
            var b = new Point<int>(1, 2);
            var c = new Point<int>(1, 3);

            Assert.IsTrue(a.Equals(b));
            Assert.IsFalse(a.Equals(c));
        }
        [TestMethod]
        public void EqualsMethodShouldWorkForFloats()
        {
            var a = new Point<float>(1, 2);
            var b = new Point<float>(1, 2);
            var c = new Point<float>(1, 3);

            Assert.IsTrue(a.Equals(b));
            Assert.IsFalse(a.Equals(c));
        }
        [TestMethod]
        public void EqualsOperatorShouldWorkForInts()
        {
            var a = new Point<int>(1, 2);
            var b = new Point<int>(1, 2);
            var c = new Point<int>(1, 3);

            Assert.IsTrue(a == b);
            Assert.IsFalse(a == c);
        }
        [TestMethod]
        public void EqualsOperatorShouldWorkForFloats()
        {
            var a = new Point<float>(1, 2);
            var b = new Point<float>(1, 2);
            var c = new Point<float>(1, 3);

            Assert.IsTrue(a == b);
            Assert.IsFalse(a == c);
        }
        [TestMethod]
        public void NotEqualsOperatorShouldWorkForInts()
        {
            var a = new Point<float>(1, 2);
            var b = new Point<float>(1, 2);
            var c = new Point<float>(1, 3);

            Assert.IsTrue(a != c);
            Assert.IsFalse(a != b);
        }
        [TestMethod]
        public void NotEqualsOperatorShouldWorkForFloats()
        {
            var a = new Point<float>(1, 2);
            var b = new Point<float>(1, 2);
            var c = new Point<float>(1, 3);

            Assert.IsTrue(a != c);
            Assert.IsFalse(a != b);
        }
        [TestMethod]
        public void GetHashCodeShouldProducePredictableValues()
        {
            Assert.AreEqual(new Point<int>(1, 1).GetHashCode(), new Point<int>(1, 1).GetHashCode());
        }
        [TestMethod]
        public void ToFloatShouldConvertToFloat()
        {
            var a = new Point<int>(1, 2);
            var b = a.ToFloat();

            Assert.AreEqual((float)a.x, b.x);
            Assert.AreEqual(a.x, (int)b.x);
        }
        [TestMethod]
        public void ToIntShouldConvertToInt()
        {
            var a = new Point<float>(1, 2);
            var b = a.ToInt();

            Assert.AreEqual((int)a.x, b.x);
            Assert.AreEqual(a.x, (float)b.x);
        }
        [TestMethod]
        public void PointAdditionInt()
        {
            var a = new Point<int>(1, 1);
            var b = new Point<int>(2, 2);

            Assert.AreEqual(PointUtil.Add(a, b), new Point<int>(3, 3));
            Assert.AreEqual(PointUtil.Add(a, 2), new Point<int>(3, 3));
        }
        [TestMethod]
        public void PointAdditionFloat()
        {
            var a = new Point<float>(.1f, .1f);
            var b = new Point<float>(.2f, .2f);

            Assert.AreEqual(PointUtil.Add(a, b), new Point<float>(.3f, .3f));
            Assert.AreEqual(PointUtil.Add(a, .2f), new Point<float>(.3f, .3f));
        }
        [TestMethod]
        public void PointSubtractionInt()
        {
            var a = new Point<int>(1, 1);
            var b = new Point<int>(2, 2);

            Assert.AreEqual(PointUtil.Subtract(a, b), new Point<int>(-1, -1));
            Assert.AreEqual(PointUtil.Subtract(a, 2), new Point<int>(-1, -1));
        }
        [TestMethod]
        public void PointSubtractionFloat()
        {
            var a = new Point<float>(.1f, .1f);
            var b = new Point<float>(.2f, .2f);

            Assert.AreEqual(PointUtil.Subtract(a, b), new Point<float>(-.1f, -.1f));
            Assert.AreEqual(PointUtil.Subtract(a, .2f), new Point<float>(-.1f, -.1f));
        }
        [TestMethod]
        public void PointMultiplicationInt()
        {
            var a = new Point<int>(2, 2);
            var b = new Point<int>(4, 4);

            Assert.AreEqual(PointUtil.Multiply(a, b), new Point<int>(8, 8));
            Assert.AreEqual(PointUtil.Multiply(b, 2), new Point<int>(8, 8));
        }
        [TestMethod]
        public void PointMultiplicationFloat()
        {
            var a = new Point<float>(.2f, .2f);
            var b = new Point<float>(4, 4);

            Assert.AreEqual(PointUtil.Multiply(a, b), new Point<float>(.8f, .8f));
            Assert.AreEqual(PointUtil.Multiply(a, 4), new Point<float>(.8f, .8f));
        }
        [TestMethod]
        public void PointDivisionInt()
        {
            var a = new Point<int>(4, 4);
            var b = new Point<int>(2, 2);

            Assert.AreEqual(PointUtil.Divide(a, b), new Point<int>(2, 2));
            Assert.AreEqual(PointUtil.Divide(a, 2), new Point<int>(2, 2));
        }
        [TestMethod]
        public void PointDivisionFloat()
        {
            var a = new Point<float>(1, 1);
            var b = new Point<float>(2, 2);

            Assert.AreEqual(PointUtil.Divide(a, b), new Point<float>(.5f, .5f));
            Assert.AreEqual(PointUtil.Divide(a, 2), new Point<float>(.5f, .5f));
        }
        [TestMethod]
        public void ClampShouldApplyMinimum()
        {
            Assert.AreEqual(10, Util.Clamp(0, 10, 20));
        }
        [TestMethod]
        public void ClampShouldApplyMaximum()
        {
            Assert.AreEqual(Util.Clamp(0, -20, -10), -10);
        }
        [TestMethod]
        public void LerpShouldInterpolate()
        {
            Assert.AreEqual(Util.Lerp(5, 10, .5f), 7.5f);
        }
        [TestMethod]
        public void LerpShouldWorkForReversedRange()
        {
            Assert.AreEqual(Util.Lerp(10, 5, .5f), 7.5f);
        }
        [TestMethod]
        public void LerpShouldClampPercentageTo0To100()
        {
            const float low = 10;
            const float high = 20;
            Assert.AreEqual(Util.Lerp(low, high, 1000), high);
            Assert.AreEqual(Util.Lerp(low, high, -1000), low);
        }
        [TestMethod]
        public void MagnitudeShouldReturnVectorLength()
        {
            Assert.AreEqual(Util.Magnitude(new Point<float>(3, 4)), 5);
        }
        [TestMethod]
        public void NearlyEqualShouldRespectTolerance()
        {
            const float tolerance = 0.001f;
            const float a = 10;
            const float b = a + tolerance/2;
            const float c = a + 2*tolerance;

            Assert.IsTrue(Util.NearlyEqual(a, b, tolerance));
            Assert.IsFalse(Util.NearlyEqual(a, c, tolerance));

            Assert.IsFalse(Util.NearlyEqual(new Point<float>(a, a), new Point<float>(c, c)));
        }
    }
}
