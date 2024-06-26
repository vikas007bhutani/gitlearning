USE [master]
GO
/****** Object:  Database [SALES_ERP]    Script Date: 06-06-2020 12:10:49 ******/
CREATE DATABASE [SALES_ERP]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SALES_ERP', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\SALES_ERP.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SALES_ERP_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\SALES_ERP_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [SALES_ERP] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SALES_ERP].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SALES_ERP] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SALES_ERP] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SALES_ERP] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SALES_ERP] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SALES_ERP] SET ARITHABORT OFF 
GO
ALTER DATABASE [SALES_ERP] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [SALES_ERP] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SALES_ERP] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SALES_ERP] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SALES_ERP] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SALES_ERP] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SALES_ERP] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SALES_ERP] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SALES_ERP] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SALES_ERP] SET  ENABLE_BROKER 
GO
ALTER DATABASE [SALES_ERP] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SALES_ERP] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SALES_ERP] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SALES_ERP] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SALES_ERP] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SALES_ERP] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SALES_ERP] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SALES_ERP] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [SALES_ERP] SET  MULTI_USER 
GO
ALTER DATABASE [SALES_ERP] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SALES_ERP] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SALES_ERP] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SALES_ERP] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SALES_ERP] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [SALES_ERP] SET QUERY_STORE = OFF
GO
USE [SALES_ERP]
GO
/****** Object:  Schema [acc]    Script Date: 06-06-2020 12:10:49 ******/
CREATE SCHEMA [acc]
GO
/****** Object:  Schema [agent]    Script Date: 06-06-2020 12:10:49 ******/
CREATE SCHEMA [agent]
GO
/****** Object:  Schema [comm]    Script Date: 06-06-2020 12:10:49 ******/
CREATE SCHEMA [comm]
GO
/****** Object:  Schema [mr]    Script Date: 06-06-2020 12:10:49 ******/
CREATE SCHEMA [mr]
GO
/****** Object:  Schema [sales]    Script Date: 06-06-2020 12:10:49 ******/
CREATE SCHEMA [sales]
GO
/****** Object:  Table [acc].[Agent_User]    Script Date: 06-06-2020 12:10:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [acc].[Agent_User](
	[agent_id] [int] IDENTITY(1,1) NOT NULL,
	[agent_type_id] [int] NULL,
	[agent_code] [nvarchar](50) NULL,
	[name] [nvarchar](50) NULL,
	[address] [nvarchar](500) NULL,
	[city] [nvarchar](50) NULL,
	[country] [nvarchar](50) NULL,
	[contractstartdate] [datetime] NULL,
	[panno] [nvarchar](50) NULL,
	[parcheeid] [int] NULL,
	[status] [varchar](20) NULL,
	[website] [nvarchar](100) NULL,
	[contractformalities] [nvarchar](500) NULL,
	[createddatetime] [datetime] NULL,
	[createdby] [int] NULL,
	[is_active] [bit] NULL,
	[updateddatetime] [datetime] NULL,
 CONSTRAINT [PK_Agent_User] PRIMARY KEY CLUSTERED 
(
	[agent_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [acc].[LoginRoles]    Script Date: 06-06-2020 12:10:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [acc].[LoginRoles](
	[role_id] [int] IDENTITY(1,1) NOT NULL,
	[role_name] [varchar](50) NULL,
	[role_descripton] [varchar](100) NULL,
	[created_datetime] [datetime] NULL,
	[updated_datetime] [datetime] NULL,
	[updated_by] [int] NULL,
	[is_active] [bit] NULL,
 CONSTRAINT [PK_LoginRoles] PRIMARY KEY CLUSTERED 
(
	[role_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [acc].[roleclaim]    Script Date: 06-06-2020 12:10:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [acc].[roleclaim](
	[claim_id] [int] IDENTITY(1,1) NOT NULL,
	[role_id] [int] NULL,
	[user_id] [int] NULL,
	[created_datetime] [datetime] NULL,
	[updated_datetime] [datetime] NULL,
	[updated_by] [int] NULL,
	[is_active] [bit] NULL,
 CONSTRAINT [PK_roleclaim] PRIMARY KEY CLUSTERED 
(
	[claim_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [acc].[UserLogin]    Script Date: 06-06-2020 12:10:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [acc].[UserLogin](
	[user_id] [int] IDENTITY(1,1) NOT NULL,
	[user_name] [nvarchar](100) NULL,
	[user_pass] [nvarchar](100) NULL,
	[user_phone] [varchar](20) NULL,
	[user_email] [nvarchar](100) NULL,
	[created_datetime] [datetime] NULL,
	[updated_datetime] [datetime] NULL,
	[updated_by] [int] NULL,
	[is_active] [bit] NULL,
 CONSTRAINT [PK_UserLogin] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [agent].[Agent_Contact]    Script Date: 06-06-2020 12:10:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [agent].[Agent_Contact](
	[contact_id] [int] IDENTITY(1,1) NOT NULL,
	[agent_id] [int] NULL,
	[mobile] [nvarchar](20) NULL,
	[telephone] [nvarchar](20) NULL,
	[email] [nvarchar](100) NULL,
	[createddatetime] [datetime] NULL,
	[createdby] [int] NULL,
	[updateddatetime] [datetime] NULL,
	[is_active] [bit] NULL,
 CONSTRAINT [PK_Agent_Contact] PRIMARY KEY CLUSTERED 
(
	[contact_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [agent].[Bank_Details]    Script Date: 06-06-2020 12:10:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [agent].[Bank_Details](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Agent_Id] [int] NULL,
	[Bank_Id] [int] NULL,
	[Account_No] [varchar](50) NULL,
	[Createddatetime] [datetime] NULL,
	[Createdby] [int] NULL,
	[Updateddatetime] [datetime] NULL,
	[Is_Active] [bit] NULL,
 CONSTRAINT [PK_Bank_Details] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [agent].[Vehicle_Details]    Script Date: 06-06-2020 12:10:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [agent].[Vehicle_Details](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[agent_id] [int] NULL,
	[vehicle_id] [int] NULL,
	[vehicle_no] [nvarchar](50) NULL,
	[createddatetime] [datetime] NULL,
	[updateddatetime] [datetime] NULL,
	[createdby] [int] NULL,
	[is_active] [bit] NULL,
 CONSTRAINT [PK_Vehicle_Details] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [comm].[Bank_Master]    Script Date: 06-06-2020 12:10:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [comm].[Bank_Master](
	[bankid] [int] IDENTITY(1,1) NOT NULL,
	[bankname] [nvarchar](100) NULL,
	[bankaddress] [nvarchar](500) NULL,
	[bankcode] [nvarchar](10) NULL,
	[IFSC] [nvarchar](20) NULL,
	[createddatetime] [datetime] NULL,
	[updateddatetime] [datetime] NULL,
	[createdby] [int] NULL,
	[is_active] [bit] NULL,
 CONSTRAINT [PK_Bank_Master] PRIMARY KEY CLUSTERED 
(
	[bankid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [comm].[Series_Master]    Script Date: 06-06-2020 12:10:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [comm].[Series_Master](
	[series_id] [int] IDENTITY(1,1) NOT NULL,
	[series_name] [nvarchar](50) NULL,
	[series_code] [nvarchar](50) NULL,
	[createddatetime] [datetime] NULL,
	[createdby] [int] NULL,
	[updateddatetime] [datetime] NULL,
	[is_active] [bit] NULL,
 CONSTRAINT [PK_Series_Master] PRIMARY KEY CLUSTERED 
(
	[series_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sale_Details]    Script Date: 06-06-2020 12:10:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sale_Details](
	[sale_id] [bigint] IDENTITY(1,1) NOT NULL,
	[sale_amount] [decimal](18, 2) NULL,
	[sale_date] [datetime] NULL,
	[paid_by_cash] [decimal](18, 2) NULL,
	[paid_by_card] [decimal](18, 2) NULL,
	[paid_in_store] [decimal](18, 2) NULL,
	[pay_later] [decimal](18, 2) NULL,
	[HDcharge_amount] [decimal](18, 2) NULL,
	[Card_Charge_Percentage] [float] NULL,
	[GST] [float] NULL,
	[IGST] [float] NULL,
	[createddatetime] [datetime] NULL,
	[createdby] [int] NULL,
	[updateddatetime] [datetime] NULL,
	[is_active] [bit] NULL,
 CONSTRAINT [PK_Sale_Details] PRIMARY KEY CLUSTERED 
(
	[sale_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [mr].[Agent_Master]    Script Date: 06-06-2020 12:10:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [mr].[Agent_Master](
	[agent_id] [int] IDENTITY(1,1) NOT NULL,
	[agent_type] [varchar](50) NULL,
	[agent_code] [nvarchar](10) NULL,
	[created_datetime] [datetime] NULL,
	[updated_datetime] [datetime] NULL,
	[updated_by] [int] NULL,
	[is_active] [bit] NULL,
 CONSTRAINT [PK_Agent] PRIMARY KEY CLUSTERED 
(
	[agent_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [mr].[Commission_Details]    Script Date: 06-06-2020 12:10:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [mr].[Commission_Details](
	[comm_id] [bigint] IDENTITY(1,1) NOT NULL,
	[comm_date] [datetime] NULL,
	[mirror_id] [bigint] NULL,
	[agent_id] [int] NULL,
	[sale_id] [bigint] NULL,
	[comm_pecentage] [int] NULL,
	[comm_amount] [decimal](18, 2) NULL,
	[createddatetime] [datetime] NULL,
	[createdby] [int] NULL,
	[updateddatetime] [datetime] NULL,
	[is_active] [bit] NULL,
 CONSTRAINT [PK_Commission_Details] PRIMARY KEY CLUSTERED 
(
	[comm_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [mr].[Countries_Master]    Script Date: 06-06-2020 12:10:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [mr].[Countries_Master](
	[country_id] [int] IDENTITY(1,1) NOT NULL,
	[country_name] [varchar](50) NULL,
	[country_code] [varchar](10) NULL,
	[created_datetime] [datetime] NULL,
	[updated_datetime] [datetime] NULL,
	[updated_by] [int] NULL,
	[is_active] [bit] NULL,
 CONSTRAINT [PK_Countries] PRIMARY KEY CLUSTERED 
(
	[country_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [mr].[Languages_Master]    Script Date: 06-06-2020 12:10:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [mr].[Languages_Master](
	[language_id] [int] IDENTITY(1,1) NOT NULL,
	[language_name] [varchar](50) NULL,
	[language_code] [varchar](10) NULL,
	[created_datetime] [datetime] NULL,
	[updated_datetime] [datetime] NULL,
	[updated_by] [int] NULL,
	[is_active] [bit] NULL,
 CONSTRAINT [PK_Languages] PRIMARY KEY CLUSTERED 
(
	[language_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [mr].[Mirror_Agent]    Script Date: 06-06-2020 12:10:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [mr].[Mirror_Agent](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[mirrorid] [bigint] NULL,
	[agentid] [bigint] NULL,
	[parchiamount] [decimal](18, 2) NULL,
	[isparchi] [bit] NULL,
	[createddatetime] [datetime] NULL,
	[createdby] [int] NULL,
	[updateddatetime] [datetime] NULL,
	[is_active] [bit] NULL,
 CONSTRAINT [PK_Mirror_Agent] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [mr].[Mirror_Details]    Script Date: 06-06-2020 12:10:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [mr].[Mirror_Details](
	[mirror_id] [bigint] IDENTITY(1,1) NOT NULL,
	[mirror_date] [datetime] NULL,
	[principle_agent_id] [int] NULL,
	[excursion_agent_id] [int] NULL,
	[demo_person_id] [int] NULL,
	[tour_leader_id] [int] NULL,
	[tour_escort_id] [int] NULL,
	[tour_guide_id] [int] NULL,
	[driver_id] [int] NULL,
	[pax] [int] NULL,
	[countryid] [int] NULL,
	[languageid] [int] NULL,
	[pool_id] [bigint] NULL,
	[series_id] [int] NULL,
	[status_id] [int] NULL,
	[remarks] [nvarchar](500) NULL,
	[createddatetime] [datetime] NULL,
	[updateddatetime] [datetime] NULL,
	[createdby] [int] NULL,
	[is_active] [bit] NULL,
 CONSTRAINT [PK_Mirror_Details] PRIMARY KEY CLUSTERED 
(
	[mirror_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [mr].[Pay_Details]    Script Date: 06-06-2020 12:10:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [mr].[Pay_Details](
	[pay_id] [bigint] IDENTITY(1,1) NOT NULL,
	[pay_date] [datetime] NULL,
	[agent_id] [int] NULL,
	[mirror_id] [bigint] NULL,
	[amount] [decimal](18, 2) NULL,
	[comm_id] [bigint] NULL,
	[createddatetime] [datetime] NULL,
	[createdby] [int] NULL,
	[updateddatetime] [datetime] NULL,
	[is_active] [bit] NULL,
 CONSTRAINT [PK_Pay_Details] PRIMARY KEY CLUSTERED 
(
	[pay_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [mr].[Pool_Master]    Script Date: 06-06-2020 12:10:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [mr].[Pool_Master](
	[pool_id] [bigint] IDENTITY(1,1) NOT NULL,
	[pool_start_date] [datetime] NULL,
	[pool_end_date] [datetime] NULL,
	[pool_name] [nvarchar](100) NULL,
	[pool_desc] [nvarchar](500) NULL,
	[createddatetime] [datetime] NULL,
	[createdby] [int] NULL,
	[updateddatetime] [datetime] NULL,
	[is_active] [bit] NULL,
 CONSTRAINT [PK_Pool_Master] PRIMARY KEY CLUSTERED 
(
	[pool_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [mr].[Vehicle_Master]    Script Date: 06-06-2020 12:10:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [mr].[Vehicle_Master](
	[vehicle_id] [int] IDENTITY(1,1) NOT NULL,
	[vehicle_type] [varchar](50) NULL,
	[created_datetime] [datetime] NULL,
	[updated_datetime] [datetime] NULL,
	[updated_by] [int] NULL,
	[is_active] [bit] NULL,
 CONSTRAINT [PK_Vehicle] PRIMARY KEY CLUSTERED 
(
	[vehicle_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [acc].[Agent_User] ADD  CONSTRAINT [DF_Agent_User_createddatetime]  DEFAULT (getdate()) FOR [createddatetime]
GO
ALTER TABLE [acc].[Agent_User] ADD  CONSTRAINT [DF_Agent_User_updateddatetime]  DEFAULT (getdate()) FOR [updateddatetime]
GO
ALTER TABLE [agent].[Agent_Contact] ADD  CONSTRAINT [DF_Agent_Contact_createddatetime]  DEFAULT (getdate()) FOR [createddatetime]
GO
ALTER TABLE [agent].[Agent_Contact] ADD  CONSTRAINT [DF_Agent_Contact_createdby]  DEFAULT ((0)) FOR [createdby]
GO
ALTER TABLE [agent].[Agent_Contact] ADD  CONSTRAINT [DF_Agent_Contact_updateddatetime]  DEFAULT (getdate()) FOR [updateddatetime]
GO
ALTER TABLE [agent].[Agent_Contact] ADD  CONSTRAINT [DF_Agent_Contact_is_active]  DEFAULT ((1)) FOR [is_active]
GO
ALTER TABLE [agent].[Vehicle_Details] ADD  CONSTRAINT [DF_Vehicle_Details_createddatetime]  DEFAULT (getdate()) FOR [createddatetime]
GO
ALTER TABLE [agent].[Vehicle_Details] ADD  CONSTRAINT [DF_Vehicle_Details_updateddatetime]  DEFAULT (getdate()) FOR [updateddatetime]
GO
ALTER TABLE [agent].[Vehicle_Details] ADD  CONSTRAINT [DF_Vehicle_Details_createdby]  DEFAULT ((0)) FOR [createdby]
GO
ALTER TABLE [comm].[Bank_Master] ADD  CONSTRAINT [DF_Bank_Master_createddatetime]  DEFAULT (getdate()) FOR [createddatetime]
GO
ALTER TABLE [comm].[Bank_Master] ADD  CONSTRAINT [DF_Bank_Master_createdby]  DEFAULT ((0)) FOR [createdby]
GO
ALTER TABLE [comm].[Bank_Master] ADD  CONSTRAINT [DF_Bank_Master_is_active]  DEFAULT ((1)) FOR [is_active]
GO
ALTER TABLE [comm].[Series_Master] ADD  CONSTRAINT [DF_Series_Master_createddatetime]  DEFAULT (getdate()) FOR [createddatetime]
GO
ALTER TABLE [comm].[Series_Master] ADD  CONSTRAINT [DF_Series_Master_createdby]  DEFAULT ((0)) FOR [createdby]
GO
ALTER TABLE [comm].[Series_Master] ADD  CONSTRAINT [DF_Series_Master_updateddatetime]  DEFAULT (getdate()) FOR [updateddatetime]
GO
ALTER TABLE [comm].[Series_Master] ADD  CONSTRAINT [DF_Series_Master_is_active]  DEFAULT ((1)) FOR [is_active]
GO
ALTER TABLE [dbo].[Sale_Details] ADD  CONSTRAINT [DF_Sale_Details_Card_Charge_Percentage]  DEFAULT ((0)) FOR [Card_Charge_Percentage]
GO
ALTER TABLE [dbo].[Sale_Details] ADD  CONSTRAINT [DF_Sale_Details_GST]  DEFAULT ((0)) FOR [GST]
GO
ALTER TABLE [dbo].[Sale_Details] ADD  CONSTRAINT [DF_Sale_Details_IGST]  DEFAULT ((0)) FOR [IGST]
GO
ALTER TABLE [dbo].[Sale_Details] ADD  CONSTRAINT [DF_Sale_Details_createddatetime]  DEFAULT (getdate()) FOR [createddatetime]
GO
ALTER TABLE [dbo].[Sale_Details] ADD  CONSTRAINT [DF_Sale_Details_createdby]  DEFAULT ((0)) FOR [createdby]
GO
ALTER TABLE [dbo].[Sale_Details] ADD  CONSTRAINT [DF_Sale_Details_updateddatetime]  DEFAULT (getdate()) FOR [updateddatetime]
GO
ALTER TABLE [dbo].[Sale_Details] ADD  CONSTRAINT [DF_Sale_Details_is_active]  DEFAULT ((1)) FOR [is_active]
GO
ALTER TABLE [mr].[Agent_Master] ADD  CONSTRAINT [DF_Agent_created_datetime]  DEFAULT (getdate()) FOR [created_datetime]
GO
ALTER TABLE [mr].[Agent_Master] ADD  CONSTRAINT [DF_Agent_updated_datetime]  DEFAULT (getdate()) FOR [updated_datetime]
GO
ALTER TABLE [mr].[Agent_Master] ADD  CONSTRAINT [DF_Agent_updated_by]  DEFAULT ((0)) FOR [updated_by]
GO
ALTER TABLE [mr].[Agent_Master] ADD  CONSTRAINT [DF_Agent_is_active]  DEFAULT ((1)) FOR [is_active]
GO
ALTER TABLE [mr].[Commission_Details] ADD  CONSTRAINT [DF_Commission_Details_createddatetime]  DEFAULT (getdate()) FOR [createddatetime]
GO
ALTER TABLE [mr].[Commission_Details] ADD  CONSTRAINT [DF_Commission_Details_createdby]  DEFAULT ((0)) FOR [createdby]
GO
ALTER TABLE [mr].[Commission_Details] ADD  CONSTRAINT [DF_Commission_Details_updateddatetime]  DEFAULT (getdate()) FOR [updateddatetime]
GO
ALTER TABLE [mr].[Countries_Master] ADD  CONSTRAINT [DF_Countries_created_datetime]  DEFAULT (getdate()) FOR [created_datetime]
GO
ALTER TABLE [mr].[Countries_Master] ADD  CONSTRAINT [DF_Countries_updated_datetime]  DEFAULT (getdate()) FOR [updated_datetime]
GO
ALTER TABLE [mr].[Countries_Master] ADD  CONSTRAINT [DF_Countries_updated_by]  DEFAULT ((0)) FOR [updated_by]
GO
ALTER TABLE [mr].[Countries_Master] ADD  CONSTRAINT [DF_Countries_is_active]  DEFAULT ((1)) FOR [is_active]
GO
ALTER TABLE [mr].[Languages_Master] ADD  CONSTRAINT [DF_Languages_created_datetime]  DEFAULT (getdate()) FOR [created_datetime]
GO
ALTER TABLE [mr].[Languages_Master] ADD  CONSTRAINT [DF_Languages_updated_datetime]  DEFAULT (getdate()) FOR [updated_datetime]
GO
ALTER TABLE [mr].[Languages_Master] ADD  CONSTRAINT [DF_Languages_updated_by]  DEFAULT ((0)) FOR [updated_by]
GO
ALTER TABLE [mr].[Languages_Master] ADD  CONSTRAINT [DF_Languages_is_active]  DEFAULT ((1)) FOR [is_active]
GO
ALTER TABLE [mr].[Mirror_Agent] ADD  CONSTRAINT [DF_Mirror_Agent_createddatetime]  DEFAULT (getdate()) FOR [createddatetime]
GO
ALTER TABLE [mr].[Mirror_Agent] ADD  CONSTRAINT [DF_Mirror_Agent_createdby]  DEFAULT ((0)) FOR [createdby]
GO
ALTER TABLE [mr].[Mirror_Agent] ADD  CONSTRAINT [DF_Mirror_Agent_updateddatetime]  DEFAULT (getdate()) FOR [updateddatetime]
GO
ALTER TABLE [mr].[Mirror_Agent] ADD  CONSTRAINT [DF_Mirror_Agent_is_active]  DEFAULT ((1)) FOR [is_active]
GO
ALTER TABLE [mr].[Mirror_Details] ADD  CONSTRAINT [DF_Mirror_Details_status_id]  DEFAULT ((0)) FOR [status_id]
GO
ALTER TABLE [mr].[Mirror_Details] ADD  CONSTRAINT [DF_Mirror_Details_createddatetime]  DEFAULT (getdate()) FOR [createddatetime]
GO
ALTER TABLE [mr].[Mirror_Details] ADD  CONSTRAINT [DF_Mirror_Details_updateddatetime]  DEFAULT (getdate()) FOR [updateddatetime]
GO
ALTER TABLE [mr].[Mirror_Details] ADD  CONSTRAINT [DF_Mirror_Details_createdby]  DEFAULT ((0)) FOR [createdby]
GO
ALTER TABLE [mr].[Mirror_Details] ADD  CONSTRAINT [DF_Mirror_Details_is_active]  DEFAULT ((1)) FOR [is_active]
GO
ALTER TABLE [mr].[Pay_Details] ADD  CONSTRAINT [DF_Pay_Details_createddatetime]  DEFAULT (getdate()) FOR [createddatetime]
GO
ALTER TABLE [mr].[Pay_Details] ADD  CONSTRAINT [DF_Pay_Details_createdby]  DEFAULT ((0)) FOR [createdby]
GO
ALTER TABLE [mr].[Pay_Details] ADD  CONSTRAINT [DF_Pay_Details_updateddatetime]  DEFAULT (getdate()) FOR [updateddatetime]
GO
ALTER TABLE [mr].[Pay_Details] ADD  CONSTRAINT [DF_Pay_Details_is_active]  DEFAULT ((1)) FOR [is_active]
GO
ALTER TABLE [mr].[Pool_Master] ADD  CONSTRAINT [DF_Pool_Master_createddatetime]  DEFAULT (getdate()) FOR [createddatetime]
GO
ALTER TABLE [mr].[Pool_Master] ADD  CONSTRAINT [DF_Pool_Master_createdby]  DEFAULT ((0)) FOR [createdby]
GO
ALTER TABLE [mr].[Pool_Master] ADD  CONSTRAINT [DF_Pool_Master_updateddatetime]  DEFAULT (getdate()) FOR [updateddatetime]
GO
ALTER TABLE [mr].[Pool_Master] ADD  CONSTRAINT [DF_Pool_Master_is_active]  DEFAULT ((1)) FOR [is_active]
GO
ALTER TABLE [mr].[Vehicle_Master] ADD  CONSTRAINT [DF_Vehicle_created_datetime]  DEFAULT (getdate()) FOR [created_datetime]
GO
ALTER TABLE [mr].[Vehicle_Master] ADD  CONSTRAINT [DF_Vehicle_updated_datetime]  DEFAULT (getdate()) FOR [updated_datetime]
GO
ALTER TABLE [mr].[Vehicle_Master] ADD  CONSTRAINT [DF_Vehicle_updated_by]  DEFAULT ((0)) FOR [updated_by]
GO
ALTER TABLE [mr].[Vehicle_Master] ADD  CONSTRAINT [DF_Vehicle_is_active]  DEFAULT ((1)) FOR [is_active]
GO
ALTER TABLE [acc].[Agent_User]  WITH CHECK ADD  CONSTRAINT [FK_Agent_User_Agent_type] FOREIGN KEY([agent_type_id])
REFERENCES [mr].[Agent_Master] ([agent_id])
GO
ALTER TABLE [acc].[Agent_User] CHECK CONSTRAINT [FK_Agent_User_Agent_type]
GO
ALTER TABLE [acc].[Agent_User]  WITH CHECK ADD  CONSTRAINT [FK_Agent_User_Agent_User] FOREIGN KEY([agent_id])
REFERENCES [acc].[Agent_User] ([agent_id])
GO
ALTER TABLE [acc].[Agent_User] CHECK CONSTRAINT [FK_Agent_User_Agent_User]
GO
ALTER TABLE [acc].[Agent_User]  WITH CHECK ADD  CONSTRAINT [FK_Agent_User_login_User] FOREIGN KEY([createdby])
REFERENCES [acc].[UserLogin] ([user_id])
GO
ALTER TABLE [acc].[Agent_User] CHECK CONSTRAINT [FK_Agent_User_login_User]
GO
ALTER TABLE [acc].[roleclaim]  WITH CHECK ADD  CONSTRAINT [FK_roleclaim_loginrole] FOREIGN KEY([role_id])
REFERENCES [acc].[LoginRoles] ([role_id])
GO
ALTER TABLE [acc].[roleclaim] CHECK CONSTRAINT [FK_roleclaim_loginrole]
GO
ALTER TABLE [acc].[roleclaim]  WITH CHECK ADD  CONSTRAINT [FK_roleclaim_userlogin] FOREIGN KEY([user_id])
REFERENCES [acc].[UserLogin] ([user_id])
GO
ALTER TABLE [acc].[roleclaim] CHECK CONSTRAINT [FK_roleclaim_userlogin]
GO
ALTER TABLE [agent].[Agent_Contact]  WITH CHECK ADD  CONSTRAINT [FK_Agent_Contact_Agent_User] FOREIGN KEY([agent_id])
REFERENCES [acc].[Agent_User] ([agent_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [agent].[Agent_Contact] CHECK CONSTRAINT [FK_Agent_Contact_Agent_User]
GO
ALTER TABLE [agent].[Bank_Details]  WITH CHECK ADD  CONSTRAINT [FK_agent_user_Bank_Details] FOREIGN KEY([Agent_Id])
REFERENCES [acc].[Agent_User] ([agent_id])
GO
ALTER TABLE [agent].[Bank_Details] CHECK CONSTRAINT [FK_agent_user_Bank_Details]
GO
ALTER TABLE [agent].[Bank_Details]  WITH CHECK ADD  CONSTRAINT [FK_Bank_Details_Bank_master] FOREIGN KEY([Bank_Id])
REFERENCES [comm].[Bank_Master] ([bankid])
GO
ALTER TABLE [agent].[Bank_Details] CHECK CONSTRAINT [FK_Bank_Details_Bank_master]
GO
ALTER TABLE [agent].[Vehicle_Details]  WITH CHECK ADD  CONSTRAINT [FK_Vehicle_Details_Agent_User] FOREIGN KEY([agent_id])
REFERENCES [acc].[Agent_User] ([agent_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [agent].[Vehicle_Details] CHECK CONSTRAINT [FK_Vehicle_Details_Agent_User]
GO
ALTER TABLE [agent].[Vehicle_Details]  WITH CHECK ADD  CONSTRAINT [FK_Vehicle_Details_Vehicle_Master] FOREIGN KEY([vehicle_id])
REFERENCES [mr].[Vehicle_Master] ([vehicle_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [agent].[Vehicle_Details] CHECK CONSTRAINT [FK_Vehicle_Details_Vehicle_Master]
GO
ALTER TABLE [mr].[Mirror_Agent]  WITH CHECK ADD  CONSTRAINT [FK_Mirror_Agent_Mirror_details] FOREIGN KEY([mirrorid])
REFERENCES [mr].[Mirror_Details] ([mirror_id])
GO
ALTER TABLE [mr].[Mirror_Agent] CHECK CONSTRAINT [FK_Mirror_Agent_Mirror_details]
GO
ALTER TABLE [mr].[Mirror_Details]  WITH CHECK ADD  CONSTRAINT [FK_Mirror_Details_User_login] FOREIGN KEY([createdby])
REFERENCES [acc].[UserLogin] ([user_id])
GO
ALTER TABLE [mr].[Mirror_Details] CHECK CONSTRAINT [FK_Mirror_Details_User_login]
GO
ALTER TABLE [mr].[Pay_Details]  WITH CHECK ADD  CONSTRAINT [FK_Pay_Details_Agent_User] FOREIGN KEY([agent_id])
REFERENCES [acc].[Agent_User] ([agent_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [mr].[Pay_Details] CHECK CONSTRAINT [FK_Pay_Details_Agent_User]
GO
ALTER TABLE [mr].[Pay_Details]  WITH CHECK ADD  CONSTRAINT [FK_Pay_Details_Commission_Details] FOREIGN KEY([comm_id])
REFERENCES [mr].[Commission_Details] ([comm_id])
GO
ALTER TABLE [mr].[Pay_Details] CHECK CONSTRAINT [FK_Pay_Details_Commission_Details]
GO
ALTER TABLE [mr].[Pay_Details]  WITH CHECK ADD  CONSTRAINT [FK_Pay_Details_Mirror] FOREIGN KEY([mirror_id])
REFERENCES [mr].[Mirror_Details] ([mirror_id])
GO
ALTER TABLE [mr].[Pay_Details] CHECK CONSTRAINT [FK_Pay_Details_Mirror]
GO
USE [master]
GO
ALTER DATABASE [SALES_ERP] SET  READ_WRITE 
GO
