using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Unite.Data.Context.Services.Tasks;
using Unite.Data.Entities.Tasks.Enums;
using Unite.Specimens.Feed.Web.Configuration.Constants;
using Unite.Specimens.Feed.Web.Models.Specimens;
using Unite.Specimens.Feed.Web.Models.Specimens.Binders;
using Unite.Specimens.Feed.Web.Submissions;

namespace Unite.Specimens.Feed.Web.Controllers;

[Route("api/entries/line")]
[Authorize(Policy = Policies.Data.Writer)]
public class LinesController : Controller
{
    private readonly SpecimensSubmissionService _submissionService;
    private readonly SubmissionTaskService _submissionTaskService;

    public LinesController(
        SpecimensSubmissionService submissionService, 
        SubmissionTaskService submissionTaskService)
    {
        _submissionService = submissionService;
        _submissionTaskService = submissionTaskService;
    }


    [HttpPost]
    public IActionResult Post([FromBody]LineModel[] models)
    {
        var submissionId = _submissionService.AddLinesSubmission(models);

        _submissionTaskService.CreateTask(SubmissionTaskType.LNE, submissionId);

        return Ok();
    }

    [HttpPost("tsv")]
    public IActionResult PostTsv([ModelBinder(typeof(LineTsvModelsBinder))]LineModel[] models)
    {
        return Post(models);
    }
}
