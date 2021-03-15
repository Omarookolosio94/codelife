
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 03/11/2021 06:54:46
-- Generated from EDMX file: C:\Users\A240990\Documents\Codelife\Codelife\Data\CodelifeDbContext.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Codelife];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK__Comments__author__38996AB5]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comments] DROP CONSTRAINT [FK__Comments__author__38996AB5];
GO
IF OBJECT_ID(N'[dbo].[FK__Comments__postId__398D8EEE]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comments] DROP CONSTRAINT [FK__Comments__postId__398D8EEE];
GO
IF OBJECT_ID(N'[dbo].[FK__Posts__authorId__34C8D9D1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Posts] DROP CONSTRAINT [FK__Posts__authorId__34C8D9D1];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Authors]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Authors];
GO
IF OBJECT_ID(N'[dbo].[Comments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Comments];
GO
IF OBJECT_ID(N'[dbo].[Posts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Posts];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Authors'
CREATE TABLE [dbo].[Authors] (
    [authorId] int IDENTITY(1,1) NOT NULL,
    [email] nvarchar(255)  NOT NULL,
    [password] varchar(255)  NULL,
    [username] varchar(50)  NULL,
    [dateRegistered] datetime  NULL,
    [profile] varchar(500)  NOT NULL
);
GO

-- Creating table 'Comments'
CREATE TABLE [dbo].[Comments] (
    [commentId] int IDENTITY(1,1) NOT NULL,
    [comment1] varchar(500)  NOT NULL,
    [commentator] varchar(50)  NOT NULL,
    [commentDate] datetime  NULL,
    [authorId] int  NULL,
    [postId] int  NULL
);
GO

-- Creating table 'Posts'
CREATE TABLE [dbo].[Posts] (
    [postId] int IDENTITY(1,1) NOT NULL,
    [title] varchar(500)  NOT NULL,
    [content] varchar(max)  NOT NULL,
    [publicationStatus] bit  NULL,
    [createDate] datetime  NULL,
    [updateDate] datetime  NULL,
    [tag] varchar(500)  NULL,
    [authorId] int  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [authorId] in table 'Authors'
ALTER TABLE [dbo].[Authors]
ADD CONSTRAINT [PK_Authors]
    PRIMARY KEY CLUSTERED ([authorId] ASC);
GO

-- Creating primary key on [commentId] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [PK_Comments]
    PRIMARY KEY CLUSTERED ([commentId] ASC);
GO

-- Creating primary key on [postId] in table 'Posts'
ALTER TABLE [dbo].[Posts]
ADD CONSTRAINT [PK_Posts]
    PRIMARY KEY CLUSTERED ([postId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [authorId] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [FK__Comments__author__38996AB5]
    FOREIGN KEY ([authorId])
    REFERENCES [dbo].[Authors]
        ([authorId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Comments__author__38996AB5'
CREATE INDEX [IX_FK__Comments__author__38996AB5]
ON [dbo].[Comments]
    ([authorId]);
GO

-- Creating foreign key on [authorId] in table 'Posts'
ALTER TABLE [dbo].[Posts]
ADD CONSTRAINT [FK__Posts__authorId__34C8D9D1]
    FOREIGN KEY ([authorId])
    REFERENCES [dbo].[Authors]
        ([authorId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Posts__authorId__34C8D9D1'
CREATE INDEX [IX_FK__Posts__authorId__34C8D9D1]
ON [dbo].[Posts]
    ([authorId]);
GO

-- Creating foreign key on [postId] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [FK__Comments__postId__398D8EEE]
    FOREIGN KEY ([postId])
    REFERENCES [dbo].[Posts]
        ([postId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Comments__postId__398D8EEE'
CREATE INDEX [IX_FK__Comments__postId__398D8EEE]
ON [dbo].[Comments]
    ([postId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------