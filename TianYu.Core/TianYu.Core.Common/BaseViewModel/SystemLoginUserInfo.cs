using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianYu.Core.Common.BaseViewModel
{
    /// <summary>
    /// 系统登录用户信息
    /// </summary>
    public class SystemLoginUserInfo
    {
        ///<summary>
        /// Id
        ///</summary>
        public int Id { get; set; }
        ///<summary>
        /// 登录名称
        ///</summary>
        public string LoginName { get; set; }
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
    }
}
