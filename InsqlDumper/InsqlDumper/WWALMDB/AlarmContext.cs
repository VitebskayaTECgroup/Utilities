using InsqlDumper.WWALMDB.Output;
using LinqToDB;
using LinqToDB.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InsqlDumper.WWALMDB
{
	public class AlarmContext : DataConnection
	{
		public AlarmContext() : base("WWALMDB")
		{
			this.CommandTimeout = 2400;
		}

		public ITable<AlarmConsolidated> AlarmConsolidated
			=> this.GetTable<AlarmConsolidated>();

		public ITable<AlarmMaster> AlarmMaster
			=> this.GetTable<AlarmMaster>();

		public ITable<CommentTable> Comment
			=> this.GetTable<CommentTable>();

		public ITable<Events> Events
			=> this.GetTable<Events>();

		public ITable<ProviderSession> ProviderSession
			=> this.GetTable<ProviderSession>();

		public List<Alarm> DumpAlarms(DateTime start, DateTime end)
		{
			var list = new List<Alarm>();

			var query = from m in AlarmMaster
						from a in AlarmConsolidated.InnerJoin(x => x.AlarmId == m.AlarmId)
						from p in ProviderSession.InnerJoin(x => x.ProviderId == m.ProviderId)
						from c in Comment.LeftJoin(x => x.CommentId == a.CommentId)
						where m.OriginationTime >= start && m.OriginationTime < end
						select new AlarmBase
						{
							AlarmMaster_TagName = m.TagName,
							AlarmMaster_bActive = m.bActive,
							AlarmMaster_AlarmType = m.AlarmType,
							AlarmMaster_LimitString = m.LimitString,
							AlarmMaster_ValueString = m.ValueString,
							AlarmMaster_OriginationTime = m.OriginationTime,
							AlarmConsolidated_AlarmType = a.AlarmType,
							AlarmConsolidated_LimitString = a.LimitString,
							AlarmConsolidated_ValueString = a.ValueString,
							AlarmConsolidated_ReturnValueString = a.ReturnValueString,
							AlarmConsolidated_AlarmTime = a.AlarmTime,
							AlarmConsolidated_AckTime = a.AckTime ?? new DateTime(1970, 1, 1),
							AlarmConsolidated_ReturnTime = a.ReturnTime ?? new DateTime(1970, 1, 1),
							ProviderSession_NodeName = p.NodeName,
							Comment_Comment = c.Comment,
						};

			list.AddRange(query
				.ToList()
				.Select(x => x.ToAlarm())
				.Where(x => x != null));

			list.AddRange(Events
				.Where(x => x.EventTime >= start.AddHours(-3))
				.Where(x => x.EventTime < end.AddHours(-3))
				.Select(x => new Alarm
				{
					Class = x.EventState,
					Date = x.EventTime.AddHours(3),
					TagName = x.TagName,
					Comment = x.Comment,
					Type = x.EventType,
					Node = x.OperatorNode,
					Value = x.ValueString,
					ControlValue = x.LimitString,
				})
				.ToList());

			return list;
		}
	}
}
