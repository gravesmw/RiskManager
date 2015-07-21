CREATE PROCEDURE Report.spDeleteDefectGroup
	@DefectGroupID			INT
AS
SET NOCOUNT ON
BEGIN
	DELETE	Content.DefectGroup
	FROM	Content.DefectGroup
	WHERE	DefectGroupID = @DefectGroupID
END