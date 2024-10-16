using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Unite.Data.Context.Services.Tasks;
using Unite.Data.Entities.Tasks.Enums;
using Unite.Specimens.Feed.Web.Configuration.Constants;
using Unite.Specimens.Feed.Web.Models.Specimens;
using Unite.Specimens.Feed.Web.Models.Specimens.Binders;
using Unite.Specimens.Feed.Web.Submissions;

namespace Unite.Specimens.Feed.Web.Controllers;

[Route("api/interventions")]
[Authorize(Policy = Policies.Data.Writer)]
public class InterventionsController : Controller
{
    private readonly SpecimensSubmissionService _submissionService;
    private readonly SubmissionTaskService _submissionTaskService;

    public InterventionsController(
        SpecimensSubmissionService submissionService, 
        SubmissionTaskService submissionTaskService)
    {
        _submissionService = submissionService;
        _submissionTaskService = submissionTaskService;
    }

    [HttpPost]
    public IActionResult Post([FromBody]InterventionsModel[] models)
    {
        var submissionId = _submissionService.AddIntervensionsSubmission(models);

        _submissionTaskService.CreateTask(SubmissionTaskType.SPE_INT, submissionId);

        return Ok();
    }

    [HttpPost("tsv")]
    public IActionResult PosTsv([ModelBinder(typeof(InterventionsTsvModelsBinder))]InterventionsModel[] models)
    {
        return Post(models);
    }
}
