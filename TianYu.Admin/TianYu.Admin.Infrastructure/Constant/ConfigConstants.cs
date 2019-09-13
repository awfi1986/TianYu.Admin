using TianYu.Core.Common;

namespace TianYu.Admin.Infrastructure.Constant
{
    public class ConfigConstants
    {
        
        /// <summary>
        /// 用户数据库链接Key
        /// </summary>
        public const string SystemPowerKey = "TianYuSystemPowerContext";
        /// <summary>
        /// 用户数据库链接地址
        /// </summary>

        public static string SystemPowerConnectionString
        {
            get
            {
                return ConfigHelper.GetAppsettingValue(SystemPowerKey);
            }
        }

    }
}
