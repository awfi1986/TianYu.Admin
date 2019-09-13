using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using TianYu.Admin.Domain.BaseModel;

namespace TianYu.Admin.Domain
{
    /// <summary>
    /// 字典类型表
    /// </summary>
    public partial class DictionaryType: AggregateRoot
    {
        public DictionaryType()
        {
            this.Id= 0;
            this.DicTypeCode= string.Empty;
            this.DicTypeName= string.Empty;
            this.Remarks= string.Empty;
        }

        /// <summary>
        /// 主键Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 字典类型编号（如：1000、1001）
        /// </summary>
        public string DicTypeCode { get; set; }
        /// <summary>
        /// 字典类型名称
        /// </summary>
        public string DicTypeName { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public System.DateTime? CreateTime { get; set; }
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public System.DateTime? UpdateTime { get; set; }
        /// <summary>
        /// 是否有效（1有效、0无效）
        /// </summary>
        public bool IsValid { get; set; }

    }
}