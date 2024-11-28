using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Unite.Data.Context.Services.Tasks;
using Unite.Data.Entities.Tasks.Enums;
using Unite.Specimens.Feed.Web.Configuration.Constants;
using Unite.Specimens.Feed.Web.Models.Specimens;
using Unite.Specimens.Feed.Web.Models.Specimens.Binders;
using Unite.Specimens.Feed.Web.Submissions;

namespace Unite.Specimens.Feed.Web.Controllers;

[Route("api/entries/organoid")]
[Authorize(Policy = Policies.Data.Writer)]
public class OrganoidsController : Controller
{
    private readonly SpecimensSubmissionService _submissionService;
    private readonly SubmissionTaskService _submissionTaskService;

    public OrganoidsController(
        SpecimensSubmissionService submissionService, 
        SubmissionTaskService submissionTaskService)
    {
        _submissionService = submissionService;
        _submissionTaskService = submissionTaskService;
    }

    [HttpGet("{id}")]
    public IActionResult Get(long id)
    {
        var task = _submissionTaskService.GetTask(id);

        var submission = _submissionService.FindOrganoidsSubmission(task.Target);

        return Ok(submission);
    }

    [HttpPost("")]
    public IActionResult Post([FromBody]OrganoidModel[] models, [FromQuery] bool review = true)
    {
        var submissionId = _submissionService.AddOrganoidsSubmission(models);

        var taskStatus = review ? TaskStatusType.Preparing : TaskStatusType.Prepared;

        var taskId = _submissionTaskService.CreateTask(SubmissionTaskType.ORG, submissionId, taskStatus);

        return Ok(taskId);
    }

    [HttpPost("tsv")]
    public IActionResult PostTsv([ModelBinder(typeof(OrganoidTsvModelsBinder))]OrganoidModel[] models, [FromQuery] bool review = true)
    {
        return Post(models, review);
    }
}
