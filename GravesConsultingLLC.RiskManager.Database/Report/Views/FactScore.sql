





CREATE VIEW [Report].[FactScore]
AS
SELECT	c.ObjectID AS ObjectKey,
		COALESCE(o.RootObjectID, o.ObjectID) AS RootObjectKey,
		c.DefectID AS DefectKey,
		s.Score
FROM	Content.Object o WITH (NOLOCK)
JOIN	Metric.ObjectDefect c WITH (NOLOCK) ON c.ObjectID = o.ObjectID 
JOIN	Content.Defect s WITH (NOLOCK) ON s.DefectID = c.DefectID





