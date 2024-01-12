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

[Route("api/organoid-interventions")]
[Authorize(Policy = Policies.Data.Writer)]
public class OrganoidInterventionsController : Controller
{
    private readonly OrganoidInterventionsDataWriter _dataWriter;
    private readonly SpecimenIndexingTasksService _indexingTaskService;
    private readonly ILogger _logger;

    private readonly OrganoidInterventionsDataModelsConverter _defaultModelsConverter;
    private readonly OrganoidInterventionDataFlatModelsConverter _flatModelsConverter;
    
    
    public OrganoidInterventionsController(
        OrganoidInterventionsDataWriter dataWriter, 
        SpecimenIndexingTasksService indexingTaskService, 
        ILogger<OrganoidInterventionsController> logger)
    {
        _dataWriter = dataWriter;
        _indexingTaskService = indexingTaskService;
        _logger = logger;

        _defaultModelsConverter = new OrganoidInterventionsDataModelsConverter();
        _flatModelsConverter = new OrganoidInterventionDataFlatModelsConverter();
    }


    [HttpPost("")]
    [Consumes("application/json")]
    public IActionResult Post([FromBody]OrganoidInterventionsDataModel[] models)
    {
        var dataModels = _defaultModelsConverter.Convert(models);

        return PostData(dataModels);
    }

    [HttpPost("tsv")]
    [Consumes("text/tab-separated-values")]
    public IActionResult PosTsv([ModelBinder(typeof(OrganoidInterventionFlatModelsBinder))]OrganoidInterventionDataFlatModel[] models)
    {
        var dataModels = _flatModelsConverter.Convert(models);

        return PostData(dataModels);
    }


    private IActionResult PostData(Data.Specimens.Models.SpecimenModel[] models)
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
