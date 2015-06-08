CREATE VIEW [Report].[DimObjectType]
AS
SELECT	ObjectTypeID AS ObjectTypeKey,
		Name,
		ParentObjectTypeID AS ParentObjectTypeKey
FROM	Content.ObjectType WITH (NOLOCK)