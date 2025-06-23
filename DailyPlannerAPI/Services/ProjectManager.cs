using DailyPlannerAPI.Models;

namespace DailyPlannerAPI.Services {
	public class ProjectManager(IStorageService storageService) {
		private IStorageService _storageService = storageService ?? throw new ArgumentNullException(nameof(storageService));

		public void AddProject(Project project) {
			_storageService.SaveAsync($"project/{project.Id}", project);
		}

		public void RemoveProject(string projectId) {
			_storageService.DeleteAsync($"project/{projectId}");
		}

		public void AddTimeline(string projectId, Timeline timeline) {
			if (timeline == null) throw new ArgumentNullException(nameof(timeline));

			Project project = _storageService.LoadAsync<Project>($"project/{projectId}").Result 
				?? throw new EntryPointNotFoundException(projectId);

			project.AddTimeline(timeline);

			_storageService.SaveAsync($"project/{projectId}", project);
		}

		public void RemoveTimeline(string projectId, string timelineId) {
			Project project = _storageService.LoadAsync<Project>($"project/{projectId}").Result
				?? throw new EntryPointNotFoundException(projectId);

			project.RemoveTimeline(timelineId);

			_storageService.SaveAsync($"project/{projectId}", project);
		}

		public void AddItem(string projectId, string timelineId, TimelineItem item) {
			if (item == null) throw new ArgumentNullException(nameof(item));

			Project project = _storageService.LoadAsync<Project>($"project/{projectId}").Result
				?? throw new EntryPointNotFoundException(projectId);

			Timeline timeline = project.Timelines.Where(t => t.Id == timelineId).FirstOrDefault()
				?? throw new EntryPointNotFoundException(timelineId);

			timeline.AddTask(item);

			_storageService.SaveAsync($"project/{projectId}", project);
		}

		public void RemoveItem(string projectId, string timelineId, string itemId) {
			if (string.IsNullOrEmpty(itemId)) throw new ArgumentNullException(nameof(itemId));

			Project project = _storageService.LoadAsync<Project>($"project/{projectId}").Result
				?? throw new EntryPointNotFoundException(projectId);

			Timeline timeline = project.Timelines.Where(t => t.Id == timelineId).FirstOrDefault()
				?? throw new EntryPointNotFoundException(timelineId);

			timeline.RemoveTask(itemId);
			_storageService.SaveAsync($"project/{projectId}", project);
		}

		public List<Project> GetProjects() {
			List<Project> projects = [];

			foreach (var key in _storageService.GetAllKeysAsync().Result) {
				if (_storageService.ExistsAsync(key).Result) {
					Project? project = _storageService.LoadAsync<Project>(key).Result;
					if (project != null) {
						projects.Add(project);
					}
				}
			}

			return projects;
		}

		public TaskItem[] SampleTasks(int amount, int focus, List<string> projectIds) {
			TaskItem[] tasks = new TaskItem[amount];

			TaskItem? task = null;

			for (int i = 0; i < amount; i++) {
				while (task == null) {
					string projectId = projectIds[Random.Shared.Next(0, projectIds.Count)];

					Project project = _storageService.LoadAsync<Project>($"project/{projectId}").Result ?? throw new EntryPointNotFoundException(projectId);

					task = project.SampleTask(focus);
				}

				tasks[i] = task;
			}

			return tasks;
		}
	}
}
