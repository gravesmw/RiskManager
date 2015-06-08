CREATE VIEW Report.ObjectToDefectBridge
AS
SELECT	ObjectID AS ObjectKey,
		DefectID AS DefectKey
FROM	Metric.ObjectDefect WITH (NOLOCK)