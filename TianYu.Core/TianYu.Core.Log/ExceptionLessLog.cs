using Exceptionless;
using Exceptionless.Json;
using Exceptionless.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianYu.Core.Log
{
    internal class ExceptionLessLog : ILog
    {
        private static ExceptionlessClient _client = null;
        private static readonly InMemoryExceptionlessLog _log = new InMemoryExceptionlessLog();
        public ExceptionLessLog()
        {
            ExceptionlessClient.Default.Configuration.ApiKey = ConfigurationManager.AppSettings["exceptionlessApiKey"];
            ExceptionlessClient.Default.Configuration.ServerUrl = ConfigurationManager.AppSettings["exceptionlessServerUrl"];
            ExceptionlessClient.Default.Configuration.UseLogger(_log);
            _client = ExceptionlessClient.Default;

        }

        public void LogError(string source, string message , params string[] args)
        {
            if(args.Length > 0)
            {
                _client.CreateLog(source, message, LogLevel.Error).AddTags(args).Submit();
            }
            else
            {
                _client.SubmitLog(source, message, LogLevel.Error);
            }
        }

        public void LogDebug(string source, string message , params string[] args)
        {
            if (args.Length > 0)
            {
                _client.CreateLog(source, message, LogLevel.Debug).AddTags(args).Submit();
            }
            else
            {
                _client.SubmitLog(source, message, LogLevel.Debug);
            }
        }

        public void LogInfo(string source, string message , params string[] args)
        {
            if (args.Length > 0)
            {
                _client.CreateLog(source, message, LogLevel.Info).AddTags(args).Submit();
            }
            else
            {
                _client.SubmitLog(source, message, LogLevel.Info);
            }
        }

        public void LogWarn(string source, string message , params string[] args)
        {
            if (args.Length > 0)
            {
                _client.CreateLog(source, message, LogLevel.Warn).AddTags(args).Submit();
            }
            else
            {
                _client.SubmitLog(source, message, LogLevel.Warn);
            }
        }
    }
}
