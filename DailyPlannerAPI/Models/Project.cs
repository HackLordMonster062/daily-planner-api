namespace DailyPlannerAPI.Models {
	public class Project(
		string Title,
		string Description,
		ProjectType type
	) {
		public string Id { get; init; } = Guid.NewGuid().ToString();
		public string Title { get; } = Title;
		public string Description { get; } = Description;
		public ProjectType Type { get; } = type;

		public List<Timeline> Timelines { get; init; } = [];

		public void AddTimeline(Timeline timeline) {
			if (timeline == null) throw new ArgumentNullException(nameof(timeline));
			Timelines.Add(timeline);
		}

		public void RemoveTimeline(string timelineId) {
			if (string.IsNullOrEmpty(timelineId)) throw new ArgumentNullException(nameof(timelineId));

			Timelines.RemoveAll(t => t.Id == timelineId);
		}

		public TaskItem? SampleTask(int focus) {
			Timeline timeline = Timelines[Random.Shared.Next(0, Timelines.Count)];

			return timeline.GetTask(focus);
		}

		public override string ToString() {
			return $"{Title} ({Type}) - {Description}";
		}
	}

	public enum ProjectType {
		Personal,
		Work,
		Study
	}
}
