using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TianYu.Core.Common;

namespace TianYu.Core.UnitTestProject1
{
    [TestClass]
    public class TestDecimalExtension
    {
        [TestMethod]
        public void TestMethod1()
        {
            decimal dd = 1.23456789m;
            var v = dd.ToAccuracy(AccuracyType.JustGiveUp);
            Console.WriteLine(v);
            Assert.AreEqual(1.23m, v);

            v = dd.ToAccuracy(AccuracyType.OnlyEnter);
            Console.WriteLine(v);
            Assert.AreEqual(1.24m, v);

            v = dd.ToAccuracy(AccuracyType.Rounding);
            Console.WriteLine(v);
            Assert.AreEqual(1.23m, v);

            v = dd.ToAccuracy(AccuracyType.Rounding,3);
            Console.WriteLine(v);
            Assert.AreEqual(1.235m, v);
        }
    }
}
