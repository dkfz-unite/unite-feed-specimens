using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Unite.Specimens.Feed.Data.Specimens;
using Unite.Specimens.Feed.Data.Specimens.Exceptions;
using Unite.Specimens.Feed.Web.Configuration.Constants;
using Unite.Specimens.Feed.Web.Models;
using Unite.Specimens.Feed.Web.Models.Binders;
using Unite.Specimens.Feed.Web.Models.Converters;
using Unite.Specimens.Feed.Web.Services;

namespace Unite.Specimens.Feed.Web.Controllers;

[Route("api/[controller]")]
[Authorize(Policy = Policies.Data.Writer)]
public class DrugsController : Controller
{
    private readonly DrugScreeningsDataWriter _dataWriter;
    private readonly SpecimenIndexingTasksService _indexingTaskService;
    private readonly ILogger _logger;

    private readonly DrugScreeningsDataModelConverter _defaultConverter;
    private readonly DrugScreeningDataFlatModelConverter _flatConverter;


    public DrugsController(
        DrugScreeningsDataWriter dataWriter,
        SpecimenIndexingTasksService indexingTaskService,
        ILogger<DrugsController> logger)
    {
        _dataWriter = dataWriter;
        _indexingTaskService = indexingTaskService;
        _logger = logger;

        _defaultConverter = new DrugScreeningsDataModelConverter();
        _flatConverter = new DrugScreeningDataFlatModelConverter();
    }

    [HttpPost("")]
    [Consumes("application/json")]
    public IActionResult Post([FromBody] DrugScreeningsDataModel[] models)
    {
        var dataModels = models.Select(model => _defaultConverter.Convert(model)).ToArray();

        return PostData(dataModels);
    }

    [HttpPost("tsv")]
    [Consumes("text/tab-separated-values")]
    public IActionResult PostTsv([ModelBinder(typeof(DrugsTsvModelBinder))] DrugScreeningDataFlatModel[] models)
    {
        var dataModels = models.Select(model => _flatConverter.Convert(model)).ToArray();

        return PostData(dataModels);
    }

    private IActionResult PostData(Data.Specimens.Models.SpecimenModel[] models)
    {
        try
        {
            _dataWriter.SaveData(models, out var audit);

            _logger.LogInformation(audit.ToString());

            _indexingTaskService.PopulateTasks(audit.Specimens);

            return Ok();
        }
        catch (NotFoundException exception)
        {
            _logger.LogError(exception.Message);

            return BadRequest(exception.Message);
        }
    }
}
