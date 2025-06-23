using DailyPlannerAPI.Models;
using DailyPlannerAPI.Models.Requests;
using DailyPlannerAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace DailyPlannerAPI.Controllers {
	[ApiController]
	[Route("[controller]")]
	public class ProjectController(ProjectManager projectManager) : Controller {
		private readonly ProjectManager _projectManager = projectManager ?? throw new ArgumentNullException(nameof(projectManager));

		[HttpPost("AddProject")]
		public IActionResult AddProject([FromBody] AddProjectRequest request) {
			if (request == null) {
				return BadRequest("Project cannot be null.");
			}

			Project newProject = new Project(
				request.Title,
				request.Description,
				request.Type
			);

			_projectManager.AddProject(newProject);
			return Ok(newProject.Id);
		}

		[HttpDelete("RemoveProject/{projectId}")]
		public IActionResult RemoveProject(string projectId) {
			if (string.IsNullOrEmpty(projectId)) {
				return BadRequest("Project ID cannot be null or empty.");
			}

			_projectManager.RemoveProject(projectId);
			return Ok("Project removed successfully.");
		}

		[HttpGet]
		public IActionResult GetProjects() {
			try {
				var projects = _projectManager.GetProjects();
				return Ok(projects);
			} catch (Exception ex) {
				return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while retrieving projects: {ex.Message}");
			}
		}

		[HttpPost("SampleTasks")]
		public IActionResult SampleTasks([FromBody] SampleTasksRequest request) {
			if (request == null) {
				return BadRequest("Request cannot be null.");
			}

			if (request.Amount <= 0 || request.Focus < 0 || request.ProjectIds == null || request.ProjectIds.Count == 0) {
				return BadRequest("Invalid request parameters.");
			}

			try {
				TaskItem[] tasks = _projectManager.SampleTasks(request.Amount, request.Focus, request.ProjectIds);
				return Ok(tasks);
			} catch (Exception ex) {
				return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while sampling tasks: {ex.Message}");
			}
		}
	}
}
