using Microsoft.VisualStudio.TestTools.UnitTesting;
using SoWay.BaseFramework.Cache;
using SoWay.BaseFramework.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SoWay.BaseFramework.UnitTestProject1
{
    [TestClass]
    public class TestCache
    {
        [TestMethod]
        public void TestCacheTimeOut()
        {
            string key = "abcd";
            string value = "test";
            CacheHelper.Insert(key, value, DateTime.Now.AddDays(7));
            Thread.Sleep(20000);
            var v = CacheHelper.Get(key);
            Assert.AreEqual(value, v);

            //CacheHelper.Insert(key, value, new TimeSpan(7, 0, 0, 0, 0).TotalSeconds.ToInt(), true);

            //Thread.Sleep(20000);
            //v = CacheHelper.Get(key);
            //Assert.AreEqual(value, v);
        }
    }
}
