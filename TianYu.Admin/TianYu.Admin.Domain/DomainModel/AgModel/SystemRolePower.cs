using TianYu.Admin.Domain.BaseModel;

namespace TianYu.Admin.Domain
{
    /// <summary>
    /// 角色权限表
    /// </summary>
    public partial class SystemRolePower: AggregateRoot
    {
        public SystemRolePower()
        {
            this.Id= 0;
            this.RoleId= 0;
            this.PowerId= 0;
        }

        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 后台角色Id
        /// </summary>
        public int RoleId { get; set; }
        /// <summary>
        /// 后台权限Id
        /// </summary>
        public int PowerId { get; set; }

    }
}