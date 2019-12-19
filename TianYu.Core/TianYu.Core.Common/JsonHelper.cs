using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TianYu.Core.Common
{
    /// <summary>
    /// Json操作帮助类
    /// </summary>
    public sealed class JsonHelper
    {

        /// <summary>
        /// 将对象序列化成Json字符串
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>Json字符串</returns>
        public static string SerializeObject(object obj)
        {
            return JsonConvert.SerializeObject(obj, new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
        }

        /// <summary>
        /// 将对象序列化成Json字符串
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="converter"></param>
        /// <returns>Json字符串</returns>
        public static string SerializeObject(object obj, params JsonConverter[] converters)
        {
            return JsonConvert.SerializeObject(obj, converters);
        }

        /// <summary>
        /// 将Json字符串反序列化成Json对象
        /// </summary>
        /// <param name="json">Json字符串</param>
        /// <returns>Json对象</returns>
        public static dynamic DeserializeObject(string json)
        {
            return JsonConvert.DeserializeObject(json);
        }
        /// <summary>
        /// 将Json字符串反序列化成T对象
        /// </summary>
        /// <param name="json">Json字符串</param>
        /// <returns>T对象</returns>
        public static T DeserializeObject<T>(string json) where T :class
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// 将Json字符串反序列化成T对象
        /// </summary>
        /// <param name="json">Json字符串</param>
        /// <param name="converter"></param>
        /// <returns>T对象</returns>
        public static T DeserializeObject<T>(string json, params JsonConverter[] converter)
        {
            return JsonConvert.DeserializeObject<T>(json, converter);
        }

    }

}
