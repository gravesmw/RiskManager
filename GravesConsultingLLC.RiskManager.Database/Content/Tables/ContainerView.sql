CREATE TABLE [Content].[ContainerView] (
    [ViewID] INT           IDENTITY (1, 1) NOT NULL,
    [Name]   VARCHAR (128) NOT NULL,
    CONSTRAINT [PK_ContainerView] PRIMARY KEY CLUSTERED ([ViewID] ASC)
);

