CREATE VIEW [Report].[DimDefectGroup]
AS
SELECT	DefectGroupID AS DefectGroupKey,
		Name,
		ParentDefectGroupID AS ParentDefectGroupKey
FROM	Content.DefectGroup