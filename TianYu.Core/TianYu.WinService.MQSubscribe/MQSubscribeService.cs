using TianYu.Core.MQSubscribeWinService.Code;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace TianYu.Core.MQSubscribeWinService
{
    public partial class MQSubscribeService : ServiceBase
    {
        TaskWorker worker = new TaskWorker();
        public MQSubscribeService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            worker.Run();
        }

        protected override void OnStop()
        {
            worker.Stop();
        }
    }
}
