
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/14/2015 12:29:12
-- Generated from EDMX file: d:\repository\dev\Adbase\Models\ContructionsModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [SciencecomTest];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_LightConstructions_Owners]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LightConstructions] DROP CONSTRAINT [FK_LightConstructions_Owners];
GO
IF OBJECT_ID(N'[dbo].[FK_MetalConstructions_Owners]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MetalConstructions] DROP CONSTRAINT [FK_MetalConstructions_Owners];
GO
IF OBJECT_ID(N'[dbo].[FK_LightConstructions_Owners1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LightConstructions] DROP CONSTRAINT [FK_LightConstructions_Owners1];
GO
IF OBJECT_ID(N'[dbo].[FK_MetalConstructions_Owners1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MetalConstructions] DROP CONSTRAINT [FK_MetalConstructions_Owners1];
GO
IF OBJECT_ID(N'[dbo].[FK_OwnerBillboards]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Billboards1] DROP CONSTRAINT [FK_OwnerBillboards];
GO
IF OBJECT_ID(N'[dbo].[FK_BillboardSide]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sides] DROP CONSTRAINT [FK_BillboardSide];
GO
IF OBJECT_ID(N'[dbo].[FK_SideSurface]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Surfaces] DROP CONSTRAINT [FK_SideSurface];
GO
IF OBJECT_ID(N'[dbo].[FK_AdTypeBillboard]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Billboards1] DROP CONSTRAINT [FK_AdTypeBillboard];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[LightConstructions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LightConstructions];
GO
IF OBJECT_ID(N'[dbo].[MetalConstructions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MetalConstructions];
GO
IF OBJECT_ID(N'[dbo].[Owners]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Owners];
GO
IF OBJECT_ID(N'[dbo].[Streets]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Streets];
GO
IF OBJECT_ID(N'[dbo].[IllegalConstructions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[IllegalConstructions];
GO
IF OBJECT_ID(N'[dbo].[Billboards1]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Billboards1];
GO
IF OBJECT_ID(N'[dbo].[AdTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AdTypes];
GO
IF OBJECT_ID(N'[dbo].[Sides]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sides];
GO
IF OBJECT_ID(N'[dbo].[Surfaces]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Surfaces];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'LightConstructions'
CREATE TABLE [dbo].[LightConstructions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [IdOwner] int  NOT NULL,
    [Support] int  NOT NULL,
    [Street1] nvarchar(50)  NOT NULL,
    [Street2] nvarchar(50)  NOT NULL,
    [FromStreet] nvarchar(50)  NOT NULL,
    [StartDate] datetime  NOT NULL,
    [FinishDate] datetime  NOT NULL,
    [IsSocial] bit  NOT NULL,
    [OnStatement] bit  NOT NULL,
    [Locality] nvarchar(max)  NULL,
    [IdWhoAdded] int  NOT NULL,
    [DateOfSocial] varchar(10)  NULL,
    [Shirota] real  NOT NULL,
    [Dolgota] real  NOT NULL
);
GO

-- Creating table 'MetalConstructions'
CREATE TABLE [dbo].[MetalConstructions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [IdOwner] int  NOT NULL,
    [Support] int  NOT NULL,
    [Street1] nvarchar(50)  NOT NULL,
    [Street2] nvarchar(50)  NOT NULL,
    [FromStreet] nvarchar(50)  NOT NULL,
    [StartDate] datetime  NOT NULL,
    [Locality] nvarchar(max)  NULL,
    [IdWhoAdded] int  NOT NULL,
    [Shirota] real  NOT NULL,
    [Dolgota] real  NOT NULL
);
GO

-- Creating table 'Owners'
CREATE TABLE [dbo].[Owners] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Address] varchar(max)  NOT NULL,
    [Telephone] nvarchar(13)  NOT NULL
);
GO

-- Creating table 'Streets'
CREATE TABLE [dbo].[Streets] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Type] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'IllegalConstructions'
CREATE TABLE [dbo].[IllegalConstructions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Street1] nvarchar(50)  NOT NULL,
    [Street2] nvarchar(50)  NULL,
    [FromStreet] nvarchar(50)  NULL,
    [DetectionDate] datetime  NULL,
    [AdditionDate] datetime  NOT NULL,
    [Locality] nvarchar(max)  NOT NULL,
    [NotingDate] datetime  NULL,
    [SolvingDate] datetime  NULL,
    [Status] int  NOT NULL,
    [Note] varchar(max)  NULL,
    [Shirota] real  NOT NULL,
    [Dolgota] real  NOT NULL,
    [WhoAdded] nvarchar(max)  NOT NULL,
    [WhoTakeNote] nvarchar(max)  NULL,
    [WhoLastEdited] nvarchar(max)  NULL
);
GO

-- Creating table 'Billboards1'
CREATE TABLE [dbo].[Billboards1] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Street1] nvarchar(max)  NOT NULL,
    [House1] nvarchar(max)  NOT NULL,
    [Street2] nvarchar(max)  NOT NULL,
    [FromStreet] nvarchar(max)  NOT NULL,
    [Comment] nvarchar(max)  NOT NULL,
    [ContractNumber] nvarchar(max)  NOT NULL,
    [PassportNumber] nvarchar(max)  NOT NULL,
    [StartDate] datetime  NOT NULL,
    [EndDate] datetime  NOT NULL,
    [OnAgreement] bit  NOT NULL,
    [Latitude] float  NOT NULL,
    [Longitude] float  NOT NULL,
    [Owner_Id] int  NOT NULL,
    [AdType_Id] int  NOT NULL
);
GO

-- Creating table 'AdTypes'
CREATE TABLE [dbo].[AdTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [DisplayName] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Sides'
CREATE TABLE [dbo].[Sides] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Billboard_Id] int  NOT NULL
);
GO

-- Creating table 'Surfaces'
CREATE TABLE [dbo].[Surfaces] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Height] int  NOT NULL,
    [Width] int  NOT NULL,
    [Space] int  NOT NULL,
    [IsSocial] bit  NOT NULL,
    [Story] nvarchar(max)  NOT NULL,
    [StartDate] datetime  NOT NULL,
    [EndDate] datetime  NOT NULL,
    [Side_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'LightConstructions'
ALTER TABLE [dbo].[LightConstructions]
ADD CONSTRAINT [PK_LightConstructions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MetalConstructions'
ALTER TABLE [dbo].[MetalConstructions]
ADD CONSTRAINT [PK_MetalConstructions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Owners'
ALTER TABLE [dbo].[Owners]
ADD CONSTRAINT [PK_Owners]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Streets'
ALTER TABLE [dbo].[Streets]
ADD CONSTRAINT [PK_Streets]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'IllegalConstructions'
ALTER TABLE [dbo].[IllegalConstructions]
ADD CONSTRAINT [PK_IllegalConstructions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Billboards1'
ALTER TABLE [dbo].[Billboards1]
ADD CONSTRAINT [PK_Billboards1]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AdTypes'
ALTER TABLE [dbo].[AdTypes]
ADD CONSTRAINT [PK_AdTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Sides'
ALTER TABLE [dbo].[Sides]
ADD CONSTRAINT [PK_Sides]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Surfaces'
ALTER TABLE [dbo].[Surfaces]
ADD CONSTRAINT [PK_Surfaces]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [IdOwner] in table 'LightConstructions'
ALTER TABLE [dbo].[LightConstructions]
ADD CONSTRAINT [FK_LightConstructions_Owners]
    FOREIGN KEY ([IdOwner])
    REFERENCES [dbo].[Owners]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LightConstructions_Owners'
CREATE INDEX [IX_FK_LightConstructions_Owners]
ON [dbo].[LightConstructions]
    ([IdOwner]);
GO

-- Creating foreign key on [IdOwner] in table 'MetalConstructions'
ALTER TABLE [dbo].[MetalConstructions]
ADD CONSTRAINT [FK_MetalConstructions_Owners]
    FOREIGN KEY ([IdOwner])
    REFERENCES [dbo].[Owners]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MetalConstructions_Owners'
CREATE INDEX [IX_FK_MetalConstructions_Owners]
ON [dbo].[MetalConstructions]
    ([IdOwner]);
GO

-- Creating foreign key on [IdWhoAdded] in table 'LightConstructions'
ALTER TABLE [dbo].[LightConstructions]
ADD CONSTRAINT [FK_LightConstructions_Owners1]
    FOREIGN KEY ([IdWhoAdded])
    REFERENCES [dbo].[Owners]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LightConstructions_Owners1'
CREATE INDEX [IX_FK_LightConstructions_Owners1]
ON [dbo].[LightConstructions]
    ([IdWhoAdded]);
GO

-- Creating foreign key on [IdWhoAdded] in table 'MetalConstructions'
ALTER TABLE [dbo].[MetalConstructions]
ADD CONSTRAINT [FK_MetalConstructions_Owners1]
    FOREIGN KEY ([IdWhoAdded])
    REFERENCES [dbo].[Owners]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MetalConstructions_Owners1'
CREATE INDEX [IX_FK_MetalConstructions_Owners1]
ON [dbo].[MetalConstructions]
    ([IdWhoAdded]);
GO

-- Creating foreign key on [Owner_Id] in table 'Billboards1'
ALTER TABLE [dbo].[Billboards1]
ADD CONSTRAINT [FK_OwnerBillboards]
    FOREIGN KEY ([Owner_Id])
    REFERENCES [dbo].[Owners]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OwnerBillboards'
CREATE INDEX [IX_FK_OwnerBillboards]
ON [dbo].[Billboards1]
    ([Owner_Id]);
GO

-- Creating foreign key on [Billboard_Id] in table 'Sides'
ALTER TABLE [dbo].[Sides]
ADD CONSTRAINT [FK_BillboardSide]
    FOREIGN KEY ([Billboard_Id])
    REFERENCES [dbo].[Billboards1]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BillboardSide'
CREATE INDEX [IX_FK_BillboardSide]
ON [dbo].[Sides]
    ([Billboard_Id]);
GO

-- Creating foreign key on [Side_Id] in table 'Surfaces'
ALTER TABLE [dbo].[Surfaces]
ADD CONSTRAINT [FK_SideSurface]
    FOREIGN KEY ([Side_Id])
    REFERENCES [dbo].[Sides]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SideSurface'
CREATE INDEX [IX_FK_SideSurface]
ON [dbo].[Surfaces]
    ([Side_Id]);
GO

-- Creating foreign key on [AdType_Id] in table 'Billboards1'
ALTER TABLE [dbo].[Billboards1]
ADD CONSTRAINT [FK_AdTypeBillboard]
    FOREIGN KEY ([AdType_Id])
    REFERENCES [dbo].[AdTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AdTypeBillboard'
CREATE INDEX [IX_FK_AdTypeBillboard]
ON [dbo].[Billboards1]
    ([AdType_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------