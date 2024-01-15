IF OBJECT_ID('dbo.ActionsPings', 'U') IS NOT NULL DROP TABLE [dbo].[ActionsPings]; 
CREATE TABLE [dbo].[ActionsPings]
(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Template] [nvarchar](max) NULL,
	[Target] [nvarchar](max) NULL,
	[Interval] [int] NOT NULL,
	[Value] [int] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];

IF OBJECT_ID('dbo.ActionsSql', 'U') IS NOT NULL DROP TABLE [dbo].[ActionsSql]; 
CREATE TABLE [dbo].[ActionsSql](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Interval] [int] NOT NULL,
	[DatabaseType] [nvarchar](max) NULL,
	[ConnectionString] [nvarchar](max) NULL,
	[CommandTimeout] [int] NOT NULL,
	[CommandCode] [nvarchar](max) NULL,
	[ComparersJson] [nvarchar](max) NULL,
	[SwitchersJson] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];

IF OBJECT_ID('dbo.ActionsWatchers', 'U') IS NOT NULL DROP TABLE [dbo].[ActionsWatchers]; 
CREATE TABLE [dbo].[ActionsWatchers](
	[Id] [int] NULL,
	[Name] [nvarchar](max) NULL,
	[WatcherId] [int] NULL,
	[Comparer] [nvarchar](max) NOT NULL,
	[Value] [int] NOT NULL,
	[Template] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];

IF OBJECT_ID('dbo.Channels', 'U') IS NOT NULL DROP TABLE [dbo].[Channels]; 
CREATE TABLE [dbo].[Channels](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Type] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];

IF OBJECT_ID('dbo.Logs', 'U') IS NOT NULL DROP TABLE [dbo].[Logs]; 
CREATE TABLE [dbo].[Logs](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Endpoint] [nvarchar](max) NOT NULL,
	[TimeStamp] [datetime2](7) NOT NULL,
	[LogFilterId] [int] NOT NULL,
	[Journal] [nvarchar](max) NULL,
	[Source] [nvarchar](max) NULL,
	[EventId] [int] NOT NULL,
	[Category] [nvarchar](max) NULL,
	[Type] [nvarchar](max) NULL,
	[Username] [nvarchar](max) NULL,
	[Message] [nvarchar](max) NULL,
	[Checked] [bit] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];

IF OBJECT_ID('dbo.LogsFilters', 'U') IS NOT NULL DROP TABLE [dbo].[LogsFilters]; 
CREATE TABLE [dbo].[LogsFilters](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Allow] [bit] NOT NULL,
	[Endpoints] [nvarchar](max) NOT NULL,
	[Journals] [nvarchar](max) NOT NULL,
	[Sources] [nvarchar](max) NOT NULL,
	[EventIds] [nvarchar](max) NOT NULL,
	[Categories] [nvarchar](max) NOT NULL,
	[Types] [nvarchar](max) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];

IF OBJECT_ID('dbo.LogsReactions', 'U') IS NOT NULL DROP TABLE [dbo].[LogsReactions]; 
CREATE TABLE [dbo].[LogsReactions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LogId] [int] NOT NULL,
	[Username] [nvarchar](max) NOT NULL,
	[Date] [datetime] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];

IF OBJECT_ID('dbo.Rel_Log_Telegram', 'U') IS NOT NULL DROP TABLE [dbo].[Rel_Log_Telegram]; 
CREATE TABLE [dbo].[Rel_Log_Telegram](
	[LogId] [int] NOT NULL,
	[ChannelId] [int] NOT NULL,
	[IsSended] [bit] NOT NULL
) ON [PRIMARY];

IF OBJECT_ID('dbo.Rel_Log_WebView', 'U') IS NOT NULL DROP TABLE [dbo].[Rel_Log_WebView]; 
CREATE TABLE [dbo].[Rel_Log_WebView](
	[LogId] [int] NOT NULL,
	[ChannelId] [int] NOT NULL
) ON [PRIMARY];

IF OBJECT_ID('dbo.Rel_LogFilter_Channel', 'U') IS NOT NULL DROP TABLE [dbo].[Rel_LogFilter_Channel]; 
CREATE TABLE [dbo].[Rel_LogFilter_Channel](
	[ChannelId] [int] NOT NULL,
	[LogFilterId] [int] NOT NULL
) ON [PRIMARY];

IF OBJECT_ID('dbo.Rel_StationConfig_ActionPing', 'U') IS NOT NULL DROP TABLE [dbo].[Rel_StationConfig_ActionPing]; 
CREATE TABLE [dbo].[Rel_StationConfig_ActionPing](
	[StationConfigId] [int] NOT NULL,
	[PingActionId] [int] NOT NULL
) ON [PRIMARY];

IF OBJECT_ID('dbo.Rel_StationConfig_ActionSql', 'U') IS NOT NULL DROP TABLE [dbo].[Rel_StationConfig_ActionSql]; 
CREATE TABLE [dbo].[Rel_StationConfig_ActionSql](
	[StationConfigId] [int] NOT NULL,
	[ActionSqlId] [int] NOT NULL
) ON [PRIMARY];

IF OBJECT_ID('dbo.Rel_StationConfig_ActionWatcher', 'U') IS NOT NULL DROP TABLE [dbo].[Rel_StationConfig_ActionWatcher]; 
CREATE TABLE [dbo].[Rel_StationConfig_ActionWatcher](
	[StationConfigId] [int] NOT NULL,
	[ActionWatcherId] [int] NOT NULL
) ON [PRIMARY];

IF OBJECT_ID('dbo.Rel_StationConfig_LogFilter', 'U') IS NOT NULL DROP TABLE [dbo].[Rel_StationConfig_LogFilter]; 
CREATE TABLE [dbo].[Rel_StationConfig_LogFilter](
	[StationConfigId] [int] NOT NULL,
	[LogFilterId] [int] NOT NULL
) ON [PRIMARY];

IF OBJECT_ID('dbo.Settings', 'U') IS NOT NULL DROP TABLE [dbo].[Settings]; 
CREATE TABLE [dbo].[Settings](
	[LastUpdate] [datetime] NOT NULL
) ON [PRIMARY];

IF OBJECT_ID('dbo.Stations', 'U') IS NOT NULL DROP TABLE [dbo].[Stations]; 
CREATE TABLE [dbo].[Stations](
	[Endpoint] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[StationConfigId] [int] NOT NULL,
	[AgentVersion] [nvarchar](max) NULL,
	[LastTimeAlive] [datetime2](7) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];

IF OBJECT_ID('dbo.StationsConfigs', 'U') IS NOT NULL DROP TABLE [dbo].[StationsConfigs]; 
CREATE TABLE [dbo].[StationsConfigs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];

IF OBJECT_ID('dbo.StationsSpecs', 'U') IS NOT NULL DROP TABLE [dbo].[StationsSpecs]; 
CREATE TABLE [dbo].[StationsSpecs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Endpoint] [nvarchar](max) NOT NULL,
	[Page] [nvarchar](max) NULL,
	[Device] [nvarchar](max) NULL,
	[ItemGroup] [nvarchar](max) NULL,
	[ItemId] [nvarchar](max) NULL,
	[Item] [nvarchar](max) NULL,
	[Value] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];

INSERT INTO [dbo].[Channels]
	([Name], [Type])
VALUES 
	('Веб-консоль','web'),
	('Telegram','telegram');

INSERT INTO [dbo].[Settings]
	([LastUpdate])
VALUES 
	(GetDate());