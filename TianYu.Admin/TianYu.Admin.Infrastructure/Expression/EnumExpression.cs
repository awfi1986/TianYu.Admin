using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianYu.Admin.Infrastructure.Expression
{
    /// <summary>
    /// 枚举扩展类
    /// </summary>
    public static class EnumExpression
    {
        /// <summary>
        /// 获取枚举集合
        /// </summary>
        /// <returns>枚举集合</returns>
        public static List<System.Enum> GetEnumList<TEnum>() where TEnum : new()
        {
            List<System.Enum> ListEnum = new List<System.Enum>();
            Array array = System.Enum.GetValues(typeof(TEnum));

            for (int i = 0; i < array.Length; i++)
            {
                ListEnum.Add((System.Enum)(System.Enum.Parse(typeof(TEnum), array.GetValue(i).ToString())));
            }
            return ListEnum;
        }
    }
}
