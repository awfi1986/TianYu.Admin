using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianYu.Core.Log
{
    internal class FourNetLog : ILog
    {
        private static readonly log4net.ILog log_err = log4net.LogManager.GetLogger("logerror");
        private static readonly log4net.ILog log_debug = log4net.LogManager.GetLogger("logdebug");
        private static readonly log4net.ILog log_info = log4net.LogManager.GetLogger("loginfo");
        private static readonly log4net.ILog log_warn = log4net.LogManager.GetLogger("logwarn");
      
        public  void LogError(string source, string message , params string[] args )
        {
            if (message == null)
            {
                log_err.Error(source);
            }
            else
            {
                log_err.Error(source + " " + message);
            }
        }


        public  void LogDebug(string source, string message , params string[] args)
        {
            if (message == null)
            {
                log_debug.Debug(source);
            }
            else
            {
                log_debug.Debug(source + " " + message);
            }
                
        }

        public  void LogInfo(string source, string message , params string[] args)
        {
            if (message == null)
            {
                log_info.Info(source);
            }
            else
            {
                log_info.Info(source + " " + message);
            }
        }

        public void LogWarn(string source, string message , params string[] args)
        {
            if (message == null)
            {
                log_warn.Warn(source);
            }
            else
            {
                log_warn.Warn(source + " " +message);
            }
        }
    }
}
