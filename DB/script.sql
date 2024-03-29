USE [mshop]
GO
/****** Object:  Table [dbo].[m_category]    Script Date: 21/07/2019 13:04:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[m_category](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NULL,
	[picture] [text] NULL,
	[status] [nvarchar](50) NULL,
	[created_date] [date] NULL,
	[created_by] [nvarchar](50) NULL,
	[modified_date] [date] NULL,
	[modified_by] [nvarchar](50) NULL,
 CONSTRAINT [PK_m_category] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[m_item]    Script Date: 21/07/2019 13:04:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[m_item](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NULL,
	[description] [text] NULL,
	[quantity] [float] NULL,
	[cost_price] [decimal](18, 0) NULL,
	[sale_price] [decimal](18, 0) NULL,
	[main_image] [text] NULL,
	[item_code] [nvarchar](50) NULL,
	[item_category] [int] NULL,
	[status] [nvarchar](50) NULL,
	[created_date] [date] NULL,
	[created_by] [nvarchar](50) NULL,
	[modified_date] [date] NULL,
	[modified_by] [nvarchar](50) NULL,
 CONSTRAINT [PK_m_item] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[m_user]    Script Date: 21/07/2019 13:04:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[m_user](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[username] [nvarchar](50) NULL,
	[password] [nvarchar](50) NULL,
	[display_name] [nvarchar](50) NULL,
	[status] [nvarchar](50) NULL,
	[role] [nvarchar](50) NULL,
	[created_date] [date] NULL,
	[created_by] [nvarchar](50) NULL,
	[modified_date] [date] NULL,
	[modified_by] [nvarchar](50) NULL,
	[profile_picture] [text] NULL,
	[email] [nvarchar](50) NULL,
 CONSTRAINT [PK_m_user] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[m_category] ON 

INSERT [dbo].[m_category] ([id], [name], [picture], [status], [created_date], [created_by], [modified_date], [modified_by]) VALUES (14, N'Sports', N'/SystemData/CategoryPicture/005aadf3-04e0-4653-be4a-bebff9e13aec.jpg', N'Active', CAST(N'2019-07-20' AS Date), N'Self', CAST(N'2019-07-20' AS Date), N'Self')
SET IDENTITY_INSERT [dbo].[m_category] OFF
SET IDENTITY_INSERT [dbo].[m_item] ON 

INSERT [dbo].[m_item] ([id], [name], [description], [quantity], [cost_price], [sale_price], [main_image], [item_code], [item_category], [status], [created_date], [created_by], [modified_date], [modified_by]) VALUES (1, N'A50', N'Sumsung', 1, CAST(55000 AS Decimal(18, 0)), CAST(56000 AS Decimal(18, 0)), N'/SystemData/CategoryPicture/b399e5b2-24f7-4ee7-86ea-7f0582011b3f.jpg', N'12201', 12545, N'On Hold', CAST(N'2019-07-09' AS Date), N'Staff', NULL, NULL)
SET IDENTITY_INSERT [dbo].[m_item] OFF
SET IDENTITY_INSERT [dbo].[m_user] ON 

INSERT [dbo].[m_user] ([id], [username], [password], [display_name], [status], [role], [created_date], [created_by], [modified_date], [modified_by], [profile_picture], [email]) VALUES (1, N'admin', N'123456', N'heart', NULL, N'Admin', CAST(N'2019-07-21' AS Date), NULL, CAST(N'2019-07-21' AS Date), NULL, NULL, N'shahzadaptech143@gmail.com')
INSERT [dbo].[m_user] ([id], [username], [password], [display_name], [status], [role], [created_date], [created_by], [modified_date], [modified_by], [profile_picture], [email]) VALUES (2, N'staff', N'123', N'soft', NULL, N'Staff', CAST(N'2019-07-21' AS Date), NULL, CAST(N'2019-07-21' AS Date), NULL, NULL, N'shahzadaptech143@gmail.com')
INSERT [dbo].[m_user] ([id], [username], [password], [display_name], [status], [role], [created_date], [created_by], [modified_date], [modified_by], [profile_picture], [email]) VALUES (29, N'Shahzad', N'12345', N'Pearl', N'Active', N'Admin', CAST(N'2019-07-21' AS Date), N'Self', CAST(N'2019-07-21' AS Date), N'Self', N'/SystemData/ProfilePicture/8140a29a-a7b4-473e-8432-47470d1b9867.jpg', N'shahzadaptech143@gmail.com')
SET IDENTITY_INSERT [dbo].[m_user] OFF
