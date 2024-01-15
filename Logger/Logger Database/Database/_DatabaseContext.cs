using LinqToDB;
using LinqToDB.Data;

namespace Logger.Database
{
	public class DatabaseContext : DataConnection
	{
		public DatabaseContext(): base("Default") { }

		// Объекты

		public ITable<Station> Stations
			=> GetTable<Station>();

		public ITable<StationConfig> StationsConfigs
			=> GetTable<StationConfig>();

		public ITable<StationSpec> StationsSpecs
			=> GetTable<StationSpec>();

		public ITable<LogFilter> LogsFilters
			=> GetTable<LogFilter>();

		public ITable<Log> Logs
			=> GetTable<Log>();

		public ITable<LogReaction> LogsReactions
			=> GetTable<LogReaction>();

		public ITable<Channel> Channels
			=> GetTable<Channel>();

		public ITable<ActionPing> ActionsPings
			=> GetTable<ActionPing>();

		public ITable<ActionWatcher> ActionsWatchers
			=> GetTable<ActionWatcher>();

		public ITable<ActionSql> ActionsSql
			=> GetTable<ActionSql>();

		public ITable<Settings> Settings
			=> GetTable<Settings>();

		// Связи

		public ITable<Rel_StationConfig_LogFilter> Rel_StationConfig_LogFilter
			=> GetTable<Rel_StationConfig_LogFilter>();

		public ITable<Rel_StationConfig_ActionPing> Rel_StationConfig_ActionPing
			=> GetTable<Rel_StationConfig_ActionPing>();

		public ITable<Rel_StationConfig_ActionWatcher> Rel_StationConfig_ActionWatcher
			=> GetTable<Rel_StationConfig_ActionWatcher>();

		public ITable<Rel_LogFilter_Channel> Rel_LogFilter_Channel
			=> GetTable<Rel_LogFilter_Channel>();

		public ITable<Rel_Log_WebView> Rel_Log_WebView
			=> GetTable<Rel_Log_WebView>();

		public ITable<Rel_Log_Telegram> Rel_Log_Telegram
			=> GetTable<Rel_Log_Telegram>();

		public ITable<Rel_StationConfig_ActionSql> Rel_StationConfig_ActionSql
			=> GetTable<Rel_StationConfig_ActionSql>();
	}
}
