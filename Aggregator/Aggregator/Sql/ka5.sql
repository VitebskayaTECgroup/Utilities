﻿SET QUOTED_IDENTIFIER OFF
SELECT
    DateAdd(day, DateDiff(day, 0, DateTime), 0) as "DateTime",
    "@Mode"                                     as "Mode",
	CAST(AVG(KA5_VG1_K001) as decimal(15, 2))   as "KA5_VG1_K001",
	CAST(AVG(KA5_VG1_K002) as decimal(15, 2))   as "KA5_VG1_K002",
	CAST(AVG(KA5_VG1_K003) as decimal(15, 2))   as "KA5_VG1_K003",
	CAST(AVG(KA5_VG1_K004) as decimal(15, 2))   as "KA5_VG1_K004",
	CAST(AVG(KA5_VG1_K005) as decimal(15, 2))   as "KA5_VG1_K005",
	CAST(AVG(KA5_VG1_K006) as decimal(15, 2))   as "KA5_VG1_K006",
	CAST(AVG(KA5_VG1_K007) as decimal(15, 2))   as "KA5_VG1_K007",
	CAST(AVG(KA5_VG1_K008) as decimal(15, 2))   as "KA5_VG1_K008",
	CAST(AVG(KA5_VG1_K009) as decimal(15, 2))   as "KA5_VG1_K009",
	CAST(AVG(KA5_VG1_K010) as decimal(15, 2))   as "KA5_VG1_K010",
	CAST(AVG(KA5_VG1_K011) as decimal(15, 2))   as "KA5_VG1_K011",
	CAST(AVG(KA5_VG1_K012) as decimal(15, 2))   as "KA5_VG1_K012",
	CAST(AVG(KA5_VG1_K013) as decimal(15, 2))   as "KA5_VG1_K013",
	CAST(AVG(KA5_VG1_K014) as decimal(15, 2))   as "KA5_VG1_K014",
	CAST(AVG(KA5_VG1_K015) as decimal(15, 2))   as "KA5_VG1_K015",
	CAST(AVG(KA5_VG1_K016) as decimal(15, 2))   as "KA5_VG1_K016",
	CAST(AVG(KA5_VG1_K017) as decimal(15, 2))   as "KA5_VG1_K017",
	CAST(AVG(KA5_VG1_K018) as decimal(15, 2))   as "KA5_VG1_K018",
	CAST(AVG(KA5_VG1_K019) as decimal(15, 2))   as "KA5_VG1_K019",
	CAST(AVG(KA5_VG1_K020) as decimal(15, 2))   as "KA5_VG1_K020",
	CAST(SUM(KA5_VG1_K021) as decimal(15, 2))   as "KA5_VG1_K021",
	CAST(SUM(KA5_VG1_K022) as decimal(15, 2))   as "KA5_VG1_K022",
	CAST(SUM(KA5_VG1_K023) as decimal(15, 2))   as "KA5_VG1_K023",
	CAST(SUM(KA5_VG1_K024) as decimal(15, 2))   as "KA5_VG1_K024",
	CAST(AVG(KA5_VG1_K025) as decimal(15, 2))   as "KA5_VG1_K025",
	CAST(AVG(KA5_VG1_K026) as decimal(15, 2))   as "KA5_VG1_K026",
	CAST(AVG(KA5_VG1_K027) as decimal(15, 2))   as "KA5_VG1_K027",
	CAST(AVG(KA5_VG1_K028) as decimal(15, 2))   as "KA5_VG1_K028",
	CAST(AVG(KA5_VG1_K029) as decimal(15, 2))   as "KA5_VG1_K029",
	CAST(AVG(KA5_VG1_K030) as decimal(15, 2))   as "KA5_VG1_K030",
	CAST(AVG(KA5_VG1_K031) as decimal(15, 2))   as "KA5_VG1_K031",
	CAST(AVG(KA5_VG1_K032) as decimal(15, 2))   as "KA5_VG1_K032",
	CAST(AVG(KA5_VG1_K033) as decimal(15, 2))   as "KA5_VG1_K033",
	CAST(AVG(KA5_VG1_K034) as decimal(15, 2))   as "KA5_VG1_K034",
	CAST(AVG(KA5_VG1_K035) as decimal(15, 2))   as "KA5_VG1_K035",
	CAST(AVG(KA5_VG1_K036) as decimal(15, 2))   as "KA5_VG1_K036",
	CAST(AVG(KA5_VG1_K037) as decimal(15, 2))   as "KA5_VG1_K037",
	CAST(AVG(KA5_VG1_K038) as decimal(15, 2))   as "KA5_VG1_K038",
	CAST(AVG(KA5_VG1_K039) as decimal(15, 2))   as "KA5_VG1_K039",
	CAST(AVG(KA5_VG1_K040) as decimal(15, 2))   as "KA5_VG1_K040",
	CAST(AVG(KA5_VG1_K041) as decimal(15, 2))   as "KA5_VG1_K041",
	CAST(AVG(KA5_VG1_K042) as decimal(15, 2))   as "KA5_VG1_K042",
	CAST(AVG(KA5_VG1_K043) as decimal(15, 2))   as "KA5_VG1_K043",
	CAST(AVG(KA5_VG1_K044) as decimal(15, 2))   as "KA5_VG1_K044",
	CAST(AVG(KA5_VG1_K045) as decimal(15, 2))   as "KA5_VG1_K045",
	CAST(AVG(KA5_VG1_K046) as decimal(15, 2))   as "KA5_VG1_K046",
	CAST(AVG(KA5_VG1_K047) as decimal(15, 2))   as "KA5_VG1_K047",
	CAST(AVG(KA5_VG1_K048) as decimal(15, 2))   as "KA5_VG1_K048",
	CAST(AVG(KA5_VG1_K049) as decimal(15, 2))   as "KA5_VG1_K049",
	CAST(AVG(KA5_VG1_K050) as decimal(15, 2))   as "KA5_VG1_K050",
	CAST(AVG(KA5_VG1_K051) as decimal(15, 2))   as "KA5_VG1_K051",
	CAST(AVG(KA5_VG1_K052) as decimal(15, 2))   as "KA5_VG1_K052",
	CAST(AVG(KA5_VG1_K053) as decimal(15, 2))   as "KA5_VG1_K053",
	CAST(AVG(KA5_VG1_K054) as decimal(15, 2))   as "KA5_VG1_K054",
	CAST(AVG(KA5_VG1_K055) as decimal(15, 2))   as "KA5_VG1_K055",
	CAST(AVG(KA5_VG1_K056) as decimal(15, 2))   as "KA5_VG1_K056",
	CAST(AVG(KA5_VG1_K057) as decimal(15, 2))   as "KA5_VG1_K057",
	CAST(AVG(KA5_VG1_K058) as decimal(15, 2))   as "KA5_VG1_K058",
	CAST(AVG(KA5_VG1_K059) as decimal(15, 2))   as "KA5_VG1_K059",
	CAST(AVG(KA5_VG1_K060) as decimal(15, 2))   as "KA5_VG1_K060",
	CAST(AVG(KA5_VG1_K061) as decimal(15, 2))   as "KA5_VG1_K061",
	CAST(AVG(KA5_VG1_K062) as decimal(15, 2))   as "KA5_VG1_K062",
	CAST(AVG(KA5_VG1_K063) as decimal(15, 2))   as "KA5_VG1_K063",
	CAST(AVG(KA5_VG1_K064) as decimal(15, 2))   as "KA5_VG1_K064",
	CAST(AVG(KA5_VG1_K065) as decimal(15, 2))   as "KA5_VG1_K065",
	CAST(AVG(KA5_VG1_K066) as decimal(15, 2))   as "KA5_VG1_K066",
	CAST(AVG(KA5_VG1_K067) as decimal(15, 2))   as "KA5_VG1_K067",
	CAST(AVG(KA5_VG1_K068) as decimal(15, 2))   as "KA5_VG1_K068",
	CAST(AVG(KA5_VG1_K069) as decimal(15, 2))   as "KA5_VG1_K069",
	CAST(AVG(KA5_VG1_K070) as decimal(15, 2))   as "KA5_VG1_K070",
	CAST(AVG(KA5_VG1_K071) as decimal(15, 2))   as "KA5_VG1_K071",
	CAST(AVG(KA5_VG1_K072) as decimal(15, 2))   as "KA5_VG1_K072",
	CAST(AVG(KA5_VG1_K073) as decimal(15, 2))   as "KA5_VG1_K073",
	CAST(AVG(KA5_VG1_K074) as decimal(15, 2))   as "KA5_VG1_K074",
	CAST(AVG(KA5_VG1_K075) as decimal(15, 2))   as "KA5_VG1_K075",
	CAST(AVG(KA5_VG1_K076) as decimal(15, 2))   as "KA5_VG1_K076",
	CAST(AVG(KA5_VG1_K077) as decimal(15, 2))   as "KA5_VG1_K077",
	CAST(AVG(KA5_VG1_K078) as decimal(15, 2))   as "KA5_VG1_K078",
	CAST(AVG(KA5_VG1_K079) as decimal(15, 2))   as "KA5_VG1_K079",
	CAST(AVG(KA5_VG1_K080) as decimal(15, 2))   as "KA5_VG1_K080",
	CAST(AVG(KA5_VG1_K081) as decimal(15, 2))   as "KA5_VG1_K081",
	CAST(AVG(KA5_VG1_K082) as decimal(15, 2))   as "KA5_VG1_K082",
	CAST(AVG(KA5_VG1_K083) as decimal(15, 2))   as "KA5_VG1_K083",
	CAST(AVG(KA5_VG1_K084) as decimal(15, 2))   as "KA5_VG1_K084",
	CAST(AVG(KA5_VG1_K085) as decimal(15, 2))   as "KA5_VG1_K085",
	CAST(AVG(KA5_VG1_K086) as decimal(15, 2))   as "KA5_VG1_K086",
	CAST(AVG(KA5_VG1_K087) as decimal(15, 2))   as "KA5_VG1_K087",
	CAST(AVG(KA5_VG1_K088) as decimal(15, 2))   as "KA5_VG1_K088",
	CAST(AVG(KA5_VG1_K089) as decimal(15, 2))   as "KA5_VG1_K089",
	CAST(AVG(KA5_VG1_K090) as decimal(15, 2))   as "KA5_VG1_K090",
	CAST(AVG(KA5_VG1_K091) as decimal(15, 2))   as "KA5_VG1_K091",
	CAST(AVG(KA5_VG1_K092) as decimal(15, 2))   as "KA5_VG1_K092",
	CAST(AVG(KA5_VG1_K093) as decimal(15, 2))   as "KA5_VG1_K093",
	CAST(AVG(KA5_VG1_K094) as decimal(15, 2))   as "KA5_VG1_K094",
	CAST(AVG(KA5_VG1_K095) as decimal(15, 2))   as "KA5_VG1_K095",
	CAST(AVG(KA5_VG1_K096) as decimal(15, 2))   as "KA5_VG1_K096",
	CAST(AVG(KA5_VG1_K097) as decimal(15, 2))   as "KA5_VG1_K097",
	CAST(AVG(KA5_VG1_K098) as decimal(15, 2))   as "KA5_VG1_K098",
	CAST(AVG(KA5_VG1_K099) as decimal(15, 2))   as "KA5_VG1_K099",
	CAST(AVG(KA5_VG1_K100) as decimal(15, 2))   as "KA5_VG1_K100",
	CAST(AVG(KA5_VG1_K101) as decimal(15, 2))   as "KA5_VG1_K101",
	CAST(AVG(KA5_VG1_K102) as decimal(15, 2))   as "KA5_VG1_K102",
	CAST(AVG(KA5_VG1_K103) as decimal(15, 2))   as "KA5_VG1_K103",
	CAST(AVG(KA5_VG1_K104) as decimal(15, 2))   as "KA5_VG1_K104",
	CAST(AVG(KA5_VG1_K105) as decimal(15, 2))   as "KA5_VG1_K105",
	CAST(AVG(KA5_VG1_K106) as decimal(15, 2))   as "KA5_VG1_K106",
	CAST(AVG(KA5_VG1_K107) as decimal(15, 2))   as "KA5_VG1_K107",
	CAST(AVG(KA5_VG1_K108) as decimal(15, 2))   as "KA5_VG1_K108",
	CAST(AVG(KA5_VG1_K109) as decimal(15, 2))   as "KA5_VG1_K109",
	CAST(AVG(KA5_VG1_K110) as decimal(15, 2))   as "KA5_VG1_K110",
	CAST(AVG(KA5_VG1_K111) as decimal(15, 2))   as "KA5_VG1_K111",
	CAST(AVG(KA5_VG1_K112) as decimal(15, 2))   as "KA5_VG1_K112",
	CAST(AVG(KA5_VG1_K113) as decimal(15, 2))   as "KA5_VG1_K113",
	CAST(AVG(KA5_VG1_K114) as decimal(15, 2))   as "KA5_VG1_K114",
	CAST(AVG(KA5_VG1_K115) as decimal(15, 2))   as "KA5_VG1_K115",
	CAST(AVG(KA5_VG1_K116) as decimal(15, 2))   as "KA5_VG1_K116"
FROM (
    SELECT
		DateTime as "DateTime",
		CASE WHEN ABS(ISNULL(KA5_VG1_K001, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K001, 0) as decimal(15, 2)) END as "KA5_VG1_K001",
		CASE WHEN ABS(ISNULL(KA5_VG1_K002, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K002, 0) as decimal(15, 2)) END as "KA5_VG1_K002",
		CASE WHEN ABS(ISNULL(KA5_VG1_K003, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K003, 0) as decimal(15, 2)) END as "KA5_VG1_K003",
		CASE WHEN ABS(ISNULL(KA5_VG1_K004, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K004, 0) as decimal(15, 2)) END as "KA5_VG1_K004",
		CASE WHEN ABS(ISNULL(KA5_VG1_K005, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K005, 0) as decimal(15, 2)) END as "KA5_VG1_K005",
		CASE WHEN ABS(ISNULL(KA5_VG1_K006, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K006, 0) as decimal(15, 2)) END as "KA5_VG1_K006",
		CASE WHEN ABS(ISNULL(KA5_VG1_K007, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K007, 0) as decimal(15, 2)) END as "KA5_VG1_K007",
		CASE WHEN ABS(ISNULL(KA5_VG1_K008, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K008, 0) as decimal(15, 2)) END as "KA5_VG1_K008",
		CASE WHEN ABS(ISNULL(KA5_VG1_K009, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K009, 0) as decimal(15, 2)) END as "KA5_VG1_K009",
		CASE WHEN ABS(ISNULL(KA5_VG1_K010, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K010, 0) as decimal(15, 2)) END as "KA5_VG1_K010",
		CASE WHEN ABS(ISNULL(KA5_VG1_K011, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K011, 0) as decimal(15, 2)) END as "KA5_VG1_K011",
		CASE WHEN ABS(ISNULL(KA5_VG1_K012, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K012, 0) as decimal(15, 2)) END as "KA5_VG1_K012",
		CASE WHEN ABS(ISNULL(KA5_VG1_K013, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K013, 0) as decimal(15, 2)) END as "KA5_VG1_K013",
		CASE WHEN ABS(ISNULL(KA5_VG1_K014, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K014, 0) as decimal(15, 2)) END as "KA5_VG1_K014",
		CASE WHEN ABS(ISNULL(KA5_VG1_K015, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K015, 0) as decimal(15, 2)) END as "KA5_VG1_K015",
		CASE WHEN ABS(ISNULL(KA5_VG1_K016, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K016, 0) as decimal(15, 2)) END as "KA5_VG1_K016",
		CASE WHEN ABS(ISNULL(KA5_VG1_K017, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K017, 0) as decimal(15, 2)) END as "KA5_VG1_K017",
		CASE WHEN ABS(ISNULL(KA5_VG1_K018, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K018, 0) as decimal(15, 2)) END as "KA5_VG1_K018",
		CASE WHEN ABS(ISNULL(KA5_VG1_K019, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K019, 0) as decimal(15, 2)) END as "KA5_VG1_K019",
		CASE WHEN ABS(ISNULL(KA5_VG1_K020, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K020, 0) as decimal(15, 2)) END as "KA5_VG1_K020",
		CASE WHEN ABS(ISNULL(KA5_VG1_K021, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K021, 0) as decimal(15, 2)) END as "KA5_VG1_K021",
		CASE WHEN ABS(ISNULL(KA5_VG1_K022, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K022, 0) as decimal(15, 2)) END as "KA5_VG1_K022",
		CASE WHEN ABS(ISNULL(KA5_VG1_K023, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K023, 0) as decimal(15, 2)) END as "KA5_VG1_K023",
		CASE WHEN ABS(ISNULL(KA5_VG1_K024, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K024, 0) as decimal(15, 2)) END as "KA5_VG1_K024",
		CASE WHEN ABS(ISNULL(KA5_VG1_K025, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K025, 0) as decimal(15, 2)) END as "KA5_VG1_K025",
		CASE WHEN ABS(ISNULL(KA5_VG1_K026, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K026, 0) as decimal(15, 2)) END as "KA5_VG1_K026",
		CASE WHEN ABS(ISNULL(KA5_VG1_K027, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K027, 0) as decimal(15, 2)) END as "KA5_VG1_K027",
		CASE WHEN ABS(ISNULL(KA5_VG1_K028, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K028, 0) as decimal(15, 2)) END as "KA5_VG1_K028",
		CASE WHEN ABS(ISNULL(KA5_VG1_K029, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K029, 0) as decimal(15, 2)) END as "KA5_VG1_K029",
		CASE WHEN ABS(ISNULL(KA5_VG1_K030, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K030, 0) as decimal(15, 2)) END as "KA5_VG1_K030",
		CASE WHEN ABS(ISNULL(KA5_VG1_K031, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K031, 0) as decimal(15, 2)) END as "KA5_VG1_K031",
		CASE WHEN ABS(ISNULL(KA5_VG1_K032, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K032, 0) as decimal(15, 2)) END as "KA5_VG1_K032",
		CASE WHEN ABS(ISNULL(KA5_VG1_K033, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K033, 0) as decimal(15, 2)) END as "KA5_VG1_K033",
		CASE WHEN ABS(ISNULL(KA5_VG1_K034, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K034, 0) as decimal(15, 2)) END as "KA5_VG1_K034",
		CASE WHEN ABS(ISNULL(KA5_VG1_K035, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K035, 0) as decimal(15, 2)) END as "KA5_VG1_K035",
		CASE WHEN ABS(ISNULL(KA5_VG1_K036, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K036, 0) as decimal(15, 2)) END as "KA5_VG1_K036",
		CASE WHEN ABS(ISNULL(KA5_VG1_K037, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K037, 0) as decimal(15, 2)) END as "KA5_VG1_K037",
		CASE WHEN ABS(ISNULL(KA5_VG1_K038, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K038, 0) as decimal(15, 2)) END as "KA5_VG1_K038",
		CASE WHEN ABS(ISNULL(KA5_VG1_K039, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K039, 0) as decimal(15, 2)) END as "KA5_VG1_K039",
		CASE WHEN ABS(ISNULL(KA5_VG1_K040, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K040, 0) as decimal(15, 2)) END as "KA5_VG1_K040",
		CASE WHEN ABS(ISNULL(KA5_VG1_K041, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K041, 0) as decimal(15, 2)) END as "KA5_VG1_K041",
		CASE WHEN ABS(ISNULL(KA5_VG1_K042, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K042, 0) as decimal(15, 2)) END as "KA5_VG1_K042",
		CASE WHEN ABS(ISNULL(KA5_VG1_K043, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K043, 0) as decimal(15, 2)) END as "KA5_VG1_K043",
		CASE WHEN ABS(ISNULL(KA5_VG1_K044, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K044, 0) as decimal(15, 2)) END as "KA5_VG1_K044",
		CASE WHEN ABS(ISNULL(KA5_VG1_K045, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K045, 0) as decimal(15, 2)) END as "KA5_VG1_K045",
		CASE WHEN ABS(ISNULL(KA5_VG1_K046, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K046, 0) as decimal(15, 2)) END as "KA5_VG1_K046",
		CASE WHEN ABS(ISNULL(KA5_VG1_K047, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K047, 0) as decimal(15, 2)) END as "KA5_VG1_K047",
		CASE WHEN ABS(ISNULL(KA5_VG1_K048, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K048, 0) as decimal(15, 2)) END as "KA5_VG1_K048",
		CASE WHEN ABS(ISNULL(KA5_VG1_K049, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K049, 0) as decimal(15, 2)) END as "KA5_VG1_K049",
		CASE WHEN ABS(ISNULL(KA5_VG1_K050, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K050, 0) as decimal(15, 2)) END as "KA5_VG1_K050",
		CASE WHEN ABS(ISNULL(KA5_VG1_K051, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K051, 0) as decimal(15, 2)) END as "KA5_VG1_K051",
		CASE WHEN ABS(ISNULL(KA5_VG1_K052, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K052, 0) as decimal(15, 2)) END as "KA5_VG1_K052",
		CASE WHEN ABS(ISNULL(KA5_VG1_K053, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K053, 0) as decimal(15, 2)) END as "KA5_VG1_K053",
		CASE WHEN ABS(ISNULL(KA5_VG1_K054, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K054, 0) as decimal(15, 2)) END as "KA5_VG1_K054",
		CASE WHEN ABS(ISNULL(KA5_VG1_K055, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K055, 0) as decimal(15, 2)) END as "KA5_VG1_K055",
		CASE WHEN ABS(ISNULL(KA5_VG1_K056, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K056, 0) as decimal(15, 2)) END as "KA5_VG1_K056",
		CASE WHEN ABS(ISNULL(KA5_VG1_K057, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K057, 0) as decimal(15, 2)) END as "KA5_VG1_K057",
		CASE WHEN ABS(ISNULL(KA5_VG1_K058, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K058, 0) as decimal(15, 2)) END as "KA5_VG1_K058",
		CASE WHEN ABS(ISNULL(KA5_VG1_K059, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K059, 0) as decimal(15, 2)) END as "KA5_VG1_K059",
		CASE WHEN ABS(ISNULL(KA5_VG1_K060, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K060, 0) as decimal(15, 2)) END as "KA5_VG1_K060",
		CASE WHEN ABS(ISNULL(KA5_VG1_K061, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K061, 0) as decimal(15, 2)) END as "KA5_VG1_K061",
		CASE WHEN ABS(ISNULL(KA5_VG1_K062, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K062, 0) as decimal(15, 2)) END as "KA5_VG1_K062",
		CASE WHEN ABS(ISNULL(KA5_VG1_K063, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K063, 0) as decimal(15, 2)) END as "KA5_VG1_K063",
		CASE WHEN ABS(ISNULL(KA5_VG1_K064, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K064, 0) as decimal(15, 2)) END as "KA5_VG1_K064",
		CASE WHEN ABS(ISNULL(KA5_VG1_K065, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K065, 0) as decimal(15, 2)) END as "KA5_VG1_K065",
		CASE WHEN ABS(ISNULL(KA5_VG1_K066, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K066, 0) as decimal(15, 2)) END as "KA5_VG1_K066",
		CASE WHEN ABS(ISNULL(KA5_VG1_K067, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K067, 0) as decimal(15, 2)) END as "KA5_VG1_K067",
		CASE WHEN ABS(ISNULL(KA5_VG1_K068, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K068, 0) as decimal(15, 2)) END as "KA5_VG1_K068",
		CASE WHEN ABS(ISNULL(KA5_VG1_K069, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K069, 0) as decimal(15, 2)) END as "KA5_VG1_K069",
		CASE WHEN ABS(ISNULL(KA5_VG1_K070, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K070, 0) as decimal(15, 2)) END as "KA5_VG1_K070",
		CASE WHEN ABS(ISNULL(KA5_VG1_K071, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K071, 0) as decimal(15, 2)) END as "KA5_VG1_K071",
		CASE WHEN ABS(ISNULL(KA5_VG1_K072, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K072, 0) as decimal(15, 2)) END as "KA5_VG1_K072",
		CASE WHEN ABS(ISNULL(KA5_VG1_K073, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K073, 0) as decimal(15, 2)) END as "KA5_VG1_K073",
		CASE WHEN ABS(ISNULL(KA5_VG1_K074, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K074, 0) as decimal(15, 2)) END as "KA5_VG1_K074",
		CASE WHEN ABS(ISNULL(KA5_VG1_K075, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K075, 0) as decimal(15, 2)) END as "KA5_VG1_K075",
		CASE WHEN ABS(ISNULL(KA5_VG1_K076, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K076, 0) as decimal(15, 2)) END as "KA5_VG1_K076",
		CASE WHEN ABS(ISNULL(KA5_VG1_K077, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K077, 0) as decimal(15, 2)) END as "KA5_VG1_K077",
		CASE WHEN ABS(ISNULL(KA5_VG1_K078, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K078, 0) as decimal(15, 2)) END as "KA5_VG1_K078",
		CASE WHEN ABS(ISNULL(KA5_VG1_K079, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K079, 0) as decimal(15, 2)) END as "KA5_VG1_K079",
		CASE WHEN ABS(ISNULL(KA5_VG1_K080, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K080, 0) as decimal(15, 2)) END as "KA5_VG1_K080",
		CASE WHEN ABS(ISNULL(KA5_VG1_K081, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K081, 0) as decimal(15, 2)) END as "KA5_VG1_K081",
		CASE WHEN ABS(ISNULL(KA5_VG1_K082, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K082, 0) as decimal(15, 2)) END as "KA5_VG1_K082",
		CASE WHEN ABS(ISNULL(KA5_VG1_K083, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K083, 0) as decimal(15, 2)) END as "KA5_VG1_K083",
		CASE WHEN ABS(ISNULL(KA5_VG1_K084, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K084, 0) as decimal(15, 2)) END as "KA5_VG1_K084",
		CASE WHEN ABS(ISNULL(KA5_VG1_K085, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K085, 0) as decimal(15, 2)) END as "KA5_VG1_K085",
		CASE WHEN ABS(ISNULL(KA5_VG1_K086, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K086, 0) as decimal(15, 2)) END as "KA5_VG1_K086",
		CASE WHEN ABS(ISNULL(KA5_VG1_K087, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K087, 0) as decimal(15, 2)) END as "KA5_VG1_K087",
		CASE WHEN ABS(ISNULL(KA5_VG1_K088, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K088, 0) as decimal(15, 2)) END as "KA5_VG1_K088",
		CASE WHEN ABS(ISNULL(KA5_VG1_K089, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K089, 0) as decimal(15, 2)) END as "KA5_VG1_K089",
		CASE WHEN ABS(ISNULL(KA5_VG1_K090, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K090, 0) as decimal(15, 2)) END as "KA5_VG1_K090",
		CASE WHEN ABS(ISNULL(KA5_VG1_K091, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K091, 0) as decimal(15, 2)) END as "KA5_VG1_K091",
		CASE WHEN ABS(ISNULL(KA5_VG1_K092, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K092, 0) as decimal(15, 2)) END as "KA5_VG1_K092",
		CASE WHEN ABS(ISNULL(KA5_VG1_K093, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K093, 0) as decimal(15, 2)) END as "KA5_VG1_K093",
		CASE WHEN ABS(ISNULL(KA5_VG1_K094, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K094, 0) as decimal(15, 2)) END as "KA5_VG1_K094",
		CASE WHEN ABS(ISNULL(KA5_VG1_K095, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K095, 0) as decimal(15, 2)) END as "KA5_VG1_K095",
		CASE WHEN ABS(ISNULL(KA5_VG1_K096, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K096, 0) as decimal(15, 2)) END as "KA5_VG1_K096",
		CASE WHEN ABS(ISNULL(KA5_VG1_K097, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K097, 0) as decimal(15, 2)) END as "KA5_VG1_K097",
		CASE WHEN ABS(ISNULL(KA5_VG1_K098, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K098, 0) as decimal(15, 2)) END as "KA5_VG1_K098",
		CASE WHEN ABS(ISNULL(KA5_VG1_K099, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K099, 0) as decimal(15, 2)) END as "KA5_VG1_K099",
		CASE WHEN ABS(ISNULL(KA5_VG1_K100, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K100, 0) as decimal(15, 2)) END as "KA5_VG1_K100",
		CASE WHEN ABS(ISNULL(KA5_VG1_K101, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K101, 0) as decimal(15, 2)) END as "KA5_VG1_K101",
		CASE WHEN ABS(ISNULL(KA5_VG1_K102, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K102, 0) as decimal(15, 2)) END as "KA5_VG1_K102",
		CASE WHEN ABS(ISNULL(KA5_VG1_K103, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K103, 0) as decimal(15, 2)) END as "KA5_VG1_K103",
		CASE WHEN ABS(ISNULL(KA5_VG1_K104, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K104, 0) as decimal(15, 2)) END as "KA5_VG1_K104",
		CASE WHEN ABS(ISNULL(KA5_VG1_K105, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K105, 0) as decimal(15, 2)) END as "KA5_VG1_K105",
		CASE WHEN ABS(ISNULL(KA5_VG1_K106, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K106, 0) as decimal(15, 2)) END as "KA5_VG1_K106",
		CASE WHEN ABS(ISNULL(KA5_VG1_K107, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K107, 0) as decimal(15, 2)) END as "KA5_VG1_K107",
		CASE WHEN ABS(ISNULL(KA5_VG1_K108, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K108, 0) as decimal(15, 2)) END as "KA5_VG1_K108",
		CASE WHEN ABS(ISNULL(KA5_VG1_K109, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K109, 0) as decimal(15, 2)) END as "KA5_VG1_K109",
		CASE WHEN ABS(ISNULL(KA5_VG1_K110, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K110, 0) as decimal(15, 2)) END as "KA5_VG1_K110",
		CASE WHEN ABS(ISNULL(KA5_VG1_K111, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K111, 0) as decimal(15, 2)) END as "KA5_VG1_K111",
		CASE WHEN ABS(ISNULL(KA5_VG1_K112, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K112, 0) as decimal(15, 2)) END as "KA5_VG1_K112",
		CASE WHEN ABS(ISNULL(KA5_VG1_K113, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K113, 0) as decimal(15, 2)) END as "KA5_VG1_K113",
		CASE WHEN ABS(ISNULL(KA5_VG1_K114, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K114, 0) as decimal(15, 2)) END as "KA5_VG1_K114",
		CASE WHEN ABS(ISNULL(KA5_VG1_K115, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K115, 0) as decimal(15, 2)) END as "KA5_VG1_K115",
		CASE WHEN ABS(ISNULL(KA5_VG1_K116, 0)) > 1000000 THEN 0 ELSE CAST(ISNULL(KA5_VG1_K116, 0) as decimal(15, 2)) END as "KA5_VG1_K116"

	FROM OpenQuery(INSQL,
	"SELECT
		DateTime,
		KA5_VG1_K001,
		KA5_VG1_K002,
		KA5_VG1_K003,
		KA5_VG1_K004,
		KA5_VG1_K005,
		KA5_VG1_K006,
		KA5_VG1_K007,
		KA5_VG1_K008,
		KA5_VG1_K009,
		KA5_VG1_K010,
		KA5_VG1_K011,
		KA5_VG1_K012,
		KA5_VG1_K013,
		KA5_VG1_K014,
		KA5_VG1_K015,
		KA5_VG1_K016,
		KA5_VG1_K017,
		KA5_VG1_K018,
		KA5_VG1_K019,
		KA5_VG1_K020,
		KA5_VG1_K021,
		KA5_VG1_K022,
		KA5_VG1_K023,
		KA5_VG1_K024,
		KA5_VG1_K025,
		KA5_VG1_K026,
		KA5_VG1_K027,
		KA5_VG1_K028,
		KA5_VG1_K029,
		KA5_VG1_K030,
		KA5_VG1_K031,
		KA5_VG1_K032,
		KA5_VG1_K033,
		KA5_VG1_K034,
		KA5_VG1_K035,
		KA5_VG1_K036,
		KA5_VG1_K037,
		KA5_VG1_K038,
		KA5_VG1_K039,
		KA5_VG1_K040,
		KA5_VG1_K041,
		KA5_VG1_K042,
		KA5_VG1_K043,
		KA5_VG1_K044,
		KA5_VG1_K045,
		KA5_VG1_K046,
		KA5_VG1_K047,
		KA5_VG1_K048,
		KA5_VG1_K049,
		KA5_VG1_K050,
		KA5_VG1_K051,
		KA5_VG1_K052,
		KA5_VG1_K053,
		KA5_VG1_K054,
		KA5_VG1_K055,
		KA5_VG1_K056,
		KA5_VG1_K057,
		KA5_VG1_K058,
		KA5_VG1_K059,
		KA5_VG1_K060,
		KA5_VG1_K061,
		KA5_VG1_K062,
		KA5_VG1_K063,
		KA5_VG1_K064,
		KA5_VG1_K065,
		KA5_VG1_K066,
		KA5_VG1_K067,
		KA5_VG1_K068,
		KA5_VG1_K069,
		KA5_VG1_K070,
		KA5_VG1_K071,
		KA5_VG1_K072,
		KA5_VG1_K073,
		KA5_VG1_K074,
		KA5_VG1_K075,
		KA5_VG1_K076,
		KA5_VG1_K077,
		KA5_VG1_K078,
		KA5_VG1_K079,
		KA5_VG1_K080,
		KA5_VG1_K081,
		KA5_VG1_K082,
		KA5_VG1_K083,
		KA5_VG1_K084,
		KA5_VG1_K085,
		KA5_VG1_K086,
		KA5_VG1_K087,
		KA5_VG1_K088,
		KA5_VG1_K089,
		KA5_VG1_K090,
		KA5_VG1_K091,
		KA5_VG1_K092,
		KA5_VG1_K093,
		KA5_VG1_K094,
		KA5_VG1_K095,
		KA5_VG1_K096,
		KA5_VG1_K097,
		KA5_VG1_K098,
		KA5_VG1_K099,
		KA5_VG1_K100,
		KA5_VG1_K101,
		KA5_VG1_K102,
		KA5_VG1_K103,
		KA5_VG1_K104,
		KA5_VG1_K105,
		KA5_VG1_K106,
		KA5_VG1_K107,
		KA5_VG1_K108,
		KA5_VG1_K109,
		KA5_VG1_K110,
		KA5_VG1_K111,
		KA5_VG1_K112,
		KA5_VG1_K113,
		KA5_VG1_K114,
		KA5_VG1_K115,
		KA5_VG1_K116
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