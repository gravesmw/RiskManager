
CREATE PROCEDURE [Report].[spDeleteObjectType]
	@ObjectTypeID			INT
AS
SET NOCOUNT ON
BEGIN
	DELETE	Content.ObjectType
	FROM	Content.ObjectType
	WHERE	ObjectTypeID = @ObjectTypeID
END