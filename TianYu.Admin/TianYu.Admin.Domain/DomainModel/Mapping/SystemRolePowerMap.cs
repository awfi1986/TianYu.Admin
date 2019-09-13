using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TianYu.Admin.Domain.Mapping
{
    public partial class SystemRolePowerMap
        : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<TianYu.Admin.Domain.SystemRolePower>
    {
        public SystemRolePowerMap()
        {
            // table
            ToTable("s_SystemRolePower", "dbo");

            // keys
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();
            Property(t => t.RoleId)
                .HasColumnName("RoleId")
                .IsRequired();
            Property(t => t.PowerId)
                .HasColumnName("PowerId")
                .IsRequired();

            // Relationships
        }
    }
}
