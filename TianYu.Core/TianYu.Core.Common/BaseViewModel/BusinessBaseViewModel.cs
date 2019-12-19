using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianYu.Core.Common.BaseViewModel
{
    /// <summary>
    /// 业务处理响应基础实体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BusinessBaseViewModel<T> //where T : OutputBaseModel
    {
        /// <summary>
        /// 业务状态
        /// </summary>
        public ResponseStatus Status { get; set; } = ResponseStatus.Fail;
        /// <summary>
        /// 业务数据
        /// </summary>
        public T BusinessData { get; set; }
        private string _errorMessage = string.Empty;
        /// <summary>
        /// 业务错误消息
        /// </summary>
        public string ErrorMessage
        {
            get
            {
                if (_errorMessage.IsNullOrEmpty())
                {
                    return Status.GetEnumDescription();
                }
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
            }
        }
    }

    /// <summary>
    /// 业务处理响应基础实体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BusinessPagedBaseViewModel<T> : BusinessBaseViewModel<IEnumerable<T>>
    {
        /// <summary>
        /// 数据总行数
        /// </summary>
        public int Total { get; set; }
    }
}
