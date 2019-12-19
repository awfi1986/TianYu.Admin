using TianYu.Core.Common.FilterAttribute.AuthorizeCode;
using TianYu.Core.Common.BaseViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text; 
using System.Web;
using System.Web.Http.Controllers;

namespace TianYu.Core.Common.Security
{
    /// <summary>
    /// 授权验证帮助类
    /// </summary>
    public sealed class AuthenticationHelper
    {
        private const string SignKeyConst = "Sign";
        private const string TimestampConst = "Timestamp";
        private const string PrivateTicketSecret = "TianYuNet";
        /// <summary>
        /// 计算签名
        /// </summary>
        /// <param name="dictionary">参数字典</param>
        /// <param name="key">私玥</param>
        /// <returns></returns>
        public static string GetAuthenticationCode(Dictionary<string, object> dictionary, string key)
        {
            SortedDictionary<string, object> sortedDictionary = new SortedDictionary<string, object>(dictionary);
            StringBuilder sb = new StringBuilder();
            foreach (var keyvalue in sortedDictionary)
            {
                if (keyvalue.Value != null && keyvalue.Value.GetType().FullName != "System.String" && (keyvalue.Value.GetType().IsClass || keyvalue.Value.GetType().IsInterface))
                {
                    sb.AppendFormat("{0}={1}&", keyvalue.Key, JsonHelper.SerializeObject(keyvalue.Value));
                }
                else
                {
                    sb.AppendFormat("{0}={1}&", keyvalue.Key, keyvalue.Value);
                }

            }

            return GetAuthenticationCode(sb.ToString().TrimEnd('&'), key);
        }
        public static string GetAuthenticationCode(Dictionary<string, object> dictionary, string key, out string parms)
        {
            SortedDictionary<string, object> sortedDictionary = new SortedDictionary<string, object>(dictionary);
            StringBuilder sb = new StringBuilder();
            foreach (var keyvalue in sortedDictionary)
            {
                if (keyvalue.Value != null && keyvalue.Value.GetType().FullName != "System.String" && (keyvalue.Value.GetType().IsClass || keyvalue.Value.GetType().IsInterface))
                {
                    sb.AppendFormat("{0}={1}&", keyvalue.Key, JsonHelper.SerializeObject(keyvalue.Value));
                }
                else
                {
                    sb.AppendFormat("{0}={1}&", keyvalue.Key, keyvalue.Value);
                }
            }
            parms = sb.ToString().TrimEnd('&') + key;
            return GetAuthenticationCode(sb.ToString().TrimEnd('&'), key);
        }
        /// <summary>
        /// 计算签名
        /// </summary>
        /// <param name="parms">参数字符串</param>
        /// <param name="key">私玥</param>
        /// <returns></returns>
        public static string GetAuthenticationCode(string parms, string key)
        {
            MD5 d5 = new MD5CryptoServiceProvider();
            Encoding encoding = Encoding.UTF8;
            string hashCode = string.Format("{0}{1}", parms, key);
            return hashCode.ToMd5();
        }
        /// <summary>
        /// 加密票据
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="clientType"></param>
        /// <param name="deviceNo"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetEncryptTicket(string appId, string clientType, string deviceNo, string key)
        {
            string desKey = key.Substring(12, 8);
            string text = string.Format("{0}&{1}&{2}&{3}&{4}", appId, clientType, deviceNo, DateTime.Now.Ticks, key);
            //string text = string.Format("{0}&{1}&{2}", appId, clientType, deviceNo);
            return DESEncrypt.Encrypt(text, PrivateTicketSecret);
        }
        /// <summary>
        /// 解密票据
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static AuthenticationTicketDetailsModel GetDecryptTicket(string token)
        {

            string tokenText = DESEncrypt.Decrypt(token, PrivateTicketSecret);
            var tokenInfo = tokenText.ToSplitArray('&');
            return new AuthenticationTicketDetailsModel
            {
                TicketAppID = tokenInfo[0],
                ClientType = tokenInfo[1],
                DeviceNo = tokenInfo[2],
                AppSecret = tokenInfo[4]
            };
        }
        /// <summary>
        /// 计算用户token
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="deviceNo">设备号</param>
        /// <param name="ticket">票据</param>
        /// <returns></returns>
        public static string GetUserToken(string userName, string password, string deviceNo, string ticket)
        {
            string text = string.Format("{0}&{1}&{2}&{3}&{4}", userName, password, deviceNo, ticket, DateTime.Now.Ticks);
            return DESEncrypt.Encrypt(text);
        }
        /// <summary>
        /// 接续用户登录token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetDecryptUserToken(string token)
        {
            string text = DESEncrypt.Decrypt(token);
            string[] values = text.Split('&');
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["userName"] = values[0];
            dictionary["password"] = values[1];
            dictionary["deviceNo"] = values[2];
            dictionary["ticket"] = values[3];
            return dictionary;
        }
        /// <summary>
        /// 跳过授权验证
        /// </summary>
        /// <param name="actionContext"></param>
        /// <returns></returns>
        public static bool SkipValidateAuthorize(HttpActionContext actionContext)
        {
            return actionContext.ActionDescriptor.GetCustomAttributes<IgnoreAuthenticationAttribute>().Any() || actionContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes<IgnoreAuthenticationAttribute>().Any();
        }
        /// <summary>
        /// 跳过签名验证
        /// </summary>
        /// <param name="actionContext"></param>
        /// <returns></returns>
        private bool SkipValidateSignature(HttpActionContext actionContext)
        {
            return actionContext.ActionDescriptor.GetCustomAttributes<IgnoreValidateSignatureAttribute>().Any() || actionContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes<IgnoreValidateSignatureAttribute>().Any();
        }
        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="key"></param>
        /// <param name="paramsDictionary"></param>
        /// <returns></returns>
        public static ResponseStatus ValidateSignature(string key, Dictionary<string, object> paramsDictionary)
        {
            if (!paramsDictionary.ContainsKey(SignKeyConst))
            {
                return ResponseStatus.UnSignatureParamsError;
            }

            string signature = paramsDictionary[SignKeyConst].ToString();
            paramsDictionary.Remove(SignKeyConst);
            //验证签名
            string codesign = GetAuthenticationCode(paramsDictionary, key);
            if (!signature.Equals(codesign, StringComparison.CurrentCultureIgnoreCase))
            {
                return ResponseStatus.UnSignatureError;
            }
            //验证时效性
            if (!paramsDictionary.ContainsKey(TimestampConst))
            {
                return ResponseStatus.UnTimeSpanParamsError;
            }

            return CheckTimeStamp(paramsDictionary[TimestampConst].ToString());
        }
        /// <summary>
        /// 检查时间戳是否有效
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static ResponseStatus CheckTimeStamp(string ts)
        {
            if (string.IsNullOrEmpty(ts))
            {
                return ResponseStatus.UnTimeSpanError;
            }
            else if (ts.ToInt(0) == 0)
            {
                return ResponseStatus.UnTimeSpanFromatError;
            }
            long timestamp = Math.Abs(DateTime.Now.ToTimeSpan() - ts.ToInt());
            if (timestamp > 120 || timestamp < 0)
            {
                return ResponseStatus.UnTimeSpanError;
            }
            return ResponseStatus.Success;
        }
        /// <summary>
        /// 生成票据
        /// </summary>
        /// <param name="bodyModel"></param>
        /// <returns></returns>
        public static BusinessBaseViewModel<string> BuildToken(ApiAuthorizeRequestModel bodyModel)
        {
            var response = new BusinessBaseViewModel<string>() { Status = ResponseStatus.Fail };
            if (string.IsNullOrEmpty(bodyModel.AppId)
                || string.IsNullOrEmpty(bodyModel.Timestamp)
                || string.IsNullOrEmpty(bodyModel.ClientType)
                || string.IsNullOrEmpty(bodyModel.DeviceNo)
                || string.IsNullOrEmpty(bodyModel.Noncestr)
                || string.IsNullOrEmpty(bodyModel.Signature))
            {
                response.ErrorMessage = "请求参数错误";
                return response;
            }

            Dictionary<string, object> dictionary = new Dictionary<string, object>
            {
                { "AppId", bodyModel.AppId },
                { "Timestamp", bodyModel.Timestamp },
                { "Noncestr", bodyModel.Noncestr },
                { "DeviceNo", bodyModel.DeviceNo },
                 { "ClientType", bodyModel.ClientType }
            };
            var authenticationData = new AuthenticationDataHelper();
            var applocationAuthor = authenticationData.GetApplocationAuthorModel(bodyModel.AppId);

            if (applocationAuthor == null)
            {
                response.ErrorMessage = "不存在的app";
                return response;
            }
            if (!applocationAuthor.ClientType.Equals(bodyModel.ClientType, StringComparison.CurrentCultureIgnoreCase))
            {
                response.ErrorMessage = "请求APP客户端错误";
                return response;
            }
            string parms = string.Empty;
            string codesign = GetAuthenticationCode(dictionary, applocationAuthor.AppSecret, out parms);
            if (!bodyModel.Signature.Equals(codesign, StringComparison.CurrentCultureIgnoreCase))
            {
                if (HttpContext.Current.Request.Url.Host.Contains("localhost"))
                {
                    response.BusinessData = (new { sign = codesign, md5String = parms }).ToJsonString();
                }
                response.ErrorMessage = "参数签名错误";
                return response;
            }
            response.Status = CheckTimeStamp(bodyModel.Timestamp);
            if (response.Status != ResponseStatus.Success)
            {
                return response;
            }

            //生成票据
            var ticket = GetEncryptTicket(bodyModel.AppId, bodyModel.ClientType, bodyModel.DeviceNo, applocationAuthor.AppSecret);
            var model = authenticationData.GetCheckTicket(ticket);
            if (model.IsNull())
            {
                authenticationData.AddAuthenticationTicketDetails(new FilterAttribute.AuthorizeCode.AuthenticationTicketDetailsModel()
                {
                    Ticket = ticket,
                    TicketAppID = bodyModel.AppId,
                    TicketId = Utils.NewGuid(),
                    LastRefreshDate = DateTime.Now,
                    TicketSecond = 7200,//单位秒，默认7200秒
                    DeviceNo = bodyModel.DeviceNo,
                    ClientType = bodyModel.ClientType,
                    CreateTime = DateTime.Now,
                    AppSecret = applocationAuthor.AppSecret
                });
            }
            response.BusinessData = ticket;
            response.Status = ResponseStatus.Success;
            return response;

        }
    }
}
