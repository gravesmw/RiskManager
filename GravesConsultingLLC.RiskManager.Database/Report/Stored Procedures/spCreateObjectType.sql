
CREATE PROCEDURE [Report].[spCreateObjectType]
	@Name					VARCHAR(128),
	@Description			VARCHAR(512)=NULL,
	@ParentObjectTypeID	INT=NULL,
	@ObjectTypeID			INT OUTPUT
AS
SET NOCOUNT ON
BEGIN
	SELECT	@ObjectTypeID = ObjectTypeID
	FROM	Content.ObjectType
	WHERE	Name = @Name

	IF ISNULL(@ObjectTypeID, 0) = 0
	BEGIN
		INSERT INTO Content.ObjectType(Name, Description, ParentObjectTypeID)
		VALUES(@Name, @Description, @ParentObjectTypeID)

		SET @ObjectTypeID = SCOPE_IDENTITY()
	END
	RETURN @ObjectTypeID
END