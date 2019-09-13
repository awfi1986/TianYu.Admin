using System;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TianYu.Admin.Service.IService;

namespace UnitTestProject1
{
    [TestClass]
    public class MQBusinessTest
    {
        private IMQBusinessConfigService _mQBusinessConfigService;

        public MQBusinessTest()
        {
            AutoFacConfig.RegisterAutoFac();
            using (var scope = AutoFacConfig.RegisterAutoFac().BeginLifetimeScope())
            {
                _mQBusinessConfigService = scope.Resolve<IMQBusinessConfigService>();
            }
        }

        [TestMethod]
        public void FindList()
        {
            _mQBusinessConfigService.FindList(new TianYu.Admin.Domain.ViewModel.MQBusinessRequest() { KeyWords="订单" });
        }
    }
}
