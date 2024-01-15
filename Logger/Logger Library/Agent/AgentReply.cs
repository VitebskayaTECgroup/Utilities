using Logger_Library.Database;
using System;
using System.Collections.Generic;

namespace Logger_Library.Agent
{
	public class AgentReply
	{
		public int AgentId { get; set; }

		public DateTime Date { get; set; }

		public List<Log> Logs { get; set; }

		public List<Specification> Specifications { get; set; }
	}
}