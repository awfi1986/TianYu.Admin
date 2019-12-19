using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianYu.Core.Common
{
    public static class ByteExtension
    {
        /// <summary>
        /// 将字节数组 由UTF-8编码 转化为 字符串
        /// </summary>
        /// <param name="buff">字节数组</param>
        /// <returns>字符串</returns>
        public static string DeserializeUtf8(this byte[] buff)
        {
            return System.Text.Encoding.UTF8.GetString(buff);
        }
    }
}
