﻿// <autogenerated>
//   This file was generated by T4 code generator Template.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

 
using System;
using TianYu.Admin.Domain.BaseModel; 

namespace TianYu.Admin.Domain 
{    
    /// <summary> 
	/// 实体-系统账号 
	/// </summary> 
	public partial class SystemStaff: AggregateRoot    
    {    
	    public SystemStaff()
        {
                }

                ///<summary>
        /// Id
        ///</summary>
        public int Id { get; set; }
        ///<summary>
        /// 登录名称
        ///</summary>
        public string LoginName { get; set; }
        ///<summary>
        /// 登录密码
        ///</summary>
        public string LoginPwd { get; set; }
        ///<summary>
        /// 盐值
        ///</summary>
        public string MaskCode { get; set; }
        ///<summary>
        /// 手机
        ///</summary>
        public string Mobile { get; set; }
        ///<summary>
        /// 电话
        ///</summary>
        public string Tel { get; set; }
        ///<summary>
        /// 邮箱
        ///</summary>
        public string Eamil { get; set; }
        ///<summary>
        /// 呢称
        ///</summary>
        public string NickName { get; set; }
        ///<summary>
        /// 状态：0有效，1禁用
        ///</summary>
        public int Status { get; set; }
        ///<summary>
        /// 所属部门
        ///</summary>
        public string SectionId { get; set; }
        ///<summary>
        /// 最近登录时间
        ///</summary>
        public DateTime? LastLoginTime { get; set; }
        ///<summary>
        /// 创建时间
        ///</summary>
        public DateTime CreateTime { get; set; }
        ///<summary>
        /// 修改时间
        ///</summary>
        public DateTime ModifyTime { get; set; }
    }
}
