using System;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TianYu.Admin.Service.IService;

namespace UnitTestProject1
{
    [TestClass]
    public class ScheduleTaskTest
    {
        private IScheduleTaskConfigService _scheduleTaskConfigService;

        public ScheduleTaskTest()
        {
            AutoFacConfig.RegisterAutoFac();
            using (var scope = AutoFacConfig.RegisterAutoFac().BeginLifetimeScope())
            {
                _scheduleTaskConfigService = scope.Resolve<IScheduleTaskConfigService>();
            }
        }

        [TestMethod]
        public void FindList()
        {
            _scheduleTaskConfigService.FindList(new TianYu.Admin.Domain.ViewModel.ScheduleTaskRequest() { });
        }

        [TestMethod]
        public void Add()
        {
            //_scheduleTaskConfigService.AddOrUpsert(new TianYu.Admin.Domain.ScheduleTaskConfig()
            //{
            //    ApiUrl = "https://www.baidu.com/",
            //    CreateTime = DateTime.Now,
            //    DiffSeconds = 2,
            //    ExceptionNumber = 2,
            //    ExecuteType = 0,
            //    Method = "POST",
            //    Parameters = "",
            //    RunStatus = 0,
            //    Status = 1,
            //    TaskName = "测试任务"
            //});
        }

    }
}
