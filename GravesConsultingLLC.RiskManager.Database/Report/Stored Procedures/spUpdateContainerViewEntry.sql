
CREATE PROCEDURE Report.spUpdateContainerViewEntry
	@ContainerViewID		INT,
	@ParentContainerViewID	INT
AS
SET NOCOUNT ON
BEGIN
	UPDATE	Content.ContainerViews
	SET		ParentContainerViewID = @ParentContainerViewID
	WHERE	ContainerViewID = @ContainerViewID

END