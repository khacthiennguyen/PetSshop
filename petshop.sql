USE [master]
GO
/****** Object:  Database [PetShop]    Script Date: 4/11/2024 11:32:26 PM ******/
CREATE DATABASE [PetShop]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PetShop', FILENAME = N'E:\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\PetShop.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PetShop_log', FILENAME = N'E:\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\PetShop_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [PetShop] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PetShop].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PetShop] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PetShop] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PetShop] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PetShop] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PetShop] SET ARITHABORT OFF 
GO
ALTER DATABASE [PetShop] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PetShop] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PetShop] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PetShop] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PetShop] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PetShop] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PetShop] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PetShop] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PetShop] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PetShop] SET  ENABLE_BROKER 
GO
ALTER DATABASE [PetShop] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PetShop] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PetShop] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PetShop] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PetShop] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PetShop] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PetShop] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PetShop] SET RECOVERY FULL 
GO
ALTER DATABASE [PetShop] SET  MULTI_USER 
GO
ALTER DATABASE [PetShop] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PetShop] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PetShop] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PetShop] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PetShop] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PetShop] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'PetShop', N'ON'
GO
ALTER DATABASE [PetShop] SET QUERY_STORE = ON
GO
ALTER DATABASE [PetShop] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [PetShop]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 4/11/2024 11:32:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[CategoryId] [nvarchar](20) NOT NULL,
	[CategoryName] [nvarchar](50) NOT NULL,
	[CategoryDescription] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Member]    Script Date: 4/11/2024 11:32:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Member](
	[MemberId] [varchar](32) NOT NULL,
	[Email] [varchar](64) NOT NULL,
	[Password] [varchar](128) NOT NULL,
	[Name] [nvarchar](64) NOT NULL,
	[GivenName] [nvarchar](16) NOT NULL,
	[SurName] [nvarchar](32) NULL,
	[LoginDate] [datetime] NOT NULL,
	[RegisterDate] [datetime] NOT NULL,
	[Token] [char](32) NULL,
	[RoleId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MemberId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 4/11/2024 11:32:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ProductId] [nvarchar](20) NOT NULL,
	[ProductName] [nvarchar](50) NOT NULL,
	[CategoryId] [nvarchar](20) NOT NULL,
	[ProductStar] [int] NOT NULL,
	[ProductQuantity] [int] NOT NULL,
	[ProductPrice] [decimal](18, 2) NOT NULL,
	[ProductDescription] [nvarchar](max) NOT NULL,
	[ProductStatus] [nvarchar](100) NOT NULL,
	[ProductImg] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK__Product__B40CC6CDC73ADD9A] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 4/11/2024 11:32:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[RoleId] [int] NOT NULL,
	[RoleName] [varchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Member] ADD  DEFAULT (getdate()) FOR [LoginDate]
GO
ALTER TABLE [dbo].[Member] ADD  DEFAULT (getdate()) FOR [RegisterDate]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK__Product__Product__55009F39] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([CategoryId])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK__Product__Product__55009F39]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [CK__Product__Product__531856C7] CHECK  (([ProductStar]>=(0) AND [ProductStar]<=(5)))
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [CK__Product__Product__531856C7]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [CK__Product__Product__540C7B00] CHECK  (([ProductQuantity]>=(0)))
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [CK__Product__Product__540C7B00]
GO
/****** Object:  StoredProcedure [dbo].[AddCategory]    Script Date: 4/11/2024 11:32:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddCategory]
    @CategoryId NVARCHAR(20),
    @CategoryName NVARCHAR(50),
    @CategoryDescription NVARCHAR(100)
AS
BEGIN

    IF EXISTS (SELECT 1 FROM Category WHERE CategoryId = @CategoryId)
    BEGIN
        PRINT 'CategoryId exits'
        RETURN
    END

    INSERT INTO Category (CategoryId, CategoryName, CategoryDescription)
    VALUES (@CategoryId, @CategoryName, @CategoryDescription)
    
END

GO
/****** Object:  StoredProcedure [dbo].[AddMember]    Script Date: 4/11/2024 11:32:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[AddMember](
@MemberId VARCHAR(32),
@Email VARCHAR(64),
@Password VARCHAR(128),
@Name NVARCHAR(64),
@GivenName NVARCHAR(16),
@Surname NVARCHAR(32),
@RoleId int)

as

IF NOT EXISTS(SELECT * FROM Member WHERE MemberId = @MemberId)
INSERT INTO Member (MemberId, Email, Password, Name, GivenName, SurName, RoleId) VALUES
(@MemberId, @Email, @Password, @Name, @GivenName, @SurName, @RoleId)


GO
/****** Object:  StoredProcedure [dbo].[AddProduct]    Script Date: 4/11/2024 11:32:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[AddProduct]
    @ProductId NVARCHAR(20),
    @ProductName NVARCHAR(50),
    @CategoryId NVARCHAR(20),
    @ProductStar INT,
    @ProductQuantity INT,
    @ProductPrice DECIMAL(18, 2),
    @ProductDescription NVARCHAR(MAX),
    @ProductStatus NVARCHAR(10),
    @ProductImg NVARCHAR(100)
AS
BEGIN
    -- Kiểm tra xem CategoryId có tồn tại trong bảng Category hay không
    IF NOT EXISTS (SELECT 1 FROM Category WHERE CategoryId = @CategoryId)
    BEGIN
        RAISERROR('CategoryId does not exist', 16, 1);
        RETURN;
    END

    -- Thêm sản phẩm vào bảng Product
    INSERT INTO Product (ProductId, ProductName, CategoryId, ProductStar, ProductQuantity, ProductPrice, ProductDescription, ProductStatus, ProductImg)
    VALUES (@ProductId, @ProductName, @CategoryId, @ProductStar, @ProductQuantity, @ProductPrice, @ProductDescription, @ProductStatus, @ProductImg);
END
GO
/****** Object:  StoredProcedure [dbo].[FindMemberByEmail]    Script Date: 4/11/2024 11:32:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[FindMemberByEmail]
    @Email NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT  *
    FROM Member
    WHERE Email = @Email;
END;
GO
USE [master]
GO
ALTER DATABASE [PetShop] SET  READ_WRITE 
GO
