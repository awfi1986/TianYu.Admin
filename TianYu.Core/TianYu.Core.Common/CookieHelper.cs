using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace TianYu.Core.Common
{
    /// <summary>
    ///  Cookie 操作帮助类
    /// </summary>
    public class CookieHelper
    {
        private static TimeSpan defaultTimeSpan = new TimeSpan(0, 30, 0);
        /// <summary>
        /// 添加cookie
        /// </summary>
        /// <param name="name">键</param>
        /// <param name="value">值</param>
        /// <param name="minutes">过期时间（默认30分钟）</param>
        /// <param name="domain">cookie 根域</param>
        /// <param name="path">cookie路径</param>
        public static void AddCookie(string name, string value, int minutes = 0, string domain = TianYuConsts.DefaultDomainName, string path = "")
        {
            HttpCookie cookie = new HttpCookie(name, value);
            cookie.Domain = domain;
            if (!path.IsNullOrWhiteSpace())
            {
                cookie.Path = path;
            }
            if (minutes == 0)
            {
                cookie.Expires = DateTime.Now.Add(defaultTimeSpan);
            }
            else
            {
                cookie.Expires = DateTime.Now.AddMinutes(minutes);
            }
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
        /// <summary>
        /// 获取cookie值
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static string GetCookieValue(string name, string defaultValue = "")
        {
            var cookie = HttpContext.Current.Request.Cookies[name];
            if (!cookie.IsNull())
            {
                return cookie.Value;
            }

            return defaultValue;
        }
        /// <summary>
        /// 清除cookie
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static void RemoveCookie(string name)
        {
            var cookie = HttpContext.Current.Request.Cookies[name];
            if (cookie.IsNull())
            {
                return;
            }

            cookie.Expires = DateTime.Now.AddYears(-1);
            HttpContext.Current.Response.Cookies.Add(cookie);

            return;
        }
        /// <summary>
        /// 追加cookie时间
        /// </summary>
        /// <param name="name"></param>
        /// <param name="hours">小时数</param>
        /// <returns></returns>
        public static void AppendCookieTime(string name, int hours)
        {
            var cookie = HttpContext.Current.Request.Cookies[name];
            if (!cookie.IsNull())
            {
                cookie.Expires = DateTime.Now.AddHours(hours);
                HttpContext.Current.Request.Cookies.Add(cookie);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
            return;
        }
    }
}
