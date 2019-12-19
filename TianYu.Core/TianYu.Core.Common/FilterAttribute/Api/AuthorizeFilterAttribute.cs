using TianYu.Core.Common.FilterAttribute.AuthorizeCode;
using TianYu.Core.Common.BaseViewModel; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http; 
using System.Web.Http;
using System.Web.Http.Controllers;

namespace TianYu.Core.Common.FilterAttribute.Api
{
    /// <summary>
    /// 处理请求鉴权
    /// </summary>
    public class AuthorizeFilterAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            //跳过鉴权
            if (SkipValidateAuthorize(actionContext) || ValidateInnerService(actionContext))
            {
                base.OnAuthorization(actionContext);
            }
            else //if (actionContext.ActionDescriptor.GetCustomAttributes<RequestAuthorizeAttribute>().Any() || actionContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes<RequestAuthorizeAttribute>().Any())
            {
                if (actionContext.Request.Headers.Authorization == null || string.IsNullOrEmpty(actionContext.Request.Headers.Authorization.Parameter))
                {
                    HandleUnauthorizedRequest(actionContext);
                    return;
                }
                string ticket = actionContext.Request.Headers.Authorization.Parameter;

                if (ValidateTicket(ticket, out AuthenticationTicketDetailsModel ticketModel))
                {
                    actionContext.Request.Properties["ticket"] = true;
                    actionContext.Request.Properties["ticketValue"] = ticket;
                    base.IsAuthorized(actionContext);
                }
                else
                {
                    HandleUnauthorizedRequest(actionContext);
                }
            }
        }
        /// <summary>
        /// 验证Token（并刷新）
        /// </summary>
        /// <param name="ticket"></param>
        /// <param name="ticketModel">票据实体</param>
        /// <returns></returns>
        public bool ValidateTicket(string ticket, out AuthenticationTicketDetailsModel ticketModel)
        {
            var helper = new AuthenticationDataHelper();

            ticketModel = helper.GetCheckTicket(ticket);
            if (ticketModel.IsNull())
            {

                return false;

            }
            //后面可以增加 http 头部 增加 appid、clientType、uuid
            //if (ticketModel.TicketAppID)
            //{

            //}
            if (ticketModel.LastRefreshDate.AddSeconds(ticketModel.TicketSecond) < DateTime.Now)
            {
                return false;
            }
            ////验证票据设备号是否相符
            //if (ticketModel.DeviceNo != AuthenticationHelper.GetDecryptTicket(ticket).DeviceNo)
            //{
            //    return false;
            //}
            helper.RefreshTicketDate(ticket);
            return true;
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            if (SkipValidateAuthorize(actionContext))
            {
                return true;
            }
            else if (actionContext.Request.Properties.ContainsKey("ticket") && actionContext.Request.Properties["ticket"].ToBoolean() == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        protected override void HandleUnauthorizedRequest(HttpActionContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);

            filterContext.Response = filterContext.Request.CreateResponse(HttpStatusCode.OK, new BusinessBaseViewModel<string>()
            {
                Status = ResponseStatus.UnAuthorityError,
            });
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
        /// 跳过授权验证
        /// </summary>
        /// <param name="actionContext"></param>
        /// <returns></returns>
        private bool SkipValidateAuthorize(HttpActionContext actionContext)
        {
            string ip = Utils.GetClientIp();
            return actionContext.ActionDescriptor.GetCustomAttributes<IgnoreAuthorizeAttribute>().Any() ||
                actionContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes<IgnoreAuthorizeAttribute>().Any() ||
                WhiteListHelper.IsWhiteIp(ip);
        }

    }
}