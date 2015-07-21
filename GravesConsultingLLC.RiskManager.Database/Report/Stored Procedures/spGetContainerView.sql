CREATE PROCEDURE [Report].[spGetContainerView] 
	@ViewID	INT
AS
SET NOCOUNT ON
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
BEGIN
	WITH Hierarchy AS (
		SELECT	cv.ContainerViewID,
				c.Name,
				NULL AS ParentContainerViewID,
				0 AS Level	
		FROM	Content.ContainerViews cv
		JOIN	Content.Container c ON c.ContainerID = cv.ContainerID
		WHERE	ViewID = @ViewID
		AND		ParentContainerViewID IS NULL
		UNION ALL
		SELECT	cv.ContainerViewID,
				c.Name,
				cv.ParentContainerViewID,
				h.Level + 1
		FROM	Hierarchy h
		JOIN	Content.ContainerViews cv ON cv.ParentContainerViewID = h.ContainerViewID
		JOIN	Content.Container c ON c.ContainerID = cv.ContainerID
	
	)

	SELECT	ContainerViewID AS NodeID,
			Name,
			ParentContainerViewID AS ParentID,
			Level
	FROM	Hierarchy
	ORDER BY Level, Name
END