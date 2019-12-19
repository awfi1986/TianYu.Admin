using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TianYu.Core.Common;

namespace TianYu.Core.UnitTestProject1
{
    [TestClass]
    public class UnitTestAutofac
    {

        public UnitTestAutofac()
        {
            AutofacRegister.RegisterAutoFac();
        }
        [TestMethod]
        public void TestMethod1()
        {
            TestClass t = new TestClass();
            t.testService.Show();
            t.testRepository.Show();
        }
    }

    public class TestClass
    {
        public IOCTest.ITestRepository testRepository { get; set; }

        public IOCTest.ITestService testService { get; set; }
    }
}
