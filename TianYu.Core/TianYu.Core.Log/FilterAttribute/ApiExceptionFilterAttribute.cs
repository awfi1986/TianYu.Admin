
using TianYu.Core.Common.BaseViewModel;
using TianYu.Core.Common; 
using System.Linq;
using System.Net.Http; 
using System.Web.Http.Filters;
using System.Net; 
using TianYu.Core.Common.FilterAttribute.Api;

namespace TianYu.Core.Log.FilterAttribute
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnException(actionExecutedContext);

            string tagValue = string.Empty;
            if (actionExecutedContext.Request.Headers.Contains("token"))
            {
                tagValue = actionExecutedContext.Request.Headers.GetValues("token").FirstOrDefault();
            }
            else
            {
                tagValue = "";
            }
            string responseResult = string.Empty;
            if (!actionExecutedContext.ActionContext.ActionDescriptor.GetCustomAttributes<IgnoreResultFormatAttribute>().Any() &&
              !actionExecutedContext.ActionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<IgnoreResultFormatAttribute>().Any())
            {
                var errorMessage = actionExecutedContext.Exception.Message;
                var result = new BaseResponse()
                {
                    Status = actionExecutedContext.Response.IsNull() ? System.Net.HttpStatusCode.ServiceUnavailable : actionExecutedContext.Response.StatusCode,
                    ErrorMessage = errorMessage
                };
                actionExecutedContext.Response = new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(result.ToJsonString())
                };

                responseResult = result.ToJsonString();
            }
            else
            {
                if (actionExecutedContext.ActionContext.Response.IsNull() || actionExecutedContext.ActionContext.Response.Content.IsNull())
                {
                    actionExecutedContext.Response = new HttpResponseMessage()
                    {
                        StatusCode = HttpStatusCode.OK,
                        Content = new StringContent("程序错误，请联系系统管理员")
                    };
                }
                else
                {
                    responseResult = actionExecutedContext.ActionContext.Response.Content.ReadAsStringAsync().Result;
                    actionExecutedContext.Response = new HttpResponseMessage()
                    {
                        StatusCode = HttpStatusCode.OK,
                        Content = new StringContent(responseResult)
                    };
                }
            }
            LogHelper.LogError(string.Format("请求:{0}", actionExecutedContext.Request.RequestUri.ToString()),
                                            string.Format("异常信息:{0}", actionExecutedContext.Exception.ToString()),
                                            actionExecutedContext.ActionContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                                            actionExecutedContext.ActionContext.ActionDescriptor.ActionName,
                                            tagValue, "error");


        }
    }
}
