SET QUOTED_IDENTIFIER OFF
SELECT
	GroupDate as 'DateTime',
	CAST(AVG(K1_AI1PV * (1 - ROUND(K1_AI1PV / 32767,  0, 1))) as decimal(15, 2)) as 'K1_AI1PV',
	CAST(AVG(K1_AI2PV * (1 - ROUND(K1_AI2PV / 32767,  0, 1))) as decimal(15, 2)) as 'K1_AI2PV',
	CAST(AVG(K1_AI3PV * (1 - ROUND(K1_AI3PV / 327.67, 0, 1))) as decimal(15, 2)) as 'K1_AI3PV',
	CAST(AVG(K2_AI1PV * (1 - ROUND(K2_AI1PV / 32767,  0, 1))) as decimal(15, 2)) as 'K2_AI1PV',
	CAST(AVG(K2_AI2PV * (1 - ROUND(K2_AI2PV / 32767,  0, 1))) as decimal(15, 2)) as 'K2_AI2PV',
	CAST(AVG(K2_AI3PV * (1 - ROUND(K2_AI3PV / 327.67, 0, 1))) as decimal(15, 2)) as 'K2_AI3PV',
	CAST(AVG(K3_AI1PV * (1 - ROUND(K3_AI1PV / 32767,  0, 1))) as decimal(15, 2)) as 'K3_AI1PV',
	CAST(AVG(K3_AI2PV * (1 - ROUND(K3_AI2PV / 32767,  0, 1))) as decimal(15, 2)) as 'K3_AI2PV',
	CAST(AVG(K3_AI3PV * (1 - ROUND(K3_AI3PV / 327.67, 0, 1))) as decimal(15, 2)) as 'K3_AI3PV',
	CAST(AVG(K3_AI4PV * (1 - ROUND(K3_AI4PV / 327.67, 0, 1))) as decimal(15, 2)) as 'K3_AI4PV',
	CAST(AVG(K4_AI1PV * (1 - ROUND(K4_AI1PV / 32767,  0, 1))) as decimal(15, 2)) as "K4_AI1PV",
	CAST(AVG(K4_AI2PV * (1 - ROUND(K4_AI2PV / 32767,  0, 1))) as decimal(15, 2)) as "K4_AI2PV",
	CAST(AVG(K4_AI3PV * (1 - ROUND(K4_AI3PV / 327.67, 0, 1))) as decimal(15, 2)) as "K4_AI3PV",
	CAST(AVG(K4_AI4PV * (1 - ROUND(K4_AI4PV / 327.67, 0, 1))) as decimal(15, 2)) as "K4_AI4PV",
	CAST(AVG(K5_AI1PV * (1 - ROUND(K5_AI1PV / 32767,  0, 1))) as decimal(15, 2)) as 'K5_AI1PV',
	CAST(AVG(K5_AI2PV * (1 - ROUND(K5_AI2PV / 32767,  0, 1))) as decimal(15, 2)) as 'K5_AI2PV',
	CAST(AVG(K5_AI3PV * (1 - ROUND(K5_AI3PV / 327.67, 0, 1))) as decimal(15, 2)) as 'K5_AI3PV',
	CAST(AVG(K5_AI4PV * (1 - ROUND(K5_AI4PV / 327.67, 0, 1))) as decimal(15, 2)) as 'K5_AI4PV'
FROM (
	SELECT
		DateAdd(day, DateDiff(day, 0, DateAdd(minute, -1, DateTime)), 0) as GroupDate,
		*
	FROM
		OpenQuery(INSQL,
		"SELECT
			DateTime,
			K1_AI1PV, K1_AI2PV, K1_AI3PV,
			K2_AI1PV, K2_AI2PV, K2_AI3PV,
			K3_AI1PV, K3_AI2PV, K3_AI3PV, K3_AI4PV,
			K4_AI1PV, K4_AI2PV, K4_AI3PV, K4_AI4PV,
			K5_AI1PV, K5_AI2PV, K5_AI3PV, K5_AI4PV
		FROM Runtime.dbo.AnalogWideHistory
		WHERE wwVersion = 'Latest'
		AND wwRetrievalMode = 'Cyclic'
		AND wwResolution = 60000
		AND DateTime >= '@start 00:01:00.000'
		AND DateTime <= '@finish 00:00:00.000'")
	) AS T1
GROUP BY
	GroupDate
ORDER BY
	GroupDate