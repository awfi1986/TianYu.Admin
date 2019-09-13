using System.Web.Mvc;
using TianYu.Core.Common.FilterAttribute.Mvc;

namespace TianYu.Admin.WebMvc.Controllers
{
    /// <summary>
    /// 错误页面控制器
    /// </summary>
    [MvcIgnoreLogin]
    public class ErrorController : Controller
    {
        /// <summary>
        /// 默认错误页面
        /// </summary>
        /// <param name="msg">错误消息</param>
        /// <returns></returns>
        public ActionResult Index(string msg)
        {
            msg = string.IsNullOrEmpty(msg) ? "" : this.HttpContext.Server.UrlDecode(msg);
            ViewBag.Message = msg;
            return View();
        }
    }
}