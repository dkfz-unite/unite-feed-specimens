using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Unite.Data.Context.Services.Tasks;
using Unite.Data.Entities.Tasks.Enums;
using Unite.Specimens.Feed.Web.Configuration.Constants;
using Unite.Specimens.Feed.Web.Models.Base;
using Unite.Specimens.Feed.Web.Models.Drugs;
using Unite.Specimens.Feed.Web.Models.Drugs.Binders;
using Unite.Specimens.Feed.Web.Submissions;

namespace Unite.Specimens.Feed.Web.Controllers.Drugs;

[Route("api/analysis/drugs")]
[Authorize(Policy = Policies.Data.Writer)]
public class DrugsController : Controller
{
    private readonly SpecimensSubmissionService _submissionService;
    private readonly SubmissionTaskService _submissionTaskService;

    public DrugsController(
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

        var submission = _submissionService.FindDrugsSubmission(task.Target);

        return Ok(submission);
    }

    [HttpPost("")]
    public IActionResult Post([FromBody]AnalysisModel<DrugScreeningModel> model, [FromQuery] bool review = true)
    {
        var submissionId = _submissionService.AddDrugsSubmission(model);

        var taskStatus = review ? TaskStatusType.Preparing : TaskStatusType.Prepared;

        var taskId = _submissionTaskService.CreateTask(SubmissionTaskType.SPE_DRG, submissionId, taskStatus);

        return Ok(taskId);
    }

    [HttpPost("tsv")]
    public IActionResult PostTsv([ModelBinder(typeof(AnalysisTsvModelBinder))]AnalysisModel<DrugScreeningModel> model, [FromQuery] bool review = true)
    {
        return Post(model, review);
    }
}
