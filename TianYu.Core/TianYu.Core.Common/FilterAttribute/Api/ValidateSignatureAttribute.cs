using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Net.Http;
using System.Net;
using System.Web.Http.Controllers;
using System.IO;
using System.Text;
using TianYu.Core.Common.BaseViewModel;
using TianYu.Core.Common.FilterAttribute.AuthorizeCode;
using TianYu.Core.Common.Security;

namespace TianYu.Core.Common.FilterAttribute.Api
{
    /// <summary>
    /// 验证签名
    /// </summary>
    public class ValidateSignatureAttribute : ActionFilterAttribute
    {
        private readonly string signKey = "Signature";

        /// <summary>
        ///     在调用操作方法之前发生。
        /// </summary>
        /// <param name="actionContext">操作上下文。</param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (SkipValidateSignature(actionContext) || ValidateInnerService(actionContext))//是否该类标记为NoLog
            {
                base.OnActionExecuting(actionContext);
            }
            else //if (actionContext.ActionDescriptor.GetCustomAttributes<RequestAuthorizeAttribute>().Any() || actionContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes<RequestAuthorizeAttribute>().Any())
            {
                var dictionary = new Dictionary<string,object>(actionContext.ActionArguments);

                string ticket = actionContext.Request.Headers.Authorization != null ? actionContext.Request.Headers.Authorization.Parameter : "";

                if (actionContext.Request.Method == HttpMethod.Post)
                {
                    actionContext.Request.Content.ReadAsStreamAsync().Result.Seek(0, SeekOrigin.Begin);
                    string requestContext = actionContext.Request.Content.ReadAsStringAsync().Result;
                    actionContext.Request.Content.ReadAsStreamAsync().Result.Seek(0, SeekOrigin.Begin);

                    dictionary = JsonHelper.DeserializeObject<Dictionary<string, object>>(requestContext);
                }
                var keys = dictionary.Keys.ToList();
                foreach (var key in keys)
                {
                    //处理对象类型签名错误问题
                    if (dictionary[key] != null && dictionary[key].GetType().Namespace == "Newtonsoft.Json.Linq")
                    {
                        dictionary[key] = JsonHelper.SerializeObject(dictionary[key]);
                    }
                }
                if(actionContext.ActionDescriptor.ActionName == "GetTokenTicket" && !string.IsNullOrWhiteSpace(ticket ))
                {
                    ticket = "";
                }
                var response = ValidateSignature(ticket, dictionary);
                if (response.Status != ResponseStatus.Success)
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK, response);
                    return;
                }

                base.OnActionExecuting(actionContext);
            }

        }
        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="ticket"></param>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        private BusinessBaseViewModel<string> ValidateSignature(string ticket, Dictionary<string, object> dictionary)
        {
            BusinessBaseViewModel<string> response = new BusinessBaseViewModel<string> { Status = ResponseStatus.Fail };
            string appSecret = string.Empty;
            if (string.IsNullOrEmpty(ticket))
            {
                if (!dictionary.ContainsKey("AppId"))
                {
                    response.Status = ResponseStatus.ParameterError;
                    return response;
                }
                var appModel = new AuthenticationDataHelper().GetApplocationAuthorModel(dictionary["AppId"].ToString());
                appSecret = appModel.AppSecret;
            }
            else
            {

                var ticketModel = AuthenticationHelper.GetDecryptTicket(ticket);//  new AuthenticationDataHelper().GetApplocationAuthorModel(ticket);
                appSecret = ticketModel.AppSecret;
            }
            if (!dictionary.ContainsKey(signKey))
            {
                response.Status = ResponseStatus.UnSignatureParamsError;
                return response;
            }

            string signature = dictionary[signKey].ToString();
            dictionary.Remove(signKey);
            //验证签名
            string codesign = AuthenticationHelper.GetAuthenticationCode(dictionary, appSecret);
            if (!signature.Equals(codesign, StringComparison.CurrentCultureIgnoreCase))
            {
                bool flag = HttpContext.Current.Request.Url.Host.Equals("localhost", StringComparison.CurrentCultureIgnoreCase);

                response.Status = ResponseStatus.UnSignatureError;
                response.BusinessData = flag ? codesign : "";
                return response;
            }
            //验证时效性
            if (!dictionary.ContainsKey("Timestamp"))
            {
                response.Status = ResponseStatus.UnTimeSpanFromatError;
                return response;
            }
            response.Status = AuthenticationHelper.CheckTimeStamp(dictionary["Timestamp"].ToString());

            return response;
        }

        /// <summary>
        /// 验证内部接口访问权限
        /// </summary>
        /// <param name="actionContext"></param>
        /// <returns></returns>
        private bool ValidateInnerService(HttpActionContext actionContext)
        {
            string ip = Utils.GetClientIp();
            if (actionContext.ActionDescriptor.GetCustomAttributes<InnerServiceAttribute>().Any() ||
                  actionContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes<InnerServiceAttribute>().Any())
            {
                return WhiteListHelper.IsWhiteIp(ip);
            }
            return false;
        }
        /// <summary>
        /// 跳过签名验证
        /// </summary>
        /// <param name="actionContext"></param>
        /// <returns></returns>
        private bool SkipValidateSignature(HttpActionContext actionContext)
        {
            string ip = Utils.GetClientIp();
            return actionContext.ActionDescriptor.GetCustomAttributes<IgnoreSignatureAttribute>().Any() ||
                actionContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes<IgnoreSignatureAttribute>().Any() ||
                WhiteListHelper.IsWhiteIp(ip);
        }
    }
}