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
    /// 菜单实体映射类
    /// </summary>
    public partial class SystemMenuMap
        : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<TianYu.Admin.Domain.SystemMenu>
    {
        public SystemMenuMap()
        {
            // table
            ToTable("s_SystemMenu", "dbo");

			// keys
		    HasKey(t => t.Id);
	 
			// Properties
		           Property(t => t.Id)
                   .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                   .HasColumnName("Id")
                   .IsRequired();
           Property(t => t.MenuName)
                   .HasMaxLength(50)
                   .HasColumnName("MenuName")
                   .IsRequired();
           Property(t => t.MenuDesc)
                   .HasMaxLength(512)
                   .HasColumnName("MenuDesc")
                   .IsOptional();
           Property(t => t.MenuIcon)
                   .HasMaxLength(200)
                   .HasColumnName("MenuIcon")
                   .IsOptional();
           Property(t => t.MenuUrl)
                   .HasMaxLength(200)
                   .HasColumnName("MenuUrl")
                   .IsOptional();
           Property(t => t.ParentId)
                   .HasColumnName("ParentId")
                   .IsRequired();
           Property(t => t.Enabled)
                   .HasColumnName("Enabled")
                   .IsRequired();
           Property(t => t.MenuButtonId)
                   .HasMaxLength(200)
                   .HasColumnName("MenuButtonId")
                   .IsOptional();
           Property(t => t.Level)
                   .HasColumnName("Level")
                   .IsOptional();
           Property(t => t.MenuSort)
                   .HasColumnName("MenuSort")
                   .IsRequired();
           Property(t => t.CreateTime)
                   .HasColumnName("CreateTime")
                   .IsRequired();
           Property(t => t.ModifyTime)
                   .HasColumnName("ModifyTime")
                   .IsRequired();
           Property(t => t.MenuCode)
                   .HasColumnName("MenuCode")
                   .IsRequired();
           Property(t => t.ParentCode)
                   .HasColumnName("ParentCode")
                   .IsOptional();
		 	 
        }
    }
}

