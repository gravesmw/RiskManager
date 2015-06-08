CREATE TABLE [Metric].[ObjectDefect] (
    [ObjectID] BIGINT NOT NULL,
    [DefectID] INT    NOT NULL,
    CONSTRAINT [PK_ObjectDefect] PRIMARY KEY CLUSTERED ([ObjectID] ASC, [DefectID] ASC),
    CONSTRAINT [FK_ObjectDefect_Defect] FOREIGN KEY ([DefectID]) REFERENCES [Content].[Defect] ([DefectID]),
    CONSTRAINT [FK_ObjectDefect_Object] FOREIGN KEY ([ObjectID]) REFERENCES [Content].[Object] ([ObjectID])
);

