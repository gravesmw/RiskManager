CREATE PROCEDURE Report.spGetDefectGroups
AS
SET NOCOUNT ON
BEGIN
	WITH Hierarchy AS(
		SELECT	DefectGroupID,
				Name,
				ParentDefectGroupID,
				0 AS Level
		FROM	Content.DefectGroup
		WHERE	ParentDefectGroupID IS NULL
		UNION ALL
		SELECT	g.DefectGroupID,
				g.Name,
				g.ParentDefectGroupID,
				h.Level + 1
		FROM	Hierarchy h
		JOIN	Content.DefectGroup g ON g.ParentDefectGroupID = h.DefectGroupID
	)


	SELECT	DefectGroupID AS NodeID,
			Name,
			ParentDefectGroupID AS ParentID,
			Level
	FROM	Hierarchy
	ORDER BY Level, Name
END