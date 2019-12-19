using System.Net; 

namespace TianYu.Core.Common.BaseViewModel
{
    /// <summary>
    /// Http请求消息响应对象
    /// </summary>
    public class BaseResponse<T>
    {
        /// <summary>
        /// HTTP 状态码
        /// </summary>
        public HttpStatusCode Status { get; set; }
        /// <summary>
        /// 数据内容
        /// </summary>
        public T Data { get; set; }
        /// <summary>
        /// 当发生错误时，表示错误消息。没有错误时，为NULL或""
        /// </summary>
        public string ErrorMessage { get; set; }
    }
    /// <summary>
    /// Http请求消息响应对象
    /// </summary>
    public class BaseResponse : BaseResponse<object>
    {

    }
}
