CREATE TABLE [Content].[DefectGroup] (
    [DefectGroupID]       INT           IDENTITY (1, 1) NOT NULL,
    [Name]                VARCHAR (128) NOT NULL,
    [ParentDefectGroupID] INT           NULL,
    CONSTRAINT [PK_DefectGroup] PRIMARY KEY CLUSTERED ([DefectGroupID] ASC),
    CONSTRAINT [FK_DefectGroup_DefectGroup] FOREIGN KEY ([ParentDefectGroupID]) REFERENCES [Content].[DefectGroup] ([DefectGroupID])
);

