using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TianYu.Admin.Domain.Mapping
{
    public partial class SystemPowerItemMap
        : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<TianYu.Admin.Domain.SystemPowerItem>
    {
        public SystemPowerItemMap()
        {
            // table
            ToTable("s_SystemPowerItem", "dbo");

            // keys
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();
            Property(t => t.UpdateTime)
                .HasColumnName("UpdateTime")
                .IsRequired();
            Property(t => t.ParentCode)
                .HasColumnName("ParentCode")
                .HasMaxLength(30)
                .IsRequired();
            Property(t => t.PowerCode)
                .HasColumnName("PowerCode")
                .HasMaxLength(30)
                .IsRequired();
            Property(t => t.PowerName)
                .HasColumnName("PowerName")
                .HasMaxLength(32)
                .IsRequired();
            Property(t => t.IconCode)
                .HasColumnName("IconCode")
                .HasMaxLength(64)
                .IsOptional();
            Property(t => t.BussionModule)
                .HasColumnName("BussionModule")
                .HasMaxLength(64)
                .IsRequired();
            Property(t => t.ActionUrl)
                .HasColumnName("ActionUrl")
                .HasMaxLength(256);
            Property(t => t.SortCode)
                .HasColumnName("SortCode")
                .IsOptional();
            Property(t => t.Remark)
                .HasColumnName("Remark")
                .HasMaxLength(256)
                .IsOptional();
            Property(t => t.Status)
                .HasColumnName("Status")
                .IsRequired();
            Property(t => t.CreateTime)
                .HasColumnName("CreateTime")
                .IsRequired();
            Property(t => t.Level)
                .HasColumnName("Level")
                .IsOptional();
            Property(t => t.SystemName)
              .HasColumnName("SystemName")
               .HasMaxLength(100)
              .IsOptional();
            // Relationships
        }
    }
}
