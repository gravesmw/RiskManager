CREATE PROCEDURE [Report].[spGetPossibleContainers]
	@ViewID INT
AS
SET NOCOUNT ON
BEGIN
	SELECT	c.ContainerID AS NodeID,
			c.Name
	FROM	Content.Container c
	LEFT JOIN Content.ContainerViews v ON v.ViewID = @ViewID
		AND v.ContainerID = c.ContainerID
	WHERE	v.ContainerViewID IS NULL
	ORDER BY c.Name			
END