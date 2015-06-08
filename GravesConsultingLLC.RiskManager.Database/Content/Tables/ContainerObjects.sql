CREATE TABLE [Content].[ContainerObjects] (
    [ContainerViewID] INT    NOT NULL,
    [ObjectID]        BIGINT NOT NULL,
    CONSTRAINT [PK_ContainerObjects] PRIMARY KEY CLUSTERED ([ContainerViewID] ASC, [ObjectID] ASC),
    CONSTRAINT [FK_ContainerObjects_ContainerViews] FOREIGN KEY ([ContainerViewID]) REFERENCES [Content].[ContainerViews] ([ContainerViewID]),
    CONSTRAINT [FK_ContainerObjects_Object] FOREIGN KEY ([ObjectID]) REFERENCES [Content].[Object] ([ObjectID])
);

