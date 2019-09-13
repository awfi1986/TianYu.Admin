using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TianYu.Admin.Domain.Mapping
{
    public partial class DictionaryItemMap
        : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<TianYu.Admin.Domain.DictionaryItem>
    {
        public DictionaryItemMap()
        {
            // table
            ToTable("DictionaryItem", "dbo");

            // keys
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();
            Property(t => t.DicTypeCode)
                .HasColumnName("DicTypeCode")
                .HasMaxLength(10)
                .IsOptional();
            Property(t => t.DicCode)
                .HasColumnName("DicCode")
                .HasMaxLength(16)
                .IsOptional();
            Property(t => t.DicName)
                .HasColumnName("DicName")
                .HasMaxLength(512)
                .IsOptional();
            Property(t => t.DicParentCode)
                .HasColumnName("DicParentCode")
                .HasMaxLength(16)
                .IsOptional();
            Property(t => t.Remarks)
                .HasColumnName("Remarks")
                .IsOptional();
            Property(t => t.CreateTime)
                .HasColumnName("CreateTime")
                .IsOptional();
            Property(t => t.UpdateTime)
                .HasColumnName("UpdateTime")
                .IsOptional();
            Property(t => t.IsValid)
                .HasColumnName("IsValid")
                .IsRequired();

            // Relationships
        }
    }
}
