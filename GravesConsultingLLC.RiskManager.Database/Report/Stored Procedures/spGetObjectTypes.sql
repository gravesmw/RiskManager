CREATE PROCEDURE [Report].[spGetObjectTypes]
AS
SET NOCOUNT ON
BEGIN
	WITH Hierarchy AS(
		SELECT	ObjectTypeID,
				Name,
				ParentObjectTypeID,
				0 AS Level
		FROM	Content.ObjectType
		WHERE	ParentObjectTypeID IS NULL
		UNION ALL
		SELECT	t.ObjectTypeID,
				t.Name,
				t.ParentObjectTypeID,
				h.Level + 1
		FROM	Hierarchy h
		JOIN	Content.ObjectType t ON t.ParentObjectTypeID = h.ObjectTypeID
	)


	SELECT	ObjectTypeID AS NodeID,
			Name,
			ParentObjectTypeID AS ParentID,
			Level
	FROM	Hierarchy
	ORDER BY Level, Name
END