using TianYu.Core.Common.BaseViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Net;
using System.IO;
using System.Net.Http;
using System.Web;
using Newtonsoft.Json;

namespace TianYu.Core.Log.FilterAttribute
{
    public class MvcExceptionFilterAttribute : System.Web.Mvc.HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);

            string tagValue = string.Empty;
            if (!string.IsNullOrEmpty(filterContext.HttpContext.Request.Headers["token"]))
            {
                tagValue = filterContext.HttpContext.Request.Headers["token"].ToString();
            }
            else
            {
                tagValue = "";
            }

            var errorMessage = JsonConvert.SerializeObject(filterContext.Exception);

            Exception exception = filterContext.Exception;

            HttpException httpException = new HttpException(null, exception);
            var result = new BaseResponse()
            {
                ErrorMessage = errorMessage
            };

            if (httpException != null && (httpException.GetHttpCode() == 400 || httpException.GetHttpCode() == 404))
            {
                result.Status = (HttpStatusCode)(404);
            }
            else
            {
                result.Status = (HttpStatusCode)(500);
            }
            //               filterContext.HttpContext.Response.StatusCode = 500;
            //               filterContext.HttpContext.Response.Write("服务器内部错误");
            //                filterContext.HttpContext.Response.WriteFile("~/HttpError/500.html");

            LogHelper.LogError(string.Format("请求:{0}", filterContext.HttpContext.Request.RawUrl),
                                            string.Format("异常信息:{0}", JsonConvert.SerializeObject(result)),
                                            filterContext.RouteData.Values["controller"].ToString(),
                                            filterContext.RouteData.Values["action"].ToString(),
                                            tagValue, "error");


            result.ErrorMessage = "程序错误，请联系系统管理员";
            if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest() /*&& (filterContext.Controller.ViewBag.ViewType == null || (filterContext.Controller.ViewBag.ViewType as string) != "PartialView")*/)
            {
                filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;

                filterContext.Result = new ContentResult()
                {
                    Content = JsonConvert.SerializeObject(result),
                    ContentEncoding = Encoding.GetEncoding("UTF-8"),
                    ContentType = "application/json"
                };
            }
            else
            {

                var redirect = new RedirectResult("/Error/index?msg=" + result.ErrorMessage);
                filterContext.Result = redirect;
            }
            filterContext.ExceptionHandled = true;
            //在派生类中重写时，获取或设置一个值，该值指定是否禁用IIS自定义错误。
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;

        }
    }
}
