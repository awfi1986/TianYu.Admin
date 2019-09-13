using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianYu.Admin.Domain.ViewModel
{
    /// <summary>
    /// 
    /// </summary>
    public class SystemRoleViewModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 用户账号Id
        /// </summary>
        public int StaffId{get;set;}
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }
    }
}
