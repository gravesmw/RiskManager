CREATE PROCEDURE Report.spUpdateDefectGroup
	@DefectGroupID			INT,
	@Name					VARCHAR(128),
	@Description			VARCHAR(512) = NULL,
	@ParentDefectGroupID	INT = NULL

AS
SET NOCOUNT ON
BEGIN
	UPDATE	Content.DefectGroup
	SET		Name = @Name, 
			Description = @Description, 
			ParentDefectGroupID = @ParentDefectGroupID
	WHERE	DefectGroupID = @DefectGroupID
END