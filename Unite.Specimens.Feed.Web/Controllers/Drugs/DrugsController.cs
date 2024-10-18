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

    [HttpPost("")]
    public IActionResult Post([FromBody]AnalysisModel<DrugScreeningModel> model)
    {
        var submissionId = _submissionService.AddDrugsSubmission(model);

        _submissionTaskService.CreateTask(SubmissionTaskType.SPE_DRG, submissionId);

        return Ok();
    }

    [HttpPost("tsv")]
    public IActionResult PostTsv([ModelBinder(typeof(AnalysisTsvModelBinder))]AnalysisModel<DrugScreeningModel> model)
    {
        return Post(model);
    }
}
