using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianYu.Core.Common.BaseViewModel
{
    /// <summary>
    /// 文件上传响应业务视图
    /// </summary>
    public class FileUploadViewModel 
    {
        /// <summary>
        /// 文件上传处理结果
        /// </summary>
        public IList<FileUploadItemViewModel> UploadResult { get; set; }
    }
    /// <summary>
    /// 文件上传项
    /// </summary>
    public class FileUploadItemViewModel
    {
        /// <summary>
        /// 文件大小：以字节为单位
        /// </summary>
        public int FileSize { get; set; }
        /// <summary>
        /// 源文件名
        /// </summary>
        public string SourceFileName { get; set; }
        /// <summary>
        /// 服务器存储相对路径地址
        /// </summary>
        public string ServerFilePath { get; set; }
        /// <summary>
        /// 网络访问路径
        /// </summary>
        public string FileUrl { get; set; }
        /// <summary>
        /// 处理是否成功
        /// </summary>
        public bool IsSuccess { get; set; }
    }
}
