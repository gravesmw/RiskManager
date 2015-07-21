CREATE PROCEDURE Report.spCreateDefectGroup
	@Name					VARCHAR(128),
	@Description			VARCHAR(512)=NULL,
	@ParentDefectGroupID	INT=NULL,
	@DefectGroupID			INT OUTPUT
AS
SET NOCOUNT ON
BEGIN
	SELECT	@DefectGroupID = DefectGroupID
	FROM	Content.DefectGroup
	WHERE	Name = @Name

	IF ISNULL(@DefectGroupID, 0) = 0
	BEGIN
		INSERT INTO Content.DefectGroup(Name, Description, ParentDefectGroupID)
		VALUES(@Name, @Description, @ParentDefectGroupID)

		SET @DefectGroupID = SCOPE_IDENTITY()
	END
	RETURN @DefectGroupID
END