CREATE PROCEDURE [Report].[spUpdateObjectType]
	@ObjectTypeID			INT,
	@Name					VARCHAR(128),
	@Description			VARCHAR(512) = NULL,
	@ParentObjectTypeID		INT = NULL

AS
SET NOCOUNT ON
BEGIN
	UPDATE	Content.ObjectType
	SET		Name = @Name, 
			Description = @Description, 
			ParentObjectTypeID = @ParentObjectTypeID
	WHERE	ObjectTypeID = @ObjectTypeID
END