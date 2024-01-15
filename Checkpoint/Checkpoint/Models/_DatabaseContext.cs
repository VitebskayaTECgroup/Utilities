using Checkpoint.Database;
using LinqToDB;
using LinqToDB.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Checkpoint.Models
{
	public class DatabaseContext : DataConnection
	{
		public DatabaseContext() : base("Checkpoint") { }


		public ITable<ACCESS_GROUPS> ACCESS_GROUPS
			=> GetTable<ACCESS_GROUPS>();

		public ITable<EVENTS> EVENTS
			=> GetTable<EVENTS>();

		public ITable<EVENT_DESC> EVENT_DESC
			=> GetTable<EVENT_DESC>();

		public ITable<OPERATORS> OPERATORS
			=> GetTable<OPERATORS>();

		public ITable<OPER_RIGHTS> OPER_RIGHTS
			=> GetTable<OPER_RIGHTS>();

		public ITable<OPER_RIGHTS_DESC> OPER_RIGHTS_DESC
			=> GetTable<OPER_RIGHTS_DESC>();

		public ITable<OWNERS> OWNERS
			=> GetTable<OWNERS>();

		public ITable<OWNER_DEPS> OWNER_DEPS
			=> GetTable<OWNER_DEPS>();

		public ITable<OWNER_CARDS> OWNER_CARDS
			=> GetTable<OWNER_CARDS>();

		public ITable<OWNER_JOBS> OWNER_JOBS
			=> GetTable<OWNER_JOBS>();

		public List<Owner> GetTableData(string search, string date, string department, string group, string onlybad, string onlyunseen, string onlyrefused)
		{
			var viewDate = DateTime.TryParse(date, out DateTime _date) ? _date : DateTime.Today;
			var departmentId = int.TryParse(department, out int i) ? i : 0;
			var groupId = int.TryParse(group, out i) ? i : 0;
			var onlyBad = onlybad == "true";
			var onlyUnseen = onlyunseen == "true";
			var onlyRefused = onlyrefused == "true";
			search = search.ToLower();

			var ownersQuery = from ow in OWNERS
							  from od in OWNER_DEPS.InnerJoin(x => x.OD_ID == ow.OW_DEP)
							  from ca in OWNER_CARDS.InnerJoin(x => x.OC_OW_ID == ow.OW_ID)
							  from ag in ACCESS_GROUPS.InnerJoin(x => x.AGR_ID == ca.OC_ACCESS_GROUP)
							  where
								 ca.OC_RETDATE == null && ca.OC_ARCHIVE == 0
								 && (string.IsNullOrEmpty(search)
									? ((departmentId == 0 || departmentId == od.OD_ID) && (groupId == 0 || groupId == ag.AGR_ID))
									: (ow.OW_FNAME + ow.OW_LNAME + ow.OW_MNAME + ag.AGR_NAME + od.OD_NAME + ow.OW_TABNUM).ToLower().Contains(search)
								 )
							  orderby ag.AGR_NAME, od.OD_NAME, ow.OW_LNAME
							  select new Owner
							  {
								  Official = ow.OW_LNAME + ' ' + ow.OW_FNAME + ' ' + ow.OW_MNAME,
								  TableId = ow.OW_TABNUM,
								  Id = ow.OW_ID,
								  Department = od.OD_NAME,
								  Group = ag.AGR_NAME,
								  ViewDate = viewDate,
							  };

			var owners = ownersQuery.ToList();

			var eventsQuery = from ev in EVENTS
							  where
								 ev.EV_TSTAMP >= viewDate
								 && ev.EV_TSTAMP < viewDate.AddDays(1)
								 && ev.EV_OW_ID != null
								 && owners.Select(x => x.Id).ToList().Contains(ev.EV_OW_ID.Value)
								 && new[] { 1, 2, 1001, 1002 }.Contains(ev.EV_ADDR)
								 && ((onlyRefused ? (ev.EV_TYPE == 5) : (ev.EV_TYPE == 1 || ev.EV_TYPE == 52)) || new[] { 340, 341, 342, 343 }.Contains(ev.EV_TYPE))
							  orderby ev.EV_OW_ID, ev.EV_TSTAMP
							  select new Event
							  {
								  Id = ev.EV_ID,
								  OwnerId = ev.EV_OW_ID.Value,
								  Date = ev.EV_TSTAMP,
								  Type = ev.EV_ADDR,
								  Code = ev.EV_TYPE,
							  };

			var events = eventsQuery.ToList();

			foreach (var ow in owners)
			{
				ow.Events = events.Where(x => x.OwnerId == ow.Id).ToList();
				//if (!onlyRefused) events = events.Where(x => x.InitialType == 1);
				ow.Process(new WorkBoundaries(viewDate));
			}

			if (onlyBad)
			{
				owners = owners.Where(x => x.IsFault).ToList();
			}

			if (onlyUnseen)
			{
				owners = owners.Where(x => x.Events.Count == 0).ToList();
			}

			return owners;
		}
	}
}