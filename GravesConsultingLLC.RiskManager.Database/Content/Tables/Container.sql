CREATE TABLE [Content].[Container] (
    [ContainerID] INT           IDENTITY (1, 1) NOT NULL,
    [Name]        VARCHAR (128) NULL,
    CONSTRAINT [PK_Container] PRIMARY KEY CLUSTERED ([ContainerID] ASC)
);

