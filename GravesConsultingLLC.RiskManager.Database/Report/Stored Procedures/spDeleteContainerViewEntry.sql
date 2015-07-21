
CREATE PROCEDURE [Report].[spDeleteContainerViewEntry]
	@ContainerViewID		INT
AS
SET NOCOUNT ON
BEGIN
	DELETE	Content.ContainerViews
	FROM	Content.ContainerViews
	WHERE	ContainerViewID = @ContainerViewID
END