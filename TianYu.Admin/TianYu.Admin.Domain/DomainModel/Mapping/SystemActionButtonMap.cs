﻿// <autogenerated>
//   This file was generated by T4 code generator Template.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

 
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TianYu.Admin.Domain.Mapping
{
    /// <summary>
    /// 按键实体映射类
    /// </summary>
    public partial class SystemActionButtonMap
        : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<TianYu.Admin.Domain.SystemActionButton>
    {
        public SystemActionButtonMap()
        {
            // table
            ToTable("s_SystemActionButton", "dbo");

			// keys
		    HasKey(t => t.Id);
	 
			// Properties
		           Property(t => t.Id)
                   .HasColumnName("Id")
                   .IsRequired();
           Property(t => t.ButtonName)
                   .HasMaxLength(50)
                   .HasColumnName("ButtonName")
                   .IsRequired();
           Property(t => t.ButtonCode)
                   .HasMaxLength(50)
                   .HasColumnName("ButtonCode")
                   .IsRequired();
           Property(t => t.ButtonDesc)
                   .HasMaxLength(1000)
                   .HasColumnName("ButtonDesc")
                   .IsOptional();
           Property(t => t.Enabled)
                   .HasColumnName("Enabled")
                   .IsRequired();
           Property(t => t.Sort)
                   .HasColumnName("Sort")
                   .IsRequired();
           Property(t => t.CreateTime)
                   .HasColumnName("CreateTime")
                   .IsRequired();
           Property(t => t.ModifyTime)
                   .HasColumnName("ModifyTime")
                   .IsRequired();
		 	 
        }
    }
}

