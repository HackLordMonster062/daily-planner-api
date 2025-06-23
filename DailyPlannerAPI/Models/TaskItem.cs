namespace DailyPlannerAPI.Models {
	public class TaskItem(
		string title,
		string description,
		DateTime? dueDate,
		bool isCompleted,
		int focus,
		int priority
	) : TimelineItem(title) {
		public string Description { get; } = description;
		public DateTime? DueDate { get; } = dueDate;
		public bool IsCompleted { get; set; } = isCompleted;
		public int Focus { get; } = focus;
		public int Priority { get; } = priority;

		public override TaskItem? GetTask(int focus) {
			if (!IsCompleted) {
				return this;
			}
			return null;
		}

		public void Complete() {
			IsCompleted = true;
		}

		public override string ToString() {
			return $"{Title} (Due: {DueDate?.ToShortDateString() ?? "No Due Date"}, Focus: {Focus}, Priority: {Priority})";
		}

		public TaskItem Clone() {
			return new TaskItem(Title, Description, DueDate, IsCompleted, Focus, Priority);
		}
	}
}