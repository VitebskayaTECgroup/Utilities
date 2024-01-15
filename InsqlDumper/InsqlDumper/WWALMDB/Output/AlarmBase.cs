using System;
using System.Collections.Generic;

namespace InsqlDumper.WWALMDB.Output
{
	public class AlarmBase
	{
		public string AlarmMaster_TagName { get; set; }
		public string AlarmMaster_bActive { get; set; }
		public string AlarmMaster_AlarmType { get; set; }
		public string AlarmMaster_LimitString { get; set; }
		public string AlarmMaster_ValueString { get; set; }
		public DateTime AlarmMaster_OriginationTime { get; set; }

		public string AlarmConsolidated_AlarmType { get; set; }
		public string AlarmConsolidated_LimitString { get; set; }
		public string AlarmConsolidated_ValueString { get; set; }
		public string AlarmConsolidated_ReturnValueString { get; set; }
		public DateTime AlarmConsolidated_AlarmTime { get; set; }
		public DateTime AlarmConsolidated_AckTime { get; set; }
		public DateTime AlarmConsolidated_ReturnTime { get; set; }

		public string ProviderSession_NodeName { get; set; }

		public string Comment_Comment { get; set; }

		public Alarm ToAlarm()
		{
			DateTime def = DateTime.Parse("9999-12-12 23:59:59.997");

			var list = new List<Alarm>();

			if (AlarmConsolidated_AlarmTime != def)
			{
				if (AlarmMaster_OriginationTime != AlarmConsolidated_AlarmTime)
				{
					return new Alarm
					{
						Date = AlarmConsolidated_AlarmTime.AddHours(3),
						Class = "UNACK_ALM",
						TagName = AlarmMaster_TagName,
						Comment = Comment_Comment,
						Type = AlarmConsolidated_AlarmType,
						Value = AlarmConsolidated_ValueString,
						ControlValue = AlarmConsolidated_LimitString,
						Node = ProviderSession_NodeName,
					};
				}
				else
				{
					return new Alarm
					{
						Date = AlarmMaster_OriginationTime.AddHours(3),
						Class = "UNACK_ALM",
						TagName = AlarmMaster_TagName,
						Comment = Comment_Comment,
						Type = AlarmMaster_AlarmType,
						Value = AlarmMaster_ValueString,
						ControlValue = AlarmMaster_LimitString,
						Node = ProviderSession_NodeName,
					};
				}
			}

			if (AlarmConsolidated_AckTime != def)
			{
				if (AlarmConsolidated_AckTime < AlarmConsolidated_ReturnTime && (AlarmConsolidated_AlarmTime == def || AlarmConsolidated_AckTime > AlarmConsolidated_AlarmTime))
				{
					return new Alarm
					{
						Date = AlarmConsolidated_AckTime.AddHours(3),
						Class = "ACK_ALM",
						TagName = AlarmMaster_TagName,
						Comment = Comment_Comment,
						Type = AlarmConsolidated_AlarmType,
						Value = AlarmConsolidated_ValueString,
						ControlValue = AlarmConsolidated_LimitString,
						Node = ProviderSession_NodeName,
					};
				}
				else if (AlarmConsolidated_AckTime == AlarmConsolidated_ReturnTime)
				{
					return new Alarm
					{
						Date = AlarmConsolidated_AckTime.AddHours(3),
						Class = "ACK",
						TagName = AlarmMaster_TagName,
						Comment = Comment_Comment,
						Type = AlarmConsolidated_AlarmType,
						Value = AlarmConsolidated_ValueString,
						ControlValue = AlarmConsolidated_LimitString,
						Node = ProviderSession_NodeName,
					};
				}
				else if (AlarmConsolidated_ReturnTime != def && AlarmMaster_bActive != "1" && AlarmConsolidated_AckTime <= AlarmConsolidated_ReturnTime && (AlarmConsolidated_AlarmTime == def || AlarmConsolidated_AckTime > AlarmConsolidated_AlarmTime))
				{
					return new Alarm
					{
						Date = AlarmConsolidated_ReturnTime.AddHours(3),
						Class = "ACK_RTN",
						TagName = AlarmMaster_TagName,
						Comment = Comment_Comment,
						Type = AlarmConsolidated_AlarmType,
						Value = AlarmConsolidated_ReturnValueString,
						ControlValue = AlarmConsolidated_LimitString,
						Node = ProviderSession_NodeName,
					};
				}
				else if (AlarmConsolidated_ReturnTime != def && AlarmMaster_bActive != "1" && AlarmConsolidated_AckTime > AlarmConsolidated_ReturnTime)
				{
					return new Alarm
					{
						Date = AlarmConsolidated_ReturnTime.AddHours(3),
						Class = "ACK_RTN",
						TagName = AlarmMaster_TagName,
						Comment = Comment_Comment,
						Type = AlarmConsolidated_AlarmType,
						Value = AlarmConsolidated_ReturnValueString,
						ControlValue = AlarmConsolidated_LimitString,
						Node = ProviderSession_NodeName,
					};
				}
			}

			if (AlarmConsolidated_ReturnTime != def && (AlarmConsolidated_AckTime == def || AlarmConsolidated_AckTime > AlarmConsolidated_ReturnTime || (AlarmConsolidated_AlarmTime != def && AlarmConsolidated_AckTime < AlarmConsolidated_AlarmTime)))
			{
				list.Add(new Alarm
				{
					Date = AlarmConsolidated_ReturnTime.AddHours(3),
					Class = "UNACK_RTN",
					TagName = AlarmMaster_TagName,
					Comment = Comment_Comment,
					Type = AlarmConsolidated_AlarmType,
					Value = AlarmConsolidated_ReturnValueString,
					ControlValue = AlarmConsolidated_LimitString,
					Node = ProviderSession_NodeName,
				});
			}

			return null;
		}
	}
}
