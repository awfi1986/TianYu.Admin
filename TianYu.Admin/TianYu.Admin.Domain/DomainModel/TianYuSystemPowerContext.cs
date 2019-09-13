using System;
using System.Data.Entity;
using System.Collections.Generic;
using TianYu.Admin.Domain;
using TianYu.Admin.Domain.Mapping;

namespace TianYu.Admin.Domain
{
    public partial class TianYuSystemPowerContext : DbContext
    {
        static TianYuSystemPowerContext()
        {
            Database.SetInitializer<TianYuSystemPowerContext>(null);
        }
        public TianYuSystemPowerContext()
            : base("Name=TianYuSystemPowerContext")
        { }

        public TianYuSystemPowerContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        { }

        public virtual DbSet<SystemStaff> SystemStaffs { get; set; }
        public virtual DbSet<SystemStaffRole> SystemStaffRoles { get; set; }
        //public virtual DbSet<SystemPowerItem> SystemPowerItems { get; set; }
        public virtual DbSet<SystemRole> SystemRoles { get; set; }
        //public virtual DbSet<SystemRolePower> SystemRolePowers { get; set; } 
        public virtual DbSet<SystemSection> SystemSections { get; set; }
        //public virtual DbSet<DictionaryItem> DictionaryItems { get; set; }
        //public virtual DbSet<DictionaryType> DictionaryTypes { get; set; }
        //public virtual DbSet<MQBusinessConfig> MQBusinessConfigs { get; set; }
        //public virtual DbSet<ScheduleTaskConfig> ScheduleTaskConfigs { get; set; }
        public virtual DbSet<SystemRoleRules> SystemRoleRules { get; set; }
        public virtual DbSet<SystemMenu> SystemMenus { get; set; }
        public virtual DbSet<SystemActionButton> SystemActionButtons { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new SystemStaffMap());
            modelBuilder.Configurations.Add(new SystemStaffRoleMap());
            //modelBuilder.Configurations.Add(new SystemPowerItemMap());
            modelBuilder.Configurations.Add(new SystemRoleMap());
            //modelBuilder.Configurations.Add(new SystemRolePowerMap());
            modelBuilder.Configurations.Add(new SystemSectionMap());
            //modelBuilder.Configurations.Add(new DictionaryItemMap());
            //modelBuilder.Configurations.Add(new DictionaryTypeMap());
            //modelBuilder.Configurations.Add(new MQBusinessConfigMap());
            //modelBuilder.Configurations.Add(new ScheduleTaskConfigMap());
            modelBuilder.Configurations.Add(new SystemRoleRulesMap());
            modelBuilder.Configurations.Add(new SystemMenuMap());
            modelBuilder.Configurations.Add(new SystemActionButtonMap());
        }
    }
}