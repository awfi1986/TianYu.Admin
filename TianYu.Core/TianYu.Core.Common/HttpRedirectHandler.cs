using TianYu.Core.Common; 
using System; 
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http; 
using System.Threading;
using System.Threading.Tasks;

namespace TianYu.Core.Middleware
{
    public class HttpRedirectHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //LogHelper.LogInfo("原请求地址:", request.RequestUri.AbsoluteUri);
            var requestMessage = this.RouteMapConfig(request);
            //LogHelper.LogInfo("转发请求地址:", requestMessage.RequestUri.AbsoluteUri);

            string requestData = string.Empty;
            //获取请求头
            string token = "", sessionId = "", ApiVersion = "";
            if (requestMessage.Headers.Contains("Authorization"))
            {
                token = requestMessage.Headers.GetValues("Authorization").FirstOrDefault();
                //LogHelper.LogInfo("请求token:", token);
            }
            if (requestMessage.Headers.Contains("SessionId"))
            {
                sessionId = requestMessage.Headers.GetValues("SessionId").FirstOrDefault();
                //LogHelper.LogInfo("请求SessionId:", sessionId);
            }
            if (requestMessage.Headers.Contains("ApiVersion"))
            {
                ApiVersion = requestMessage.Headers.GetValues("ApiVersion").FirstOrDefault();
                //LogHelper.LogInfo("请求ApiVersion:", ApiVersion);
            }
            using (StreamReader sr = new StreamReader(requestMessage.Content.ReadAsStreamAsync().Result))
            {
                sr.BaseStream.Position = 0;
                requestData = sr.ReadToEnd();
            }
            //LogHelper.LogInfo("请求参数:", requestData);
            HttpResult result = null;

            if (requestMessage.Method == HttpMethod.Post)
            {
                result = HttpHelper.HttpPost(requestMessage.RequestUri.AbsoluteUri, requestData, token, sessionId, ApiVersion);

            }
            else
            {
                result = HttpHelper.HttpGet(requestMessage.RequestUri.AbsoluteUri, requestData, token, sessionId, ApiVersion);
            }

            //LogHelper.LogInfo("返回结果:", result.Html.ToJsonString());

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(result.Html)
            };
            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return tsc.Task;
        }


        private HttpRequestMessage RouteMapConfig(HttpRequestMessage request)
        {
            string appName = string.Empty;

            if (request.Properties.ContainsKey("ApiVersion"))
            {
                var apiVersion = request.Headers.GetValues("ApiVersion").FirstOrDefault();
                //包含版本号 通过版本号获取主机地址
                appName = ConfigurationManager.AppSettings[apiVersion].ToString();
                if (string.IsNullOrWhiteSpace(appName))
                {
                    appName = ConfigurationManager.AppSettings["defaultVersion"].ToString();
                }
            }
            else
            {
                //如果没有版本号 获取默认主机地址
                appName = ConfigurationManager.AppSettings["defaultVersion"].ToString();
            }

            //LogHelper.LogInfo("请求Host", request.RequestUri.Host);
            //LogHelper.LogInfo("请求PORT", request.RequestUri.Port.ToString());

            //重新构造请求路径
            string requestUrl = request.RequestUri.AbsoluteUri.Replace("http://", "").Replace("https://", "");
            if (request.RequestUri.Port.ToString() == "80")
            {
                var domain = ConfigurationManager.AppSettings["domain"].ToString();
                requestUrl = domain + "/" + appName + requestUrl.Replace(requestUrl.Substring(0, requestUrl.IndexOf('/')), "");
                //          requestUrl = "http://172.18.177.86:8051/API/UserBalanceInner/FindUserBalanceInfo";
            }
            else
            {
                var host = ConfigurationManager.AppSettings["host"].ToString();
                requestUrl = host + "/" + appName + requestUrl.Replace(requestUrl.Substring(0, requestUrl.IndexOf('/')), "");
            }

            //LogHelper.LogInfo("替换后URL", requestUrl);
            request.RequestUri = new Uri(requestUrl);
            return request;
        }
    }
}
