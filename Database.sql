USE [master]
GO
/****** Object:  Database [webMVC]    Script Date: 4/10/2021 9:29:40 PM ******/
CREATE DATABASE [webMVC]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'webMVC', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\webMVC.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'webMVC_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\webMVC_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [webMVC] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [webMVC].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [webMVC] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [webMVC] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [webMVC] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [webMVC] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [webMVC] SET ARITHABORT OFF 
GO
ALTER DATABASE [webMVC] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [webMVC] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [webMVC] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [webMVC] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [webMVC] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [webMVC] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [webMVC] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [webMVC] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [webMVC] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [webMVC] SET  DISABLE_BROKER 
GO
ALTER DATABASE [webMVC] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [webMVC] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [webMVC] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [webMVC] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [webMVC] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [webMVC] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [webMVC] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [webMVC] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [webMVC] SET  MULTI_USER 
GO
ALTER DATABASE [webMVC] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [webMVC] SET DB_CHAINING OFF 
GO
ALTER DATABASE [webMVC] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [webMVC] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [webMVC] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [webMVC] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [webMVC] SET QUERY_STORE = OFF
GO
USE [webMVC]
GO
/****** Object:  User [sa1]    Script Date: 4/10/2021 9:29:40 PM ******/
CREATE USER [sa1] FOR LOGIN [sa1] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[Bo_De]    Script Date: 4/10/2021 9:29:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bo_De](
	[Ma_BoDe] [bigint] IDENTITY(1,1) NOT NULL,
	[NoiDung] [text] NULL,
	[Ma_NguoiTao] [nchar](50) NULL,
	[Ma_Mon] [bigint] NULL,
	[SoCau] [int] NULL,
	[ThoiGianThi] [nvarchar](20) NULL,
	[Xoa] [bit] NULL,
	[NgayTao] [datetime] NULL,
	[LuotThich] [int] NULL,
	[LuotTai] [int] NULL,
	[Luuothi] [int] NULL,
	[LinkTai] [text] NULL,
 CONSTRAINT [PK_De_Thi] PRIMARY KEY CLUSTERED 
(
	[Ma_BoDe] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CauHoi]    Script Date: 4/10/2021 9:29:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CauHoi](
	[Ma_BoDe] [bigint] NOT NULL,
	[Ma_CauHoi] [bigint] NOT NULL,
	[MaCT] [bigint] IDENTITY(1,1) NOT NULL,
	[trangThai] [bit] NULL,
 CONSTRAINT [PK_CauHoi_1] PRIMARY KEY CLUSTERED 
(
	[MaCT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CauHoiDeThi]    Script Date: 4/10/2021 9:29:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CauHoiDeThi](
	[MaDeThi] [bigint] NULL,
	[MaCauHoi] [bigint] NULL,
	[MaCT] [bigint] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_CauHoiDeThi] PRIMARY KEY CLUSTERED 
(
	[MaCT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Da_SVLuaChon]    Script Date: 4/10/2021 9:29:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Da_SVLuaChon](
	[MaDeThi] [bigint] NULL,
	[Ma_DAN] [bigint] NULL,
	[MA_CT] [bigint] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Da_SVLuaChon] PRIMARY KEY CLUSTERED 
(
	[MA_CT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Dap_AN]    Script Date: 4/10/2021 9:29:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dap_AN](
	[MA_DAN] [bigint] IDENTITY(1,1) NOT NULL,
	[NoiDung] [ntext] NULL,
	[HinhAnh] [ntext] NULL,
	[Ma_CauHoi] [bigint] NULL,
	[TrangThai] [bit] NULL,
 CONSTRAINT [PK_Dap_AN] PRIMARY KEY CLUSTERED 
(
	[MA_DAN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[De_Thi]    Script Date: 4/10/2021 9:29:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[De_Thi](
	[Ma_SV] [nchar](50) NULL,
	[Ma_BoDe] [bigint] NULL,
	[TrangThai] [bit] NULL,
	[MaDeThi] [bigint] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_TronDe] PRIMARY KEY CLUSTERED 
(
	[MaDeThi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Kho_CauHoi]    Script Date: 4/10/2021 9:29:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Kho_CauHoi](
	[Ma_CauHoi] [bigint] IDENTITY(1,1) NOT NULL,
	[NoiDung] [ntext] NULL,
	[HinhAnh] [ntext] NULL,
	[MucDo] [nvarchar](20) NULL,
	[Ma_Chuong] [bigint] NULL,
	[TrangThai] [bit] NULL,
 CONSTRAINT [PK_CauHoi] PRIMARY KEY CLUSTERED 
(
	[Ma_CauHoi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MonHoc]    Script Date: 4/10/2021 9:29:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MonHoc](
	[MaMon] [bigint] IDENTITY(1,1) NOT NULL,
	[TenMon] [nvarchar](50) NULL,
 CONSTRAINT [PK_MonHoc] PRIMARY KEY CLUSTERED 
(
	[MaMon] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaiKhoan]    Script Date: 4/10/2021 9:29:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaiKhoan](
	[TaiKhoan] [nchar](50) NOT NULL,
	[MatKhau] [nchar](10) NULL,
	[TenDangNhap] [nvarchar](50) NULL,
 CONSTRAINT [PK_TaiKhoan] PRIMARY KEY CLUSTERED 
(
	[TaiKhoan] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Bo_De]  WITH CHECK ADD  CONSTRAINT [FK_Bo_De_MonHoc] FOREIGN KEY([Ma_Mon])
REFERENCES [dbo].[MonHoc] ([MaMon])
GO
ALTER TABLE [dbo].[Bo_De] CHECK CONSTRAINT [FK_Bo_De_MonHoc]
GO
ALTER TABLE [dbo].[Bo_De]  WITH CHECK ADD  CONSTRAINT [FK_Bo_De_TaiKhoan1] FOREIGN KEY([Ma_NguoiTao])
REFERENCES [dbo].[TaiKhoan] ([TaiKhoan])
GO
ALTER TABLE [dbo].[Bo_De] CHECK CONSTRAINT [FK_Bo_De_TaiKhoan1]
GO
ALTER TABLE [dbo].[CauHoi]  WITH CHECK ADD  CONSTRAINT [FK_CauHoi_Bo_De] FOREIGN KEY([Ma_BoDe])
REFERENCES [dbo].[Bo_De] ([Ma_BoDe])
GO
ALTER TABLE [dbo].[CauHoi] CHECK CONSTRAINT [FK_CauHoi_Bo_De]
GO
ALTER TABLE [dbo].[CauHoi]  WITH CHECK ADD  CONSTRAINT [FK_CauHoi_Kho_CauHoi] FOREIGN KEY([Ma_CauHoi])
REFERENCES [dbo].[Kho_CauHoi] ([Ma_CauHoi])
GO
ALTER TABLE [dbo].[CauHoi] CHECK CONSTRAINT [FK_CauHoi_Kho_CauHoi]
GO
ALTER TABLE [dbo].[CauHoiDeThi]  WITH CHECK ADD  CONSTRAINT [FK_CauHoiDeThi_De_Thi] FOREIGN KEY([MaDeThi])
REFERENCES [dbo].[De_Thi] ([MaDeThi])
GO
ALTER TABLE [dbo].[CauHoiDeThi] CHECK CONSTRAINT [FK_CauHoiDeThi_De_Thi]
GO
ALTER TABLE [dbo].[CauHoiDeThi]  WITH CHECK ADD  CONSTRAINT [FK_CauHoiDeThi_Kho_CauHoi] FOREIGN KEY([MaCauHoi])
REFERENCES [dbo].[Kho_CauHoi] ([Ma_CauHoi])
GO
ALTER TABLE [dbo].[CauHoiDeThi] CHECK CONSTRAINT [FK_CauHoiDeThi_Kho_CauHoi]
GO
ALTER TABLE [dbo].[Da_SVLuaChon]  WITH CHECK ADD  CONSTRAINT [FK_Da_SVLuaChon_Dap_AN] FOREIGN KEY([Ma_DAN])
REFERENCES [dbo].[Dap_AN] ([MA_DAN])
GO
ALTER TABLE [dbo].[Da_SVLuaChon] CHECK CONSTRAINT [FK_Da_SVLuaChon_Dap_AN]
GO
ALTER TABLE [dbo].[Da_SVLuaChon]  WITH CHECK ADD  CONSTRAINT [FK_Da_SVLuaChon_De_Thi] FOREIGN KEY([MaDeThi])
REFERENCES [dbo].[De_Thi] ([MaDeThi])
GO
ALTER TABLE [dbo].[Da_SVLuaChon] CHECK CONSTRAINT [FK_Da_SVLuaChon_De_Thi]
GO
ALTER TABLE [dbo].[Dap_AN]  WITH CHECK ADD  CONSTRAINT [FK_Dap_AN_Kho_CauHoi] FOREIGN KEY([Ma_CauHoi])
REFERENCES [dbo].[Kho_CauHoi] ([Ma_CauHoi])
GO
ALTER TABLE [dbo].[Dap_AN] CHECK CONSTRAINT [FK_Dap_AN_Kho_CauHoi]
GO
ALTER TABLE [dbo].[De_Thi]  WITH CHECK ADD  CONSTRAINT [FK_De_Thi_Bo_De] FOREIGN KEY([Ma_BoDe])
REFERENCES [dbo].[Bo_De] ([Ma_BoDe])
GO
ALTER TABLE [dbo].[De_Thi] CHECK CONSTRAINT [FK_De_Thi_Bo_De]
GO
ALTER TABLE [dbo].[De_Thi]  WITH CHECK ADD  CONSTRAINT [FK_De_Thi_TaiKhoan] FOREIGN KEY([Ma_SV])
REFERENCES [dbo].[TaiKhoan] ([TaiKhoan])
GO
ALTER TABLE [dbo].[De_Thi] CHECK CONSTRAINT [FK_De_Thi_TaiKhoan]
GO
USE [master]
GO
ALTER DATABASE [webMVC] SET  READ_WRITE 
GO
