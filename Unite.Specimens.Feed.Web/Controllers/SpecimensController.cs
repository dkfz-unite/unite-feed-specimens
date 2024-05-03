using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Unite.Specimens.Feed.Data;
using Unite.Specimens.Feed.Web.Configuration.Constants;
using Unite.Specimens.Feed.Web.Models;
using Unite.Specimens.Feed.Web.Services;
using Unite.Specimens.Indices.Services;

namespace Unite.Specimens.Feed.Web.Controllers;

[Route("api/[controller]")]
[Authorize(Policy = Policies.Data.Writer)]
public class SpecimensController : SpecimensControllerBase
{
    public SpecimensController(
        SpecimensDataWriter dataWriter,
        SpecimensDataRemover dataRemover,
        SpecimenIndexRemovalService indexRemover,
        SpecimenIndexingTasksService indexingTaskService,
        ILogger<SpecimensController> logger) : base(dataWriter, dataRemover, indexRemover, indexingTaskService, logger)
    {
    }

    [HttpPost("")]
    public IActionResult Post([FromBody]SpecimenDataModel[] models)
    {
        var dataModels = _converter.Convert(models);

        return PostData(dataModels);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        return DeleteData(id);
    }
}
