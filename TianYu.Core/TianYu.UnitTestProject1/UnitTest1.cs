using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using TianYu.Core.Cache;
using TianYu.Core.Common;
using TianYu.Core.Common.Extension;
using TianYu.Core.Common.Security;
using TianYu.Core.Log;
using TianYu.Core.MQSubscribeWinService.Code;
using StackExchange.Redis;

namespace TianYu.Core.UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestToken()
        {
            string token = "77D7BFA7BB897C565B9776F75036AC2B40321A65FBB8081ADC775789FF6C72C5775C94885B3C216F9C6476807007C3D46146CCD9D7F26C0E11E31114AF479A5BB0E510F11AB9DEE4BD7CFCFF44FC28EBACAA855A8367CA20211BAD95B0D9E5169696FA71B58F439B78913259802D9441C46198F0581CA087";
            var t = AuthenticationHelper.GetDecryptTicket(token);
            Console.WriteLine(t.ToJsonString());
            Assert.Fail();
        }
        [TestMethod]
        public void TestMethod2()
        {
            var flag = "450702198808120014".IsIdCard();
            Assert.IsTrue(flag);
        }
        [TestMethod]
        public void TestMethod1()
        {
            Console.WriteLine("suwei*123123d1fd72beeaef4cc5869c2dd9a97e1785".ToMd5());
            var nn = Utils.GetDistance("126.70374500000000", "35.41204300000000", "126.70374500000000", "35.41204300000000");
            Console.WriteLine(nn);
            Assert.AreEqual(nn, 0);
            //a aa = new a();
            //aa.test();
            //people meinv = new people
            //{
            //    age = 18,
            //    sex = "女",
            //    height = 90
            //};
            //var trans = CacheHelper.BeginTrans();
            //var result = CacheHelper.TransCondition(trans, Condition.KeyExists("001"));
            //var tmp = CacheHelper.InsertAsync("9529", "Bennson.l", trans);
            //tmp = CacheHelper.InsertAsync("9530",   meinv, trans);
            //CacheHelper.CommitTrans(trans);

            //LogHelper.LogInfo("9529", JsonConvert.SerializeObject( CacheHelper.Get("9527")));
            //LogHelper.LogInfo("9530", JsonConvert.SerializeObject(CacheHelper.Get("9528")));
            //TaskWorker worker = new TaskWorker();
            //worker.Run();
            //Thread.Sleep(100000);

        }
        [TestMethod]
        public void redisTest()
        {
            CacheHelper.Exists("aa");
            Thread.Sleep(6000);
            CacheHelper.Exists("bb");
            Thread.Sleep(6000);
            CacheHelper.Exists("dd");
            Thread.Sleep(6000);
            CacheHelper.Exists("cc");
            Thread.Sleep(6000);
            return;
        }
        [TestMethod]
        public void TestQrCode()
        {
            QrCodeHelper.Encoder("http://t-mall.TianYunet.com/ScanCode/index?StoreId=DP10000&CashierId=SY10000", @"E:\二维码啊.jpg", @"D:\job\微信图片_20181130090222.png");
        }

        [TestMethod]
        public void TestDistance()
        {

            var d = Utils.GetDistance(114.063848, 22.543942, 114.062052, 22.545528);
            Console.WriteLine(d);
            var d2 = Utils.GetDistance(22.543942, 114.063848, 22.545528, 114.062052);
            Console.WriteLine(d2);
        }
        [TestMethod]
        public void TestValidate()
        {
            var flag = "246488".IsMobile();
            Console.WriteLine(flag);
        }


    }
    public class a
    {
        public int count = 100;
        public void test()
        {
            Console.WriteLine(count);
            lock (this)
            {
                count++;
                Console.WriteLine("aaaaaaaaaaaaa");
                test();
            }
            if (count > 10000)
            {
                return;
            }
        }
    }
    public class people
    {
        public int age;
        public string sex;
        public int height;
    }


    //[TestMethod]
    //public void mylogin()
    //{
    //    //       LogHelper.LogInfo("asda",''asd");
    //    return;
    //}

}
