USE [master]
GO
/****** Object:  Database [SamSunVina]    Script Date: 08/22/2016 15:57:52 ******/
CREATE DATABASE [SamSunVina] ON  PRIMARY 
( NAME = N'SamSunVina', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.SQL2008\MSSQL\DATA\SamSunVina.mdf' , SIZE = 2304KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'SamSunVina_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.SQL2008\MSSQL\DATA\SamSunVina_log.LDF' , SIZE = 768KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [SamSunVina] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SamSunVina].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SamSunVina] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [SamSunVina] SET ANSI_NULLS OFF
GO
ALTER DATABASE [SamSunVina] SET ANSI_PADDING OFF
GO
ALTER DATABASE [SamSunVina] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [SamSunVina] SET ARITHABORT OFF
GO
ALTER DATABASE [SamSunVina] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [SamSunVina] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [SamSunVina] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [SamSunVina] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [SamSunVina] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [SamSunVina] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [SamSunVina] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [SamSunVina] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [SamSunVina] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [SamSunVina] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [SamSunVina] SET  ENABLE_BROKER
GO
ALTER DATABASE [SamSunVina] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [SamSunVina] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [SamSunVina] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [SamSunVina] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [SamSunVina] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [SamSunVina] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [SamSunVina] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [SamSunVina] SET  READ_WRITE
GO
ALTER DATABASE [SamSunVina] SET RECOVERY FULL
GO
ALTER DATABASE [SamSunVina] SET  MULTI_USER
GO
ALTER DATABASE [SamSunVina] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [SamSunVina] SET DB_CHAINING OFF
GO
EXEC sys.sp_db_vardecimal_storage_format N'SamSunVina', N'ON'
GO
USE [SamSunVina]
GO
/****** Object:  Table [dbo].[RequestPasswords]    Script Date: 08/22/2016 15:57:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RequestPasswords](
	[Id] [uniqueidentifier] NOT NULL,
	[KeyRef] [nvarchar](100) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[Email] [nvarchar](100) NULL,
	[ExternalId] [nvarchar](max) NULL,
 CONSTRAINT [PK_RequestPasswords] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[RequestPasswords] ([Id], [KeyRef], [CreatedDate], [Email], [ExternalId]) VALUES (N'bc3cdc93-6c85-6bc7-3aa6-08d3aeb8d5db', N'9fzl9xib2m5yt735fk5r15297xbj14q617u05fg7g83rj13fni', CAST(0x0000A6470033DA5A AS DateTime), N'anhductask@gmail.com', NULL)
/****** Object:  Table [dbo].[RefreshTokens]    Script Date: 08/22/2016 15:57:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RefreshTokens](
	[Id] [nvarchar](1000) NOT NULL,
	[Subject] [nvarchar](500) NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[IssuedUtc] [datetime] NOT NULL,
	[ExpiresUtc] [datetime] NOT NULL,
	[ProtectedTicket] [nvarchar](2000) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 08/22/2016 15:57:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [uniqueidentifier] NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[CategoryId] [uniqueidentifier] NOT NULL,
	[ProductCode] [nvarchar](max) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[Status] [smallint] NOT NULL,
	[Cost] [decimal](18, 2) NOT NULL,
	[Vat] [decimal](18, 2) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[UnitId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_CreatedDate] ON [dbo].[Products] 
(
	[CreatedDate] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_UpdateDate] ON [dbo].[Products] 
(
	[UpdateDate] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
INSERT [dbo].[Products] ([Id], [Price], [CategoryId], [ProductCode], [CreatedDate], [UpdateDate], [Status], [Cost], [Vat], [Name], [UnitId]) VALUES (N'8b936e7b-cd58-12ca-55b4-08d3c4e6ff22', CAST(0.00 AS Decimal(18, 2)), N'6dee5e09-5bf2-78c2-c8c1-08d3c583af9a', N'Pepsi', CAST(0x0000A663008D7116 AS DateTime), CAST(0x0000A667004173A1 AS DateTime), 10, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'Pepsi', N'7ba3467a-df4d-9cca-8a91-08d3c58e227a')
INSERT [dbo].[Products] ([Id], [Price], [CategoryId], [ProductCode], [CreatedDate], [UpdateDate], [Status], [Cost], [Vat], [Name], [UnitId]) VALUES (N'1452f9d5-f7f6-6ec5-9214-08d3c71dc20e', CAST(0.00 AS Decimal(18, 2)), N'4e36cab0-64cf-37c9-eccf-08d3c67f12a2', N'0112', CAST(0x0000A666004565F6 AS DateTime), CAST(0x0000A6680081157B AS DateTime), 10, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'c', N'7ba3467a-df4d-9cca-8a91-08d3c58e227a')
INSERT [dbo].[Products] ([Id], [Price], [CategoryId], [ProductCode], [CreatedDate], [UpdateDate], [Status], [Cost], [Vat], [Name], [UnitId]) VALUES (N'3b10d8f6-4c50-e1c4-5559-08d3c71e1cf9', CAST(0.00 AS Decimal(18, 2)), N'4e36cab0-64cf-37c9-eccf-08d3c67f12a2', N'0254', CAST(0x0000A6660045D399 AS DateTime), CAST(0x0000A6660045D399 AS DateTime), 10, CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'c4565', N'7ba3467a-df4d-9cca-8a91-08d3c58e227a')
/****** Object:  Table [dbo].[ProductImages]    Script Date: 08/22/2016 15:57:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductImages](
	[Id] [uniqueidentifier] NOT NULL,
	[ImagePath] [nvarchar](100) NULL,
	[ProductId] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[Visible] [bit] NOT NULL,
	[Represent] [bit] NOT NULL,
 CONSTRAINT [PK_ProductImages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_ProductId] ON [dbo].[ProductImages] 
(
	[ProductId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [NonClusteredIndex-CreatedDate] ON [dbo].[ProductImages] 
(
	[CreatedDate] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
INSERT [dbo].[ProductImages] ([Id], [ImagePath], [ProductId], [CreatedDate], [Visible], [Represent]) VALUES (N'4772ed34-b6fc-ddc3-c5fa-08d3c96c5878', N'124d818f-6fb9-4a35-8076-1d7b9d8ce896.jpg', N'1452f9d5-f7f6-6ec5-9214-08d3c71dc20e', CAST(0x0000A66900303A1C AS DateTime), 1, 0)
/****** Object:  Table [dbo].[HtmlContents]    Script Date: 08/22/2016 15:57:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[HtmlContents](
	[Id] [uniqueidentifier] NOT NULL,
	[LanguageCode] [varchar](10) NULL,
	[Type] [smallint] NOT NULL,
	[Content] [nvarchar](max) NULL,
	[HtmlContentDisplayType] [smallint] NOT NULL,
	[Header] [nvarchar](max) NULL,
 CONSTRAINT [PK_HtmlContents] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[HtmlContents] ([Id], [LanguageCode], [Type], [Content], [HtmlContentDisplayType], [Header]) VALUES (N'6d801001-d53e-25c6-6093-08d29e0fc7ce', N'en-US', 5, N'<div>An email has been sent to your email address. Please check mail to finish final steps.</div>
', 5, NULL)
INSERT [dbo].[HtmlContents] ([Id], [LanguageCode], [Type], [Content], [HtmlContentDisplayType], [Header]) VALUES (N'c02a172a-6609-ffc6-5ba0-08d29e0fcdd2', N'vi-VN', 5, N'<div>Một email đ&atilde; gửi tới hộp thư của bạn. H&atilde;y kiểm tra email v&agrave; l&agrave;m theo hướng dẫn để ho&agrave;n tất qu&aacute; tr&igrave;nh kh&ocirc;i phục mật khẩu</div>
', 5, NULL)
INSERT [dbo].[HtmlContents] ([Id], [LanguageCode], [Type], [Content], [HtmlContentDisplayType], [Header]) VALUES (N'21884bb8-e16e-f0ce-35bc-08d29e35cc92', N'en-US', 10, N'<div>An email has been sent to you for confirm external id</div>
', 5, NULL)
INSERT [dbo].[HtmlContents] ([Id], [LanguageCode], [Type], [Content], [HtmlContentDisplayType], [Header]) VALUES (N'5ae61182-09d7-d0c1-3e8c-08d29e35d152', N'vi-VN', 10, N'<div>Một email đ&atilde; gửi tới bạn để x&aacute;c nhận định danh.</div>
', 5, NULL)
INSERT [dbo].[HtmlContents] ([Id], [LanguageCode], [Type], [Content], [HtmlContentDisplayType], [Header]) VALUES (N'ead47098-0f81-28c9-9d2e-08d2a4948e17', N'en-US', 15, N'<div>Congratulations!.</div>
', 5, NULL)
INSERT [dbo].[HtmlContents] ([Id], [LanguageCode], [Type], [Content], [HtmlContentDisplayType], [Header]) VALUES (N'0b9bf7ce-11ab-a0c9-4880-08d2a4948f99', N'vi-VN', 15, N'<div>Xin ch&uacute;c mừng!</div>
', 5, NULL)
INSERT [dbo].[HtmlContents] ([Id], [LanguageCode], [Type], [Content], [HtmlContentDisplayType], [Header]) VALUES (N'e818f24e-cf50-fcc4-169c-08d2a7b0a7e3', N'en-US', 20, N'<div>Congratulations!.</div>
', 5, NULL)
INSERT [dbo].[HtmlContents] ([Id], [LanguageCode], [Type], [Content], [HtmlContentDisplayType], [Header]) VALUES (N'461008c6-91ba-4dcb-8132-08d2a7b0a9c6', N'vi-VN', 20, N'<div>Xin ch&uacute;c mừng! Bạn đ&atilde; đặt h&agrave;ng th&agrave;nh c&ocirc;ng. Ch&uacute;ng t&ocirc;i sẽ li&ecirc;n hệ với bạn trong thời gian sớm nhất!.</div>
', 5, NULL)
INSERT [dbo].[HtmlContents] ([Id], [LanguageCode], [Type], [Content], [HtmlContentDisplayType], [Header]) VALUES (N'36dff774-1313-5ec6-5daa-08d2af695e7e', N'en-US', 1000, N'<div>Hi [FName],</div>

<div>You got a quick trip.</div>

<div>Booking Number : [BookingNumber]</div>

<div>Pickup location : [PickupLocation]<br />
Dropoff location : [DropoffLocation]</div>
', 15, N'You got a quick trip')
INSERT [dbo].[HtmlContents] ([Id], [LanguageCode], [Type], [Content], [HtmlContentDisplayType], [Header]) VALUES (N'6559eeea-741c-a8ca-9b73-08d2af6bdc40', N'vi-VN', 1000, N'<div>Xin ch&agrave;o [FName]</div>

<div>Bạn c&oacute; một chuyến đi tức thời</div>

<div>Số đăng k&yacute; :&nbsp;[BookingNumber]</div>

<div>Điểm đ&oacute;n : [PickupLocation]<br />
Điểm đến : [DropoffLocation]</div>
', 15, N'Bạn có một chuyến đi tức thời')
INSERT [dbo].[HtmlContents] ([Id], [LanguageCode], [Type], [Content], [HtmlContentDisplayType], [Header]) VALUES (N'afd133e2-953e-2fc1-7a9d-08d2af6be343', N'vi-VN', 1000, N'Xin chào [FName]
Bạn có một chuyến đi tức thời
Số đăng ký : [BookingNumber]
Điểm đón : [PickupLocation]
Điểm đến : [DropoffLocation]', 10, NULL)
INSERT [dbo].[HtmlContents] ([Id], [LanguageCode], [Type], [Content], [HtmlContentDisplayType], [Header]) VALUES (N'9fdea29f-c28f-c8cc-2f54-08d2af6be6de', N'en-US', 1000, N'Hi [FName]
You got a quick trip
Booking Number : [BookingNumber]
Pickup location : [PickupLocation]
Dropoff location : [DropoffLocation]', 10, NULL)
INSERT [dbo].[HtmlContents] ([Id], [LanguageCode], [Type], [Content], [HtmlContentDisplayType], [Header]) VALUES (N'33bfe1b3-a28d-acc0-8615-08d2af6bf845', N'en-US', 1005, N'Hi [FName]
We so sorry. No driver availble now. So, your trip is rejected', 10, NULL)
INSERT [dbo].[HtmlContents] ([Id], [LanguageCode], [Type], [Content], [HtmlContentDisplayType], [Header]) VALUES (N'9f8d4a4e-7425-85cf-49db-08d2af6bf9ab', N'en-US', 1005, N'<div>Hi [FName]<br />
We so sorry. No driver availble now. So, your trip is rejected</div>
', 15, N'Trip Rejected')
INSERT [dbo].[HtmlContents] ([Id], [LanguageCode], [Type], [Content], [HtmlContentDisplayType], [Header]) VALUES (N'61b0646a-f4ce-bec0-4818-08d2af6c00ee', N'vi-VN', 1005, N'<div>Xin ch&agrave;o [FName]<br />
V&igrave; hiện tại tất &nbsp;cả t&agrave;i xế đ&atilde; c&oacute; lịch, n&ecirc;n chuyến đi của bạn bị từ chối.&nbsp;</div>
', 15, N'Chuyến đi bị từ chối')
INSERT [dbo].[HtmlContents] ([Id], [LanguageCode], [Type], [Content], [HtmlContentDisplayType], [Header]) VALUES (N'1560074b-5cde-87c8-60d2-08d2af6c06ad', N'vi-VN', 1005, N'Xin chào [FName]
Vì tất cả tài xế đã có lịch đi, nên chuyến đi của bạn bị từ chối.', 10, NULL)
INSERT [dbo].[HtmlContents] ([Id], [LanguageCode], [Type], [Content], [HtmlContentDisplayType], [Header]) VALUES (N'2ea57ccd-3c30-90cd-bccb-08d2af77bdba', N'en-US', 1010, N'Hi [FName]
Trip canceled
Trip number : [BookingNumber] ', 10, NULL)
INSERT [dbo].[HtmlContents] ([Id], [LanguageCode], [Type], [Content], [HtmlContentDisplayType], [Header]) VALUES (N'9b74fca1-c5ff-b1cc-44a2-08d2af77bee4', N'en-US', 1010, N'<div>Hi [FName]<br />
Trip canceled<br />
Trip number : [BookingNumber]&nbsp;</div>
', 15, N'Trip Canceled')
INSERT [dbo].[HtmlContents] ([Id], [LanguageCode], [Type], [Content], [HtmlContentDisplayType], [Header]) VALUES (N'a84357d9-25d3-fdca-e6f6-08d2af77c4d1', N'vi-VN', 1010, N'Xin chào  [FName]
Chuyến đi bị hủy bỏ
Số đăng ký : [BookingNumber]', 10, NULL)
INSERT [dbo].[HtmlContents] ([Id], [LanguageCode], [Type], [Content], [HtmlContentDisplayType], [Header]) VALUES (N'34b06e04-3b51-b1cb-80e8-08d2af77c595', N'vi-VN', 1010, N'<div>Xin ch&agrave;o &nbsp;[FName]<br />
Chuyến đi bị hủy bỏ<br />
Số đăng k&yacute; : [BookingNumber]</div>
', 15, N'Chuyến đi bị hủy bỏ')
INSERT [dbo].[HtmlContents] ([Id], [LanguageCode], [Type], [Content], [HtmlContentDisplayType], [Header]) VALUES (N'c873e3c5-9ba1-66cf-7444-08d2af84c55e', N'en-US', 1015, N'Hi [FName]
You got a plan trip
Booking Number : [BookingNumber]', 10, NULL)
INSERT [dbo].[HtmlContents] ([Id], [LanguageCode], [Type], [Content], [HtmlContentDisplayType], [Header]) VALUES (N'35b3d139-e9ae-8ace-7c68-08d2af84c83e', N'en-US', 1015, N'<div>Hi [FName]<br />
You got a plan trip<br />
Booking Number : [BookingNumber]</div>
', 15, N'You got a plan trip')
INSERT [dbo].[HtmlContents] ([Id], [LanguageCode], [Type], [Content], [HtmlContentDisplayType], [Header]) VALUES (N'ef5db7ca-6219-03c2-aa86-08d2af84cf6f', N'vi-VN', 1015, N'<div>Xin ch&agrave;o&nbsp;[FName]</div>

<div>Bạn c&oacute; kế hoạch chuyến đi</div>

<div>Số đăng k&yacute; :&nbsp;[BookingNumber]</div>
', 15, N'Bạn có kế hoạch chuyến đi')
INSERT [dbo].[HtmlContents] ([Id], [LanguageCode], [Type], [Content], [HtmlContentDisplayType], [Header]) VALUES (N'8a34ae73-2392-3dc9-470a-08d2af84d06e', N'vi-VN', 1015, N'Xin chào [FName]
Bạn có kế hoạch chuyến đi
Số đăng ký : [BookingNumber]', 10, NULL)
INSERT [dbo].[HtmlContents] ([Id], [LanguageCode], [Type], [Content], [HtmlContentDisplayType], [Header]) VALUES (N'8cc3706a-3d07-18c7-b45d-08d2af84d543', N'vi-VN', 1020, N'Xin chào [FName]
Kế hoạch chuyến đi của bạn bị từ chối', 10, NULL)
INSERT [dbo].[HtmlContents] ([Id], [LanguageCode], [Type], [Content], [HtmlContentDisplayType], [Header]) VALUES (N'd4603512-4e86-1bc6-e0f2-08d2af84d7bd', N'vi-VN', 1020, N'<div>Xin ch&agrave;o [FName]<br />
Kế hoạch chuyến đi của bạn bị từ chối</div>
', 15, N'Kế hoạch chuyến đi bị từ chối')
INSERT [dbo].[HtmlContents] ([Id], [LanguageCode], [Type], [Content], [HtmlContentDisplayType], [Header]) VALUES (N'7d10ea1f-047f-2fc7-181a-08d2af84db4c', N'en-US', 1020, N'<div>Hi&nbsp;[FName]</div>

<div>Your plan trip is rejected</div>
', 15, N'Plan Trip Rejected')
INSERT [dbo].[HtmlContents] ([Id], [LanguageCode], [Type], [Content], [HtmlContentDisplayType], [Header]) VALUES (N'8714b7e2-f764-71c4-632d-08d2af84dc9f', N'en-US', 1020, N'Hi [FName]
Your plan trip is rejected', 10, NULL)
INSERT [dbo].[HtmlContents] ([Id], [LanguageCode], [Type], [Content], [HtmlContentDisplayType], [Header]) VALUES (N'8426ef0b-f7ba-03cf-95a7-08d2af84e073', N'en-US', 1025, N'Hi [FName]
You got airport pickup trip
Booking number : [BookingNumber]', 10, NULL)
INSERT [dbo].[HtmlContents] ([Id], [LanguageCode], [Type], [Content], [HtmlContentDisplayType], [Header]) VALUES (N'2c025fdf-3e3f-2bc3-f976-08d2af84e1c0', N'en-US', 1025, N'<div>Hi [FName]<br />
You got airport pickup trip<br />
Booking number : [BookingNumber]</div>
', 15, N'You got airport pickup trip')
INSERT [dbo].[HtmlContents] ([Id], [LanguageCode], [Type], [Content], [HtmlContentDisplayType], [Header]) VALUES (N'62b7f2c5-a5a2-c2cf-9933-08d2af84e72a', N'vi-VN', 1025, N'<div>Xin ch&agrave;o&nbsp;[FName]</div>

<div>Bạn c&oacute; chuyến đi đ&oacute;n kh&aacute;ch tại s&acirc;n bay</div>

<div>Số đăng k&yacute; :&nbsp;[BookingNumber]</div>
', 15, N'Bạn có chuyến đi đón khách tại sân bay')
INSERT [dbo].[HtmlContents] ([Id], [LanguageCode], [Type], [Content], [HtmlContentDisplayType], [Header]) VALUES (N'fea7082a-d793-7ec9-cecd-08d2af84e80b', N'vi-VN', 1025, N'Xin chào [FName]
Bạn có chuyến đi đón khách tại sân bay
Số đăng ký : [BookingNumber]', 10, NULL)
INSERT [dbo].[HtmlContents] ([Id], [LanguageCode], [Type], [Content], [HtmlContentDisplayType], [Header]) VALUES (N'765fe4b3-a2c7-d9c7-140a-08d2af84effd', N'vi-VN', 1030, N'Xin chào [FName]
Chuyến đi của bạn đã bị hủy bỏ', 10, NULL)
INSERT [dbo].[HtmlContents] ([Id], [LanguageCode], [Type], [Content], [HtmlContentDisplayType], [Header]) VALUES (N'2ab74eca-81a0-33cb-6065-08d2af84f128', N'vi-VN', 1030, N'<div>Xin ch&agrave;o [FName]<br />
Chuyến đi của bạn đ&atilde; bị hủy bỏ</div>
', 15, N'Chuyến đi của bạn đã bị hủy bỏ')
INSERT [dbo].[HtmlContents] ([Id], [LanguageCode], [Type], [Content], [HtmlContentDisplayType], [Header]) VALUES (N'62961bb2-87f4-becb-4283-08d2af84f636', N'en-US', 1030, N'<div>Hi&nbsp;[FName]</div>

<div>Your trip is rejected</div>
', 15, N'Your trip is rejected')
INSERT [dbo].[HtmlContents] ([Id], [LanguageCode], [Type], [Content], [HtmlContentDisplayType], [Header]) VALUES (N'4e24b561-cee3-2bc6-dc1c-08d2af84f6db', N'en-US', 1030, N'Hi [FName]
Your trip is rejected', 10, NULL)
INSERT [dbo].[HtmlContents] ([Id], [LanguageCode], [Type], [Content], [HtmlContentDisplayType], [Header]) VALUES (N'3d22f2f5-af1b-74c8-5d24-08d2af850392', N'en-US', 1035, N'Hi [FName]
You got a airport dropoff trip
Booking number : [BookingNumber]', 10, NULL)
INSERT [dbo].[HtmlContents] ([Id], [LanguageCode], [Type], [Content], [HtmlContentDisplayType], [Header]) VALUES (N'412643f3-f16b-4cca-01c3-08d2af8504a0', N'en-US', 1035, N'<div>Hi [FName]<br />
You got a airport dropoff trip<br />
Booking number : [BookingNumber]</div>
', 15, N'You got a airport dropoff trip')
INSERT [dbo].[HtmlContents] ([Id], [LanguageCode], [Type], [Content], [HtmlContentDisplayType], [Header]) VALUES (N'15b978fb-242e-c9ce-b29c-08d2af850817', N'vi-VN', 1035, N'<div>Xin ch&agrave;o&nbsp;[FName]</div>

<div>Bạn c&oacute; chuyến đưa kh&aacute;ch ra s&acirc;n bay</div>

<div>Số đăng k&yacute; :&nbsp;[BookingNumber]</div>
', 15, N'Bạn có chuyến đưa khách ra sân bay')
INSERT [dbo].[HtmlContents] ([Id], [LanguageCode], [Type], [Content], [HtmlContentDisplayType], [Header]) VALUES (N'464ba1f2-86a2-1ec1-e0f9-08d2af8508f4', N'vi-VN', 1035, N'Xin chào [FName]
Bạn có chuyến đưa khách ra sân bay
Số đăng ký : [BookingNumber]', 10, NULL)
INSERT [dbo].[HtmlContents] ([Id], [LanguageCode], [Type], [Content], [HtmlContentDisplayType], [Header]) VALUES (N'fbc9d07c-a764-7ec8-80c1-08d2af850bd2', N'vi-VN', 1040, N'Xin chào [FName]
Chuyến đi của bạn đã bị từ chối', 10, NULL)
INSERT [dbo].[HtmlContents] ([Id], [LanguageCode], [Type], [Content], [HtmlContentDisplayType], [Header]) VALUES (N'4d15ca86-029b-82c8-cd64-08d2af850cc3', N'vi-VN', 1040, N'<div>Xin ch&agrave;o [FName]<br />
Chuyến đi của bạn đ&atilde; bị từ chối</div>
', 15, N'Chuyến đi của bạn đã bị từ chối')
INSERT [dbo].[HtmlContents] ([Id], [LanguageCode], [Type], [Content], [HtmlContentDisplayType], [Header]) VALUES (N'5c28c200-1b56-87c8-8bd0-08d2af850efa', N'en-US', 1040, N'<div>Hello&nbsp;[FName]</div>

<div>Your trip is rejected</div>
', 15, N'Your trip is rejected')
INSERT [dbo].[HtmlContents] ([Id], [LanguageCode], [Type], [Content], [HtmlContentDisplayType], [Header]) VALUES (N'f7037e6b-5881-bec4-2a95-08d2af850f92', N'en-US', 1040, N'Hello [FName]
Your trip is rejected', 10, NULL)
INSERT [dbo].[HtmlContents] ([Id], [LanguageCode], [Type], [Content], [HtmlContentDisplayType], [Header]) VALUES (N'95fcd906-d914-44cb-0dfa-08d2af9a1aba', N'en-US', 1045, N'Hello [FName]
Driver have just set price : [Price]', 10, NULL)
INSERT [dbo].[HtmlContents] ([Id], [LanguageCode], [Type], [Content], [HtmlContentDisplayType], [Header]) VALUES (N'90bb472a-3a45-8fcd-73c2-08d2af9a1cca', N'en-US', 1045, N'<div>Hello [FName]<br />
Driver have just set price : [Price]</div>
', 15, N'Driver set Trip Price')
INSERT [dbo].[HtmlContents] ([Id], [LanguageCode], [Type], [Content], [HtmlContentDisplayType], [Header]) VALUES (N'1bc020f0-ca57-26c3-6161-08d2af9a20e0', N'vi-VN', 1045, N'<div>Xin ch&agrave;o&nbsp;[FName]</div>

<div>T&agrave;i xế cho biết gi&aacute; chuyến đi l&agrave; :&nbsp;[Price]</div>
', 15, N'Tài xế cho biết giá chuyến đi')
INSERT [dbo].[HtmlContents] ([Id], [LanguageCode], [Type], [Content], [HtmlContentDisplayType], [Header]) VALUES (N'4e09460e-5b8f-41ca-f98d-08d2af9a2171', N'vi-VN', 1045, N'Xin chào [FName]
Tài xế cho biết giá chuyến đi là : [Price]', 10, NULL)
INSERT [dbo].[HtmlContents] ([Id], [LanguageCode], [Type], [Content], [HtmlContentDisplayType], [Header]) VALUES (N'79b44a37-23b4-02c3-e269-08d2b1df2dfa', N'en-US', 40, N'<div>Register successfully. Please login</div>
', 5, NULL)
INSERT [dbo].[HtmlContents] ([Id], [LanguageCode], [Type], [Content], [HtmlContentDisplayType], [Header]) VALUES (N'231e0178-1f09-f0cb-f051-08d2b1df2ef2', N'en-US', 35, N'<div>Register successfully. Please login</div>
', 5, NULL)
INSERT [dbo].[HtmlContents] ([Id], [LanguageCode], [Type], [Content], [HtmlContentDisplayType], [Header]) VALUES (N'761af959-1697-08c9-5565-08d2b1df305f', N'vi-VN', 40, N'<div>Đăng k&yacute; th&agrave;nh c&ocirc;ng. Mời bạn đăng nhập</div>
', 5, NULL)
INSERT [dbo].[HtmlContents] ([Id], [LanguageCode], [Type], [Content], [HtmlContentDisplayType], [Header]) VALUES (N'f6afa682-05a6-13c0-ada4-08d2b1df30fc', N'vi-VN', 35, N'<div>Đăng k&yacute; th&agrave;nh c&ocirc;ng. Mời bạn đăng nhập</div>
', 5, NULL)
INSERT [dbo].[HtmlContents] ([Id], [LanguageCode], [Type], [Content], [HtmlContentDisplayType], [Header]) VALUES (N'eb87682c-8825-fec1-9e5e-08d2b4e8ca46', N'en-US', 2005, N'<div>[InviterName] is inviting you to drive with [SiteName]!. Sign up now and get up to [Reward] when you start driving :</div>

<div>[Link]</div>

<div>[Message]</div>
', 15, N'Invite to drive')
INSERT [dbo].[HtmlContents] ([Id], [LanguageCode], [Type], [Content], [HtmlContentDisplayType], [Header]) VALUES (N'e348082f-45fe-eccd-6b8b-08d2b4e8cb69', N'en-US', 2005, N'[InviterName] is inviting you to drive with [SiteName]!. Sign up now and get up to [Reward] when you start driving :
[Link]
[Message]', 10, NULL)
INSERT [dbo].[HtmlContents] ([Id], [LanguageCode], [Type], [Content], [HtmlContentDisplayType], [Header]) VALUES (N'd5032e65-c1e7-60c8-b55a-08d2b4e8ce2e', N'en-US', 2010, N'[Message]
[Link]', 10, NULL)
INSERT [dbo].[HtmlContents] ([Id], [LanguageCode], [Type], [Content], [HtmlContentDisplayType], [Header]) VALUES (N'9266fa85-558f-e0cc-c044-08d2b4e8ce34', N'en-US', 2010, N'[InviterName] is inviting you to ride with XeDuaDon!. Sign up now and get up to [Reward] when you start ride :
[Link]', 10, NULL)
INSERT [dbo].[HtmlContents] ([Id], [LanguageCode], [Type], [Content], [HtmlContentDisplayType], [Header]) VALUES (N'19f9f391-12e7-23cd-ea01-08d2b4e8d04a', N'en-US', 2010, N'<div>[Message]</div>

<div>[Link]</div>
', 15, N'Invite to ride')
INSERT [dbo].[HtmlContents] ([Id], [LanguageCode], [Type], [Content], [HtmlContentDisplayType], [Header]) VALUES (N'd137f1f9-db2f-bbc1-06a9-08d2b5078eea', N'en-US', 2005, N'I use  [SiteName] to make money with my car & you can too. Use my link for [Reward] extra: [Link]', 5, NULL)
INSERT [dbo].[HtmlContents] ([Id], [LanguageCode], [Type], [Content], [HtmlContentDisplayType], [Header]) VALUES (N'f9f95db9-ad3e-83c6-6c81-08d2b507952d', N'en-US', 2010, N'I love [SiteName]. Sign up with my promo code and get [Reward] off your first ride! [Link]', 5, NULL)
INSERT [dbo].[HtmlContents] ([Id], [LanguageCode], [Type], [Content], [HtmlContentDisplayType], [Header]) VALUES (N'73a4b36d-d6c5-57c8-c00e-08d35ab4f90d', N'vi-VN', 30, N'<h1><strong>Bạn đ&atilde; hủy bỏ thanh to&aacute;n!</strong></h1>

<div>&nbsp;</div>

<div>Cảm ơn bạn đ&atilde; lựa chọn cửa h&agrave;ng của ch&uacute;ng t&ocirc;i!</div>
', 5, NULL)
/****** Object:  Table [dbo].[Files]    Script Date: 08/22/2016 15:57:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Files](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[PhysicName] [nvarchar](50) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Files] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DataLogs]    Script Date: 08/22/2016 15:57:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DataLogs](
	[Id] [uniqueidentifier] NOT NULL,
	[Table] [smallint] NOT NULL,
	[Action] [smallint] NOT NULL,
	[BeUserId] [uniqueidentifier] NOT NULL,
	[OldData] [nvarchar](max) NULL,
	[NewData] [nvarchar](max) NULL,
	[LogDate] [datetime] NOT NULL,
	[ItemId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_DataLogs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_BeUserId] ON [dbo].[DataLogs] 
(
	[BeUserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_ItemId] ON [dbo].[DataLogs] 
(
	[ItemId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_LogDate] ON [dbo].[DataLogs] 
(
	[LogDate] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
INSERT [dbo].[DataLogs] ([Id], [Table], [Action], [BeUserId], [OldData], [NewData], [LogDate], [ItemId]) VALUES (N'060f1e8f-5ff3-14cc-45c1-08d3ca683670', 5, 10, N'f3447e33-a6da-4fb4-b327-80fd3ae6ebaf', N'{"ClinicEmail":"","Address":null,"CreatedDate":"\/Date(1471812750920)\/","CustomerType":30,"ClinicId":"fsdf","ClinicName":"sdf","City":0,"District":0,"ClinicPhone":null,"Website":null,"NumberOfDentist":null,"NumberOfStaff":null,"NumberOfChair":null,"UsingRC":null,"UsingDevices":"#7c79d5e3-c80d-1bc1-b866-08d3c67f118a#9ec768d3-305e-94cc-d102-08d3c96d0d79#4e36cab0-64cf-37c9-eccf-08d3c67f12a2#ad852192-e82c-bbcc-4229-08d3c67f10a8#89187ba8-b4fb-f5c3-8843-08d3c97da8ba","CreatedBy":"f3447e33-a6da-4fb4-b327-80fd3ae6ebaf","Lat":null,"Lng":null,"DentistName":null,"Gender":null,"Age":null,"DentistPhone":null,"DentistEmail":null,"Specialization":null,"MaritalStatus":null,"JoinSeminar":null,"JoinedDate":null,"InterestingDevice":"3b10d8f6-4c50-e1c4-5559-08d3c71e1cf9#8b936e7b-cd58-12ca-55b4-08d3c4e6ff22","LeadTime":null,"SundayWork":false,"SundayStart":null,"SundayEnd":null,"MondayWork":false,"MondayStart":null,"MondayEnd":null,"TuesdayWork":false,"TuesdayStart":null,"TuesdayEnd":null,"WednesdayWork":false,"WednesdayStart":null,"WednesdayEnd":null,"ThursdayWork":false,"ThursdayStart":null,"ThursdayEnd":null,"FridayWork":false,"FridayStart":null,"FridayEnd":null,"SaturdayWork":false,"SaturdayStart":null,"SaturdayEnd":null,"AssignTo":null,"Remark":null,"Images":[],"Visits":[],"Id":"ba6ef795-bb3c-4eca-bafc-08d3ca3fbe04","IsNew":false}', N'{"ClinicEmail":"","Address":null,"CreatedDate":"\/Date(1471812750920)\/","CustomerType":30,"ClinicId":"fsdf","ClinicName":"sdf","City":0,"District":0,"ClinicPhone":null,"Website":null,"NumberOfDentist":null,"NumberOfStaff":null,"NumberOfChair":null,"UsingRC":null,"UsingDevices":"#7c79d5e3-c80d-1bc1-b866-08d3c67f118a#9ec768d3-305e-94cc-d102-08d3c96d0d79#4e36cab0-64cf-37c9-eccf-08d3c67f12a2#ad852192-e82c-bbcc-4229-08d3c67f10a8#89187ba8-b4fb-f5c3-8843-08d3c97da8ba","CreatedBy":"f3447e33-a6da-4fb4-b327-80fd3ae6ebaf","Lat":10.839732734324095,"Lng":106.69292492636714,"DentistName":null,"Gender":null,"Age":null,"DentistPhone":null,"DentistEmail":null,"Specialization":null,"MaritalStatus":null,"JoinSeminar":null,"JoinedDate":null,"InterestingDevice":"3b10d8f6-4c50-e1c4-5559-08d3c71e1cf9#8b936e7b-cd58-12ca-55b4-08d3c4e6ff22","LeadTime":null,"SundayWork":false,"SundayStart":null,"SundayEnd":null,"MondayWork":false,"MondayStart":null,"MondayEnd":null,"TuesdayWork":false,"TuesdayStart":null,"TuesdayEnd":null,"WednesdayWork":false,"WednesdayStart":null,"WednesdayEnd":null,"ThursdayWork":false,"ThursdayStart":null,"ThursdayEnd":null,"FridayWork":false,"FridayStart":null,"FridayEnd":null,"SaturdayWork":false,"SaturdayStart":null,"SaturdayEnd":null,"AssignTo":null,"Remark":null,"Images":[],"Visits":[],"Id":"ba6ef795-bb3c-4eca-bafc-08d3ca3fbe04","IsNew":false}', CAST(0x0000A66A008F6E22 AS DateTime), N'ba6ef795-bb3c-4eca-bafc-08d3ca3fbe04')
INSERT [dbo].[DataLogs] ([Id], [Table], [Action], [BeUserId], [OldData], [NewData], [LogDate], [ItemId]) VALUES (N'e1ed3ee2-c151-2bc5-5b5b-08d3ca6859d0', 5, 10, N'f3447e33-a6da-4fb4-b327-80fd3ae6ebaf', N'{"ClinicEmail":"","Address":null,"CreatedDate":"\/Date(1471812750920)\/","CustomerType":30,"ClinicId":"fsdf","ClinicName":"sdf","City":0,"District":0,"ClinicPhone":null,"Website":null,"NumberOfDentist":null,"NumberOfStaff":null,"NumberOfChair":null,"UsingRC":null,"UsingDevices":"#7c79d5e3-c80d-1bc1-b866-08d3c67f118a#9ec768d3-305e-94cc-d102-08d3c96d0d79#4e36cab0-64cf-37c9-eccf-08d3c67f12a2#ad852192-e82c-bbcc-4229-08d3c67f10a8#89187ba8-b4fb-f5c3-8843-08d3c97da8ba","CreatedBy":"f3447e33-a6da-4fb4-b327-80fd3ae6ebaf","Lat":10.839732734324095,"Lng":106.69292492636714,"DentistName":null,"Gender":null,"Age":null,"DentistPhone":null,"DentistEmail":null,"Specialization":null,"MaritalStatus":null,"JoinSeminar":null,"JoinedDate":null,"InterestingDevice":"3b10d8f6-4c50-e1c4-5559-08d3c71e1cf9#8b936e7b-cd58-12ca-55b4-08d3c4e6ff22","LeadTime":null,"SundayWork":false,"SundayStart":null,"SundayEnd":null,"MondayWork":false,"MondayStart":null,"MondayEnd":null,"TuesdayWork":false,"TuesdayStart":null,"TuesdayEnd":null,"WednesdayWork":false,"WednesdayStart":null,"WednesdayEnd":null,"ThursdayWork":false,"ThursdayStart":null,"ThursdayEnd":null,"FridayWork":false,"FridayStart":null,"FridayEnd":null,"SaturdayWork":false,"SaturdayStart":null,"SaturdayEnd":null,"AssignTo":null,"Remark":null,"Images":[],"Visits":[],"Id":"ba6ef795-bb3c-4eca-bafc-08d3ca3fbe04","IsNew":false}', N'{"ClinicEmail":"","Address":null,"CreatedDate":"\/Date(1471812750920)\/","CustomerType":30,"ClinicId":"fsdf","ClinicName":"sdf","City":0,"District":0,"ClinicPhone":null,"Website":null,"NumberOfDentist":null,"NumberOfStaff":null,"NumberOfChair":null,"UsingRC":null,"UsingDevices":"#7c79d5e3-c80d-1bc1-b866-08d3c67f118a#9ec768d3-305e-94cc-d102-08d3c96d0d79#4e36cab0-64cf-37c9-eccf-08d3c67f12a2#ad852192-e82c-bbcc-4229-08d3c67f10a8#89187ba8-b4fb-f5c3-8843-08d3c97da8ba","CreatedBy":"f3447e33-a6da-4fb4-b327-80fd3ae6ebaf","Lat":10.842430297128885,"Lng":106.5971378780273,"DentistName":null,"Gender":null,"Age":null,"DentistPhone":null,"DentistEmail":null,"Specialization":null,"MaritalStatus":null,"JoinSeminar":null,"JoinedDate":null,"InterestingDevice":"3b10d8f6-4c50-e1c4-5559-08d3c71e1cf9#8b936e7b-cd58-12ca-55b4-08d3c4e6ff22","LeadTime":null,"SundayWork":false,"SundayStart":null,"SundayEnd":null,"MondayWork":false,"MondayStart":null,"MondayEnd":null,"TuesdayWork":false,"TuesdayStart":null,"TuesdayEnd":null,"WednesdayWork":false,"WednesdayStart":null,"WednesdayEnd":null,"ThursdayWork":false,"ThursdayStart":null,"ThursdayEnd":null,"FridayWork":false,"FridayStart":null,"FridayEnd":null,"SaturdayWork":false,"SaturdayStart":null,"SaturdayEnd":null,"AssignTo":null,"Remark":null,"Images":[],"Visits":[],"Id":"ba6ef795-bb3c-4eca-bafc-08d3ca3fbe04","IsNew":false}', CAST(0x0000A66A0090FAB4 AS DateTime), N'ba6ef795-bb3c-4eca-bafc-08d3ca3fbe04')
INSERT [dbo].[DataLogs] ([Id], [Table], [Action], [BeUserId], [OldData], [NewData], [LogDate], [ItemId]) VALUES (N'2025579c-0892-72cf-d4ad-08d3ca6a27c5', 5, 5, N'f3447e33-a6da-4fb4-b327-80fd3ae6ebaf', N'', N'{"ClinicEmail":"","Address":null,"CreatedDate":"\/Date(1471856167102)\/","CustomerType":30,"ClinicId":"tetewt","ClinicName":"dfgdfg","City":0,"District":0,"ClinicPhone":null,"Website":null,"NumberOfDentist":null,"NumberOfStaff":null,"NumberOfChair":null,"UsingRC":null,"UsingDevices":null,"CreatedBy":"f3447e33-a6da-4fb4-b327-80fd3ae6ebaf","Lat":10.843441876909758,"Lng":106.63250012167964,"DentistName":null,"Gender":null,"Age":null,"DentistPhone":null,"DentistEmail":null,"Specialization":null,"MaritalStatus":null,"JoinSeminar":null,"JoinedDate":null,"InterestingDevice":null,"LeadTime":null,"SundayWork":false,"SundayStart":null,"SundayEnd":null,"MondayWork":false,"MondayStart":null,"MondayEnd":null,"TuesdayWork":false,"TuesdayStart":null,"TuesdayEnd":null,"WednesdayWork":false,"WednesdayStart":null,"WednesdayEnd":null,"ThursdayWork":false,"ThursdayStart":null,"ThursdayEnd":null,"FridayWork":false,"FridayStart":null,"FridayEnd":null,"SaturdayWork":false,"SaturdayStart":null,"SaturdayEnd":null,"AssignTo":null,"Remark":null,"Images":[],"Visits":[],"Id":"35426129-5710-7dc5-dea1-08d3ca6a27b6","IsNew":false}', CAST(0x0000A66A0093407A AS DateTime), N'35426129-5710-7dc5-dea1-08d3ca6a27b6')
/****** Object:  Table [dbo].[CustomerVisits]    Script Date: 08/22/2016 15:57:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerVisits](
	[Id] [uniqueidentifier] NOT NULL,
	[CustomerId] [uniqueidentifier] NOT NULL,
	[DateVisit] [datetime] NOT NULL,
	[BeUserId] [uniqueidentifier] NOT NULL,
	[Promise] [nvarchar](max) NULL,
	[Done] [smallint] NOT NULL,
 CONSTRAINT [PK_CustomerVisits] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_BeUserId] ON [dbo].[CustomerVisits] 
(
	[BeUserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_CustomerId] ON [dbo].[CustomerVisits] 
(
	[CustomerId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_DateVisit] ON [dbo].[CustomerVisits] 
(
	[DateVisit] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 08/22/2016 15:57:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Customers](
	[Id] [uniqueidentifier] NOT NULL,
	[ClinicEmail] [nvarchar](50) NULL,
	[Address] [nvarchar](3000) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CustomerType] [smallint] NOT NULL,
	[ClinicId] [nvarchar](200) NULL,
	[ClinicName] [nvarchar](2000) NULL,
	[City] [smallint] NOT NULL,
	[District] [smallint] NOT NULL,
	[ClinicPhone] [nvarchar](50) NULL,
	[Website] [nvarchar](500) NULL,
	[NumberOfDentist] [uniqueidentifier] NULL,
	[NumberOfStaff] [uniqueidentifier] NULL,
	[NumberOfChair] [uniqueidentifier] NULL,
	[UsingRC] [uniqueidentifier] NULL,
	[UsingDevices] [nvarchar](max) NULL,
	[DentistName] [nvarchar](1000) NULL,
	[Gender] [smallint] NULL,
	[Age] [uniqueidentifier] NULL,
	[DentistPhone] [varchar](50) NULL,
	[DentistEmail] [nvarchar](200) NULL,
	[Specialization] [uniqueidentifier] NULL,
	[MaritalStatus] [smallint] NULL,
	[JoinSeminar] [smallint] NULL,
	[JoinedDate] [datetime] NULL,
	[InterestingDevice] [nvarchar](max) NULL,
	[LeadTime] [uniqueidentifier] NULL,
	[SundayWork] [bit] NOT NULL,
	[SundayStart] [datetime] NULL,
	[SundayEnd] [datetime] NULL,
	[MondayWork] [bit] NOT NULL,
	[MondayStart] [datetime] NULL,
	[MondayEnd] [datetime] NULL,
	[TuesdayWork] [bit] NOT NULL,
	[TuesdayStart] [datetime] NULL,
	[TuesdayEnd] [datetime] NULL,
	[WednesdayWork] [bit] NOT NULL,
	[WednesdayStart] [datetime] NULL,
	[WednesdayEnd] [datetime] NULL,
	[ThursdayWork] [bit] NOT NULL,
	[ThursdayStart] [datetime] NULL,
	[ThursdayEnd] [datetime] NULL,
	[FridayWork] [bit] NOT NULL,
	[FridayStart] [datetime] NULL,
	[FridayEnd] [datetime] NULL,
	[SaturdayWork] [bit] NOT NULL,
	[SaturdayStart] [datetime] NULL,
	[SaturdayEnd] [datetime] NULL,
	[AssignTo] [nvarchar](max) NULL,
	[Remark] [nvarchar](max) NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[Lat] [float] NULL,
	[Lng] [float] NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
CREATE NONCLUSTERED INDEX [IDX_ClinicId] ON [dbo].[Customers] 
(
	[ClinicId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
INSERT [dbo].[Customers] ([Id], [ClinicEmail], [Address], [CreatedDate], [CustomerType], [ClinicId], [ClinicName], [City], [District], [ClinicPhone], [Website], [NumberOfDentist], [NumberOfStaff], [NumberOfChair], [UsingRC], [UsingDevices], [DentistName], [Gender], [Age], [DentistPhone], [DentistEmail], [Specialization], [MaritalStatus], [JoinSeminar], [JoinedDate], [InterestingDevice], [LeadTime], [SundayWork], [SundayStart], [SundayEnd], [MondayWork], [MondayStart], [MondayEnd], [TuesdayWork], [TuesdayStart], [TuesdayEnd], [WednesdayWork], [WednesdayStart], [WednesdayEnd], [ThursdayWork], [ThursdayStart], [ThursdayEnd], [FridayWork], [FridayStart], [FridayEnd], [SaturdayWork], [SaturdayStart], [SaturdayEnd], [AssignTo], [Remark], [CreatedBy], [Lat], [Lng]) VALUES (N'ba6ef795-bb3c-4eca-bafc-08d3ca3fbe04', N'', NULL, CAST(0x0000A66A003FDCBC AS DateTime), 30, N'fsdf', N'sdf', 0, 0, NULL, NULL, NULL, NULL, NULL, NULL, N'#7c79d5e3-c80d-1bc1-b866-08d3c67f118a#9ec768d3-305e-94cc-d102-08d3c96d0d79#4e36cab0-64cf-37c9-eccf-08d3c67f12a2#ad852192-e82c-bbcc-4229-08d3c67f10a8#89187ba8-b4fb-f5c3-8843-08d3c97da8ba', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'3b10d8f6-4c50-e1c4-5559-08d3c71e1cf9#8b936e7b-cd58-12ca-55b4-08d3c4e6ff22', NULL, 0, NULL, NULL, 0, NULL, NULL, 0, NULL, NULL, 0, NULL, NULL, 0, NULL, NULL, 0, NULL, NULL, 0, NULL, NULL, NULL, NULL, N'f3447e33-a6da-4fb4-b327-80fd3ae6ebaf', 10.842430297128885, 106.5971378780273)
INSERT [dbo].[Customers] ([Id], [ClinicEmail], [Address], [CreatedDate], [CustomerType], [ClinicId], [ClinicName], [City], [District], [ClinicPhone], [Website], [NumberOfDentist], [NumberOfStaff], [NumberOfChair], [UsingRC], [UsingDevices], [DentistName], [Gender], [Age], [DentistPhone], [DentistEmail], [Specialization], [MaritalStatus], [JoinSeminar], [JoinedDate], [InterestingDevice], [LeadTime], [SundayWork], [SundayStart], [SundayEnd], [MondayWork], [MondayStart], [MondayEnd], [TuesdayWork], [TuesdayStart], [TuesdayEnd], [WednesdayWork], [WednesdayStart], [WednesdayEnd], [ThursdayWork], [ThursdayStart], [ThursdayEnd], [FridayWork], [FridayStart], [FridayEnd], [SaturdayWork], [SaturdayStart], [SaturdayEnd], [AssignTo], [Remark], [CreatedBy], [Lat], [Lng]) VALUES (N'35426129-5710-7dc5-dea1-08d3ca6a27b6', N'', NULL, CAST(0x0000A66A00933FD3 AS DateTime), 30, N'tetewt', N'dfgdfg', 0, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, 0, NULL, NULL, 0, NULL, NULL, 0, NULL, NULL, 0, NULL, NULL, 0, NULL, NULL, 0, NULL, NULL, NULL, NULL, N'f3447e33-a6da-4fb4-b327-80fd3ae6ebaf', 10.843441876909758, 106.63250012167964)
/****** Object:  Table [dbo].[CustomerImages]    Script Date: 08/22/2016 15:57:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerImages](
	[Id] [uniqueidentifier] NOT NULL,
	[CustomerId] [uniqueidentifier] NOT NULL,
	[ImagePath] [nvarchar](100) NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_CustomerImages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_CustomerId] ON [dbo].[CustomerImages] 
(
	[CustomerId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CategoryImages]    Script Date: 08/22/2016 15:57:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CategoryImages](
	[Id] [uniqueidentifier] NOT NULL,
	[CategoryId] [uniqueidentifier] NOT NULL,
	[ImagePath] [nvarchar](100) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[Visible] [bit] NOT NULL,
 CONSTRAINT [PK_CategoryImages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 08/22/2016 15:57:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[Id] [uniqueidentifier] NOT NULL,
	[Code] [nvarchar](max) NULL,
	[ParentId] [uniqueidentifier] NULL,
	[ImagePath] [nvarchar](100) NULL,
	[SortOrder] [int] NOT NULL,
	[CategoryType] [smallint] NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Categories] ([Id], [Code], [ParentId], [ImagePath], [SortOrder], [CategoryType], [Name]) VALUES (N'19cce1df-cf5f-e3c7-f81d-08d3c4e499bf', NULL, NULL, NULL, 0, 80, N'Admin')
INSERT [dbo].[Categories] ([Id], [Code], [ParentId], [ImagePath], [SortOrder], [CategoryType], [Name]) VALUES (N'dee23f44-eda0-96c3-2785-08d3c4e49aa0', NULL, NULL, NULL, 1, 80, N'Thu ngân')
INSERT [dbo].[Categories] ([Id], [Code], [ParentId], [ImagePath], [SortOrder], [CategoryType], [Name]) VALUES (N'38f7002c-daef-a4ce-37df-08d3c4e49e8b', NULL, NULL, NULL, 2, 80, N'NVBH')
INSERT [dbo].[Categories] ([Id], [Code], [ParentId], [ImagePath], [SortOrder], [CategoryType], [Name]) VALUES (N'aebcdbf5-fa0c-52c5-57da-08d3c4e49fd8', NULL, NULL, NULL, 3, 80, N'Quản lý cửa hàng')
INSERT [dbo].[Categories] ([Id], [Code], [ParentId], [ImagePath], [SortOrder], [CategoryType], [Name]) VALUES (N'0bcee76a-c411-56c7-517d-08d3c4e4a0e2', NULL, NULL, NULL, 4, 80, N'Giám đốc')
INSERT [dbo].[Categories] ([Id], [Code], [ParentId], [ImagePath], [SortOrder], [CategoryType], [Name]) VALUES (N'c01aa7e8-a19f-1ac3-76c1-08d3c4e6fcb0', N'Water', NULL, NULL, 5, 5, N'Water')
INSERT [dbo].[Categories] ([Id], [Code], [ParentId], [ImagePath], [SortOrder], [CategoryType], [Name]) VALUES (N'6dee5e09-5bf2-78c2-c8c1-08d3c583af9a', N'Pepsi', N'c01aa7e8-a19f-1ac3-76c1-08d3c4e6fcb0', NULL, 0, 5, N'Pepsi')
INSERT [dbo].[Categories] ([Id], [Code], [ParentId], [ImagePath], [SortOrder], [CategoryType], [Name]) VALUES (N'7ba3467a-df4d-9cca-8a91-08d3c58e227a', N'Kg', NULL, NULL, 6, 10, N'Kilogram')
INSERT [dbo].[Categories] ([Id], [Code], [ParentId], [ImagePath], [SortOrder], [CategoryType], [Name]) VALUES (N'4072abb7-4483-b2cf-1c05-08d3c670f30a', NULL, NULL, NULL, 7, 70, N'rc1')
INSERT [dbo].[Categories] ([Id], [Code], [ParentId], [ImagePath], [SortOrder], [CategoryType], [Name]) VALUES (N'a729bbbf-2648-e1c6-8206-08d3c670f391', NULL, NULL, NULL, 8, 70, N'rc2')
INSERT [dbo].[Categories] ([Id], [Code], [ParentId], [ImagePath], [SortOrder], [CategoryType], [Name]) VALUES (N'a437e970-3c7c-96c8-b420-08d3c670f478', NULL, NULL, NULL, 9, 15, N'1-10')
INSERT [dbo].[Categories] ([Id], [Code], [ParentId], [ImagePath], [SortOrder], [CategoryType], [Name]) VALUES (N'8843a841-9d8f-2ec9-16f9-08d3c670f4e9', NULL, NULL, NULL, 10, 15, N'20-30')
INSERT [dbo].[Categories] ([Id], [Code], [ParentId], [ImagePath], [SortOrder], [CategoryType], [Name]) VALUES (N'280771ce-4de5-10c1-d631-08d3c670f59f', NULL, NULL, NULL, 11, 20, N'30-50')
INSERT [dbo].[Categories] ([Id], [Code], [ParentId], [ImagePath], [SortOrder], [CategoryType], [Name]) VALUES (N'b18d99d8-29ab-72c2-d9c6-08d3c670f617', NULL, NULL, NULL, 12, 20, N'50-100')
INSERT [dbo].[Categories] ([Id], [Code], [ParentId], [ImagePath], [SortOrder], [CategoryType], [Name]) VALUES (N'852cb66a-3783-dfcf-398d-08d3c670f7ac', NULL, NULL, NULL, 13, 30, N's11')
INSERT [dbo].[Categories] ([Id], [Code], [ParentId], [ImagePath], [SortOrder], [CategoryType], [Name]) VALUES (N'7d94120a-4c67-1fcc-56d9-08d3c670f812', NULL, NULL, NULL, 14, 30, N's22')
INSERT [dbo].[Categories] ([Id], [Code], [ParentId], [ImagePath], [SortOrder], [CategoryType], [Name]) VALUES (N'672d82ce-621a-39c7-f466-08d3c670f953', NULL, NULL, NULL, 15, 40, N'40-50')
INSERT [dbo].[Categories] ([Id], [Code], [ParentId], [ImagePath], [SortOrder], [CategoryType], [Name]) VALUES (N'7997d976-b771-06c9-396c-08d3c670f9d2', NULL, NULL, NULL, 16, 40, N'60-70')
INSERT [dbo].[Categories] ([Id], [Code], [ParentId], [ImagePath], [SortOrder], [CategoryType], [Name]) VALUES (N'b18ad2b1-4a3f-aec9-6860-08d3c670fb3b', NULL, NULL, NULL, 17, 50, N'10')
INSERT [dbo].[Categories] ([Id], [Code], [ParentId], [ImagePath], [SortOrder], [CategoryType], [Name]) VALUES (N'79202ca1-f818-23cc-eeca-08d3c670fb9d', NULL, NULL, NULL, 18, 50, N'20')
INSERT [dbo].[Categories] ([Id], [Code], [ParentId], [ImagePath], [SortOrder], [CategoryType], [Name]) VALUES (N'5712926a-f9b1-f4c9-291c-08d3c670fc10', NULL, NULL, NULL, 19, 50, N'> 30')
INSERT [dbo].[Categories] ([Id], [Code], [ParentId], [ImagePath], [SortOrder], [CategoryType], [Name]) VALUES (N'847810e7-b7e0-65c9-f570-08d3c670fd82', NULL, NULL, NULL, 20, 60, N'20 Months')
INSERT [dbo].[Categories] ([Id], [Code], [ParentId], [ImagePath], [SortOrder], [CategoryType], [Name]) VALUES (N'd63daf32-2ef5-97cc-9850-08d3c670fe28', NULL, NULL, NULL, 21, 60, N'30 Months')
INSERT [dbo].[Categories] ([Id], [Code], [ParentId], [ImagePath], [SortOrder], [CategoryType], [Name]) VALUES (N'ad852192-e82c-bbcc-4229-08d3c67f10a8', N'Oto', NULL, NULL, 22, 5, N'Oto')
INSERT [dbo].[Categories] ([Id], [Code], [ParentId], [ImagePath], [SortOrder], [CategoryType], [Name]) VALUES (N'7c79d5e3-c80d-1bc1-b866-08d3c67f118a', N'Benz', N'ad852192-e82c-bbcc-4229-08d3c67f10a8', NULL, 0, 5, N'Benz')
INSERT [dbo].[Categories] ([Id], [Code], [ParentId], [ImagePath], [SortOrder], [CategoryType], [Name]) VALUES (N'4e36cab0-64cf-37c9-eccf-08d3c67f12a2', N'Cadilac', N'ad852192-e82c-bbcc-4229-08d3c67f10a8', NULL, 1, 5, N'Cadilac')
INSERT [dbo].[Categories] ([Id], [Code], [ParentId], [ImagePath], [SortOrder], [CategoryType], [Name]) VALUES (N'557224d9-25fe-62c3-dd18-08d3c67f134a', N'Coca', N'c01aa7e8-a19f-1ac3-76c1-08d3c4e6fcb0', NULL, 1, 5, N'Coca')
INSERT [dbo].[Categories] ([Id], [Code], [ParentId], [ImagePath], [SortOrder], [CategoryType], [Name]) VALUES (N'996c6e85-056d-ccc0-ad98-08d3c67f14ba', N'Tank', NULL, NULL, 23, 5, N'Tank')
INSERT [dbo].[Categories] ([Id], [Code], [ParentId], [ImagePath], [SortOrder], [CategoryType], [Name]) VALUES (N'9ec768d3-305e-94cc-d102-08d3c96d0d79', N'test', N'4e36cab0-64cf-37c9-eccf-08d3c67f12a2', NULL, 0, 5, N'tet')
INSERT [dbo].[Categories] ([Id], [Code], [ParentId], [ImagePath], [SortOrder], [CategoryType], [Name]) VALUES (N'89187ba8-b4fb-f5c3-8843-08d3c97da8ba', N's1', NULL, NULL, 24, 5, N's1')
INSERT [dbo].[Categories] ([Id], [Code], [ParentId], [ImagePath], [SortOrder], [CategoryType], [Name]) VALUES (N'ea224ee0-32df-7ec8-b14c-08d3c97da944', N's2', NULL, NULL, 25, 5, N's2')
INSERT [dbo].[Categories] ([Id], [Code], [ParentId], [ImagePath], [SortOrder], [CategoryType], [Name]) VALUES (N'd029a283-f6b6-6ec5-71df-08d3c97da9d9', N's3', NULL, NULL, 26, 5, N's3')
INSERT [dbo].[Categories] ([Id], [Code], [ParentId], [ImagePath], [SortOrder], [CategoryType], [Name]) VALUES (N'85980c5d-9e0d-cfcb-7f24-08d3c97daa4e', N's4', NULL, NULL, 27, 5, N's4')
INSERT [dbo].[Categories] ([Id], [Code], [ParentId], [ImagePath], [SortOrder], [CategoryType], [Name]) VALUES (N'230ed091-7f91-36cf-42af-08d3c97daaf7', N's5', NULL, NULL, 28, 5, N's4')
INSERT [dbo].[Categories] ([Id], [Code], [ParentId], [ImagePath], [SortOrder], [CategoryType], [Name]) VALUES (N'ea0b1a68-37fd-00c8-0ead-08d3c97dab8c', N's6', NULL, NULL, 29, 5, N's6')
INSERT [dbo].[Categories] ([Id], [Code], [ParentId], [ImagePath], [SortOrder], [CategoryType], [Name]) VALUES (N'67ef812b-d19e-a0ce-4ecc-08d3c97dac42', N's7', NULL, NULL, 30, 5, N's7')
/****** Object:  Table [dbo].[BeUsers]    Script Date: 08/22/2016 15:57:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BeUsers](
	[Id] [uniqueidentifier] NOT NULL,
	[Code] [nvarchar](50) NULL,
	[Type] [smallint] NOT NULL,
	[RoleId] [uniqueidentifier] NULL,
	[HashPassword] [nvarchar](200) NULL,
	[Email] [nvarchar](100) NULL,
	[FullName] [nvarchar](500) NULL,
	[TimeZoneId] [nvarchar](50) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[LastLoginDate] [datetime] NULL,
	[ImagePath] [nvarchar](100) NULL,
	[PreferLanguage] [varchar](10) NULL,
	[Phone] [varchar](50) NULL,
	[Gender] [smallint] NULL,
	[BirthDay] [datetime] NULL,
	[Address] [nvarchar](max) NULL,
	[PositionId] [uniqueidentifier] NULL,
	[Status] [smallint] NOT NULL,
 CONSTRAINT [PK_BeUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[BeUsers] ([Id], [Code], [Type], [RoleId], [HashPassword], [Email], [FullName], [TimeZoneId], [CreatedDate], [LastLoginDate], [ImagePath], [PreferLanguage], [Phone], [Gender], [BirthDay], [Address], [PositionId], [Status]) VALUES (N'87a1a27b-2769-7acd-8035-08d3c4eae705', N'NV002', 10, N'ebe3bc80-ffe3-8bce-c7c0-08d3c5db1e45', N'Kqix/DYWGTy4TLmqpH3EoQP3//2RtOVLdVQO4PozIVVXiT5m37A1', N'test1@gmail.com', N'test1', NULL, CAST(0x0000A66300950858 AS DateTime), CAST(0x0000A6690031EF3C AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL, N'38f7002c-daef-a4ce-37df-08d3c4e49e8b', 5)
INSERT [dbo].[BeUsers] ([Id], [Code], [Type], [RoleId], [HashPassword], [Email], [FullName], [TimeZoneId], [CreatedDate], [LastLoginDate], [ImagePath], [PreferLanguage], [Phone], [Gender], [BirthDay], [Address], [PositionId], [Status]) VALUES (N'30ad1e6c-32a3-71c1-f52a-08d3c5db22e4', N'003', 10, N'9ffe3b60-1491-bdc2-727e-08d359d86fad', N'jJpvMTKf4TGq2v0upDtNUCyIrgcdUO2wuWZFy7m81k9y9AgWjQ==', N'tes343t1@gmail.com', N'sdf', NULL, CAST(0x0000A66400E202BD AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 10)
INSERT [dbo].[BeUsers] ([Id], [Code], [Type], [RoleId], [HashPassword], [Email], [FullName], [TimeZoneId], [CreatedDate], [LastLoginDate], [ImagePath], [PreferLanguage], [Phone], [Gender], [BirthDay], [Address], [PositionId], [Status]) VALUES (N'f3447e33-a6da-4fb4-b327-80fd3ae6ebaf', N'002', 5, NULL, N'YobAo1AvgIyZ+Jb238aStXcDn2i0205BZZgWGmoIBPAsYhdISAQ=', N'anhductask@gmail.com', N'Super', NULL, CAST(0x0000A49900767830 AS DateTime), CAST(0x0000A66A003468D8 AS DateTime), NULL, NULL, NULL, NULL, CAST(0x0000A5BB00000000 AS DateTime), NULL, NULL, 5)
/****** Object:  Table [dbo].[Artices]    Script Date: 08/22/2016 15:57:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Artices](
	[Id] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ImagePath] [nvarchar](50) NULL,
	[Status] [smallint] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Content] [nvarchar](max) NULL,
 CONSTRAINT [PK_Artices] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Artices] ([Id], [CreatedDate], [ImagePath], [Status], [Name], [Description], [Content]) VALUES (N'802f2bf5-0664-9cc7-33af-08d3c6a875ad', CAST(0x0000A66500EA04A8 AS DateTime), NULL, 5, N'Sdf', NULL, NULL)
/****** Object:  Table [dbo].[AppSettings]    Script Date: 08/22/2016 15:57:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppSettings](
	[Id] [uniqueidentifier] NOT NULL,
	[SettingType] [smallint] NOT NULL,
	[Name] [nvarchar](500) NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AppSettings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'15613b9b-b2df-48a4-bdc5-00422a862484', 15, N'Maine', N'ME')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'95e8ad5e-ddfa-451e-86da-019be8e151e4', 15, N'West Virginia', N'WV')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'4aacccc3-70e7-c8ce-7825-08d2989d496b', 5, N'SmtpUserName', N'prettyshopvnhcm@gmail.com')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'9c09819d-9808-1ac0-4c3a-08d2989eed71', 5, N'SmtpFullName', N'Kahchan Fashion')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'7a42cc02-e783-2fcb-1165-08d2989ef067', 5, N'SmtpPort', N'587')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'f1c86f91-379a-99cf-a5dc-08d2989ef25e', 5, N'SmtpPassword', N'prettyshopvnhcmasd123')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'd34930c5-d396-9cca-a84c-08d2989ef43b', 5, N'SmtpServer', N'smtp.gmail.com')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'bebb89d3-3fb9-a8c3-48f0-08d2989ef626', 5, N'SmtpEnableSsl', N'yes')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'1196bfab-b96a-a1c6-5bd5-08d2989ef794', 5, N'SmtpSubAccount', NULL)
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'46a21a5c-296d-0ec2-9f23-08d2989efeb9', 5, N'SmtpFrom', N'prettyshopvnhcm@gmail.com')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'db4135a0-ea3a-97cb-18a0-08d2989f0050', 5, N'SmtpSendingDomain', N'shopmaps.vn')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'd42a1969-d28f-bac8-206e-08d29cb4bf20', 20, N'EmailNotification', N'anhductask@gmail.com')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'57717b2a-a805-f2c0-dace-08d29e19c3e7', 30, N'DATE_FORMAT_JS', N'MMM DD, YYYY')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'9622f57b-650d-0bc0-4818-08d29e19d4f2', 30, N'DATE_FORMAT_CS', N'MMM dd, yyyy')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'54813896-8603-82c1-bf0c-08d29e19d78e', 30, N'MONTH_FORMAT_CS', N'MMM yyyy')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'7beb1712-621e-99c6-38cd-08d29e19d99b', 30, N'DATE_TIME_FORMAT_CS', N'MMM dd, yyyy hh:mm tt')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'fd7e3a27-a587-ddc2-ec6b-08d2a2359858', 40, N'WebGMapDefaultZoom', N'17')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'7ffa0f0e-94d2-c1cc-1242-08d2a2d0efa8', 30, N'DATE_TIME_FORMAT_JS', N'MMM DD, YYYY h:m a')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'02456a02-4ca1-15c0-f5d4-08d2a3020f16', 45, N'CompanyReportCurrency', N'USD')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'1346a285-86ac-32c4-cab4-08d2a6bb3cdb', 30, N'TIME_FORMAT_CS', N'hh:mm tt')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'59e6a76e-0c25-c1c2-1866-08d2aeaf03f7', 30, N'DATE_FORMAT_DEVICE_APP', N'MMM dd, yyyy ')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'b41554c5-54f2-21c4-5068-08d2aeaf0d5c', 30, N'DATE_TIME_FORMAT_DEVICE_APP', N'MMM dd, yyyy hh:mm')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'8f6a44f3-1f46-09c4-2d1b-08d2b8191544', 20, N'AdminNotifyLanguage', N'vi-VN')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'b1ca8bae-8b88-dfcf-36b3-08d33376907e', 55, N'SmsVendor', N'Twilio')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'11b4d242-8f49-b3c9-ec84-08d333769896', 55, N'Twilio_AccountSid', N'ACefce8806694da5855423a1c82421ba1c')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'480066a8-a8ca-3dc8-1d28-08d333769abe', 55, N'Twilio_AuthToken', N'5a4146f34412f0a45b6883b6e4054a8f')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'ad4585be-b190-61c4-4251-08d333769cac', 55, N'Twilio_FromPhone', N'4242950673')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'621780aa-3bcf-55cc-34c8-08d339cc9ef2', 50, N'SiteLogoImagePath', N'8af7860e-be30-43ef-bacc-1c00b1bd8a76.png')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'7f9c4443-7a59-0dc3-b036-08d33f53775d', 35, N'StoreSiteColor', N'green')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'46bb74bc-0e63-73ce-e379-08d35524eb3d', 35, N'DefaultDisplayLanguage', N'en-US')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'002877f4-7d92-78cd-0643-08d355e2f69a', 35, N'AdminSiteColor', N'orange-blue')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'ded67862-1a92-b5ce-194c-08d3570f7669', 35, N'Currency', N'VND')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'd92b9ac4-111d-95cb-0673-08d358bc3afd', 60, N'Address', N'12 Đường số 8 - Phường Bình An - Quận 2 - Tp HCM')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'ab475af0-5e30-b1ca-f2a9-08d358bc3d9e', 60, N'Phone', NULL)
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'80ea3a2b-473d-4fc2-2ee8-08d358bc400e', 60, N'Email', NULL)
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'f5e65770-8b9c-2bcf-09d1-08d358bc4578', 60, N'Skype', NULL)
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'a3ce20c1-aeda-1ec5-089f-08d3590c2bdd', 60, N'Copyright', NULL)
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'24e52fad-9778-65c6-8f9a-08d35ac707dd', 35, N'ExchangeRateToUSD', N'22325')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'9e29d2c9-d3f2-dec2-080c-08d35e9e9c44', 35, N'FacebookFanPageUrl', N'https://www.facebook.com/FanpageKarma/')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'9093a98e-e54e-b1c5-03a7-08d35e9e9f4f', 35, N'TwitterFanPageUrl', NULL)
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'4273a8ad-3d70-ddc9-1188-08d35e9ea04c', 35, N'GooglePlusFanPageUrl', NULL)
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'7b7e955c-5e7a-92ca-f332-08d3c1aee466', 50, N'SiteName', N'SAMSUN VINA')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'fde08d62-6247-34c5-27a8-08d3c2642634', 50, N'SiteLogoImagePath2', NULL)
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'c61c66e1-e3e0-fcc5-3686-08d3c2642635', 50, N'SiteLogoImagePath3', N'5346d5e5-ed32-4703-a452-4ae4fd3584e8.ico')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'27981efd-50de-40e6-904c-0b03acb39f44', 15, N'Iowa', N'IA')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'78764616-d595-4436-afb9-2205ec676f16', 15, N'South Carolina', N'SC')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'16a2ffed-284c-47df-a157-336c5dbfc5de', 15, N'New Hampshire', N'NH')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'5cb3d177-a737-4039-a464-34802eaec646', 15, N'Montana', N'MT')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'269cfa2f-c76c-428a-b244-391293094fd5', 15, N'Idaho', N'ID')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'32587551-eb14-442a-b3f0-3ba451c2230a', 15, N'Illinois', N'IL')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'91311398-2133-4d23-8d78-3db6587deeb3', 15, N'Utah', N'UT')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'92f08c91-24b9-41f8-a163-3eb329917bbb', 15, N'Mississippi', N'MS')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'aa92d4ca-374d-4927-bf63-41062ea0f288', 15, N'New York', N'NY')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'a92536e0-c11c-42c3-9098-4dc1f7ca2127', 15, N'Wyoming', N'WY')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'e65f48de-b8bf-4cb3-b09f-62b04d8fc2e0', 15, N'Tennessee', N'TN')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'5d43b8d2-c890-4590-b191-66f8e8d0f39e', 15, N'Arkansas', N'AR')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'beb577d4-a938-4a0f-b2a2-6a19cead5bf2', 15, N'Ohio', N'OH')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'cd4011d6-0cc6-474b-ae03-6bae06b016a1', 15, N'District of Columbia', N'DC')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'3c5b9939-03b8-4547-a3f3-6cb00ff00024', 15, N'Nevada', N'NV')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'6a23865b-e86d-4eca-b2a0-6f2d4d20ef5b', 15, N'Louisiana', N'LA')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'3ffeb4bf-bd6e-4494-af8b-76867ba93d59', 15, N'Michigan', N'MI')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'c791b516-0049-437c-8c30-7d5cb5b97e05', 15, N'Nebraska', N'NE')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'56a71a48-3a36-46e1-a56f-7ee6cd80438c', 15, N'Virginia', N'VA')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'ac7b3d65-7de3-4e26-ba3f-84f7fee2b577', 15, N'Minnesota', N'MN')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'a548d3d0-b18d-4623-8aeb-85ff5c84650d', 15, N'Kentucky', N'KY')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'97cded5a-392c-4567-a05f-8d186881302b', 15, N'Indiana', N'IN')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'fee9bc32-de45-4f3c-a845-902e36d9f9e3', 15, N'Georgia', N'GA')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'a961497a-d53d-42bc-9abc-922fa8b7c996', 15, N'Florida', N'FL')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'3419bcd7-133c-4651-a004-945480697468', 15, N'Vermont', N'VT')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'9b8923af-7e89-43db-a721-9495d874fe07', 15, N'Pennsylvania', N'PA')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'e5698366-ce5e-469d-b608-96d78bacec3b', 15, N'North Carolina', N'NC')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'c6cdad0b-0700-4e90-a4ea-982cd21f0f40', 15, N'California', N'CA')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'4ec0004e-49e5-4810-a40a-998a4b01a877', 15, N'New Mexico', N'NM')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'9fc83f10-6be0-41ae-9e2d-9afe6e6bdf21', 15, N'Maryland', N'MD')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'2b783b4b-ec4d-419b-a99c-9b6fcd28eef0', 15, N'New Jersey', N'NJ')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'd867be12-8c6a-420e-9bc5-9ef1196960a5', 15, N'Washington', N'WA')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'79479489-4f0e-4281-8e30-a12a873591c5', 15, N'Alaska', N'AK')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'c172c9bf-2d79-4050-9067-a616576f3c6d', 15, N'Wisconsin', N'WI')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'4a5a87a0-7bba-40e5-9f12-ae3f479a0ed4', 15, N'South Dakota', N'SD')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'd9fed79c-ccad-43dd-a326-b0f26ac9b9d2', 15, N'Colorado', N'CO')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'e7ad2a17-3668-4c0b-be2c-b0fb8839395f', 15, N'Delaware', N'DE')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'd807b81a-1755-4ff6-bb60-bbd4a5ada47c', 15, N'Missouri', N'MO')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'b1238476-e1c8-47e5-89a7-bc2d9fd0ca25', 15, N'Rhode Island', N'RI')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'138fa404-2cac-4cbd-8d66-be0fcddba332', 15, N'Connecticut', N'CT')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'76bed607-2f58-438d-911c-cf4bb903ed66', 15, N'North Dakota', N'ND')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'4d0c94d8-1fcd-4048-8a1b-da631382ff60', 15, N'Oklahoma', N'OK')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'a11d646a-50ae-43ef-953c-de2344ec8661', 15, N'Texas', N'TX')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'f15e4719-b060-484e-9a78-e0945f3b18e6', 15, N'Kansas', N'KS')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'8ed44433-ff1c-4a5e-a571-e676553ca769', 15, N'Hawaii', N'HI')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'c997b296-a496-4eac-b6df-f1b57e6bf50c', 15, N'Alabama', N'AL')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'd62a5e09-301c-4c0e-9ed1-f4f2a94dcf1d', 15, N'Oregon', N'OR')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'1aa7c026-14c5-4ee1-847d-fad029dc5432', 15, N'Massachusetts', N'MA')
INSERT [dbo].[AppSettings] ([Id], [SettingType], [Name], [Value]) VALUES (N'eb5e469f-5c7f-4bdd-a438-ff5779595f8e', 15, N'Arizona', N'AZ')
/****** Object:  Table [dbo].[SystemEmailTemplates]    Script Date: 08/22/2016 15:57:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemEmailTemplates](
	[Id] [uniqueidentifier] NOT NULL,
	[Type] [smallint] NOT NULL,
	[Language] [nvarchar](10) NULL,
	[Subject] [nvarchar](max) NULL,
	[Content] [nvarchar](max) NULL,
 CONSTRAINT [PK_SystemEmailTemplates] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[SystemEmailTemplates] ([Id], [Type], [Language], [Subject], [Content]) VALUES (N'ec071b90-49e9-27c8-093b-08d29802c72c', 5, N'en-US', N'Recover password email', N'<div>Hello [FName]</div>

<div>Please flow link to recover your password :</div>

<div><a href="[LinkRecoverPassword]">[LinkRecoverPassword]</a></div>
')
INSERT [dbo].[SystemEmailTemplates] ([Id], [Type], [Language], [Subject], [Content]) VALUES (N'6b968691-5838-f9cd-d53d-08d29cb57e1d', 10, N'en-US', N'Contact Us - Email Notification', N'<div>[Name]</div>

<div>[Email]</div>

<div>[Phone]</div>

<div>[Message]</div>

<div>&nbsp;</div>
')
INSERT [dbo].[SystemEmailTemplates] ([Id], [Type], [Language], [Subject], [Content]) VALUES (N'd0e0b0f2-765d-78c6-ca6f-08d355560e33', 5, N'vi-VN', N'Khôi Phục Mật Khẩu', N'<div>Xin ch&agrave;o [FName]</div>

<div>H&atilde;y click v&agrave;o link sau để kh&ocirc;i phục mật khẩu:</div>

<div><a href="[LinkRecoverPassword]">[LinkRecoverPassword]</a></div>
')
INSERT [dbo].[SystemEmailTemplates] ([Id], [Type], [Language], [Subject], [Content]) VALUES (N'7c5e7528-fa30-26c5-f4c1-08d35556113d', 10, N'vi-VN', N'Khách Hàng Liên Hệ', N'<div>
<div>[Name]</div>

<div>[Email]</div>

<div>[Phone]</div>

<div>[Message]</div>
</div>
')
INSERT [dbo].[SystemEmailTemplates] ([Id], [Type], [Language], [Subject], [Content]) VALUES (N'97a00e4e-2eee-3bcd-3ff2-08d359dfb77b', 20, N'vi-VN', N'Xác nhận định danh [ExternalTypeName]', N'<div>H&atilde;y click v&agrave;o li&ecirc;n kết sau để x&aacute;c nhận định danh&nbsp;[ExternalTypeName] của bạn:</div>

<div><a href="[Link]">[Link]</a></div>

<div>&nbsp;</div>

<div>**Ghi ch&uacute;: h&atilde;y bỏ qua thư n&agrave;y nếu như kh&ocirc;ng phải bạn.</div>
')
INSERT [dbo].[SystemEmailTemplates] ([Id], [Type], [Language], [Subject], [Content]) VALUES (N'fac2a97f-c63c-ddc7-fc87-08d35adc4653', 30, N'vi-VN', N'Có đơn hàng mới - [OrderCode]', N'<div>M&atilde; đơn h&agrave;ng:&nbsp;[OrderCode]</div>
')
INSERT [dbo].[SystemEmailTemplates] ([Id], [Type], [Language], [Subject], [Content]) VALUES (N'ad7f4410-a13e-42cb-c030-08d3c8cb2995', 40, N'en-US', N'SamSunVian - Account has been created successfull', N'<div>SamSunVian - Account has been created successfull</div>
')
/****** Object:  StoredProcedure [dbo].[spRiderCrediCardDisableAll]    Script Date: 08/22/2016 15:57:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spRiderCrediCardDisableAll]
@RiderId uniqueidentifier
as
begin

update dbo.RiderCreditCards
set [Enable] = 0
where RiderId = @RiderId

end
GO
/****** Object:  StoredProcedure [dbo].[spGetLocalEvents]    Script Date: 08/22/2016 15:57:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spGetLocalEvents]
@DistanceQuery float,
@Lat float,
@Lng float,
@ActiveStatus smallint,
@EventCategory smallint
as
begin

select *
from dbo.LocalEvents (nolock) d
where d.EventCategory = @EventCategory
and (ACOS(SIN(PI()*d.Lat/180.0)*SIN(PI()*@Lat/180.0)+COS(PI()*d.Lat/180.0)*COS(PI()*@Lat/180.0)*COS(PI()*@Lng/180.0-PI()*d.Lng/180.0))*6371) <= @DistanceQuery
and d.Status = @ActiveStatus

end
GO
/****** Object:  StoredProcedure [dbo].[spGetCategoryLocalEvents]    Script Date: 08/22/2016 15:57:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[spGetCategoryLocalEvents]
@DistanceQuery float,
@Lat float,
@Lng float,
@ActiveStatus smallint
as
begin

select distinct EventCategory
		, ImagePath
from dbo.LocalEvents (nolock) d
where (ACOS(SIN(PI()*d.Lat/180.0)*SIN(PI()*@Lat/180.0)+COS(PI()*d.Lat/180.0)*COS(PI()*@Lat/180.0)*COS(PI()*@Lng/180.0-PI()*d.Lng/180.0))*6371) <= @DistanceQuery
and d.Status = @ActiveStatus
end
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 08/22/2016 15:57:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](200) NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Roles] ([Id], [Name], [Description]) VALUES (N'9ffe3b60-1491-bdc2-727e-08d359d86fad', N'Vistors', NULL)
INSERT [dbo].[Roles] ([Id], [Name], [Description]) VALUES (N'ebe3bc80-ffe3-8bce-c7c0-08d3c5db1e45', N'Staff', NULL)
/****** Object:  Table [dbo].[RolePermissions]    Script Date: 08/22/2016 15:57:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RolePermissions](
	[Id] [uniqueidentifier] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
	[PageId] [smallint] NULL,
	[CanView] [bit] NOT NULL,
	[CanUpdate] [bit] NOT NULL,
	[CanDelete] [bit] NOT NULL,
	[CanPrint] [bit] NOT NULL,
	[CanExportExcel] [bit] NOT NULL,
	[CanImportExcel] [bit] NOT NULL,
	[CanAdd] [bit] NOT NULL,
 CONSTRAINT [PK_RolePermissions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PageId], [CanView], [CanUpdate], [CanDelete], [CanPrint], [CanExportExcel], [CanImportExcel], [CanAdd]) VALUES (N'0ffd808c-65f0-ecca-8709-08d359d86fb9', N'9ffe3b60-1491-bdc2-727e-08d359d86fad', 65, 1, 0, 0, 0, 0, 0, 0)
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PageId], [CanView], [CanUpdate], [CanDelete], [CanPrint], [CanExportExcel], [CanImportExcel], [CanAdd]) VALUES (N'927a27c5-8b7d-7ec7-8713-08d359d86fb9', N'9ffe3b60-1491-bdc2-727e-08d359d86fad', 30, 1, 0, 0, 0, 0, 0, 0)
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PageId], [CanView], [CanUpdate], [CanDelete], [CanPrint], [CanExportExcel], [CanImportExcel], [CanAdd]) VALUES (N'ace6dfd6-1a6c-a0c8-8719-08d359d86fb9', N'9ffe3b60-1491-bdc2-727e-08d359d86fad', 100, 1, 0, 0, 0, 0, 0, 0)
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PageId], [CanView], [CanUpdate], [CanDelete], [CanPrint], [CanExportExcel], [CanImportExcel], [CanAdd]) VALUES (N'05b893de-f45f-26c8-871c-08d359d86fb9', N'9ffe3b60-1491-bdc2-727e-08d359d86fad', 75, 1, 0, 0, 0, 0, 0, 0)
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PageId], [CanView], [CanUpdate], [CanDelete], [CanPrint], [CanExportExcel], [CanImportExcel], [CanAdd]) VALUES (N'44640b1b-0687-c2c8-8723-08d359d86fb9', N'9ffe3b60-1491-bdc2-727e-08d359d86fad', 110, 1, 0, 0, 0, 0, 0, 0)
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PageId], [CanView], [CanUpdate], [CanDelete], [CanPrint], [CanExportExcel], [CanImportExcel], [CanAdd]) VALUES (N'b86e8c4c-a9e7-41c3-8729-08d359d86fb9', N'9ffe3b60-1491-bdc2-727e-08d359d86fad', 90, 1, 0, 0, 0, 0, 0, 0)
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PageId], [CanView], [CanUpdate], [CanDelete], [CanPrint], [CanExportExcel], [CanImportExcel], [CanAdd]) VALUES (N'e2e26dba-2ebb-86c5-872c-08d359d86fb9', N'9ffe3b60-1491-bdc2-727e-08d359d86fad', 45, 1, 0, 0, 0, 0, 0, 0)
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PageId], [CanView], [CanUpdate], [CanDelete], [CanPrint], [CanExportExcel], [CanImportExcel], [CanAdd]) VALUES (N'168dee52-a2b4-c7c5-371f-08d35c3293fe', N'9ffe3b60-1491-bdc2-727e-08d359d86fad', 5, 1, 0, 0, 0, 0, 0, 0)
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PageId], [CanView], [CanUpdate], [CanDelete], [CanPrint], [CanExportExcel], [CanImportExcel], [CanAdd]) VALUES (N'6ccccf2f-b671-96c3-3746-08d35c3293fe', N'9ffe3b60-1491-bdc2-727e-08d359d86fad', 25, 1, 0, 0, 0, 0, 0, 0)
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PageId], [CanView], [CanUpdate], [CanDelete], [CanPrint], [CanExportExcel], [CanImportExcel], [CanAdd]) VALUES (N'fb4ab5f2-991b-6bcc-374b-08d35c3293fe', N'9ffe3b60-1491-bdc2-727e-08d359d86fad', 35, 1, 0, 0, 0, 0, 0, 0)
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PageId], [CanView], [CanUpdate], [CanDelete], [CanPrint], [CanExportExcel], [CanImportExcel], [CanAdd]) VALUES (N'7ceddb87-5ecd-45ca-be84-08d3c26f4341', N'9ffe3b60-1491-bdc2-727e-08d359d86fad', 230, 1, 0, 0, 0, 0, 0, 0)
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PageId], [CanView], [CanUpdate], [CanDelete], [CanPrint], [CanExportExcel], [CanImportExcel], [CanAdd]) VALUES (N'28d81761-37ce-5bc7-be94-08d3c26f4341', N'9ffe3b60-1491-bdc2-727e-08d359d86fad', 240, 1, 0, 0, 0, 0, 0, 0)
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PageId], [CanView], [CanUpdate], [CanDelete], [CanPrint], [CanExportExcel], [CanImportExcel], [CanAdd]) VALUES (N'12784298-5ee7-bfc2-3c9b-08d3c5db1e49', N'ebe3bc80-ffe3-8bce-c7c0-08d3c5db1e45', 320, 1, 1, 0, 0, 0, 0, 1)
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PageId], [CanView], [CanUpdate], [CanDelete], [CanPrint], [CanExportExcel], [CanImportExcel], [CanAdd]) VALUES (N'c13a3b49-6cf1-06c5-3ca1-08d3c5db1e49', N'ebe3bc80-ffe3-8bce-c7c0-08d3c5db1e45', 230, 1, 1, 0, 0, 0, 0, 1)
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PageId], [CanView], [CanUpdate], [CanDelete], [CanPrint], [CanExportExcel], [CanImportExcel], [CanAdd]) VALUES (N'0d6c6131-7651-c4c0-3ca3-08d3c5db1e49', N'ebe3bc80-ffe3-8bce-c7c0-08d3c5db1e45', 75, 1, 1, 0, 0, 0, 0, 1)
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PageId], [CanView], [CanUpdate], [CanDelete], [CanPrint], [CanExportExcel], [CanImportExcel], [CanAdd]) VALUES (N'f3a6d51b-b06c-b4cd-3ca4-08d3c5db1e49', N'ebe3bc80-ffe3-8bce-c7c0-08d3c5db1e45', 250, 1, 1, 0, 0, 0, 0, 1)
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PageId], [CanView], [CanUpdate], [CanDelete], [CanPrint], [CanExportExcel], [CanImportExcel], [CanAdd]) VALUES (N'3551d7a2-25fe-a1c4-3caf-08d3c5db1e49', N'ebe3bc80-ffe3-8bce-c7c0-08d3c5db1e45', 330, 1, 1, 0, 0, 0, 0, 1)
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PageId], [CanView], [CanUpdate], [CanDelete], [CanPrint], [CanExportExcel], [CanImportExcel], [CanAdd]) VALUES (N'da82df24-b374-adc3-3cb1-08d3c5db1e49', N'ebe3bc80-ffe3-8bce-c7c0-08d3c5db1e45', 240, 1, 1, 0, 0, 0, 0, 1)
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PageId], [CanView], [CanUpdate], [CanDelete], [CanPrint], [CanExportExcel], [CanImportExcel], [CanAdd]) VALUES (N'f727c48b-161d-abca-3cb2-08d3c5db1e49', N'ebe3bc80-ffe3-8bce-c7c0-08d3c5db1e45', 25, 1, 1, 0, 0, 0, 0, 1)
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PageId], [CanView], [CanUpdate], [CanDelete], [CanPrint], [CanExportExcel], [CanImportExcel], [CanAdd]) VALUES (N'62a57d69-1477-2bca-3cb3-08d3c5db1e49', N'ebe3bc80-ffe3-8bce-c7c0-08d3c5db1e45', 5, 1, 1, 0, 0, 0, 0, 1)
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PageId], [CanView], [CanUpdate], [CanDelete], [CanPrint], [CanExportExcel], [CanImportExcel], [CanAdd]) VALUES (N'c64677f0-63c7-64cd-3cb4-08d3c5db1e49', N'ebe3bc80-ffe3-8bce-c7c0-08d3c5db1e45', 35, 1, 1, 0, 0, 0, 0, 1)
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PageId], [CanView], [CanUpdate], [CanDelete], [CanPrint], [CanExportExcel], [CanImportExcel], [CanAdd]) VALUES (N'5ce66b18-b991-e8cd-3cb5-08d3c5db1e49', N'ebe3bc80-ffe3-8bce-c7c0-08d3c5db1e45', 30, 1, 1, 0, 0, 0, 0, 1)
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PageId], [CanView], [CanUpdate], [CanDelete], [CanPrint], [CanExportExcel], [CanImportExcel], [CanAdd]) VALUES (N'69cb2644-0895-f9cb-3cb6-08d3c5db1e49', N'ebe3bc80-ffe3-8bce-c7c0-08d3c5db1e45', 65, 1, 1, 0, 0, 0, 0, 1)
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PageId], [CanView], [CanUpdate], [CanDelete], [CanPrint], [CanExportExcel], [CanImportExcel], [CanAdd]) VALUES (N'cf974a46-7c01-5cc6-3cb8-08d3c5db1e49', N'ebe3bc80-ffe3-8bce-c7c0-08d3c5db1e45', 260, 1, 1, 0, 0, 0, 0, 1)
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PageId], [CanView], [CanUpdate], [CanDelete], [CanPrint], [CanExportExcel], [CanImportExcel], [CanAdd]) VALUES (N'56c15e63-8685-5bc0-3cbd-08d3c5db1e49', N'ebe3bc80-ffe3-8bce-c7c0-08d3c5db1e45', 310, 1, 1, 0, 0, 0, 0, 1)
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PageId], [CanView], [CanUpdate], [CanDelete], [CanPrint], [CanExportExcel], [CanImportExcel], [CanAdd]) VALUES (N'b98984ed-6f75-8cc4-3cbe-08d3c5db1e49', N'ebe3bc80-ffe3-8bce-c7c0-08d3c5db1e45', 290, 1, 1, 0, 0, 0, 0, 1)
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PageId], [CanView], [CanUpdate], [CanDelete], [CanPrint], [CanExportExcel], [CanImportExcel], [CanAdd]) VALUES (N'719999cc-027f-6dc3-3cbf-08d3c5db1e49', N'ebe3bc80-ffe3-8bce-c7c0-08d3c5db1e45', 280, 1, 1, 0, 0, 0, 0, 1)
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PageId], [CanView], [CanUpdate], [CanDelete], [CanPrint], [CanExportExcel], [CanImportExcel], [CanAdd]) VALUES (N'c75ec427-71b4-92cf-3cc0-08d3c5db1e49', N'ebe3bc80-ffe3-8bce-c7c0-08d3c5db1e45', 110, 1, 1, 0, 0, 0, 0, 1)
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PageId], [CanView], [CanUpdate], [CanDelete], [CanPrint], [CanExportExcel], [CanImportExcel], [CanAdd]) VALUES (N'127dba1e-3aec-6ec1-3cc1-08d3c5db1e49', N'ebe3bc80-ffe3-8bce-c7c0-08d3c5db1e45', 90, 1, 1, 0, 0, 0, 0, 1)
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PageId], [CanView], [CanUpdate], [CanDelete], [CanPrint], [CanExportExcel], [CanImportExcel], [CanAdd]) VALUES (N'2f31c0fb-fa70-6bc3-3cc2-08d3c5db1e49', N'ebe3bc80-ffe3-8bce-c7c0-08d3c5db1e45', 45, 1, 1, 0, 0, 0, 0, 1)
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PageId], [CanView], [CanUpdate], [CanDelete], [CanPrint], [CanExportExcel], [CanImportExcel], [CanAdd]) VALUES (N'7495ae6b-9df3-4ec8-3cc3-08d3c5db1e49', N'ebe3bc80-ffe3-8bce-c7c0-08d3c5db1e45', 300, 1, 1, 0, 0, 0, 0, 1)
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PageId], [CanView], [CanUpdate], [CanDelete], [CanPrint], [CanExportExcel], [CanImportExcel], [CanAdd]) VALUES (N'726a6664-a8fc-13c5-3cc5-08d3c5db1e49', N'ebe3bc80-ffe3-8bce-c7c0-08d3c5db1e45', 350, 1, 1, 0, 0, 0, 0, 1)
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PageId], [CanView], [CanUpdate], [CanDelete], [CanPrint], [CanExportExcel], [CanImportExcel], [CanAdd]) VALUES (N'458ccdf7-ccf1-88c0-3cca-08d3c5db1e49', N'ebe3bc80-ffe3-8bce-c7c0-08d3c5db1e45', 270, 1, 1, 0, 0, 0, 0, 1)
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PageId], [CanView], [CanUpdate], [CanDelete], [CanPrint], [CanExportExcel], [CanImportExcel], [CanAdd]) VALUES (N'dbd4d1d8-ca00-57c7-3ccb-08d3c5db1e49', N'ebe3bc80-ffe3-8bce-c7c0-08d3c5db1e45', 340, 1, 1, 0, 0, 0, 0, 1)
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PageId], [CanView], [CanUpdate], [CanDelete], [CanPrint], [CanExportExcel], [CanImportExcel], [CanAdd]) VALUES (N'ca2d020e-cae2-a4cd-5863-08d3c672e463', N'ebe3bc80-ffe3-8bce-c7c0-08d3c5db1e45', 360, 1, 1, 0, 0, 0, 0, 1)
INSERT [dbo].[RolePermissions] ([Id], [RoleId], [PageId], [CanView], [CanUpdate], [CanDelete], [CanPrint], [CanExportExcel], [CanImportExcel], [CanAdd]) VALUES (N'7990d07a-ab42-23ca-5873-08d3c672e463', N'ebe3bc80-ffe3-8bce-c7c0-08d3c5db1e45', 370, 1, 1, 0, 0, 0, 0, 1)
/****** Object:  ForeignKey [FK_RolePermissions_Roles]    Script Date: 08/22/2016 15:57:54 ******/
ALTER TABLE [dbo].[RolePermissions]  WITH CHECK ADD  CONSTRAINT [FK_RolePermissions_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RolePermissions] CHECK CONSTRAINT [FK_RolePermissions_Roles]
GO
