using System; 
using System.Drawing; 
using System.Security.Cryptography;
using System.Text; 
using System.Web;

namespace TianYu.Core.Common
{
    /// <summary>
    /// 公共方法
    /// </summary>
    public sealed class Utils
    {
        static Random random = new Random(DateTime.Now.Second);


        /// <summary>
        /// 获取一个带时序的GUID
        /// </summary>
        /// <returns></returns>
        public static Guid NewGuid()
        {
            DateTime dt = DateTime.Now;
            Guid g = Guid.NewGuid();

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}-{1}", dt.ToString("yyyyMMdd"), dt.ToString("HHmm"));
            var guids = g.ToString().Split('-');
            for (int i = 2; i < guids.Length; i++)
            {
                if (i == 2)
                {
                    sb.AppendFormat("-{0}{1}", dt.ToString("ss"), guids[i].Substring(2));
                }
                else { sb.AppendFormat("-{0}", guids[i]); }
            }

            return Guid.Parse(sb.ToString());
        }
        /// <summary>
        /// 获取两个经纬度之间的距离（单位：米）
        /// </summary>
        /// <param name="longitude1">经度1</param>
        /// <param name="latitude1">纬度1</param>
        /// <param name="longitude2">经度2</param>
        /// <param name="latitude2">纬度2</param>
        /// <returns>两经纬度之间距离（单位：米）</returns>
        public static double GetDistance(double longitude1, double latitude1, double longitude2, double latitude2)
        {
            double dd = HaversineFormula.Distance(longitude1, latitude1, longitude2, latitude2);
            dd = Math.Round(dd, 2);
            return dd;
        }
        /// <summary>
        /// 获取两个经纬度之间的距离（单位：米）
        /// </summary>
        /// <param name="longitude1">经度1</param>
        /// <param name="latitude1">纬度1</param>
        /// <param name="longitude2">经度2</param>
        /// <param name="latitude2">纬度2</param>
        /// <returns>两经纬度之间距离（单位：米）</returns>
        public static double GetDistance(string longitude1, string latitude1, string longitude2, string latitude2)
        {
            return GetDistance(longitude1.ToDouble(), latitude1.ToDouble(), longitude2.ToDouble(), latitude2.ToDouble());

        }

        /// <summary>
        /// URL字符编码
        /// </summary>
        public static string UrlEncode(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }
            str = str.Replace("'", "");
            return HttpContext.Current.Server.UrlEncode(str);
        }

        /// <summary>
        /// URL字符解码
        /// </summary>
        public static string UrlDecode(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }
            return HttpContext.Current.Server.UrlDecode(str);
        }

        #region 获取客户端IP地址

        /// <summary>
        /// 获取客户端IP地址
        /// </summary>
        /// <returns>若失败则返回回送地址</returns>
        [Obsolete("请使用GetClientIp")]
        public static string GetHostAddress()
        {
            string userHostAddress = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            if (string.IsNullOrEmpty(userHostAddress))
            {
                if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
                    userHostAddress = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString().Split(',')[0].Trim();
            }
            if (string.IsNullOrEmpty(userHostAddress))
            {
                userHostAddress = System.Web.HttpContext.Current.Request.UserHostAddress;
            }
            //最后判断获取是否成功，并检查IP地址的格式
            if (!string.IsNullOrEmpty(userHostAddress) && IsIP(userHostAddress))
            {
                return userHostAddress;
            }
            return "";
        }
        /// <summary>
        /// 检查IP地址格式
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIP(string ip)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }
        #endregion

        /// <summary>
        /// 生成随机单据号
        /// </summary>
        /// <returns></returns>
        public static string GetFlowOrderCode(string prefix = "", string timeFormat = "yyyyMMddHHmmss", int randomLength = 6)
        {
            return string.Format("{0}{1}{2}", prefix, DateTime.Now.ToString(timeFormat), GetRamdonCode(randomLength));
        }
        /// <summary>
        /// 生成短信验证码(随机生成6位数字)
        /// </summary>
        /// <returns></returns>
        public static string GetRamdonCode(int length = 6)
        {
            if (length > 21)
            {
                length = 21;
            }
            StringBuilder maxNumber = new StringBuilder();
            StringBuilder minNumber = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                maxNumber.Append("9");
                if (i == 0)
                {
                    minNumber.Append("1");
                }
                else
                {
                    minNumber.Append("0");
                }
            }
            return random.Next(minNumber.ToString().ToInt(), maxNumber.ToString().ToInt()).ToString();
        }
        /// <summary>
        /// 日期字符串转换格式
        /// </summary>
        /// <param name="date">日期字符串</param>
        /// <param name="format">当前格式</param>
        /// <param name="newformat">待转格式</param>
        /// <returns></returns>
        public static string DateStringToFormat(string date, string format, string newformat)
        {
            if (string.IsNullOrWhiteSpace(date))
                return string.Empty;
            if (string.IsNullOrWhiteSpace(format) || string.IsNullOrWhiteSpace(newformat))
                return date;
            return DateTime.ParseExact(date, format, System.Globalization.CultureInfo.CurrentCulture).ToString(newformat);
        }

        /// <summary>
        /// 小程序数据解密
        /// </summary>
        /// <param name="encryptedData">加密数据</param>
        /// <param name="key">密钥</param>
        /// <param name="iv">偏移值</param>
        /// <returns></returns>
        public static string MiniAppsAESDecrypt(string encryptedData, string key, string iv)
        {
            if (string.IsNullOrEmpty(encryptedData)) return "";
            byte[] encryptedData2 = Convert.FromBase64String(encryptedData);
            var rm = new RijndaelManaged
            {
                Key = Convert.FromBase64String(key),
                IV = Convert.FromBase64String(iv),
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7
            };
            var ctf = rm.CreateDecryptor();
            Byte[] resultArray = ctf.TransformFinalBlock(encryptedData2, 0, encryptedData2.Length);
            return Encoding.UTF8.GetString(resultArray);
        }


        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <param name="CharArray">字符串集</param>
        /// <param name="n">位数</param>
        /// <param name="IsNumber">是否纯数字</param>
        /// <returns>验证码字符串</returns>
        private static string CreateRandomCode(string[] CharArray, int n, bool IsNumber)
        {
            string randomCode = "";
            int temp = -1;
            Random rand = new Random();
            for (int i = 0; i < n; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(i * temp * ((int)DateTime.Now.Ticks));
                }
                int t = rand.Next(CharArray.Length - 1);
                //if (temp == t)
                //{
                //    return CreateRandomCode(n, IsNumber);
                //} 
                temp = t;
                randomCode += CharArray[t];
            }
            return randomCode;
        }

        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <param name="n">位数</param>
        /// <param name="IsNumber">是否仅数字</param>
        /// <returns>验证码字符串</returns>
        public static string CreateRandomCode(int n, bool IsNumber)
        {
            string charSet = "";
            if (IsNumber)
            {
                charSet = "0,1,2,3,4,5,6,8,9";
            }
            else
            {
                charSet = "2,3,4,5,6,8,9,A,B,C,D,E,F,G,H,J,K,M,N,P,R,S,U,W,X,Y,a,b,c,d,e,f,g,h,j,k,m,n,p,r,s,u,w,x,y";
            }
            string[] CharArray = charSet.Split(',');
            return CreateRandomCode(CharArray, n, IsNumber);
        }

        /// <summary>
        /// 绘画验证码图片
        /// </summary>
        /// <param name="chkCode">验证码</param>
        /// <returns></returns>
        public static Bitmap DrawRandomCode(string chkCode)
        {
            //颜色列表，用于验证码、噪线、噪点 
            Color[] color = { Color.Black, Color.Red, Color.Blue, Color.Green, Color.Orange, Color.Brown, Color.Brown, Color.DarkBlue };
            //字体列表，用于验证码 
            string[] font = { "Times New Roman", "MS Mincho", "Book Antiqua", "Gungsuh", "PMingLiU", "Impact" };

            Random rnd = new Random();
            Bitmap bmp = new Bitmap((int)Math.Ceiling(chkCode.Length * 26.0), 40);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            //画图片的干扰线
            for (int i = 0; i < 25; i++)
            {
                int x1 = rnd.Next(bmp.Width);
                int x2 = rnd.Next(bmp.Width);
                int y1 = rnd.Next(bmp.Height);
                int y2 = rnd.Next(bmp.Height);
                Color clr = color[rnd.Next(color.Length)];
                g.DrawLine(new Pen(Color.Silver), x1, x2, y1, y2);
            }
            //画验证码字符串 
            for (int i = 0; i < chkCode.Length; i++)
            {
                string fnt = font[rnd.Next(font.Length)];
                Font ft = new Font(fnt, 18);
                Color clr = color[rnd.Next(color.Length)];
                g.DrawString(chkCode[i].ToString(), ft, new SolidBrush(clr), (float)i * 20 + 8, (float)8);
            }
            //画图片的前景干扰线
            for (int i = 0; i < 100; i++)
            {
                int x = rnd.Next(bmp.Width);
                int y = rnd.Next(bmp.Height);
                Color clr = color[rnd.Next(color.Length)];
                bmp.SetPixel(x, y, clr);
            }

            g.Dispose();

            return bmp;
        }
        /// <summary>
        /// 获取客户端IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetClientIp()
        {
            string ip = "";
            if (HttpContext.Current != null && HttpContext.Current.Request != null)
            {
                ip = HttpContext.Current.Request.Headers["X-Forwarded-For"];
                if (ip.IsNullOrWhiteSpace())
                {
                    if (HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null) // using proxy 
                    {
                        ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString(); // Return real client IP. 
                    }
                    else// not using proxy or can't get the Client IP 
                    {
                        ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString(); //While it can't get the Client IP, it will return proxy IP. 
                    }

                    if (ip.IsNullOrWhiteSpace())
                    {
                        ip = HttpContext.Current.Request.UserHostAddress;
                    }
                }
            }
            return ip;
        }
        /// <summary>
        /// 获取app请求接口版本号
        /// </summary>
        /// <returns></returns>
        public static string GetRequestAppVersion()
        {
            string version = string.Empty;
            if (HttpContext.Current != null && HttpContext.Current.Request != null)
            {
                version = HttpContext.Current.Request.Headers[TianYuConsts.ApiVersionKey];
            }
            return version;
        }
        /// <summary>
        /// 获取服务器IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetServerIp()
        {
            string ip = "";
            if (HttpContext.Current != null && HttpContext.Current.Request != null)
            {
                ip = HttpContext.Current.Request.ServerVariables.Get("Local_Addr").ToString();
            }
            return ip;
        }

        /// <summary>
        /// DateTime转Utc
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public string DateTimeToUtc(DateTime dt)
        {
            //TimeZone类表示时区，TimeZone.CurrentTimeZone方法：获取当前计算机的时区。
            TimeZone tz = TimeZone.CurrentTimeZone;
            string dtGMT = tz.ToUniversalTime(dt).ToString("yyyy-MM-ddTHH\\:mm\\:ssZ");
            return dtGMT;
        }
        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public static long GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds);
        }

        /// <summary>
        /// 对字符串SHA1加密
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="encoding">编码类型</param>
        /// <returns>加密后的十六进制字符串</returns>
        public static string Sha1Encrypt(string source, Encoding encoding = null)
        {
            if (encoding == null) encoding = Encoding.UTF8;
            byte[] byteArray = encoding.GetBytes(source);
            using (HashAlgorithm hashAlgorithm = new SHA1CryptoServiceProvider())
            {
                byteArray = hashAlgorithm.ComputeHash(byteArray);
                StringBuilder stringBuilder = new StringBuilder(256);
                foreach (byte item in byteArray)
                {
                    stringBuilder.AppendFormat("{0:x2}", item);
                }
                hashAlgorithm.Clear();
                return stringBuilder.ToString();
            }
        }
    }
}
