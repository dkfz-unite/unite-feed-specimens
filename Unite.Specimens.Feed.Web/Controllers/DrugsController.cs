using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Unite.Specimens.Feed.Data;
using Unite.Specimens.Feed.Data.Exceptions;
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

    private readonly DrugScreeningsDataModelsConverter _defaultModelConverter;
    private readonly DrugScreeningDataFlatModelsConverter _flatModelsConverter;


    public DrugsController(
        DrugScreeningsDataWriter dataWriter,
        SpecimenIndexingTasksService indexingTaskService,
        ILogger<DrugsController> logger)
    {
        _dataWriter = dataWriter;
        _indexingTaskService = indexingTaskService;
        _logger = logger;

        _defaultModelConverter = new DrugScreeningsDataModelsConverter();
        _flatModelsConverter = new DrugScreeningDataFlatModelsConverter();
    }

    [HttpPost("")]
    [Consumes("application/json")]
    public IActionResult Post([FromBody] DrugScreeningsDataModel[] models)
    {
        var dataModels = _defaultModelConverter.Convert(models);

        return PostData(dataModels);
    }

    [HttpPost("tsv")]
    [Consumes("text/tab-separated-values")]
    public IActionResult PostTsv([ModelBinder(typeof(DrugScreeningTsvModelsBinder))] DrugScreeningDataFlatModel[] models)
    {
        var dataModels = _flatModelsConverter.Convert(models);

        return PostData(dataModels);
    }


    private IActionResult PostData(Data.Models.SpecimenModel[] models)
    {
        try
        {
            _dataWriter.SaveData(models, out var audit);

            _logger.LogInformation("{audit}", audit.ToString());

            _indexingTaskService.PopulateTasks(audit.Specimens);

            return Ok();
        }
        catch (NotFoundException exception)
        {
            _logger.LogWarning("{error}", exception.Message);

            return BadRequest(exception.Message);
        }
    }
}
