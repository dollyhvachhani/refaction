USE [RefactorDB]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 02-03-2018 07:49:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[DeliveryPrice] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProductOption]    Script Date: 02-03-2018 07:49:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductOption](
	[Id] [uniqueidentifier] NOT NULL,
	[ProductId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_ProductOption] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User]    Script Date: 02-03-2018 07:49:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [int] NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [DeliveryPrice]) VALUES (N'8f2e9176-35ee-4f0a-ae55-83023d2db1a3', N'Samsung Galaxy S7', N'Newest mobile product from Samsung.', CAST(1024.99 AS Decimal(18, 2)), CAST(16.99 AS Decimal(18, 2)))
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [DeliveryPrice]) VALUES (N'de1287c0-4b15-4a7b-9d8a-dd21b3cafec3', N'Apple iPhone 6S', N'Newest mobile product from Apple.', CAST(1299.99 AS Decimal(18, 2)), CAST(15.99 AS Decimal(18, 2)))
INSERT [dbo].[ProductOption] ([Id], [ProductId], [Name], [Description]) VALUES (N'fcd47771-fc70-4726-8e06-5cc8f92e321e', N'8f2e9176-35ee-4f0a-ae55-83023d2db1a3', N'Gold', N'Gold Samsung Galaxy S7')
INSERT [dbo].[ProductOption] ([Id], [ProductId], [Name], [Description]) VALUES (N'a21d5777-a655-4020-b431-624bb331e9a2', N'8f2e9176-35ee-4f0a-ae55-83023d2db1a3', N'Black', N'Black Samsung Galaxy S7')
INSERT [dbo].[ProductOption] ([Id], [ProductId], [Name], [Description]) VALUES (N'5c2996ab-54ad-4999-92d2-89245682d534', N'de1287c0-4b15-4a7b-9d8a-dd21b3cafec3', N'Rose Gold', N'Gold Apple iPhone 6S')
INSERT [dbo].[ProductOption] ([Id], [ProductId], [Name], [Description]) VALUES (N'9ae6f477-a010-4ec9-b6a8-92a85d6c5f03', N'de1287c0-4b15-4a7b-9d8a-dd21b3cafec3', N'White', N'White Apple iPhone 6S')
INSERT [dbo].[ProductOption] ([Id], [ProductId], [Name], [Description]) VALUES (N'6844145e-479c-4c6a-9373-9754cb5a9697', N'8f2e9176-35ee-4f0a-ae55-83023d2db1a3', N'Rose Gold', N'Rose Gold')
INSERT [dbo].[ProductOption] ([Id], [ProductId], [Name], [Description]) VALUES (N'4e2bc5f2-699a-4c42-802e-ce4b4d2ac0ef', N'de1287c0-4b15-4a7b-9d8a-dd21b3cafec3', N'Black', N'Black Apple iPhone 6S')
INSERT [dbo].[User] ([UserId], [UserName], [Password], [Name]) VALUES (1, N'dolly', N'dolly', N'Dolly Vachhani')
INSERT [dbo].[User] ([UserId], [UserName], [Password], [Name]) VALUES (2, N'user', N'user', N'API user')
ALTER TABLE [dbo].[ProductOption]  WITH CHECK ADD  CONSTRAINT [FK_ProductOption_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
GO
ALTER TABLE [dbo].[ProductOption] CHECK CONSTRAINT [FK_ProductOption_Product]
GO
