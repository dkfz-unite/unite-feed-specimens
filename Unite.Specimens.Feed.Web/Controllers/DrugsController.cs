using Microsoft.AspNetCore.Mvc;
using Unite.Specimens.Feed.Data.Specimens;
using Unite.Specimens.Feed.Data.Specimens.Exceptions;
using Unite.Specimens.Feed.Web.Models;
using Unite.Specimens.Feed.Web.Models.Converters;
using Unite.Specimens.Feed.Web.Services;

namespace Unite.Specimens.Feed.Web.Controllers;

[Route("api/[controller]")]
public class DrugsController : Controller
{
    private readonly DrugScreeningDataWriter _dataWriter;
    private readonly SpecimenIndexingTasksService _indexingTaskService;
    private readonly ILogger _logger;

    private readonly DrugScreeningDataModelConverter _converter;


    public DrugsController(
        DrugScreeningDataWriter dataWriter,
        SpecimenIndexingTasksService indexingTaskService,
        ILogger<DrugsController> logger)
    {
        _dataWriter = dataWriter;
        _indexingTaskService = indexingTaskService;
        _logger = logger;

        _converter = new DrugScreeningDataModelConverter();
    }


    public IActionResult Post([FromBody] DrugScreeningDataModel[] models)
    {
        try
        {
            var dataModels = models.Select(model => _converter.Convert(model));

            _dataWriter.SaveData(dataModels, out var audit);

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
