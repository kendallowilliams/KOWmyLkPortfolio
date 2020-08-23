﻿CREATE TABLE [dbo].[APIProfileService]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    [APIProfileId] INT NOT NULL, 
    [APIServiceId] INT NOT NULL, 
    [ServiceDefinedFields] VARCHAR(MAX) NULL,
    [CreatedBy] VARCHAR(128) NOT NULL, 
    [CreatedOn] DATETIME2 NOT NULL DEFAULT CURRENT_TIMESTAMP, 
    [ModifiedBy] VARCHAR(128) NOT NULL, 
    [ModifiedOn] DATETIME2 NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT [FK_APIProfileService_APIProfile] FOREIGN KEY ([APIProfileId]) REFERENCES [APIProfile]([Id]) ON DELETE CASCADE, 
    CONSTRAINT [FK_APIProfileService_APIService] FOREIGN KEY ([APIServiceId]) REFERENCES [APIService]([Id]) ON DELETE CASCADE
)
