using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TianYu.Core.FileApi.Models
{
    /// <summary>
    /// 根据网络图片上传文件请求参数
    /// </summary>
    public class UploadFileNetworkFileParamModel
    {
        /// <summary>
        /// 网络文件Url
        /// </summary>
        public string FileUrl { get; set; }
        /// <summary>
        /// 扩展参数
        /// </summary>
        public string Extpath { get; set; }
        /// <summary>
        /// 文件扩展名（如：.jpg）
        /// </summary>
        public string FileExt { get; set; } = ".jpg";
    }
}