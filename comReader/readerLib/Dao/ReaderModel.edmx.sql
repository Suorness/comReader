
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/29/2017 22:49:36
-- Generated from EDMX file: C:\__git\project\comReader\comReader\readerLib\Dao\ReaderModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [comReaderDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ControlDataSet'
CREATE TABLE [dbo].[ControlDataSet] (
    [DataId] int IDENTITY(1,1) NOT NULL,
    [DeviceNumber] nvarchar(max)  NOT NULL,
    [CardNumber] nvarchar(max)  NOT NULL,
    [Time] time  NULL,
    [Person_PersonId] int  NOT NULL
);
GO

-- Creating table 'PersonSet'
CREATE TABLE [dbo].[PersonSet] (
    [PersonId] int IDENTITY(1,1) NOT NULL,
    [CardNumber] nvarchar(max)  NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [DataId] in table 'ControlDataSet'
ALTER TABLE [dbo].[ControlDataSet]
ADD CONSTRAINT [PK_ControlDataSet]
    PRIMARY KEY CLUSTERED ([DataId] ASC);
GO

-- Creating primary key on [PersonId] in table 'PersonSet'
ALTER TABLE [dbo].[PersonSet]
ADD CONSTRAINT [PK_PersonSet]
    PRIMARY KEY CLUSTERED ([PersonId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Person_PersonId] in table 'ControlDataSet'
ALTER TABLE [dbo].[ControlDataSet]
ADD CONSTRAINT [FK_PersonControlData]
    FOREIGN KEY ([Person_PersonId])
    REFERENCES [dbo].[PersonSet]
        ([PersonId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PersonControlData'
CREATE INDEX [IX_FK_PersonControlData]
ON [dbo].[ControlDataSet]
    ([Person_PersonId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------