using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Unite.Specimens.Feed.Data;
using Unite.Specimens.Feed.Web.Configuration.Constants;
using Unite.Specimens.Feed.Web.Models.Specimens;
using Unite.Specimens.Feed.Web.Models.Specimens.Binders;
using Unite.Specimens.Feed.Web.Models.Specimens.Converters;
using Unite.Specimens.Feed.Web.Services;

namespace Unite.Specimens.Feed.Web.Controllers;

[Route("api/entries/line")]
[Authorize(Policy = Policies.Data.Writer)]
public class LinesController : Controller
{
    private readonly SpecimensWriter _dataWriter;
    private readonly SpecimenIndexingTasksService _tasksService;
    private readonly ILogger _logger;

    private readonly LineModelConverter _converter = new();


    public LinesController(
        SpecimensWriter dataWriter,
        SpecimenIndexingTasksService tasksService,
        ILogger<LinesController> logger)
    {
        _dataWriter = dataWriter;
        _tasksService = tasksService;
        _logger = logger;
    }


    [HttpPost]
    public IActionResult Post([FromBody]LineModel[] models)
    {
        var data = models.Select(_converter.Convert).ToArray();

        _dataWriter.SaveData(data, out var audit);
        _tasksService.PopulateTasks(audit.Specimens);
        _logger.LogInformation("{audit}", audit.ToString());

        return Ok();
    }

    [HttpPost("tsv")]
    public IActionResult PostTsv([ModelBinder(typeof(LineTsvModelsBinder))]LineModel[] models)
    {
        return Post(models);
    }
}
