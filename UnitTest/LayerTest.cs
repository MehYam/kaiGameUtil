using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using KaiGameUtil;

namespace UnitTest
{
    [TestClass]
    public class LayerTest
    {
        [TestMethod]
        public void WidthAndHeightConstructorShouldSetMinMaxSizeCorrectly()
        {
            const int width = 9;
            const int height = 8;
            var layer = new Layer<int>(width, height);

            Assert.AreEqual(new Point<int>(0, 0), layer.min);
            Assert.AreEqual(new Point<int>(width, height), PointUtil.Add(layer.max, 1));
            Assert.AreEqual(new Point<int>(width, height), layer.size);
        }
        [TestMethod]
        public void MinAndMaxConstructorShouldNormalize()
        {
            const int x1 = -3;
            const int x2 = 4;
            const int y1 = -5;
            const int y2 = 1;

            var layer1 = new Layer<int>(new Point<int>(x1, y1), new Point<int>(x2, y2));
            var layer2 = new Layer<int>(new Point<int>(x2, y1), new Point<int>(x1, y2));
            var layer3 = new Layer<int>(new Point<int>(x1, y2), new Point<int>(x2, y1));
            var layer4 = new Layer<int>(new Point<int>(x2, y2), new Point<int>(x1, y1));

            Assert.AreEqual(new Point<int>(x2 - x1 + 1, y2 - y1 + 1), layer1.size);
            Assert.AreEqual(layer1.size, layer2.size);
            Assert.AreEqual(layer1.size, layer3.size);
            Assert.AreEqual(layer1.size, layer4.size);

            Assert.AreEqual(new Point<int>(x1, y1), layer1.min);
            Assert.AreEqual(layer1.min, layer2.min);
            Assert.AreEqual(layer1.min, layer3.min);
            Assert.AreEqual(layer1.min, layer4.min);

            Assert.AreEqual(new Point<int>(x2, y2), layer1.max);
            Assert.AreEqual(layer1.max, layer2.max);
            Assert.AreEqual(layer1.max, layer3.max);
            Assert.AreEqual(layer1.max, layer4.max);
        }
        [TestMethod]
        public void NegativeCoordsAreAllowed()
        {
            var layer = new Layer<int>(new Point<int>(-3, -3), PointUtil.zero);
            Assert.AreEqual(new Point<int>(4, 4), layer.size);
            Assert.AreEqual(new Point<int>(-3, -3), layer.min);

            layer.Set(layer.min, 2112);

            Assert.AreEqual(2112, layer.Get(-3, -3));
        }
        [TestMethod]
        public void FindShouldFind()
        {
            var layer = new Layer<int>(PointUtil.Subtract(PointUtil.zero, 5), PointUtil.Add(PointUtil.zero, 5));
            layer.Set(-5, -5, 2112);
            layer.Set(4, 4, 2113);

            Assert.AreEqual(new Point<int>(-5, -5), layer.Find(2112));
        }
    }
}
