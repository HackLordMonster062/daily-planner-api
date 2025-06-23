namespace DailyPlannerAPI.Models {
	public class Timeline(
		string title,
		SamplingType sampling
	) : TimelineItem(title) {
		public List<TimelineItem> Tasks { get; private set; } = [];
		public DateTime CreatedAt { get; private set; } = DateTime.Now;
		public SamplingType Sampling { get; private set; } = sampling;

		public void AddTask(TimelineItem task) {
			if (task == null) throw new ArgumentNullException(nameof(task));
			Tasks.Add(task);
		}

		public void RemoveTask(string taskId) {
			if (string.IsNullOrEmpty(taskId)) throw new ArgumentNullException(nameof(taskId));
			Tasks.RemoveAll(t => t.Id == taskId);
		}

		public override TaskItem? GetTask(int focus) {
			return SampleTask(focus);
		}

		public TaskItem? SampleTask(int focus) {
			if (Tasks.Count == 0) return null;

			switch (Sampling) {
				case SamplingType.Ordered:
					foreach (var item in Tasks) {
						TaskItem? task = item.GetTask(focus);
						if (task != null) {
							if (!task.IsCompleted && task.Focus <= focus) {
								return task;
							} else {
								return null;
							}
						}
					}

					return null;
				case SamplingType.Unordered:
					List<TaskItem> sortedTasks = Tasks.Select(t => t.GetTask(focus))
												.Where(t => t != null)
												.Select(t => t!).ToList();

					if (sortedTasks.Count == 0) return null;

					sortedTasks = SortByUrgency(sortedTasks, focus);

					return ChooseTaskWeighted(sortedTasks, 0.6f);
				default:
					throw new NotImplementedException("Sampling type: " + Sampling.ToString() + " not implemented yet.");
			}
		}

		private static TaskItem? ChooseTaskWeighted(List<TaskItem> tasks, float baseWeight) {
			if (tasks.Count == 0) return null;

			float random = new Random().NextSingle();

			if (random < baseWeight) return tasks[0];

			List<float> weights = [];

			for (int i = 1; i < tasks.Count; i++) {
				weights.Add(1 / i);
			}

			float totalWeight = weights.Sum();

			List<float> probabilities = weights.Select(w => w / totalWeight * (1 - baseWeight)).ToList();

			float cumulativeProbability = 0f;

			for (int i = 0; i < probabilities.Count; i++) {
				cumulativeProbability += probabilities[i];
				if (random < cumulativeProbability) {
					return tasks[i + 1];
				}
			}

			return tasks.Last();
		}

		public static List<TaskItem> SortByUrgency(List<TaskItem> tasks, int focus) {
			if (tasks == null) throw new ArgumentNullException(nameof(tasks));

			return [.. tasks.Where(t => !t.IsCompleted && t.Focus <= focus)
						.OrderBy(t => t.DueDate ?? DateTime.MaxValue)
						.ThenByDescending(t => t.Priority)
						.ThenByDescending(t => t.Focus)];
		}
	}

	public enum SamplingType {
		Ordered,
		Unordered,
	}
}
