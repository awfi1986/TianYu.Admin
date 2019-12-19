using System.ComponentModel;

namespace TianYu.Core.Common
{
    /// <summary>
    /// 授权客户端类型
    /// </summary>
    public enum AuthClientType
    {
        /// <summary>
        /// 安卓
        /// </summary>
        [Description("android")]
        Android = 1,
        /// <summary>
        /// 苹果
        /// </summary>
        [Description("ios")]
        Ios = 2,
        /// <summary>
        /// 微信
        /// </summary>
        [Description("wechat")]
        Wechat = 3,
        /// <summary>
        /// 小程序
        /// </summary>
        [Description("samllapp")]
        SamllApp = 4,
    }
}
