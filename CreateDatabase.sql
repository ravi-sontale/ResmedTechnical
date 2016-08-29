USE [master]
GO

/****** Object:  Database [Stock]    Script Date: 05/08/2013 11:31:19 ******/
CREATE DATABASE [Stock] ON  PRIMARY 
( NAME = N'Stock', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\Stock.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Stock_log', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\Stock_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO

ALTER DATABASE [Stock] SET COMPATIBILITY_LEVEL = 100
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Stock].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [Stock] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [Stock] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [Stock] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [Stock] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [Stock] SET ARITHABORT OFF 
GO

ALTER DATABASE [Stock] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [Stock] SET AUTO_CREATE_STATISTICS ON 
GO

ALTER DATABASE [Stock] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [Stock] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [Stock] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [Stock] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [Stock] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [Stock] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [Stock] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [Stock] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [Stock] SET  DISABLE_BROKER 
GO

ALTER DATABASE [Stock] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [Stock] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [Stock] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [Stock] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [Stock] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [Stock] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [Stock] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [Stock] SET  READ_WRITE 
GO

ALTER DATABASE [Stock] SET RECOVERY FULL 
GO

ALTER DATABASE [Stock] SET  MULTI_USER 
GO

ALTER DATABASE [Stock] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [Stock] SET DB_CHAINING OFF 
GO


USE [Stock]
GO

/****** Object:  Table [dbo].[Site]    Script Date: 05/08/2013 11:31:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Site](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Format] [varchar](10) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Url] [varchar](200) NOT NULL,
 CONSTRAINT [PK_Site] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


USE [Stock]
GO

/****** Object:  Table [dbo].[StockSymbol]    Script Date: 05/08/2013 11:31:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[StockSymbol](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Symbol] [varchar](10) NOT NULL,
 CONSTRAINT [PK_StockSymbol] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


USE [Stock]
GO

/****** Object:  Table [dbo].[StockFiles]    Script Date: 11/10/2015 21:30:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[StockFiles](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SiteId] [int] NOT NULL,
	[StockSymbolId] [int] NOT NULL,
	[FileContent] [nvarchar](max) NOT NULL,
	[ImportedDateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_StockFile] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

Use [master]
GO

IF EXISTS (SELECT * FROM sys.server_principals WHERE name = N'StockUser')
	DROP LOGIN StockUser
GO
	CREATE LOGIN StockUser WITH PASSWORD='password', CHECK_POLICY = OFF
GO


Use [Stock]  
GO


IF EXISTS (SELECT * FROM sys.database_principals WHERE name = N'StockUser')
	DROP USER [StockUser]
GO
	CREATE USER [StockUser] FOR LOGIN [StockUser] WITH DEFAULT_SCHEMA=[DBO]
GO

EXEC sp_addrolemember N'db_datareader', N'StockUser'
EXEC sp_addrolemember N'db_datawriter', N'StockUser'

GO


INSERT Site(Format,Name,Url) VALUES('Xml','Yahoo','http://chartapi.finance.yahoo.com/instrument/1.0/{0}/chartdata;type=quote;range=1d/xml')
INSERT Site(Format,Name,Url) VALUES('CSV','Google','http://www.google.com/finance/getprices?i=60&p=10d&f=d,o,h,l,c,v&df=cpct&q={0}')
INSERT Site(Format,Name,Url) VALUES('Txt','Hopey','http://hopey.netfonds.no/posdump.php?date=20130423&paper={0}.O&csv_format=txt')


GO


INSERT StockSymbol(Symbol) VALUES('MSFT')
INSERT StockSymbol(Symbol) VALUES('AAPL')
INSERT StockSymbol(Symbol) VALUES('GOOG')
INSERT StockSymbol(Symbol) VALUES('YHOO')

GO