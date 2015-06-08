CREATE TABLE [Content].[Object] (
    [ObjectID]       BIGINT        IDENTITY (1, 1) NOT NULL,
    [Name]           VARCHAR (400) NOT NULL,
    [ObjectTypeID]   INT           NOT NULL,
    [ParentObjectID] BIGINT        NULL,
    [RootObjectID]   BIGINT        NULL,
    CONSTRAINT [PK_Object] PRIMARY KEY CLUSTERED ([ObjectID] ASC),
    CONSTRAINT [FK_Object_ObjectType] FOREIGN KEY ([ObjectTypeID]) REFERENCES [Content].[ObjectType] ([ObjectTypeID])
);

