using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TianYu.Core.FileApi.Models
{
    public class UploadFileBase64ParamModel
    {
        /// <summary>
        /// base64
        /// </summary>
        public string base64str { get; set; }
        /// <summary>
        /// 扩展名
        /// </summary>
        public string extpath { get; set; }

        /// <summary>
        /// 扩展名
        /// </summary>
        public string fileExtName { get; set; }
    }
}