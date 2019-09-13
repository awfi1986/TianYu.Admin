﻿// <autogenerated>
//   This file was generated by T4 code generator Template.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>


using System;
using TianYu.Core.Common.BaseViewModel;

namespace TianYu.Admin.Domain.ViewModel.Request
{
    /// <summary>
    /// 按键模型
    /// </summary>
    public class SystemActionButtonRequestModel
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
    /// 添加按键请求模型
    /// </summary>
    public class AddSystemActionButtonRequestModel: SystemActionButtonRequestModel
    {

    }
    /// <summary>
    /// 修改按键请求模型
    /// </summary>
    public class UpdateSystemActionButtonRequestModel: SystemActionButtonRequestModel
    {
        ///<summary>
        /// Id
        ///</summary>
        public int Id { get; set; }
    }
    /// <summary>
    /// 删除按键请求模型
    /// </summary>
    public class RemoveSystemActionButtonRequestModel
    {
        ///<summary>
        /// Id
        ///</summary>
        public int Id { get; set; }
    }
    /// <summary>
    /// 查询按键列表请求模型
    /// </summary>
    public class QuerySystemActionButtonRequestModel : ApiBaseRequestModel
    {

    }
    /// <summary>
    /// 查询按键详情请求模型
    /// </summary>
    public class QueryDetailSystemActionButtonRequestModel :ApiBaseRequestModel
    {

    }
}

