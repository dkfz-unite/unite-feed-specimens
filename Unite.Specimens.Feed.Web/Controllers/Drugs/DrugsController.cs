using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Unite.Specimens.Feed.Data;
using Unite.Specimens.Feed.Data.Exceptions;
using Unite.Specimens.Feed.Web.Configuration.Constants;
using Unite.Specimens.Feed.Web.Models.Base;
using Unite.Specimens.Feed.Web.Models.Drugs;
using Unite.Specimens.Feed.Web.Models.Drugs.Binders;
using Unite.Specimens.Feed.Web.Models.Drugs.Converters;
using Unite.Specimens.Feed.Web.Services;

namespace Unite.Specimens.Feed.Web.Controllers.Drugs;

[Route("api/analysis/drugs")]
[Authorize(Policy = Policies.Data.Writer)]
public class DrugsController : Controller
{
    private readonly AnalysisWriter _dataWriter;
    private readonly SpecimenIndexingTasksService _tasksService;
    private readonly ILogger _logger;

    private readonly AnalysisModelConverter _converter = new();


    public DrugsController(
        AnalysisWriter dataWriter,
        SpecimenIndexingTasksService tasksService,
        ILogger<DrugsController> logger)
    {
        _dataWriter = dataWriter;
        _tasksService = tasksService;
        _logger = logger;
    }

    [HttpPost("")]
    public IActionResult Post([FromBody]AnalysisModel<DrugScreeningModel> model)
    {
        try
        {
            var data = _converter.Convert(model);

            _dataWriter.SaveData(data, out var audit);
            _logger.LogInformation("{audit}", audit.ToString());
            _tasksService.PopulateTasks(audit.Samples);

            return Ok();
        }
        catch (NotFoundException exception)
        {
            _logger.LogWarning("{error}", exception.Message);

            return NotFound(exception.Message);
        }
    }

    [HttpPost("tsv")]
    public IActionResult PostTsv([ModelBinder(typeof(AnalysisTsvModelBinder))]AnalysisModel<DrugScreeningModel> model)
    {
        return Post(model);
    }
}
