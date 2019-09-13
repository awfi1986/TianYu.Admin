using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TianYu.Admin.Domain.Mapping
{
    public partial class MQBusinessConfigMap
        : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<TianYu.Admin.Domain.MQBusinessConfig>
    {
        public MQBusinessConfigMap()
        {
            // table
            ToTable("MQBusinessConfig", "dbo");

            // keys
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Id)
                .HasColumnName("Id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();
            Property(t => t.BusinessName)
                .HasColumnName("BusinessName")
                .HasMaxLength(128)
                .IsRequired();
            Property(t => t.ApiUrl)
                .HasColumnName("ApiUrl")
                .HasMaxLength(1024)
                .IsRequired();
            Property(t => t.RoutingKey)
                .HasColumnName("RoutingKey")
                .HasMaxLength(128)
                .IsRequired();
            Property(t => t.Exchange)
                .HasColumnName("Exchange")
                .HasMaxLength(128)
                .IsRequired();
            Property(t => t.QueueName)
                .HasColumnName("QueueName")
                .HasMaxLength(128)
                .IsRequired();
            Property(t => t.IsProperties)
                .HasColumnName("IsProperties")
                .IsRequired();
            Property(t => t.MqMessageType)
                .HasColumnName("MqMessageType")
                .IsRequired();
            Property(t => t.TimeInterval)
                .HasColumnName("TimeInterval")
                .IsRequired();
            Property(t => t.ExceptionNumber)
                .HasColumnName("ExceptionNumber")
                .IsRequired();
            Property(t => t.IsSaveFailMessage)
                .HasColumnName("IsSaveFailMessage")
                .IsRequired();
            Property(t => t.Status)
                .HasColumnName("Status")
                .IsRequired();
            Property(t => t.CreateTime)
                .HasColumnName("CreateTime")
                .IsRequired();

            // Relationships
        }
    }
}
