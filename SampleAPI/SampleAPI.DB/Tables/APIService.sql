CREATE TABLE [dbo].[APIService]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    [Name] VARCHAR(128) NOT NULL, 
    [Action] VARCHAR(128) NOT NULL, 
    [Controller] VARCHAR(128) NOT NULL, 
    [Description] VARCHAR(1024) NULL, 
    [Disabled] BIT NOT NULL DEFAULT 0,
    [DisabledResponseCode] INT NULL,
    [DisabledResponseMessage] VARCHAR(1024) NULL,
    [ServiceDefinedFields] VARCHAR(MAX) NULL,
    [ConnectionInfo] VARCHAR(MAX) NULL,
    [CreatedBy] VARCHAR(128) NOT NULL, 
    [CreatedOn] DATETIME2 NOT NULL DEFAULT CURRENT_TIMESTAMP, 
    [ModifiedBy] VARCHAR(128) NOT NULL, 
    [ModifiedOn] DATETIME2 NOT NULL DEFAULT CURRENT_TIMESTAMP
)
