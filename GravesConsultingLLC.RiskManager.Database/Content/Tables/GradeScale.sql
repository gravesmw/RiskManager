CREATE TABLE [Content].[GradeScale] (
    [Grade]         VARCHAR (2)     NOT NULL,
    [LowerBoudary]  NUMERIC (16, 3) NOT NULL,
    [UpperBoundary] NUMERIC (16, 3) NOT NULL,
    CONSTRAINT [PK_GradeScale] PRIMARY KEY CLUSTERED ([Grade] ASC)
);

