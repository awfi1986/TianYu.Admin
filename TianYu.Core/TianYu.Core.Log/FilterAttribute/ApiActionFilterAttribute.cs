using Exceptionless.Json; 
using TianYu.Core.Common.BaseViewModel;
using TianYu.Core.Common; 
using System.Linq;
using System.Net.Http; 
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using TianYu.Core.Common.FilterAttribute.Api;

namespace TianYu.Core.Log.FilterAttribute
{
    public class ApiActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception != null)
                return;
            base.OnActionExecuted(actionExecutedContext);
            string tagValue = string.Empty;
            if (actionExecutedContext.Request.Headers.Contains("Authorization"))
            {
                tagValue = actionExecutedContext.Request.Headers.GetValues("Authorization").FirstOrDefault();
            }
            else
            {
                tagValue = "";
            }
            string sessionId = string.Empty;
            if (actionExecutedContext.Request.Headers.Contains("SessionId"))
            {
                sessionId = actionExecutedContext.Request.Headers.GetValues("SessionId").FirstOrDefault();
            }
            else
            {
                sessionId = "";
            }
            string responseResult = string.Empty;
            if (!actionExecutedContext.ActionContext.ActionDescriptor.GetCustomAttributes<IgnoreResultFormatAttribute>().Any() &&
              !actionExecutedContext.ActionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<IgnoreResultFormatAttribute>().Any())
            {
                BaseResponse response = new BaseResponse();
                {
                    response.Status = actionExecutedContext.Response.StatusCode;
                    response.Data = actionExecutedContext.ActionContext.Response.Content.ReadAsAsync<object>().Result;
                };

                actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(response.Status, response);
                responseResult = response.ToJsonString();
            }
            else
            {
                responseResult = actionExecutedContext.ActionContext.Response.Content.ReadAsStringAsync().Result;
            }
            if (!actionExecutedContext.ActionContext.ActionDescriptor.GetCustomAttributes<IgnoreLogAttribute>().Any() &&
                !actionExecutedContext.ActionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<IgnoreLogAttribute>().Any())
            {
                LogHelper.LogInfo(string.Format("请求:{0}", actionExecutedContext.Request.RequestUri.ToString()),
                                        string.Format("返回结果:{0}", responseResult), 
                                        actionExecutedContext.ActionContext.ActionDescriptor.ControllerDescriptor.ControllerName, 
                                        actionExecutedContext.ActionContext.ActionDescriptor.ActionName,  
                                        tagValue);
                
            }
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            base.OnActionExecuting(actionContext);

            string tagValue = string.Empty;
            if (actionContext.Request.Headers.Contains("token"))
            {
                tagValue = actionContext.Request.Headers.GetValues("token").FirstOrDefault();
            }
            else
            {
                tagValue = "";
            }
            if (!actionContext.ActionDescriptor.GetCustomAttributes<IgnoreLogAttribute>().Any() &&
            !actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<IgnoreLogAttribute>().Any())
            {


                if (actionContext.Request.Method == HttpMethod.Get)
                {
                    LogHelper.LogInfo(string.Format("请求:{0}", actionContext.Request.RequestUri.ToString()), "",
                        actionContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                        actionContext.ActionDescriptor.ActionName,
                        tagValue);
                }
                else
                {
                    LogHelper.LogInfo(string.Format("请求:{0}", actionContext.Request.RequestUri.ToString()), string.Format("参数列表:{0}", JsonConvert.SerializeObject(actionContext.ActionArguments)),
                        actionContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                        actionContext.ActionDescriptor.ActionName,
                        tagValue);
                }
            }
        }

    }
}
