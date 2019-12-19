using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace TianYu.Core.FileApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            //日期格式化
            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";

        }
    }
}
