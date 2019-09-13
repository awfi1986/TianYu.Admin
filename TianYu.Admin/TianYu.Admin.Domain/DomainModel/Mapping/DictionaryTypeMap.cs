using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TianYu.Admin.Domain.Mapping
{
    public partial class DictionaryTypeMap
        : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<TianYu.Admin.Domain.DictionaryType>
    {
        public DictionaryTypeMap()
        {
            // table
            ToTable("DictionaryType", "dbo");

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
                .IsRequired();
            Property(t => t.DicTypeName)
                .HasColumnName("DicTypeName")
                .HasMaxLength(512)
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
