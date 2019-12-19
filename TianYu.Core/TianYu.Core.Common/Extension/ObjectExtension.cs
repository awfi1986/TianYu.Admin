using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianYu.Core.Common
{
    public static class ObjectExtension
    {
        /// <summary>
        /// 指示指定的对象是 null
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNull(this object obj)
        {
            return obj == null;
        }
        ///// <summary>
        ///// 将对象转化为json字符串
        ///// </summary>
        ///// <param name="obj"></param>
        ///// <returns></returns>
        //public static string ToJson(this object obj)
        //{
        //    return  Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        //}

        /// <summary>
        /// 转化decimal
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this object obj, decimal defaultValue = 0)
        {
            if (obj == null)
            {
                return 0;
            }
            decimal d = defaultValue;
            if (!decimal.TryParse(obj.ToString(), out d))
            {
                d = defaultValue;
            }

            return d;
           
        }
        /// <summary>
        /// 转化double
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static double ToDouble(this object obj, double defaultValue = 0)
        {
            if (obj == null)
            {
                return 0;
            }
            double d = defaultValue;
            if (!double.TryParse(obj.ToString(), out d))
            {
                d = defaultValue;
            }

            return d;
        }

        /// <summary>
        /// 转化int
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int ToInt(this object obj)
        {
            if (obj == null)
            {
                return 0;
            }
            Type t = obj.GetType();
            if (t.IsEnum)
            {
                return (int)obj;
            }
            else
            {
                return obj.ToInt(0);
            }
        }
        /// <summary>
        /// 转化int
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue">转化失败默认值</param>
        /// <returns></returns>
        public static int ToInt(this object obj, int defaultValue)
        {
            if (obj == null)
            {
                return defaultValue;
            }
            try
            {
                return Convert.ToInt32(obj);
            }
            catch (Exception ex)
            {
                return defaultValue;
            }
        }
        /// <summary>
        /// 转化时间
        /// </summary>
        /// <param name="obj"></param> 
        /// <returns></returns>
        public static DateTime ToDateTime(this object obj)
        {
            return obj.ToDateTime(DateTime.Now);
        }
        /// <summary>
        /// 转化时间
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this object obj, DateTime defaultValue)
        {
            if (obj == null)
            {
                return defaultValue;
            }
            try
            {
                return Convert.ToDateTime(obj);
            }
            catch (Exception ex)
            {
                return defaultValue;
            }
        }
     
        /// <summary>
        /// 将时间转化为时间戳表示形式
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static long ToTimeSpan(this DateTime obj)
        {
            if (obj == null)
            {
                return 0;
            }
            return (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000; 
        }
        /// <summary>
        /// 转化bool
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool ToBoolean(this object obj)
        {
            if (obj == null)
            {
                return false;
            }
            return Convert.ToBoolean(obj);
        }

        private const string towLine = "--";
        /// <summary>
        /// 转为友好字符显示
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="friendly">友好字符（默认--）</param>
        /// <param name="format">格式化（用于日期、金额等）</param>
        /// <returns></returns>
        public static string ToFriendlyString(this object obj, string friendly = towLine, string format = "")
        {
            string str = string.Empty;
            if (obj is string || obj is int || obj is byte)
            {
                str = obj?.ToString();
            }
            else if (obj is DateTime)
            {
                format = string.IsNullOrWhiteSpace(format) ? "yyyy-MM-dd" : format;
                str = (obj as DateTime?)?.ToString(format);
            }
            else if (obj is double)
            {
                format = string.IsNullOrWhiteSpace(format) ? "#0.000" : format;
                str = (obj as double?)?.ToString(format);
            }
            else if (obj is float)
            {
                format = string.IsNullOrWhiteSpace(format) ? "#0.000" : format;
                str = (obj as float?)?.ToString(format);
            }
            else if (obj is decimal)
            {
                format = string.IsNullOrWhiteSpace(format) ? "#0.000" : format;
                str = (obj as decimal?)?.ToString(format);
            }
            if (string.IsNullOrWhiteSpace(str)) { str = friendly; }
            return str;
        }
         
    }
}
