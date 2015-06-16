CREATE VIEW [Report].[ObjectToContainerBridge]
AS
SELECT	ObjectID AS ObjectKey,
		ContainerViewID AS ContainerViewKey
FROM	Content.ContainerObjects