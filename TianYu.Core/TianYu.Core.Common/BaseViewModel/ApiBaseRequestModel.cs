using System.ComponentModel.DataAnnotations;

namespace TianYu.Core.Common.BaseViewModel
{
    /// <summary>
    /// 接口请求参数基类
    /// </summary>
    public class ApiBaseRequestModel
    {
        /// <summary>
        /// 请求时间戳
        /// </summary>
        [Required]
        public string Timestamp { get; set; }
        /// <summary>
        /// 请求随机字符串
        /// </summary>
        [Required]
        public string Noncestr { get; set; }
        /// <summary>
        /// 请求签名
        /// </summary>
        [Required]
        public string Signature { get; set; }
    }
    /// <summary>
    /// 分页基本请求参数模型
    /// </summary>
    public class ApiBasePageRequestModel : ApiBaseRequestModel
    {
        /// <summary>
        /// 当前页码
        /// </summary>
        [Required]
        public int PageIndex { get; set; } = 1;
        /// <summary>
        /// 页码大小
        /// </summary>
        [Required]
        public int PageSize { get; set; } = 10;
    }

    /// <summary>
    /// 分页关键字基本请求参数模型
    /// </summary>
    public class ApiBasePageKeywordRequestModel : ApiBasePageRequestModel
    {
        /// <summary>
        /// 搜索关键字
        /// </summary>
        public string Keyword { get; set; } = "";
    }
    /// <summary>
    /// 字符串Id基本请求参数模型
    /// </summary>
    public class ApiBaseStringIdRequestModel : ApiBaseRequestModel
    {
        /// <summary>
        /// 字符串Id
        /// </summary>
        [Required]
        public string Id { get; set; } = "";
    }
    /// <summary>
    /// 整形Id基本请求参数模型
    /// </summary>
    public class ApiBaseIntIdRequestModel : ApiBaseRequestModel
    {
        /// <summary>
        /// 整形Id
        /// </summary>
        [Required]
        public int Id { get; set; } = 0;
    }
    /// <summary>
    /// Api授权请求参数实体
    /// </summary>
    public class ApiAuthorizeRequestModel : ApiBaseRequestModel
    {
        /// <summary>
        /// 应用号
        /// </summary>
        [Required]
        public string AppId { get; set; }
        /// <summary>
        /// 请求设备号（设备唯一标识符uuid）
        /// </summary>
        [Required]
        public string DeviceNo { get; set; }
        /// <summary>
        /// 客户端类型(必须与AppId一致)
        /// </summary>
        [Required]
        public string ClientType { get; set; }
    }

}
