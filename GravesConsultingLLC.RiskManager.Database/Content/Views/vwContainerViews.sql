

CREATE VIEW [Content].[vwContainerViews]
AS
WITH Hierarchy AS (
	SELECT	cv.ViewID,
			cv.ContainerViewID,
			c.Name,
			0 AS Level,
			CAST('/' + c.Name AS VARCHAR(MAX)) AS Path	
	FROM	Content.ContainerViews cv WITH (NOLOCK)
	JOIN	Content.Container c WITH (NOLOCK) ON c.ContainerID = cv.ContainerID
	WHERE	cv.ParentContainerViewID IS NULL
	UNION ALL 
	SELECT	cv.ViewID,
			cv.ContainerViewID,
			c.Name,
			h.Level + 1,
			h.Path + CAST('/' + c.Name AS VARCHAR(MAX))
	FROM	Hierarchy h 
	JOIN	Content.ContainerViews cv WITH (NOLOCK) ON cv.ParentContainerViewID = h.ContainerViewID
	JOIN	Content.Container c WITH (NOLOCK) ON c.ContainerID = cv.ContainerID

)

SELECT	*
FROM	Hierarchy

