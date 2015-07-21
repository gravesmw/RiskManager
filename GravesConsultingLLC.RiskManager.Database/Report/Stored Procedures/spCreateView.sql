

CREATE PROCEDURE [Report].[spCreateView]
	@Name		VARCHAR(128),
	@ViewID		INT OUTPUT
AS
SET NOCOUNT ON
BEGIN
	SELECT	@ViewID = [ViewID]
	FROM	[Content].[ContainerView]
	WHERE	Name = @Name

	IF ISNULL(@ViewID, 0) = 0
	BEGIN
		INSERT INTO [Content].[ContainerView](Name)
		VALUES(@Name)

		SET @ViewID = SCOPE_IDENTITY()
	END

	RETURN @ViewID
END