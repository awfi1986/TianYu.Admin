using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TianYu.Core.FileApi.Models
{
    /// <summary>
    /// 红包分享图片参数
    /// </summary>
    public class RedPacketShareImgRequestModel
    {
        /// <summary>
        /// 业务类型（1=APP，2=小程序）
        /// </summary>
        [Required]
        public int Type { get; set; } = 1;
        /// <summary>
        /// 二维码内容
        /// </summary>
        [Required]
        public string QrContent { get; set; }
        /// <summary>
        /// 文件名（建议传UserId）
        /// </summary>
        [Required]
        public string FileName { get; set; }
        /// <summary>
        /// 用户名称（可选项）
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 用户头像地址（可选项）
        /// </summary>
        public string UserImg { get; set; }
    }
}