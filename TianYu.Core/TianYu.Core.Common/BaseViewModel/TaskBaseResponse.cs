using System.Net; 

namespace TianYu.Core.Common.BaseViewModel
{
    /// <summary>
    /// task任务响应视图
    /// </summary>
    public class TaskBaseResponse
    {
        /// <summary>
        /// HTTP 状态码（200 表示业务执行成功，其他为失败）
        /// </summary>
        public HttpStatusCode Status { get; set; }     
        /// <summary>
        /// 当发生错误时，表示错误消息。没有错误时，为NULL或""
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}
