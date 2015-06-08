CREATE TABLE [Content].[Defect] (
    [DefectID]         INT            IDENTITY (1, 1) NOT NULL,
    [GlobalIdentifier] VARCHAR (400)  NOT NULL,
    [Name]             VARCHAR (256)  NOT NULL,
    [Description]      VARCHAR (2048) NOT NULL,
    [DefectGroupID]    INT            NOT NULL,
    [Score]            NUMERIC (6, 3) NOT NULL,
    CONSTRAINT [PK_Defect] PRIMARY KEY CLUSTERED ([DefectID] ASC),
    CONSTRAINT [FK_Defect_DefectGroup] FOREIGN KEY ([DefectGroupID]) REFERENCES [Content].[DefectGroup] ([DefectGroupID])
);

