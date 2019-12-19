using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace TianYu.Core.Common.FilterAttribute.AuthorizeCode
{
    public class WhiteListHelper
    {
        private static List<string> ipList = new List<string>();
        static WhiteListHelper()
        {
            string ips = ConfigHelper.GetAppsettingValue(TianYuConsts.ApiIpWhiteList);
            if (!ips.IsNullOrWhiteSpace())
            {
                ipList = ips.ToSplitArray(';').ToList();
            }
        }
        public static bool IsWhiteIp(string ip)
        {
            var flag = ipList.Any(x => x == ip);
            //if (!flag)
            //{
            //    //using (StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\" + DateTime.Now.ToString("yyMMdd") + "log.txt", true, Encoding.UTF8))
            //    //{

            //    //    //StringBuilder sb = new StringBuilder();
            //    //    //foreach (KeyValuePair<string, string> key in HttpContext.Current.Request.Headers)
            //    //    //{
            //    //    //    sb.AppendFormat("{0}={1}", key.Key, key.Value);
            //    //    //}
            //    //    sw.WriteLine(string.Format("{0}：{1}", DateTime.Now.ToString(), ip));
            //    //    //sw.WriteLine(sb.ToString());
            //    //}
            //}
            return flag;
        }
    }
}
