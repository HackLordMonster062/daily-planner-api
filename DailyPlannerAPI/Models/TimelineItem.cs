using System.Text.Json.Serialization;

namespace DailyPlannerAPI.Models {
	[JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
	[JsonDerivedType(typeof(Timeline), "timeline")]
	[JsonDerivedType(typeof(TaskItem), "taskItem")]
	public abstract class TimelineItem(string title) {
		public string Id { get; init; } = Guid.NewGuid().ToString();
		public string Title { get; } = title;

		public abstract TaskItem? GetTask(int focus);
	}
}
