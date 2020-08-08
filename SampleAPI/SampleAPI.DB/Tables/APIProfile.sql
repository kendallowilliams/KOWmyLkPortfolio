﻿CREATE TABLE [dbo].[APIProfile]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    [Name] VARCHAR(128) NOT NULL, 
    [Email] VARCHAR(256) NULL, 
    [Description] VARCHAR(1024) NULL, 
    [CreatedBy] VARCHAR(128) NOT NULL, 
    [CreatedOn] DATETIME2 NOT NULL DEFAULT CURRENT_TIMESTAMP, 
    [ModifiedBy] VARCHAR(128) NOT NULL, 
    [ModifiedOn] DATETIME2 NOT NULL DEFAULT CURRENT_TIMESTAMP
)
