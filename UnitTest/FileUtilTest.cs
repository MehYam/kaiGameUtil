using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using KaiGameUtil;
namespace UnitTest
{
    [TestClass]
    public class FileUtilTest
    {
        private bool TestSplitResult(string toSplit, string[] expected)
        {
            var result = FileUtil.SplitStringIntoLines(toSplit);
            return Enumerable.SequenceEqual(result, expected);
        }
        [TestMethod]
        public void SplitStringShouldSplit()
        {
            Assert.IsTrue(TestSplitResult("", new string[] { "" }));
            Assert.IsTrue(TestSplitResult("\r", new string[] { "\r" }));
            Assert.IsTrue(TestSplitResult("\n", new string[] { "", "" }));
            Assert.IsTrue(TestSplitResult("\r\n", new string[] { "", "" }));
            Assert.IsTrue(TestSplitResult("foo\nbar", new string[] { "foo", "bar" }));
        }
        private bool TestSplitAndTrimResult(string toSplit, char delim, string[] expected)
        {
            var result = FileUtil.SplitAndTrim(toSplit, delim);
            return Enumerable.SequenceEqual(result, expected);
        }
        [TestMethod]
        public void SplitAndTrimShouldSplitAndTrim()
        {
            Assert.IsTrue(TestSplitAndTrimResult("", ',', new string[] { "" }));
            Assert.IsTrue(TestSplitAndTrimResult("foo,bar", ',', new string[] { "foo", "bar" }));
            Assert.IsTrue(TestSplitAndTrimResult(" foo , bar ", ',', new string[] { "foo", "bar" }));
        }
    }
}
