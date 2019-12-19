using TianYu.Core.Cache;
using TianYu.Core.Common.FilterAttribute.AuthorizeCode;
using TianYu.Core.Common.BaseViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters; 

namespace TianYu.Core.Common.FilterAttribute.Api
{
    /// <summary>
    /// 验证登录特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class ValidateLoginAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 重写执行之前方法
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            //验证是否有忽略登录特性
            if (SkipValidateLogin(actionContext))
            {
                //跳过验证
                base.OnActionExecuting(actionContext);
                return;
            }

            //获取请求参数SessionId
            string sessionid = string.Empty;
            if (actionContext.Request.Headers.TryGetValues(TianYuConsts.WebApiSessionId, out IEnumerable<string> vals))
            {
                sessionid = vals.FirstOrDefault();
            }
            //验证是否已经登录
            var res = ValidateLogin(sessionid, actionContext);
            if (res.Status != ResponseStatus.Success)
            {
                //未登录，直接响应错误结果
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK, res);
                return;
            }

            //保存用户信息，便于接口使用
            actionContext.Request.Properties[TianYuConsts.WebApiCurrentUserGroup] = res.BusinessData;

            //允许访问
            base.OnActionExecuting(actionContext);
            return;
        }

        /// <summary>
        /// 验证登录
        /// </summary>
        /// <param name="sessionid">登录会话Id</param>
        /// <returns></returns>
        private BusinessBaseViewModel<object> ValidateLogin(string sessionid, HttpActionContext actionContext)
        {
            var response = new BusinessBaseViewModel<object> { Status = ResponseStatus.Fail };

            if (sessionid.IsNullOrWhiteSpace())
            {
                response.Status = ResponseStatus.UnSessionIdParamsError;
                return response;
            }
            string deviceNo = string.Empty;
            string sessionidCacheKey = TianYuConsts.GetSessionIdCacheKey(sessionid);
            var userGroup = Cache.CacheHelper.Get<SystemLoginUserInfo>(sessionidCacheKey);
            if (userGroup.IsNull())
            {
                var model = LoginSessionDataHelper.GetSessionInfoModel(sessionid);
                if (model.IsNull())
                {
                    response.Status = ResponseStatus.SessionIdError;
                    return response;
                }
                else
                {
                    deviceNo = model.DeviceNo;
                }
            }
            else
            {
               // deviceNo = userGroup.DeviceNo;
            }
            string ticket = actionContext.Request.Headers.Authorization.Parameter;
            if (ticket.IsNullOrWhiteSpace())
            {
                response.Status = ResponseStatus.UnAuthorityError;
                return response;
            }
            //解密票据
            string key = TianYuConsts.GetTicketCacheKey(ticket);
            var ticketDetailsModel = CacheHelper.Get<AuthenticationTicketDetailsModel>(key);
            if (ticketDetailsModel.IsNull())
            {
                response.Status = ResponseStatus.AuthenticationTicketTimeOut;
                return response;
            }
            if (ticketDetailsModel.ClientType != AuthClientType.SamllApp.GetEnumDescription() && ticketDetailsModel.DeviceNo != deviceNo)
            {
                response.Status = ResponseStatus.SessionIdOtherLogin;
                response.BusinessData = deviceNo;
                return response;
            }
            response.BusinessData = userGroup;
            response.Status = ResponseStatus.Success;

            return response;
        }

        /// <summary>
        /// 跳过登录验证
        /// </summary>
        /// <param name="actionContext"></param>
        /// <returns></returns>
        private bool SkipValidateLogin(HttpActionContext actionContext)
        {
            return actionContext.ActionDescriptor.GetCustomAttributes<IgnoreLoginAttribute>().Any() || actionContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes<IgnoreLoginAttribute>().Any() ||
                actionContext.ActionDescriptor.GetCustomAttributes<InnerServiceAttribute>().Any() || actionContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes<InnerServiceAttribute>().Any();
        }
    }
}
