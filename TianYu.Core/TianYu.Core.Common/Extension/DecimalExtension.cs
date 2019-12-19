using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianYu.Core.Common
{
    /// <summary>
    /// 数据精度转化
    /// </summary>
    public static class DecimalExtension
    {

        /// <summary>
        /// 小数精度计算
        /// </summary>
        /// <param name="input"></param>
        /// <param name="accuracyType"></param>
        /// <param name="decimals"></param>
        /// <returns></returns>
        public static decimal ToAccuracy(this decimal input, AccuracyType accuracyType = AccuracyType.Rounding, int decimals = 2)
        {
            if (accuracyType == AccuracyType.Rounding)
            {
                return Math.Round(input, decimals, MidpointRounding.AwayFromZero);
            }
            else
            {
                var ext = Math.Pow(10, decimals).ToInt();
                var value = input * ext;
                if (accuracyType == AccuracyType.JustGiveUp)
                {
                    return Math.Round(Math.Floor(value) / ext, decimals);
                }
                else
                {
                    return Math.Round(Math.Ceiling(value) / ext, decimals);
                }
            }
        }
        /// <summary>
        /// 小数精度计算
        /// </summary>
        /// <param name="input"></param>
        /// <param name="accuracyType"></param>
        /// <param name="decimals"></param>
        /// <returns></returns>
        public static decimal ToAccuracy(this decimal? input, AccuracyType accuracyType, int decimals)
        {

            if (!input.HasValue)
            {
                return 0;
            }
            return input.Value.ToAccuracy(accuracyType, decimals);
        }
    }
}
