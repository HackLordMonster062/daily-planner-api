using DailyPlannerAPI.Models;
using DailyPlannerAPI.Models.Requests;
using DailyPlannerAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace DailyPlannerAPI.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class TimelineController(ProjectManager projectManager) : ControllerBase {
		private readonly ProjectManager _projectManager = projectManager ?? throw new ArgumentNullException(nameof(projectManager));

		[HttpPost("AddTimeline")]
		public IActionResult AddTimeline([FromBody] AddProjectTimelineRequest request) {
			if (request == null) {
				return BadRequest("Timeline cannot be null.");
			}

			Timeline timeline = new(
				request.Title,
				request.Sampling
			);

			_projectManager.AddTimeline(request.ProjectId, timeline);
			return Ok(timeline.Id);
		}

		[HttpDelete("RemoveTimeline/{projectId}/{timelineId}")]
		public IActionResult RemoveTimeline(string projectId, string timelineId) {
			if (string.IsNullOrEmpty(projectId) || string.IsNullOrEmpty(timelineId)) {
				return BadRequest("Project ID and Timeline ID cannot be null or empty.");
			}

			_projectManager.RemoveTimeline(projectId, timelineId);
			return Ok("Timeline removed successfully.");
		}

		[HttpPost("AddItem")]
		public IActionResult AddItem([FromBody] AddTimelineItemRequest request) {
			if (request == null) {
				return BadRequest("Item cannot be null.");
			}

			TimelineItem item = request.GetItem();

			_projectManager.AddItem(request.ProjectId, request.TimelineId, item);
			return Ok(item.Id);
		}

		[HttpDelete("RemoveItem/{projectId}/{timelineId}/{itemId}")]
		public IActionResult RemoveItem(string projectId, string timelineId, string itemId) {
			if (string.IsNullOrEmpty(projectId) || string.IsNullOrEmpty(timelineId) || string.IsNullOrEmpty(itemId)) {
				return BadRequest("Project ID, Timeline ID, and Item ID cannot be null or empty.");
			}

			_projectManager.RemoveItem(projectId, timelineId, itemId);
			return Ok("Item removed successfully.");
		}
	}
}
