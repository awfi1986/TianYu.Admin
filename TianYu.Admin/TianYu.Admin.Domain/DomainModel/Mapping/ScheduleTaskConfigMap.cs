using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TianYu.Admin.Domain.Mapping
{
    public partial class ScheduleTaskConfigMap
        : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<TianYu.Admin.Domain.ScheduleTaskConfig>
    {
        public ScheduleTaskConfigMap()
        {
            // table
            ToTable("ScheduleTaskConfig", "dbo");

            // keys
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();
            Property(t => t.TaskName)
                .HasColumnName("TaskName")
                .HasMaxLength(128)
                .IsRequired();
            Property(t => t.ApiUrl)
                .HasColumnName("ApiUrl")
                .HasMaxLength(512)
                .IsRequired();
            Property(t => t.Parameters)
                .HasColumnName("Parameters")
                .HasMaxLength(512)
                .IsRequired();
            Property(t => t.Method)
                .HasColumnName("Method")
                .HasMaxLength(10)
                .IsRequired();
            Property(t => t.ExecuteType)
                .HasColumnName("ExecuteType")
                .IsRequired();
            Property(t => t.ExceptionNumber)
                .HasColumnName("ExceptionNumber")
                .IsRequired();
            Property(t => t.DiffSeconds)
                .HasColumnName("DiffSeconds")
                .IsRequired();
            Property(t => t.RunStatus)
                .HasColumnName("RunStatus")
                .IsRequired();
            Property(t => t.Status)
                .HasColumnName("Status")
                .IsRequired();
            Property(t => t.LastRunTime)
                .HasColumnName("LastRunTime")
                .IsOptional();
            Property(t => t.CreateTime)
                .HasColumnName("CreateTime")
                .IsRequired();

            // Relationships
        }
    }
}
