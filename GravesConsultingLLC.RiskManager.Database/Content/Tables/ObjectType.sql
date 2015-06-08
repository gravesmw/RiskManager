CREATE TABLE [Content].[ObjectType] (
    [ObjectTypeID]       INT           IDENTITY (1, 1) NOT NULL,
    [Name]               VARCHAR (128) NOT NULL,
    [ParentObjectTypeID] INT           NULL,
    CONSTRAINT [PK_ObjectType] PRIMARY KEY CLUSTERED ([ObjectTypeID] ASC),
    CONSTRAINT [FK_ObjectType_ObjectType] FOREIGN KEY ([ParentObjectTypeID]) REFERENCES [Content].[ObjectType] ([ObjectTypeID])
);

