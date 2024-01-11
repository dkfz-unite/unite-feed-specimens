using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Unite.Specimens.Feed.Data.Specimens;
using Unite.Specimens.Feed.Web.Configuration.Constants;
using Unite.Specimens.Feed.Web.Models;
using Unite.Specimens.Feed.Web.Services;

namespace Unite.Specimens.Feed.Web.Controllers;

[Route("api/specimens")]
[Authorize(Policy = Policies.Data.Writer)]
public class SpecimensController : SpecimensControllerBase
{
    public SpecimensController(
        SpecimenDataWriter dataWriter,
        SpecimenIndexingTasksService indexingTaskService,
        ILogger<SpecimensController> logger) : base(dataWriter, indexingTaskService, logger)
    {
    }

    [HttpPost("")]
    [Consumes("application/json")]
    public IActionResult Post([FromBody] SpecimenDataModel[] models)
    {
        return PostData(models);
    }
}
