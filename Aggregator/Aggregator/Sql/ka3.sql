SET QUOTED_IDENTIFIER OFF
SELECT
    DateAdd(day, DateDiff(day, 0, DateTime), 0)  as "DateTime",
    "@Mode"                                      as "Mode",
    CAST(AVG(K3_VG2_P18v)     as decimal(15, 2)) as "K3_VG2_P18v",
    CAST(AVG(K3_VG2_P2v)      as decimal(15, 2)) as "K3_VG2_P2v",
    CAST(AVG(K3_VG2_P3v)      as decimal(15, 2)) as "K3_VG2_P3v",
    CAST(AVG(K3_VG1_P24b)     as decimal(15, 2)) as "K3_VG1_P24b",
    CAST(SUM(K3_VG2_P1g) / 60 as decimal(15, 2)) as "K3_VG2_P1g",
    CAST(SUM(K3_VG2_P4g) / 60 as decimal(15, 2)) as "K3_VG2_P4g",
    CAST(AVG(K3_VG2_P77b)     as decimal(15, 2)) as "K3_VG2_P77b",
    CAST(SUM(K3_VG2_P2g) / 60 as decimal(15, 2)) as "K3_VG2_P2g",
    CAST(SUM(K3_VG2_P3g) / 60 as decimal(15, 2)) as "K3_VG2_P3g",
    CAST(AVG(K3_VG2_P79b)     as decimal(15, 2)) as "K3_VG2_P79b",
    CAST(AVG(K3_VG2_P89b)     as decimal(15, 2)) as "K3_VG2_P89b",
    CAST(AVG(K3_VG1_P35b)     as decimal(15, 2)) as "K3_VG1_P35b",
    CAST(AVG(K3_VG1_P36b)     as decimal(15, 2)) as "K3_VG1_P36b",
    CAST(AVG(K3_VG2_P3e)      as decimal(15, 2)) as "K3_VG2_P3e",
    CAST(AVG(K3_VG2_P4e)      as decimal(15, 2)) as "K3_VG2_P4e",
    CAST(AVG(K3_VG2_P88b)     as decimal(15, 2)) as "K3_VG2_P88b",
    CAST(AVG(K3_VG2_P80b)     as decimal(15, 2)) as "K3_VG2_P80b",
    CAST(AVG(K3_VG1_P39b)     as decimal(15, 2)) as "K3_VG1_P39b",
    CAST(AVG(K3_VG1_P40b)     as decimal(15, 2)) as "K3_VG1_P40b",
    CAST(SUM(K3_VG2_P5g) / 60 as decimal(15, 2)) as "K3_VG1_P40b"
FROM (
    SELECT
		DateTime as "DateTime",
	
		CASE WHEN ABS(ISNULL(K3_VG1_P35b, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(K3_VG1_P35b, 0) as decimal(15, 2)) END as "K3_VG1_P35b",
		CASE WHEN ABS(ISNULL(K3_VG1_P36b, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(K3_VG1_P36b, 0) as decimal(15, 2)) END as "K3_VG1_P36b",
		CASE WHEN ABS(ISNULL(K3_VG1_P39b, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(K3_VG1_P39b, 0) as decimal(15, 2)) END as "K3_VG1_P39b",
		CASE WHEN ABS(ISNULL(K3_VG1_P40b, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(K3_VG1_P40b, 0) as decimal(15, 2)) END as "K3_VG1_P40b",
		CASE WHEN ABS(ISNULL(K3_VG2_P88b, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(K3_VG2_P88b, 0) as decimal(15, 2)) END as "K3_VG2_P88b",
		CASE WHEN ABS(ISNULL(K3_VG2_P89b, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(K3_VG2_P89b, 0) as decimal(15, 2)) END as "K3_VG2_P89b",
		CASE WHEN ABS(ISNULL(K3_VG2_P79b, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(K3_VG2_P79b, 0) as decimal(15, 2)) END as "K3_VG2_P79b",
		CASE WHEN ABS(ISNULL(K3_VG2_P80b, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(K3_VG2_P80b, 0) as decimal(15, 2)) END as "K3_VG2_P80b",
		CASE WHEN ABS(ISNULL(K3_VG2_P3e, 0))  > 1000000 THEN 0 ELSE CAST(ISNULL(K3_VG2_P3e,  0) as decimal(15, 2)) END as "K3_VG2_P3e",
		CASE WHEN ABS(ISNULL(K3_VG2_P4e, 0))  > 1000000 THEN 0 ELSE CAST(ISNULL(K3_VG2_P4e,  0) as decimal(15, 2)) END as "K3_VG2_P4e",
		CASE WHEN ABS(ISNULL(K3_VG1_P24b, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(K3_VG1_P24b, 0) as decimal(15, 2)) END as "K3_VG1_P24b",
		CASE WHEN ABS(ISNULL(K3_VG2_P18v, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(K3_VG2_P18v, 0) as decimal(15, 2)) END as "K3_VG2_P18v",
		CASE WHEN ABS(ISNULL(K3_VG2_P2g, 0))  > 1000000 THEN 0 ELSE CAST(ISNULL(K3_VG2_P2g,  0) as decimal(15, 2)) END as "K3_VG2_P2g",
		CASE WHEN ABS(ISNULL(K3_VG2_P3g, 0))  > 1000000 THEN 0 ELSE CAST(ISNULL(K3_VG2_P3g,  0) as decimal(15, 2)) END as "K3_VG2_P3g",
		CASE WHEN ABS(ISNULL(K3_VG2_P2v, 0))  > 1000000 THEN 0 ELSE CAST(ISNULL(K3_VG2_P2v,  0) as decimal(15, 2)) END as "K3_VG2_P2v",
		CASE WHEN ABS(ISNULL(K3_VG2_P3v, 0))  > 1000000 THEN 0 ELSE CAST(ISNULL(K3_VG2_P3v,  0) as decimal(15, 2)) END as "K3_VG2_P3v",
		CASE WHEN ABS(ISNULL(K3_VG2_P1g, 0))  > 1000000 THEN 0 ELSE CAST(ISNULL(K3_VG2_P1g,  0) as decimal(15, 2)) END as "K3_VG2_P1g",
		CASE WHEN ABS(ISNULL(K3_VG2_P4g, 0))  > 1000000 THEN 0 ELSE CAST(ISNULL(K3_VG2_P4g,  0) as decimal(15, 2)) END as "K3_VG2_P4g",
		CASE WHEN ABS(ISNULL(K3_VG2_P5g, 0))  > 1000000 THEN 0 ELSE CAST(ISNULL(K3_VG2_P5g,  0) as decimal(15, 2)) END as "K3_VG2_P5g",
		CASE WHEN ABS(ISNULL(K3_VG2_P77b, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(K3_VG2_P77b, 0) as decimal(15, 2)) END as "K3_VG2_P77b"

	FROM OpenQuery(INSQL,
	"SELECT
		DateTime,
		K3_VG2_P18v,
        K3_VG2_P2v,
        K3_VG2_P3v,
        K3_VG1_P24b,
        K3_VG2_P1g,
        K3_VG2_P4g,
        K3_VG2_P77b,
        K3_VG2_P2g,
        K3_VG2_P3g,
        K3_VG2_P79b,
        K3_VG2_P89b,
        K3_VG1_P35b,
        K3_VG1_P36b,
        K3_VG2_P3e,
        K3_VG2_P4e,
        K3_VG2_P88b,
        K3_VG2_P80b,
        K3_VG1_P39b,
        K3_VG1_P40b,
        K3_VG2_P5g
	FROM
		Runtime.dbo.AnalogWideHistory
	WHERE
		wwVersion = 'Latest'
		AND wwRetrievalMode = 'Cyclic'
		AND wwResolution = 60000
		AND DateTime >= '@Start'
		AND DateTime <= '@End'
	")
) AS T
GROUP BY
    DateAdd(day, DateDiff(day, 0, DateTime), 0)
ORDER BY
    DateAdd(day, DateDiff(day, 0, DateTime), 0)