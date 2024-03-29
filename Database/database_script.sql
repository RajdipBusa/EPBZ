USE [master]
GO
/****** Object:  Database [EPBZ]    Script Date: 2024/02/01 1:07:17 AM ******/
CREATE DATABASE [EPBZ]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'EPBZ', FILENAME = N'D:\SqlServer\MSSQL15.MSSQLSERVER\MSSQL\DATA\EPBZ.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'EPBZ_log', FILENAME = N'D:\SqlServer\MSSQL15.MSSQLSERVER\MSSQL\DATA\EPBZ_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [EPBZ] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [EPBZ].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [EPBZ] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [EPBZ] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [EPBZ] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [EPBZ] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [EPBZ] SET ARITHABORT OFF 
GO
ALTER DATABASE [EPBZ] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [EPBZ] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [EPBZ] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [EPBZ] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [EPBZ] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [EPBZ] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [EPBZ] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [EPBZ] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [EPBZ] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [EPBZ] SET  DISABLE_BROKER 
GO
ALTER DATABASE [EPBZ] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [EPBZ] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [EPBZ] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [EPBZ] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [EPBZ] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [EPBZ] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [EPBZ] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [EPBZ] SET RECOVERY FULL 
GO
ALTER DATABASE [EPBZ] SET  MULTI_USER 
GO
ALTER DATABASE [EPBZ] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [EPBZ] SET DB_CHAINING OFF 
GO
ALTER DATABASE [EPBZ] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [EPBZ] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [EPBZ] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [EPBZ] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'EPBZ', N'ON'
GO
ALTER DATABASE [EPBZ] SET QUERY_STORE = OFF
GO
USE [EPBZ]
GO
/****** Object:  Table [dbo].[Application]    Script Date: 2024/02/01 1:07:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Application](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[Address] [nvarchar](max) NULL,
	[Gender] [bit] NOT NULL,
	[Contact] [nvarchar](50) NULL,
	[PreferredLocation] [nvarchar](max) NULL,
	[ECTC] [numeric](18, 2) NULL,
	[CCTC] [numeric](18, 2) NULL,
	[Notice] [int] NULL,
 CONSTRAINT [PK_Application] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Education]    Script Date: 2024/02/01 1:07:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Education](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AppId] [int] NOT NULL,
	[Board] [nvarchar](max) NULL,
	[Year] [int] NULL,
	[CGPA] [numeric](18, 2) NULL,
 CONSTRAINT [PK_Education] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Language]    Script Date: 2024/02/01 1:07:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Language](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AppId] [int] NOT NULL,
	[LanguageName] [nvarchar](50) NULL,
	[CanRead] [bit] NULL,
	[CanWrite] [bit] NULL,
	[CanSpeak] [bit] NULL,
 CONSTRAINT [PK_Language] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Skills]    Script Date: 2024/02/01 1:07:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Skills](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AppId] [int] NOT NULL,
	[SkillName] [nvarchar](max) NULL,
	[SkillRate] [int] NULL,
 CONSTRAINT [PK_Skills] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 2024/02/01 1:07:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[UserName] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[Password] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](50) NULL,
	[RegisteredDate] [datetime2](7) NULL,
	[LoginDate] [datetime2](7) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Work]    Script Date: 2024/02/01 1:07:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Work](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AppId] [int] NOT NULL,
	[Company] [nvarchar](max) NULL,
	[Designation] [nvarchar](max) NULL,
	[FromDate] [datetime2](7) NULL,
	[ToDate] [datetime2](7) NULL,
 CONSTRAINT [PK_Work] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Application] ON 

INSERT [dbo].[Application] ([Id], [Name], [Email], [Address], [Gender], [Contact], [PreferredLocation], [ECTC], [CCTC], [Notice]) VALUES (2, N'werwer', N'werywireu@wrwer.com', N'sfdsdkhjh', 0, N'kjhkjhk', N'Location1', CAST(23424.00 AS Numeric(18, 2)), CAST(234.00 AS Numeric(18, 2)), NULL)
INSERT [dbo].[Application] ([Id], [Name], [Email], [Address], [Gender], [Contact], [PreferredLocation], [ECTC], [CCTC], [Notice]) VALUES (4, N'kaka', N'kaka@akakkak.aaaa', N'kakaka', 1, N'kkkk', N'Location3', CAST(555555.00 AS Numeric(18, 2)), CAST(55555555.00 AS Numeric(18, 2)), 555555555)
INSERT [dbo].[Application] ([Id], [Name], [Email], [Address], [Gender], [Contact], [PreferredLocation], [ECTC], [CCTC], [Notice]) VALUES (5, N'aaaaaaaaa', N'aaaa@aaa.aa', N'aaaaaaaa', 0, N'aaaaaaaaa', N'Location1', CAST(1111.00 AS Numeric(18, 2)), CAST(11111.00 AS Numeric(18, 2)), 1111)
INSERT [dbo].[Application] ([Id], [Name], [Email], [Address], [Gender], [Contact], [PreferredLocation], [ECTC], [CCTC], [Notice]) VALUES (6, N'bbbb', N'bbb@bbb.bbb', N'bbbbbbbbbbbbbbbbbbb', 1, N'bbbbbbbbbbbb', N'Location3', CAST(222222.00 AS Numeric(18, 2)), CAST(222222.00 AS Numeric(18, 2)), 22222)
INSERT [dbo].[Application] ([Id], [Name], [Email], [Address], [Gender], [Contact], [PreferredLocation], [ECTC], [CCTC], [Notice]) VALUES (7, N'bhaibhai', N'bhai@bhai.com', N'bhai bhai', 0, N'234234234', N'Location2', NULL, NULL, NULL)
INSERT [dbo].[Application] ([Id], [Name], [Email], [Address], [Gender], [Contact], [PreferredLocation], [ECTC], [CCTC], [Notice]) VALUES (8, N'bavo', N'bavo@asas.asas', N'bavo', 0, N'kjhkjhkj', N'NoLocation', CAST(222222.00 AS Numeric(18, 2)), CAST(33333333.00 AS Numeric(18, 2)), 444)
SET IDENTITY_INSERT [dbo].[Application] OFF
GO
SET IDENTITY_INSERT [dbo].[Education] ON 

INSERT [dbo].[Education] ([Id], [AppId], [Board], [Year], [CGPA]) VALUES (2, 2, N'sdfsdf', 234234234, CAST(234.00 AS Numeric(18, 2)))
INSERT [dbo].[Education] ([Id], [AppId], [Board], [Year], [CGPA]) VALUES (7, 5, N'aaaaaaaaaaa', 1111, CAST(111.00 AS Numeric(18, 2)))
INSERT [dbo].[Education] ([Id], [AppId], [Board], [Year], [CGPA]) VALUES (8, 6, N'bbbbbbbbb', 9, CAST(222.00 AS Numeric(18, 2)))
INSERT [dbo].[Education] ([Id], [AppId], [Board], [Year], [CGPA]) VALUES (13, 4, N'sdfsdfsdfsdf', 234234, CAST(234.00 AS Numeric(18, 2)))
INSERT [dbo].[Education] ([Id], [AppId], [Board], [Year], [CGPA]) VALUES (14, 4, N'234234324', 76, CAST(5766.00 AS Numeric(18, 2)))
INSERT [dbo].[Education] ([Id], [AppId], [Board], [Year], [CGPA]) VALUES (16, 7, N'asddasd', 234234, CAST(234234.00 AS Numeric(18, 2)))
INSERT [dbo].[Education] ([Id], [AppId], [Board], [Year], [CGPA]) VALUES (18, 8, N'qwqweqw', 2342, CAST(23423.00 AS Numeric(18, 2)))
SET IDENTITY_INSERT [dbo].[Education] OFF
GO
SET IDENTITY_INSERT [dbo].[Language] ON 

INSERT [dbo].[Language] ([Id], [AppId], [LanguageName], [CanRead], [CanWrite], [CanSpeak]) VALUES (4, 5, N'Hindi', 1, 0, 0)
INSERT [dbo].[Language] ([Id], [AppId], [LanguageName], [CanRead], [CanWrite], [CanSpeak]) VALUES (5, 5, N'English', 1, 0, 0)
INSERT [dbo].[Language] ([Id], [AppId], [LanguageName], [CanRead], [CanWrite], [CanSpeak]) VALUES (6, 5, N'Gujarati', 1, 0, 0)
INSERT [dbo].[Language] ([Id], [AppId], [LanguageName], [CanRead], [CanWrite], [CanSpeak]) VALUES (13, 4, N'Hindi', 1, 1, 1)
INSERT [dbo].[Language] ([Id], [AppId], [LanguageName], [CanRead], [CanWrite], [CanSpeak]) VALUES (14, 4, N'English', 0, 0, 1)
INSERT [dbo].[Language] ([Id], [AppId], [LanguageName], [CanRead], [CanWrite], [CanSpeak]) VALUES (15, 4, N'Gujarati', 0, 1, 1)
SET IDENTITY_INSERT [dbo].[Language] OFF
GO
SET IDENTITY_INSERT [dbo].[Skills] ON 

INSERT [dbo].[Skills] ([Id], [AppId], [SkillName], [SkillRate]) VALUES (4, 5, N'PHP', 1)
INSERT [dbo].[Skills] ([Id], [AppId], [SkillName], [SkillRate]) VALUES (5, 5, N'MySql', 1)
INSERT [dbo].[Skills] ([Id], [AppId], [SkillName], [SkillRate]) VALUES (6, 5, N'Java', 1)
INSERT [dbo].[Skills] ([Id], [AppId], [SkillName], [SkillRate]) VALUES (7, 5, N'AWS', 1)
INSERT [dbo].[Skills] ([Id], [AppId], [SkillName], [SkillRate]) VALUES (14, 4, N'PHP', 3)
INSERT [dbo].[Skills] ([Id], [AppId], [SkillName], [SkillRate]) VALUES (15, 4, N'MySql', 2)
INSERT [dbo].[Skills] ([Id], [AppId], [SkillName], [SkillRate]) VALUES (16, 4, N'Java', 1)
SET IDENTITY_INSERT [dbo].[Skills] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserId], [FirstName], [LastName], [UserName], [Email], [Password], [PhoneNumber], [RegisteredDate], [LoginDate]) VALUES (1, N'string', N'string', N'string', N'admin@admin.com', N'26f0cadd82427f20ac3cba56869d263d', N'string', NULL, NULL)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET IDENTITY_INSERT [dbo].[Work] ON 

INSERT [dbo].[Work] ([Id], [AppId], [Company], [Designation], [FromDate], [ToDate]) VALUES (4, 5, N'aaaa', N'aaaa', CAST(N'2024-01-11T00:00:00.0000000' AS DateTime2), CAST(N'2024-01-16T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Work] ([Id], [AppId], [Company], [Designation], [FromDate], [ToDate]) VALUES (5, 6, N'bbbbb', N'bbbbb', CAST(N'2024-01-19T00:00:00.0000000' AS DateTime2), CAST(N'2024-01-12T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Work] ([Id], [AppId], [Company], [Designation], [FromDate], [ToDate]) VALUES (10, 4, N'7', N'576', CAST(N'2024-01-19T00:00:00.0000000' AS DateTime2), CAST(N'2024-01-18T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Work] ([Id], [AppId], [Company], [Designation], [FromDate], [ToDate]) VALUES (11, 4, N'987987', N'987', CAST(N'2024-01-24T00:00:00.0000000' AS DateTime2), CAST(N'2024-01-02T00:00:00.0000000' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Work] OFF
GO
ALTER TABLE [dbo].[Application] ADD  CONSTRAINT [DF_Application_Gender]  DEFAULT ((0)) FOR [Gender]
GO
ALTER TABLE [dbo].[Language] ADD  CONSTRAINT [DF_Table_1_Read]  DEFAULT ((0)) FOR [CanRead]
GO
ALTER TABLE [dbo].[Language] ADD  CONSTRAINT [DF_Language_CanWrite]  DEFAULT ((0)) FOR [CanWrite]
GO
ALTER TABLE [dbo].[Language] ADD  CONSTRAINT [DF_Language_CanSpeak]  DEFAULT ((0)) FOR [CanSpeak]
GO
USE [master]
GO
ALTER DATABASE [EPBZ] SET  READ_WRITE 
GO
