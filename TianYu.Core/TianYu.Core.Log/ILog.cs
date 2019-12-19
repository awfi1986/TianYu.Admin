using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianYu.Core.Log
{
    public interface  ILog
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source">來源</param>
        /// <param name="message">消息</param>
        /// <param name="args">如果是用Log4Net的話不用傳</param>
        void LogError(string source, string message, params string[] args);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source">來源</param>
        /// <param name="message">消息</param>
        /// <param name="args">如果是用Log4Net的話不用傳</param>
        void LogDebug(string source, string message, params string[] args);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source">來源</param>
        /// <param name="message">消息</param>
        /// <param name="args">如果是用Log4Net的話不用傳</param>
        void LogInfo(string source, string message, params string[] args);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source">來源</param>
        /// <param name="message">消息</param>
        /// <param name="args">如果是用Log4Net的話不用傳</param>
        void LogWarn(string source, string message, params string[] args);

    }
}
