
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/13/2020 21:32:16
-- Generated from EDMX file: C:\Users\user\source\repos\Multi-Hire\HireMockup\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [MultiHireDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Customers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Customers];
GO
IF OBJECT_ID(N'[dbo].[Employees]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Employees];
GO
IF OBJECT_ID(N'[dbo].[HireAssets]', 'U') IS NOT NULL
    DROP TABLE [dbo].[HireAssets];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'HireAssets'
CREATE TABLE [dbo].[HireAssets] (
    [hireID] int IDENTITY(1,1) NOT NULL,
    [hireName] nvarchar(max)  NOT NULL,
    [hireType] nchar(15)  NULL,
    [dailyRate] decimal(19,4)  NULL
);
GO

-- Creating table 'Employees'
CREATE TABLE [dbo].[Employees] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [employeeName] nvarchar(max)  NOT NULL,
    [employeeSurname] nvarchar(max)  NOT NULL,
    [weeklySalary] decimal(19,4)  NOT NULL,
    [contactNumber] nchar(15)  NULL,
    [jobTitle] nchar(15)  NULL
);
GO

-- Creating table 'Customers'
CREATE TABLE [dbo].[Customers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [customerName] nvarchar(max)  NOT NULL,
    [customerSurname] nvarchar(max)  NOT NULL,
    [addressLine1] nvarchar(max)  NOT NULL,
    [addressLine2] nvarchar(max)  NOT NULL,
    [accountBalance] decimal(19,4)  NOT NULL,
    [emailAddress] nvarchar(max)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [hireID] in table 'HireAssets'
ALTER TABLE [dbo].[HireAssets]
ADD CONSTRAINT [PK_HireAssets]
    PRIMARY KEY CLUSTERED ([hireID] ASC);
GO

-- Creating primary key on [Id] in table 'Employees'
ALTER TABLE [dbo].[Employees]
ADD CONSTRAINT [PK_Employees]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Customers'
ALTER TABLE [dbo].[Customers]
ADD CONSTRAINT [PK_Customers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------