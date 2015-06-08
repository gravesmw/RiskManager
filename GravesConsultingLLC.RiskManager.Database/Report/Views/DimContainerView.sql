CREATE VIEW [Report].[DimContainerView]
AS
SELECT	v.ViewID AS ViewKey,
		v.Name AS ViewName,
		cv.ContainerViewID AS ContainerViewKey,
		cv.ParentContainerViewID AS ParentContainerViewKey,
		c.Name
FROM	Content.ContainerViews cv WITH (NOLOCK)
JOIN	Content.ContainerView v WITH (NOLOCK) ON v.ViewID = cv.ViewID
JOIN	Content.Container c WITH (NOLOCK) ON c.ContainerID = cv.ContainerID