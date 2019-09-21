﻿// <autogenerated>
//   This file was generated by T4 code generator Template.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

 
using System; 

namespace TianYu.Admin.Domain.ViewModel.Response
{
    /// <summary>
    /// 数据字典模型
    /// </summary>
    public class SystemDictionaryResponseModel
    {
                ///<summary>
        /// Id
        ///</summary>
        public int Id { get; set; }
        ///<summary>
        /// 父级ID（第一级为0）
        ///</summary>
        public int ParentId { get; set; }
        ///<summary>
        /// 字典编码
        ///</summary>
        public string Code { get; set; }
        ///<summary>
        /// 字典名称
        ///</summary>
        public string Name { get; set; }
        ///<summary>
        /// 字典描述
        ///</summary>
        public string Remark { get; set; }
        ///<summary>
        /// 字典值
        ///</summary>
        public string Value { get; set; }
        ///<summary>
        /// 排序
        ///</summary>
        public int? Sort { get; set; }
        ///<summary>
        /// 创建时间
        ///</summary>
        public DateTime CreateTime { get; set; }
        ///<summary>
        /// 修改时间
        ///</summary>
        public DateTime ModifyTime { get; set; }
    }
    /// <summary>
    /// 数据字典列表查询响应模型
    /// </summary>
    public class QuerySystemDictionaryResponseModel: SystemDictionaryResponseModel
    { 
    }
    /// <summary>
    /// 数据字典详情查询响应模型
    /// </summary>
    public class QueryDetailSystemDictionaryResponseModel: SystemDictionaryResponseModel
    {  
    }
}

