namespace Paniker.Models
{
	public class TopicInfo
	{
		public string Name { get; set; } = string.Empty;

		public int CountOfChange { get; set; } = 0;

		public string CurrentState { get; set; } = string.Empty;

		public string NewState { get; set; } = string.Empty;
	}
}
