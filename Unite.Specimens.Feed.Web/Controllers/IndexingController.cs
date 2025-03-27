using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Unite.Indices.Context;
using Unite.Indices.Entities.Specimens;
using Unite.Specimens.Feed.Web.Configuration.Constants;
using Unite.Specimens.Feed.Web.Services;

namespace Unite.Specimens.Feed.Web.Controllers;

[Route("api/[controller]")]
[Authorize(Policy = Policies.Data.Writer)]
public class IndexingController : Controller
{
    private readonly IIndexService<SpecimenIndex> _indexService;
    private readonly SpecimenIndexingTasksService _tasksService;

    public IndexingController(
        IIndexService<SpecimenIndex> indexService,
        SpecimenIndexingTasksService tasksService)
    {
        _indexService = indexService;
        _tasksService = tasksService;
    }

    [HttpPost]
    public IActionResult Post()
    {
        _indexService.DeleteIndex().Wait();
        _tasksService.CreateTasks();
        
        return Ok();
    }
}
