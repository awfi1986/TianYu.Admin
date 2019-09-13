﻿// <autogenerated>
//   This file was generated by T4 code generator Template.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

 
using System; 

namespace TianYu.Admin.Domain.ViewModel.Response
{
    /// <summary>
    /// 按键模型
    /// </summary>
    public class SystemActionButtonResponseModel
    {
                ///<summary>
        /// Id
        ///</summary>
        public int Id { get; set; }
        ///<summary>
        /// 按键名称
        ///</summary>
        public string ButtonName { get; set; }
        ///<summary>
        /// 按键代码
        ///</summary>
        public string ButtonCode { get; set; }
        ///<summary>
        /// 按键描述
        ///</summary>
        public string ButtonDesc { get; set; }
        ///<summary>
        /// 是否启用
        ///</summary>
        public bool Enabled { get; set; }
        ///<summary>
        /// 排序
        ///</summary>
        public int Sort { get; set; }
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
    /// 按键列表查询响应模型
    /// </summary>
    public class QuerySystemActionButtonResponseModel: SystemActionButtonResponseModel
    { 
    }
    /// <summary>
    /// 按键详情查询响应模型
    /// </summary>
    public class QueryDetailSystemActionButtonResponseModel: SystemActionButtonResponseModel
    {  
    }
}

