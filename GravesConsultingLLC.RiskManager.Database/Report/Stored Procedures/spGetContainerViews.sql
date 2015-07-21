CREATE PROCEDURE Report.spGetContainerViews
AS
BEGIN
	SELECT	[ViewID],
			[Name]
	FROM	[Content].[ContainerView]
	ORDER BY [Name]
END