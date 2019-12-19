using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace TianYu.Core.Common
{
    /// <summary>
    /// 配置文件帮助类
    /// </summary>
    public sealed class ConfigHelper
    {
        /// <summary>
        /// 获取配置文件AppSettings值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string GetAppsettingValue(string key, string defaultValue="")
        {
            string v = ConfigurationManager.AppSettings.Get(key);
            if (v.IsNullOrWhiteSpace())
            {
                return defaultValue;
            }
            return v;
        }
        /// <summary>
        /// 保存文件目录名称（如：uploadFiles/），配置文件中键名请使用：SaveFilePath
        /// </summary>
        public static string SaveFilePath
        {
            get
            {
                string filePath = ConfigurationManager.AppSettings.Get("SaveFilePath");
                return filePath.IsNullOrWhiteSpace() ? "uploadFiles/" : filePath;
            }
        }

    }
}
