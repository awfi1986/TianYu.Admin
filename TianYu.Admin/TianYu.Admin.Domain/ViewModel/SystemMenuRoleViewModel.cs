using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianYu.Admin.Domain.ViewModel
{
    public class SystemMenuRoleViewModel
    {
        ///<summary>
        /// Id
        ///</summary>
        public int Id { get; set; }
        ///<summary>
        /// 菜单名称
        ///</summary>
        public string MenuName { get; set; }
        ///<summary>
        /// 菜单图标
        ///</summary>
        public string MenuIcon { get; set; }
        ///<summary>
        /// 菜单Url
        ///</summary>
        public string MenuUrl { get; set; }
        ///<summary>
        /// 父级ID(-1为顶级)
        ///</summary>
        public int ParentId { get; set; }
        ///<summary>
        /// 菜单按钮
        ///</summary>
        public string MenuButtonId { get; set; }
        ///<summary>
        /// 层级
        ///</summary>
        public int? Level { get; set; }
        ///<summary>
        /// 排序
        ///</summary>
        public int MenuSort { get; set; }
        ///<summary>
        /// Id
        ///</summary>
        public int? ButtonId { get; set; }
        ///<summary>
        /// 按键名称
        ///</summary>
        public string ButtonName { get; set; }
        ///<summary>
        /// 按键代码
        ///</summary>
        public string ButtonCode { get; set; }
    }
}
