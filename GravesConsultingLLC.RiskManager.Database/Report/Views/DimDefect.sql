CREATE VIEW [Report].[DimDefect]
AS
SELECT	DefectID AS DefectKey,
		Name,
		DefectGroupID AS DefectGroupKey
FROM	Content.Defect