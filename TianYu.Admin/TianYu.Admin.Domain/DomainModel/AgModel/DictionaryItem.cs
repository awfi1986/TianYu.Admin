using TianYu.Admin.Domain.BaseModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace TianYu.Admin.Domain
{
    /// <summary>
    /// 字典项信息表
    /// </summary>
    public partial class DictionaryItem: AggregateRoot
    {
        public DictionaryItem()
        {
            this.Id= 0;
            this.DicTypeCode= string.Empty;
            this.DicCode= string.Empty;
            this.DicName= string.Empty;
            this.DicParentCode= string.Empty;
            this.Remarks= string.Empty;
        }

        /// <summary>
        /// 主键Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 字典类型编号（关联：DictionaryType表DicTypeCode字段）
        /// </summary>
        public string DicTypeCode { get; set; }
        /// <summary>
        /// 字典项编号（如：1000010000）
        /// </summary>
        public string DicCode { get; set; }
        /// <summary>
        /// 字典项名称
        /// </summary>
        public string DicName { get; set; }
        /// <summary>
        /// 字典项父级编号（关联：本表DicCode字段，无父级此项：为空串）
        /// </summary>
        public string DicParentCode { get; set; }
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