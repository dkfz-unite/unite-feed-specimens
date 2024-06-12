using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Unite.Specimens.Feed.Data;
using Unite.Specimens.Feed.Web.Configuration.Constants;
using Unite.Specimens.Feed.Web.Models.Specimens;
using Unite.Specimens.Feed.Web.Models.Specimens.Binders;
using Unite.Specimens.Feed.Web.Models.Specimens.Converters;
using Unite.Specimens.Feed.Web.Services;

namespace Unite.Specimens.Feed.Web.Controllers;

[Route("api/organoids")]
[Authorize(Policy = Policies.Data.Writer)]
public class OrganoidsController : Controller
{
    private readonly SpecimensWriter _dataWriter;
    private readonly SpecimenIndexingTasksService _tasksService;
    private readonly ILogger _logger;

    private readonly OrganoidModelConverter _converter = new();


    public OrganoidsController(
        SpecimensWriter dataWriter,
        SpecimenIndexingTasksService tasksService,
        ILogger<OrganoidsController> logger)
    {
        _dataWriter = dataWriter;
        _tasksService = tasksService;
        _logger = logger;
    }


    [HttpPost]
    public IActionResult Post([FromBody]OrganoidModel[] models)
    {
        var data = models.Select(_converter.Convert).ToArray();

        _dataWriter.SaveData(data, out var audit);
        _tasksService.PopulateTasks(audit.Specimens);
        _logger.LogInformation("{audit}", audit.ToString());

        return Ok();
    }

    [HttpPost("tsv")]
    public IActionResult PostTsv([ModelBinder(typeof(OrganoidTsvModelsBinder))]OrganoidModel[] models)
    {
        return Post(models);
    }
}
