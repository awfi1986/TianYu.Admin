using System.ComponentModel; 

namespace TianYu.Core.Common
{
    public enum AccuracyType
    {
        /// <summary>
        /// 只舍不入
        /// </summary>
        [Description("只舍不入")]
        JustGiveUp = 1,
        /// <summary>
        /// 只入不舍
        /// </summary>
        [Description("只入不舍")]
        OnlyEnter = 2,
        /// <summary>
        /// 四舍五入
        /// </summary>
        [Description("四舍五入")]
        Rounding = 3,
    }
}
