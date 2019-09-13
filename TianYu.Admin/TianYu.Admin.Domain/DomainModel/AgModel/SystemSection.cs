﻿// <autogenerated>
//   This file was generated by T4 code generator Template.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

 
using System;
using TianYu.Admin.Domain.BaseModel; 

namespace TianYu.Admin.Domain 
{
    /// <summary> 
    /// 实体-部门 
    /// </summary> 
    public partial class SystemSection : AggregateRoot
    {
        public SystemSection()
        {
        }

        ///<summary>
        /// Id
        ///</summary>
        public int Id { get; set; }
        ///<summary>
        /// 父级ID
        ///</summary>
        public int ParentId { get; set; }
        ///<summary>
        /// 部门名称
        ///</summary>
        public string Name { get; set; }
        ///<summary>
        /// 部门编号
        ///</summary>
        public int Code { get; set; }
        ///<summary>
        /// 部门描述
        ///</summary>
        public string Remark { get; set; }
        ///<summary>
        /// 部门负责人
        ///</summary>
        public string Person { get; set; }
        ///<summary>
        /// 排序
        ///</summary>
        public int Sort { get; set; }
        ///<summary>
        /// 是否启用
        ///</summary>
        public bool Enabled { get; set; }
        ///<summary>
        /// 创建时间
        ///</summary>
        public DateTime CreateTime { get; set; }
        ///<summary>
        /// 修改时间
        ///</summary>
        public DateTime ModifyTime { get; set; }
        /// <summary>
        /// 层级
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// 父级编码
        /// </summary>
        public int ParentCode { get; set; } 
    }
}
