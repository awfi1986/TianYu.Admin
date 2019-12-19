using Newtonsoft.Json;
using TianYu.Core.Common;
using TianYu.Core.Common.BaseViewModel;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TianYu.Core.Common.FilterAttribute.Api;

namespace TianYu.Core.Log.FilterAttribute
{
    public class MvcActionFilterAttribute : System.Web.Mvc.ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            string tagValue = "";
            if (!string.IsNullOrEmpty(filterContext.RequestContext.HttpContext.Request.Headers["token"]))
            {
                tagValue = filterContext.RequestContext.HttpContext.Request.Headers["token"].ToString();
            }

            if (filterContext.RequestContext.HttpContext.Request.RequestType == "GET")
            {

                LogHelper.LogInfo($"请求:{filterContext.RequestContext.HttpContext.Request.Url}", "",
                    filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                    filterContext.ActionDescriptor.ActionName,
                    tagValue);
            }
            else
            {
                LogHelper.LogInfo($"请求:{filterContext.RequestContext.HttpContext.Request.Url}",
                    $"参数列表:{filterContext.ActionParameters.ToJsonString()}",
                    filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                    filterContext.ActionDescriptor.ActionName,
                    tagValue);
            }

        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Exception != null)
                return;
            base.OnActionExecuted(filterContext);

            if (filterContext.ActionDescriptor.GetCustomAttributes(typeof(IgnoreResultFormatAttribute), true).Any() || filterContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(typeof(IgnoreResultFormatAttribute), true).Any())
            {
                return;
            }
            BaseResponse response = new BaseResponse();
            response.Status = (HttpStatusCode)filterContext.HttpContext.Response.StatusCode;
            if ((filterContext.Result is ViewResult) || (filterContext.Result is FileContentResult) || (filterContext.Result is FileStreamResult) || (filterContext.Result is PartialViewResult))
            {
                response.Data = "";
            }
            else if (filterContext.Result is ContentResult)
            { 
                response.Data = JsonConvert.DeserializeObject((filterContext.Result as ContentResult).Content);
            }
            else if (filterContext.Result is JsonResult)
            {
                response.Data = (filterContext.Result as JsonResult).Data;
            }

            string tagValue = "";
            if (!string.IsNullOrEmpty(filterContext.RequestContext.HttpContext.Request.Headers["token"]))
            {
                tagValue = filterContext.RequestContext.HttpContext.Request.Headers["token"].ToString();
            } 

            LogHelper.LogInfo($"请求:{filterContext.RequestContext.HttpContext.Request.Url}",
                              $"返回结果:{response.ToJsonString()}",
                              filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                              filterContext.ActionDescriptor.ActionName,
                              tagValue);


            if ((filterContext.Result is ContentResult) || (filterContext.Result is JsonResult))
            {
                var context = new ContentResult
                {
                    Content = response.ToJsonString()
                };
                filterContext.Result = context;
                //              filterContext.HttpContext.Response.Write(JsonConvert.SerializeObject(response));
                filterContext.ExceptionHandled = true;
                filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
            }
            else
            {

            }
        }

    }
}
