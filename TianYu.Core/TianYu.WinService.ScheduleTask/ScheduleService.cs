using TianYu.Core.ScheduleTaskWinService.Code;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace TianYu.Core.ScheduleTaskWinService
{
    public partial class ScheduleService : ServiceBase
    {
        TaskWorker worker = new TaskWorker();
        public ScheduleService()
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
        protected override void OnShutdown()
        {
            base.OnShutdown();
        }
    }
}
