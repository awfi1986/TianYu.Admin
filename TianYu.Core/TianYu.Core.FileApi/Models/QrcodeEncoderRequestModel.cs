using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TianYu.Core.FileApi.Models
{
    /// <summary>
    /// 生成二维码图片请求参数
    /// </summary>
    public class QrcodeEncoderRequestModel
    {
        /// <summary>
        /// 二维码内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// logo文件相对地址
        /// </summary>
        public string LogoFilePath { get; set; }
        /// <summary>
        /// 是否使用logo
        /// </summary>
        public bool IsLogo { get; set; } = true;
    }
}