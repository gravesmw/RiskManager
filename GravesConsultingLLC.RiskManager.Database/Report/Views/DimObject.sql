
CREATE VIEW [Report].[DimObject]
AS
SELECT	ObjectID AS ObjectKey,
		Name,
		ParentObjectID AS ParentObjectKey,
		COALESCE(RootObjectID, ObjectID) AS RootObjectKey,
		ObjectTypeID AS ObjectTypeKey
FROM	Content.Object WITH (NOLOCK)
