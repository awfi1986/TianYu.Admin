using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace TianYu.Core.Common.BaseViewModel
{
   public class FileUploadInputModel
    {
        /// <summary>
        /// 扩展目录(不指定则按默认路径存储)
        /// </summary>
        public string ExtensionPath { get; set; }

    }
}
