CREATE TABLE [dbo].UserConfig
(
	[Id] INT NOT NULL IDENTITY, 
    [Username] VARCHAR(50) NOT NULL, 
    [ValueName] VARCHAR(80) NOT NULL, 
    [ValueType] VARCHAR(20) NOT NULL, 
    [ValueData] VARCHAR(MAX) NULL,
	PRIMARY KEY CLUSTERED ([Id] ASC)
)

CREATE TABLE [dbo].[SystemConfig] (
    [Id]        INT           NOT NULL IDENTITY,
    [ValueName] VARCHAR (80)  NOT NULL,
    [ValueType] VARCHAR (20)  NOT NULL,
    [ValueData] VARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);