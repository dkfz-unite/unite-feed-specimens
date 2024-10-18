using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Unite.Data.Context.Services.Tasks;
using Unite.Data.Entities.Tasks.Enums;
using Unite.Specimens.Feed.Web.Configuration.Constants;
using Unite.Specimens.Feed.Web.Models.Specimens;
using Unite.Specimens.Feed.Web.Models.Specimens.Binders;
using Unite.Specimens.Feed.Web.Submissions;

namespace Unite.Specimens.Feed.Web.Controllers;

[Route("api/entries/xenograft")]
[Authorize(Policy = Policies.Data.Writer)]
public class XenograftsController : Controller
{
   
    private readonly SpecimensSubmissionService _submissionService;
    private readonly SubmissionTaskService _submissionTaskService;

    public XenograftsController(
        SpecimensSubmissionService submissionService, 
        SubmissionTaskService submissionTaskService)
    {
        _submissionService = submissionService;
        _submissionTaskService = submissionTaskService;
    }


    [HttpPost]
    public IActionResult Post([FromBody]XenograftModel[] models)
    {
        var submissionId = _submissionService.AddXenograftsSubmission(models);

        _submissionTaskService.CreateTask(SubmissionTaskType.XEN, submissionId);

        return Ok();
    }

    [HttpPost("tsv")]
    public IActionResult PostTsv([ModelBinder(typeof(XenograftTsvModelsBinder))]XenograftModel[] models)
    {
        return Post(models);
    }
}
