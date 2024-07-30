using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Unite.Specimens.Feed.Data;
using Unite.Specimens.Feed.Data.Exceptions;
using Unite.Specimens.Feed.Web.Configuration.Constants;
using Unite.Specimens.Feed.Web.Models.Specimens;
using Unite.Specimens.Feed.Web.Models.Specimens.Binders;
using Unite.Specimens.Feed.Web.Models.Specimens.Converters;
using Unite.Specimens.Feed.Web.Services;

namespace Unite.Specimens.Feed.Web.Controllers;

[Route("api/interventions")]
[Authorize(Policy = Policies.Data.Writer)]
public class InterventionsController : Controller
{
    private readonly InterventionsWriter _dataWriter;
    private readonly SpecimenIndexingTasksService _tasksService;
    private readonly ILogger _logger;

    private readonly InterventionsModelConverter _converter = new();
    
    
    public InterventionsController(
        InterventionsWriter dataWriter, 
        SpecimenIndexingTasksService tasksService, 
        ILogger<InterventionsController> logger)
    {
        _dataWriter = dataWriter;
        _tasksService = tasksService;
        _logger = logger;
    }


    [HttpPost]
    public IActionResult Post([FromBody]InterventionsModel[] models)
    {
        try
        {
            var data = models.Select(_converter.Convert).ToArray();

            _dataWriter.SaveData(data, out var audit);
            _tasksService.PopulateTasks(audit.Specimens);
            _logger.LogInformation("{audit}", audit.ToString());

            return Ok();
        }
        catch (NotFoundException exception)
        {
            _logger.LogWarning("{error}", exception.Message);

            return NotFound(exception.Message);
        }
    }

    [HttpPost("tsv")]
    public IActionResult PosTsv([ModelBinder(typeof(InterventionsTsvModelsBinder))]InterventionsModel[] models)
    {
        return Post(models);
    }
}
