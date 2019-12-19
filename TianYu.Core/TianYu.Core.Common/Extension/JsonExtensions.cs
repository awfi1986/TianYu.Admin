using Newtonsoft.Json.Converters;

namespace TianYu.Core.Common
{
    /// <summary>
    /// Json扩展方法类
    /// </summary>
    public static class JsonExtensions
    {
        #region 字符串扩展
        /// <summary>
        /// 将当前Json字符串转为T类对象
        /// </summary>
        /// <typeparam name="T">类对象</typeparam>
        /// <param name="Json">Json字符串</param>
        /// <returns>T类对象</returns>
        public static T ToJsonObject<T>(this string Json)where T:class
        {
            return string.IsNullOrEmpty(Json) ? default(T) : JsonHelper.DeserializeObject<T>(Json);
        }
        /// <summary>
        /// 将当前Json字符串转为Json对象
        /// </summary>
        /// <param name="Json">Json字符串</param>
        /// <returns>Json对象</returns>
        public static dynamic ToJsonObject(this string Json)
        {
            return string.IsNullOrEmpty(Json) ? null : JsonHelper.DeserializeObject(Json);
        }
        #endregion

        #region 对象扩展
        /// <summary>
        /// 将对象转为Json字符串
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>Json字符串</returns>
        public static string ToJsonString(this object obj)
        {
            var timeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };
            return JsonHelper.SerializeObject(obj, timeConverter);
        }

        /// <summary>
        /// 将对象转为Json字符串
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="datetimeformats">日期格式化（如：yyyy-MM-dd HH:mm:ss）</param>
        /// <returns>Json字符串</returns>
        public static string ToJsonString(this object obj, string datetimeformats)
        {
            var timeConverter = new IsoDateTimeConverter { DateTimeFormat = datetimeformats };
            return JsonHelper.SerializeObject(obj, timeConverter);
        }
        #endregion
    }
}
