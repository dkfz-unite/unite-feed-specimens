using Microsoft.AspNetCore.Mvc;
using Unite.Specimens.Feed.Data.Specimens;
using Unite.Specimens.Feed.Data.Specimens.Exceptions;
using Unite.Specimens.Feed.Web.Models.Converters;
using Unite.Specimens.Feed.Web.Services;

namespace Unite.Specimens.Feed.Web.Controllers;

public abstract class SpecimensControllerBase : Controller
{
    protected readonly SpecimensDataWriter _dataWriter;
    protected readonly SpecimenIndexingTasksService _indexingTaskService;
    protected readonly ILogger _logger;

    protected readonly SpecimenDataModelConverter _converter;


    public SpecimensControllerBase(
        SpecimensDataWriter dataWriter,
        SpecimenIndexingTasksService indexingTaskService,
        ILogger<SpecimensControllerBase> logger)
    {
        _dataWriter = dataWriter;
        _indexingTaskService = indexingTaskService;
        _logger = logger;

        _converter = new SpecimenDataModelConverter();
    }


    protected virtual IActionResult PostData(Data.Specimens.Models.SpecimenModel[] models)
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
            _logger.LogError("{error}", exception.Message);

            return BadRequest(exception.Message);
        }
    }
}
