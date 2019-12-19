using TianYu.Core.Common.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianYu.Core.Common
{
    public static class StringExtension
    {
        /// <summary>
        /// 将字符串转化为 UTF-8 字节数组
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static byte[] SerializeUtf8(this string value)
        {
            return System.Text.Encoding.UTF8.GetBytes(value);
        }
        /// <summary>
        /// 指示指定的字符串是 null、空还是仅由空白字符组成。
        /// </summary>
        /// <param name="context"> 要测试的字符串。</param>
        /// <returns>如果 true 参数为 value 或 null，或者如果 System.String.Empty 仅由空白字符组成，则为 value。</returns>
        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }
        /// <summary>
        /// 指示指定的字符串是 null 还是 System.String.Empty 字符串。
        /// </summary>
        /// <param name="value"> 要测试的字符串。</param>
        /// <returns>如果 true 参数为 value 或 null，或者如果 System.String.Empty 仅由空白字符组成，则为 value。</returns>
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }
        ///// <summary>
        ///// 将json字符串序列化为对象
        ///// </summary>
        ///// <typeparam name="T">目标对象</typeparam>
        ///// <param name="value">要转化的字符串</param>
        ///// <returns>如果成功则返回 T 对象实例，否则抛出 Exception</returns>
        //public static T JsonToObject<T>(this string value)
        //{
        //    return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(value);
        //}

        public static string[] ToSplitArray(this string value, char split)
        {
            return value.Split(split);
        }
        /// <summary>
        /// base64编码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToBase64(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }
            var buff = Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(buff);
        }
        /// <summary>
        /// base64 解码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string FormBase64(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }
            var buff = Convert.FromBase64String(input);

            return Encoding.UTF8.GetString(buff);
        }
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToMd5(this string input)
        {
            return DESEncrypt.Md5To32Lens(input);
        }
        /// <summary>
        /// 获取签名
        /// </summary>
        /// <param name="input"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string ToSignature(this string input, string key)
        {
            return AuthenticationHelper.GetAuthenticationCode(input, key);
        }
        /// <summary>
        /// 获取签名
        /// </summary>
        /// <param name="input"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string ToSignature(this Dictionary<string, object> input, string key)
        {
            return AuthenticationHelper.GetAuthenticationCode(input, key);
        }
        /// <summary>
        /// 将服务器图片相对路径转化为HTTP路径
        /// </summary>
        /// <param name="input"></param>
        /// <param name="defaultImageType">默认图片类型，默认使用默认图</param>
        /// <returns></returns>
        public static string ToImageUrl(this string input, DefaultImageVlaueType defaultImageType = DefaultImageVlaueType.DefaultImage)
        {
            var fileServerUrl = ConfigHelper.GetAppsettingValue(TianYuConsts.FileServerUrl);
            string uploadPaths = "uploadFiles/";
            if (input.IsNullOrWhiteSpace())
            {
                if (defaultImageType == DefaultImageVlaueType.None)
                {
                    return string.Empty;
                }
                else
                {
                    //描述中应存放默认图片的地址
                    return string.Format("{0}/{1}{2}", fileServerUrl, uploadPaths, defaultImageType.GetEnumDescription());
                }
            }
            if (input.StartsWith("http://") || input.StartsWith("https://"))
            {
                return input;
            }
            return string.Format("{0}/{1}{2}", fileServerUrl, uploadPaths, input.Replace('\\','/'));
        }
        /// <summary>
        /// 转为关联显示
        /// </summary>
        /// <param name="str">第一字符串</param>
        /// <param name="substr">子字符串</param>
        /// <param name="format">0=英文括号()，1=中文括号（），2=中文中括号【】</param>
        /// <returns></returns>
        public static string ToRelationString(this string str, string substr, int format = 1)
        {
            string newStr = string.Empty;
            if (substr.IsNullOrWhiteSpace())
            {
                return str;
            }
            if (str.IsNullOrWhiteSpace())
            {
                return newStr;
            }

            string formatStr = string.Empty;
            switch (format)
            {
                case 0:
                    formatStr = "{0}({1})";
                    break;
                case 1:
                    formatStr = "{0}（{1}）";
                    break;
                case 2:
                    formatStr = "{0}【{1}】";
                    break;
            }
            newStr = string.Format(formatStr, str, substr);
            return newStr;
        }

        /// <summary>
        /// 获取带*手机号
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public static string ToHideMobile(this string mobile)
        {
            if (mobile.IsNullOrWhiteSpace())
            {
                return "";
            }
            return $"{mobile.Substring(0, 4)}****{mobile.Substring(mobile.Length - 4)}";
        }
        /// <summary>
        /// 将网络图片路径转化为服务器相对路径（将下载网络图片并保存到文件服务器）
        /// </summary>
        /// <param name="input"></param>
        /// <param name="fileExt">保存文件扩展名（如：.jpg）</param>
        /// <returns></returns>
        public static string ToShortImageUrl(this string input, string fileExt = ".jpg")
        {
            if (input.IsNullOrWhiteSpace())
            {
                return string.Empty;
            }
            if (!input.StartsWith("http://") && !input.StartsWith("https://"))
            {
                return input;
            }

            var fileServerUrl = ConfigHelper.GetAppsettingValue(TianYuConsts.FileServerUrl);
            string apiUrl = string.Format("{0}/api/File/UploadFileByNetworkFile", fileServerUrl);

            Dictionary<string, object> param = new Dictionary<string, object>()
            {
                { "FileUrl", input },
                { "FileExt", fileExt },
            };

            var resList = HttpHelper.HttpPost<BaseViewModel.BusinessBaseViewModel<BaseViewModel.FileUploadViewModel>>(apiUrl, param);
            if (resList.Status == ResponseStatus.Success && resList.BusinessData != null && resList.BusinessData.UploadResult != null && resList.BusinessData.UploadResult.Any())
            {
                //返回保存的文件服务器相对路径
                return resList.BusinessData.UploadResult.FirstOrDefault()?.ServerFilePath;
            }
            //不成功，返回原字符
            return input;
        }
    }
}
