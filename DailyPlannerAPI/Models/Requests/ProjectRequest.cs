namespace DailyPlannerAPI.Models.Requests {
	public abstract record ProjectRequest;

	public sealed record SampleTasksRequest(
		int Amount,
		int Focus,
		List<string> ProjectIds
	) : ProjectRequest;

	public sealed record AddProjectRequest(
		string Title,
		string Description,
		ProjectType Type
	) : ProjectRequest;

	public sealed record AddProjectTimelineRequest(
		string ProjectId,
		string Title,
		SamplingType Sampling
	) : ProjectRequest;
}
