using TianYu.Core.Common; 
using System.Configuration; 
using System.Threading.Tasks;

namespace TianYu.Core.Log
{
    /// <summary>
    /// 日志帮助类
    /// </summary>
    public static class LogHelper
    {
        private static ILog _ilog = null;
        private static string BudlierTempSource(string source)
        {
            return string.Format("请求IP:{0},Source:{1}，服务器Ip:{2}", Utils.GetClientIp(), source, Utils.GetServerIp());
        }
        static LogHelper()
        {
            var logType = ConfigurationManager.AppSettings["logType"];
            if (logType == "FourNetLog")
            {
                _ilog = new FourNetLog();
            }
            else if (logType == "ExceptionLessLog")
            {
                _ilog = new ExceptionLessLog();
            }
        }
        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void LogError(string source, string message, params string[] args)
        {
            Task.Run(() => { _ilog.LogError(BudlierTempSource(source), message, args); });
        }
        /// <summary>
        /// 调试日志
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void LogDebug(string source, string message, params string[] args)
        {
            Task.Run(() => { _ilog.LogDebug(BudlierTempSource(source), message, args); });
        }
        /// <summary>
        /// 信息日志
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void LogInfo(string source, string message, params string[] args)
        {
            Task.Run(() => { _ilog.LogInfo(BudlierTempSource(source), message, args); });
        }
        /// <summary>
        /// 警告日志
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void LogWarn(string source, string message, params string[] args)
        {
            Task.Run(() => { _ilog.LogWarn(BudlierTempSource(source), message, args); });
        }

    }
}
