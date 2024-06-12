using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Unite.Specimens.Feed.Web.Configuration.Constants;
using Unite.Specimens.Feed.Web.Services;

namespace Unite.Specimens.Feed.Web.Controllers;

[Route("api/[controller]")]
[Authorize(Policy = Policies.Data.Writer)]
public class IndexingController : Controller
{
    private readonly SpecimenIndexingTasksService _tasksService;

    public IndexingController(
        SpecimenIndexingTasksService tasksService)
    {
        _tasksService = tasksService;
    }

    [HttpPost]
    public IActionResult Post()
    {
        _tasksService.CreateTasks();
        
        return Ok();
    }
}
