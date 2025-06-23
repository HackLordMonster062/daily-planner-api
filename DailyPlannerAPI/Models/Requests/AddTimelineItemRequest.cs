using System.Text.Json.Serialization;

namespace DailyPlannerAPI.Models.Requests {
	[JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
	[JsonDerivedType(typeof(AddTaskRequest), "addTask")]
	[JsonDerivedType(typeof(AddTimelineRequest), "addTimeline")]
	public abstract record AddTimelineItemRequest {
		public required string Type { get; init; }
		public required string ProjectId { get; init; }
		public required string TimelineId { get; init; }

		public abstract TimelineItem GetItem();
	}

	public sealed record AddTaskRequest(
		string Title,
		int Focus,
		int Priority,
		DateTime? DueDate = null,
		string Description = ""
	) : AddTimelineItemRequest {
		public override TimelineItem GetItem() {
			return new TaskItem(
				Title,
				Description,
				DueDate,
				false,
				Focus,
				Priority
			);
		}
	}

	public sealed record AddTimelineRequest(
		string Title,
		SamplingType Sampling
	) : AddTimelineItemRequest {
		public override TimelineItem GetItem() {
			return new Timeline(
				Title,
				Sampling
			);
		}
	}
}
