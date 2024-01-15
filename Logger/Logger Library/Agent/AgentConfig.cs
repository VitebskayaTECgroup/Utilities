using Logger_Library.Database;
using System.Collections.Generic;

namespace Logger_Library.Agent
{
	public class AgentConfig
	{
		public int AgentId { get; set; }

		public string Server { get; set; }

		public int Port { get; set; } = 4330;

		public SpecSettings Spec { get; set; }

		public List<EventFilter> Filters { get; set; }
	}
}
