USE [TianYu_DB]
GO
/****** Object:  Table [dbo].[s_SystemActionButton]    Script Date: 2019/9/14 21:59:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[s_SystemActionButton](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ButtonName] [nvarchar](50) NOT NULL,
	[ButtonCode] [nvarchar](50) NOT NULL,
	[ButtonDesc] [nvarchar](1000) NULL,
	[Enabled] [bit] NOT NULL,
	[Sort] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[ModifyTime] [datetime] NOT NULL,
 CONSTRAINT [PK_S_SYSTEMACTIONBUTTON] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[s_SystemMenu]    Script Date: 2019/9/14 21:59:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[s_SystemMenu](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MenuName] [nvarchar](50) NOT NULL,
	[MenuDesc] [nvarchar](512) NULL,
	[MenuIcon] [nvarchar](200) NULL,
	[MenuUrl] [nvarchar](200) NULL,
	[ParentId] [int] NOT NULL,
	[Enabled] [bit] NOT NULL,
	[MenuButtonId] [varchar](200) NULL,
	[Level] [int] NULL,
	[MenuSort] [int] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[ModifyTime] [datetime] NOT NULL,
	[MenuCode] [int] NOT NULL,
	[ParentCode] [int] NULL,
 CONSTRAINT [PK_S_SYSTEMMENU] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[s_SystemRole]    Script Date: 2019/9/14 21:59:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[s_SystemRole](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](50) NOT NULL,
	[RoleCode] [nvarchar](20) NULL,
	[RoleDesc] [nvarchar](50) NULL,
	[Sort] [int] NOT NULL,
	[Enabled] [bit] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[ModifyTime] [datetime] NOT NULL,
 CONSTRAINT [PK_S_SYSTEMROLE] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[s_SystemRoleRules]    Script Date: 2019/9/14 21:59:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[s_SystemRoleRules](
    [Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [int] NOT NULL,
	[MenuId] [int] NOT NULL,
	[ButtonId] [int] NOT NULL, 
 CONSTRAINT [PK_s_SystemRoleRules] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[s_SystemSection]    Script Date: 2019/9/14 21:59:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[s_SystemSection](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ParentId] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[ParentCode] [int] NOT NULL,
	[Code] [int] NOT NULL,
	[Remark] [nvarchar](50) NULL,
	[Person] [nvarchar](50) NULL,
	[Sort] [int] NOT NULL,
	[Enabled] [bit] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[ModifyTime] [datetime] NOT NULL,
	[Level] [int] NULL,
 CONSTRAINT [PK_S_SYSTEMSECTION] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[s_SystemStaff]    Script Date: 2019/9/14 21:59:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[s_SystemStaff](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LoginName] [nvarchar](50) NOT NULL,
	[LoginPwd] [varchar](32) NOT NULL,
	[MaskCode] [varchar](32) NOT NULL,
	[Mobile] [nvarchar](20) NULL,
	[Tel] [nvarchar](20) NULL,
	[Eamil] [nvarchar](50) NULL,
	[NickName] [nvarchar](50) NULL,
	[Status] [int] NOT NULL,
	[SectionId] [varchar](20) NULL,
	[LastLoginTime] [datetime] NULL,
	[CreateTime] [datetime] NOT NULL,
	[ModifyTime] [datetime] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[s_SystemStaffRole]    Script Date: 2019/9/14 21:59:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[s_SystemStaffRole](
    [Id] [int] IDENTITY(1,1) NOT NULL,
	[StaffId] [int] NOT NULL,
	[RoleId] [int] NOT NULL, 
 CONSTRAINT [PK_s_SystemStaffRole] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[s_SystemActionButton] ON 

INSERT [dbo].[s_SystemActionButton] ([Id], [ButtonName], [ButtonCode], [ButtonDesc], [Enabled], [Sort], [CreateTime], [ModifyTime]) VALUES (1, N'查询', N'Query', N'列表查询', 1, 1, CAST(0x0000AA9200000000 AS DateTime), CAST(0x0000AA9200000000 AS DateTime))
INSERT [dbo].[s_SystemActionButton] ([Id], [ButtonName], [ButtonCode], [ButtonDesc], [Enabled], [Sort], [CreateTime], [ModifyTime]) VALUES (2, N'添加', N'Insert', NULL, 1, 2, CAST(0x0000AA9200000000 AS DateTime), CAST(0x0000AA9200000000 AS DateTime))
INSERT [dbo].[s_SystemActionButton] ([Id], [ButtonName], [ButtonCode], [ButtonDesc], [Enabled], [Sort], [CreateTime], [ModifyTime]) VALUES (3, N'修改', N'Update', NULL, 1, 3, CAST(0x0000AA9200000000 AS DateTime), CAST(0x0000AA9200000000 AS DateTime))
INSERT [dbo].[s_SystemActionButton] ([Id], [ButtonName], [ButtonCode], [ButtonDesc], [Enabled], [Sort], [CreateTime], [ModifyTime]) VALUES (4, N'删除', N'Remove', NULL, 1, 4, CAST(0x0000AA9200000000 AS DateTime), CAST(0x0000AA9200000000 AS DateTime))
INSERT [dbo].[s_SystemActionButton] ([Id], [ButtonName], [ButtonCode], [ButtonDesc], [Enabled], [Sort], [CreateTime], [ModifyTime]) VALUES (5, N'审核', N'Audit', NULL, 1, 5, CAST(0x0000AA9200000000 AS DateTime), CAST(0x0000AA9200000000 AS DateTime))
INSERT [dbo].[s_SystemActionButton] ([Id], [ButtonName], [ButtonCode], [ButtonDesc], [Enabled], [Sort], [CreateTime], [ModifyTime]) VALUES (6, N'打印', N'Print', NULL, 1, 6, CAST(0x0000AA9200000000 AS DateTime), CAST(0x0000AA9200000000 AS DateTime))
INSERT [dbo].[s_SystemActionButton] ([Id], [ButtonName], [ButtonCode], [ButtonDesc], [Enabled], [Sort], [CreateTime], [ModifyTime]) VALUES (7, N'导出', N'Export', NULL, 1, 7, CAST(0x0000AA9200000000 AS DateTime), CAST(0x0000AA9200000000 AS DateTime))
INSERT [dbo].[s_SystemActionButton] ([Id], [ButtonName], [ButtonCode], [ButtonDesc], [Enabled], [Sort], [CreateTime], [ModifyTime]) VALUES (8, N'设置权限', N'SetPower', NULL, 1, 8, CAST(0x0000AAB400000000 AS DateTime), CAST(0x0000AAB400000000 AS DateTime))
SET IDENTITY_INSERT [dbo].[s_SystemActionButton] OFF
SET IDENTITY_INSERT [dbo].[s_SystemMenu] ON 

INSERT [dbo].[s_SystemMenu] ([Id], [MenuName], [MenuDesc], [MenuIcon], [MenuUrl], [ParentId], [Enabled], [MenuButtonId], [Level], [MenuSort], [CreateTime], [ModifyTime], [MenuCode], [ParentCode]) VALUES (1, N'系统设置', NULL, NULL, NULL, -1, 0, NULL, 1, 3, CAST(0x0000AA9400000000 AS DateTime), CAST(0x0000AABA0148938F AS DateTime), 1001, 0)
INSERT [dbo].[s_SystemMenu] ([Id], [MenuName], [MenuDesc], [MenuIcon], [MenuUrl], [ParentId], [Enabled], [MenuButtonId], [Level], [MenuSort], [CreateTime], [ModifyTime], [MenuCode], [ParentCode]) VALUES (2, N'菜单管理', N'菜单管理', NULL, N'', 1, 0, N'', 2, 4, CAST(0x0000AA9400000000 AS DateTime), CAST(0x0000AA9400000000 AS DateTime), 100101, 1001)
INSERT [dbo].[s_SystemMenu] ([Id], [MenuName], [MenuDesc], [MenuIcon], [MenuUrl], [ParentId], [Enabled], [MenuButtonId], [Level], [MenuSort], [CreateTime], [ModifyTime], [MenuCode], [ParentCode]) VALUES (3, N'菜单列表', NULL, NULL, N'/SystemMenu/Index', 2, 0, N'1,2,3,4', 3, 5, CAST(0x0000AA9400000000 AS DateTime), CAST(0x0000AAB10152B004 AS DateTime), 10010101, 100101)
INSERT [dbo].[s_SystemMenu] ([Id], [MenuName], [MenuDesc], [MenuIcon], [MenuUrl], [ParentId], [Enabled], [MenuButtonId], [Level], [MenuSort], [CreateTime], [ModifyTime], [MenuCode], [ParentCode]) VALUES (4, N'员工管理', NULL, NULL, NULL, 1, 0, NULL, 2, 2, CAST(0x0000AA9400000000 AS DateTime), CAST(0x0000AABA00C70AB7 AS DateTime), 100102, 1001)
INSERT [dbo].[s_SystemMenu] ([Id], [MenuName], [MenuDesc], [MenuIcon], [MenuUrl], [ParentId], [Enabled], [MenuButtonId], [Level], [MenuSort], [CreateTime], [ModifyTime], [MenuCode], [ParentCode]) VALUES (5, N'员工列表', NULL, NULL, N'/SystemStaff/Index', 4, 0, N'1,2,3,4', 3, 3, CAST(0x0000AA9400000000 AS DateTime), CAST(0x0000AAB901731580 AS DateTime), 10010201, 100102)
INSERT [dbo].[s_SystemMenu] ([Id], [MenuName], [MenuDesc], [MenuIcon], [MenuUrl], [ParentId], [Enabled], [MenuButtonId], [Level], [MenuSort], [CreateTime], [ModifyTime], [MenuCode], [ParentCode]) VALUES (6, N'角色管理', NULL, NULL, NULL, 1, 0, NULL, 2, 3, CAST(0x0000AA9F010792CB AS DateTime), CAST(0x0000AABA00C702A6 AS DateTime), 100103, 1001)
INSERT [dbo].[s_SystemMenu] ([Id], [MenuName], [MenuDesc], [MenuIcon], [MenuUrl], [ParentId], [Enabled], [MenuButtonId], [Level], [MenuSort], [CreateTime], [ModifyTime], [MenuCode], [ParentCode]) VALUES (7, N'角色列表', NULL, NULL, N'/SystemRole/Index', 6, 0, N'1,2,3,4,8', 3, 1, CAST(0x0000AA9F0115067E AS DateTime), CAST(0x0000AAB9017326BC AS DateTime), 10010301, 100103)
INSERT [dbo].[s_SystemMenu] ([Id], [MenuName], [MenuDesc], [MenuIcon], [MenuUrl], [ParentId], [Enabled], [MenuButtonId], [Level], [MenuSort], [CreateTime], [ModifyTime], [MenuCode], [ParentCode]) VALUES (8, N'部门管理', NULL, NULL, NULL, 1, 0, NULL, 2, 1, CAST(0x0000AAA001719A58 AS DateTime), CAST(0x0000AABA00C6D653 AS DateTime), 100104, 1001)
INSERT [dbo].[s_SystemMenu] ([Id], [MenuName], [MenuDesc], [MenuIcon], [MenuUrl], [ParentId], [Enabled], [MenuButtonId], [Level], [MenuSort], [CreateTime], [ModifyTime], [MenuCode], [ParentCode]) VALUES (9, N'部门列表', NULL, NULL, N'/SystemSection/Index', 8, 0, N'1,2,3,4', 3, 1, CAST(0x0000AAA00171F4DF AS DateTime), CAST(0x0000AABA00C6F0B8 AS DateTime), 10010401, 100104)
SET IDENTITY_INSERT [dbo].[s_SystemMenu] OFF
SET IDENTITY_INSERT [dbo].[s_SystemRole] ON 

INSERT [dbo].[s_SystemRole] ([Id], [RoleName], [RoleCode], [RoleDesc], [Sort], [Enabled], [CreateTime], [ModifyTime]) VALUES (1, N'超级管理员', N'Admin', N'超级管理员', 1, 1, CAST(0x0000AA9400000000 AS DateTime), CAST(0x0000AA9400000000 AS DateTime))
SET IDENTITY_INSERT [dbo].[s_SystemRole] OFF
SET IDENTITY_INSERT [dbo].[s_SystemRoleRules] ON 

INSERT [dbo].[s_SystemRoleRules] ([RoleId], [MenuId], [ButtonId], [Id]) VALUES (1, 1, 0, 1)
INSERT [dbo].[s_SystemRoleRules] ([RoleId], [MenuId], [ButtonId], [Id]) VALUES (1, 2, 1, 2)
INSERT [dbo].[s_SystemRoleRules] ([RoleId], [MenuId], [ButtonId], [Id]) VALUES (1, 3, 1, 3)
INSERT [dbo].[s_SystemRoleRules] ([RoleId], [MenuId], [ButtonId], [Id]) VALUES (1, 3, 2, 4)
INSERT [dbo].[s_SystemRoleRules] ([RoleId], [MenuId], [ButtonId], [Id]) VALUES (1, 3, 3, 5)
INSERT [dbo].[s_SystemRoleRules] ([RoleId], [MenuId], [ButtonId], [Id]) VALUES (1, 4, 0, 6)
INSERT [dbo].[s_SystemRoleRules] ([RoleId], [MenuId], [ButtonId], [Id]) VALUES (1, 5, 1, 7)
INSERT [dbo].[s_SystemRoleRules] ([RoleId], [MenuId], [ButtonId], [Id]) VALUES (1, 5, 2, 8)
INSERT [dbo].[s_SystemRoleRules] ([RoleId], [MenuId], [ButtonId], [Id]) VALUES (1, 5, 3, 9) 
INSERT [dbo].[s_SystemRoleRules] ([RoleId], [MenuId], [ButtonId], [Id]) VALUES (1, 6, 0, 10)
INSERT [dbo].[s_SystemRoleRules] ([RoleId], [MenuId], [ButtonId], [Id]) VALUES (1, 7, 1, 11) 
INSERT [dbo].[s_SystemRoleRules] ([RoleId], [MenuId], [ButtonId], [Id]) VALUES (1, 7, 2, 12) 
INSERT [dbo].[s_SystemRoleRules] ([RoleId], [MenuId], [ButtonId], [Id]) VALUES (1, 7, 3, 13) 
INSERT [dbo].[s_SystemRoleRules] ([RoleId], [MenuId], [ButtonId], [Id]) VALUES (1, 7, 4, 14) 
INSERT [dbo].[s_SystemRoleRules] ([RoleId], [MenuId], [ButtonId], [Id]) VALUES (1, 7, 8, 15)
INSERT [dbo].[s_SystemRoleRules] ([RoleId], [MenuId], [ButtonId], [Id]) VALUES (1, 8, 1, 16) 
INSERT [dbo].[s_SystemRoleRules] ([RoleId], [MenuId], [ButtonId], [Id]) VALUES (1, 9, 1, 17)  
INSERT [dbo].[s_SystemRoleRules] ([RoleId], [MenuId], [ButtonId], [Id]) VALUES (1, 9, 2, 18) 
SET IDENTITY_INSERT [dbo].[s_SystemRoleRules] OFF
SET IDENTITY_INSERT [dbo].[s_SystemSection] ON 

INSERT [dbo].[s_SystemSection] ([Id], [ParentId], [Name], [ParentCode], [Code], [Remark], [Person], [Sort], [Enabled], [CreateTime], [ModifyTime], [Level]) VALUES (1, 0, N'开发部', 0, 1001, N'443', NULL, 0, 1, CAST(0x0000AAB4011B8690 AS DateTime), CAST(0x0000AAB500F5E9F6 AS DateTime), 1)
INSERT [dbo].[s_SystemSection] ([Id], [ParentId], [Name], [ParentCode], [Code], [Remark], [Person], [Sort], [Enabled], [CreateTime], [ModifyTime], [Level]) VALUES (2, 1, N'.NET开发组', 1001, 100101, N'圾玩儿', NULL, 0, 1, CAST(0x0000AAB401220D2C AS DateTime), CAST(0x0000AAB500F5DD31 AS DateTime), 2)
INSERT [dbo].[s_SystemSection] ([Id], [ParentId], [Name], [ParentCode], [Code], [Remark], [Person], [Sort], [Enabled], [CreateTime], [ModifyTime], [Level]) VALUES (3, 1, N'IOS开发组', 1001, 100102, N'IOSIOSIOS', NULL, 0, 1, CAST(0x0000AAB401228CDB AS DateTime), CAST(0x0000AAB600BEF96B AS DateTime), 2) 
SET IDENTITY_INSERT [dbo].[s_SystemSection] OFF
SET IDENTITY_INSERT [dbo].[s_SystemStaff] ON 

INSERT [dbo].[s_SystemStaff] ([Id], [LoginName], [LoginPwd], [MaskCode], [Mobile], [Tel], [Eamil], [NickName], [Status], [SectionId], [LastLoginTime], [CreateTime], [ModifyTime]) VALUES (1, N'Admin', N'8bdc698c01e3fc4ab200273021ed7c8b', N'sg345er345e345wse3445', N'13512341234', NULL, NULL, N'李四', 1, N'1', CAST(0x0000AAC80168FB89 AS DateTime), CAST(0x0000AA9400000000 AS DateTime), CAST(0x0000AAB900E86E3C AS DateTime)) 
SET IDENTITY_INSERT [dbo].[s_SystemStaff] OFF
SET IDENTITY_INSERT [dbo].[s_SystemStaffRole] ON 

INSERT [dbo].[s_SystemStaffRole] ([StaffId], [RoleId], [Id]) VALUES (1, 1, 1) 
SET IDENTITY_INSERT [dbo].[s_SystemStaffRole] OFF
/****** Object:  Index [PK_S_SYSTEMACCOUNT]    Script Date: 2019/9/14 21:59:10 ******/
ALTER TABLE [dbo].[s_SystemStaff] ADD  CONSTRAINT [PK_S_SYSTEMACCOUNT] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[s_SystemActionButton] ADD  DEFAULT ((1)) FOR [Enabled]
GO
ALTER TABLE [dbo].[s_SystemMenu] ADD  CONSTRAINT [DF__s_SystemM__Enabl__412EB0B6]  DEFAULT ((1)) FOR [Enabled]
GO
ALTER TABLE [dbo].[s_SystemRole] ADD  CONSTRAINT [DF__s_SystemR__Enabl__534D60F1]  DEFAULT ((1)) FOR [Enabled]
GO
ALTER TABLE [dbo].[s_SystemSection] ADD  CONSTRAINT [DF__s_SystemS__Enabl__3B75D760]  DEFAULT ((1)) FOR [Enabled]
GO
ALTER TABLE [dbo].[s_SystemStaff] ADD  DEFAULT ((0)) FOR [Status]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N's_SystemActionButton', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'按键名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N's_SystemActionButton', @level2type=N'COLUMN',@level2name=N'ButtonName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'按键代码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N's_SystemActionButton', @level2type=N'COLUMN',@level2name=N'ButtonCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'按键描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N's_SystemActionButton', @level2type=N'COLUMN',@level2name=N'ButtonDesc'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否启用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N's_SystemActionButton', @level2type=N'COLUMN',@level2name=N'Enabled'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N's_SystemActionButton', @level2type=N'COLUMN',@level2name=N'Sort'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N's_SystemActionButton', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N's_SystemActionButton', @level2type=N'COLUMN',@level2name=N'ModifyTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'按键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N's_SystemActionButton'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N's_SystemMenu', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N's_SystemMenu', @level2type=N'COLUMN',@level2name=N'MenuName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N's_SystemMenu', @level2type=N'COLUMN',@level2name=N'MenuDesc'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单图标' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N's_SystemMenu', @level2type=N'COLUMN',@level2name=N'MenuIcon'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单Url' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N's_SystemMenu', @level2type=N'COLUMN',@level2name=N'MenuUrl'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'父级ID(-1为顶级)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N's_SystemMenu', @level2type=N'COLUMN',@level2name=N'ParentId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否启用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N's_SystemMenu', @level2type=N'COLUMN',@level2name=N'Enabled'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单按钮' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N's_SystemMenu', @level2type=N'COLUMN',@level2name=N'MenuButtonId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'层级' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N's_SystemMenu', @level2type=N'COLUMN',@level2name=N'Level'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N's_SystemMenu', @level2type=N'COLUMN',@level2name=N'MenuSort'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N's_SystemMenu', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N's_SystemMenu', @level2type=N'COLUMN',@level2name=N'ModifyTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N's_SystemMenu', @level2type=N'COLUMN',@level2name=N'MenuCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'父级编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N's_SystemMenu', @level2type=N'COLUMN',@level2name=N'ParentCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N's_SystemMenu'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N's_SystemRole', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N's_SystemRole', @level2type=N'COLUMN',@level2name=N'RoleName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N's_SystemRole', @level2type=N'COLUMN',@level2name=N'RoleCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N's_SystemRole', @level2type=N'COLUMN',@level2name=N'RoleDesc'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N's_SystemRole', @level2type=N'COLUMN',@level2name=N'Sort'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否启用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N's_SystemRole', @level2type=N'COLUMN',@level2name=N'Enabled'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N's_SystemRole', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N's_SystemRole', @level2type=N'COLUMN',@level2name=N'ModifyTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N's_SystemRole'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N's_SystemRoleRules', @level2type=N'COLUMN',@level2name=N'RoleId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'菜单ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N's_SystemRoleRules', @level2type=N'COLUMN',@level2name=N'MenuId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'按键ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N's_SystemRoleRules', @level2type=N'COLUMN',@level2name=N'ButtonId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色权限' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N's_SystemRoleRules'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N's_SystemSection', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'父级ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N's_SystemSection', @level2type=N'COLUMN',@level2name=N'ParentId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'部门名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N's_SystemSection', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'部门编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N's_SystemSection', @level2type=N'COLUMN',@level2name=N'Code'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'部门描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N's_SystemSection', @level2type=N'COLUMN',@level2name=N'Remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'部门负责人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N's_SystemSection', @level2type=N'COLUMN',@level2name=N'Person'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N's_SystemSection', @level2type=N'COLUMN',@level2name=N'Sort'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否启用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N's_SystemSection', @level2type=N'COLUMN',@level2name=N'Enabled'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N's_SystemSection', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N's_SystemSection', @level2type=N'COLUMN',@level2name=N'ModifyTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'部门' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N's_SystemSection'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N's_SystemStaff', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'登录名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N's_SystemStaff', @level2type=N'COLUMN',@level2name=N'LoginName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'登录密码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N's_SystemStaff', @level2type=N'COLUMN',@level2name=N'LoginPwd'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'盐值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N's_SystemStaff', @level2type=N'COLUMN',@level2name=N'MaskCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'手机' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N's_SystemStaff', @level2type=N'COLUMN',@level2name=N'Mobile'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'电话' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N's_SystemStaff', @level2type=N'COLUMN',@level2name=N'Tel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'邮箱' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N's_SystemStaff', @level2type=N'COLUMN',@level2name=N'Eamil'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'呢称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N's_SystemStaff', @level2type=N'COLUMN',@level2name=N'NickName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态：0有效，1禁用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N's_SystemStaff', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'所属部门' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N's_SystemStaff', @level2type=N'COLUMN',@level2name=N'SectionId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最近登录时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N's_SystemStaff', @level2type=N'COLUMN',@level2name=N'LastLoginTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N's_SystemStaff', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N's_SystemStaff', @level2type=N'COLUMN',@level2name=N'ModifyTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'系统账号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N's_SystemStaff'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N's_SystemStaffRole', @level2type=N'COLUMN',@level2name=N'StaffId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N's_SystemStaffRole', @level2type=N'COLUMN',@level2name=N'RoleId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'账号角色' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N's_SystemStaffRole'
GO
