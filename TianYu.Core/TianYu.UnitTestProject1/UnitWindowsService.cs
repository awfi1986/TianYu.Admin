using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TianYu.Core.ScheduleTaskWinService.Code;

namespace TianYu.Core.UnitTestProject1
{
    [TestClass]
    public class UnitWindowsService
    {
        [TestMethod]
        public void TestMethod1()
        {
            TaskWorker worker = new TaskWorker();
            worker.Run();

            Console.Read();
        }
    }
}
