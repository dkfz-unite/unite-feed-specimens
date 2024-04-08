using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Unite.Specimens.Feed.Data;
using Unite.Specimens.Feed.Web.Configuration.Constants;
using Unite.Specimens.Feed.Web.Models;
using Unite.Specimens.Feed.Web.Models.Binders;
using Unite.Specimens.Feed.Web.Services;
using Unite.Specimens.Indices.Services;

namespace Unite.Specimens.Feed.Web.Controllers;

[Route("api/[controller]")]
[Authorize(Policy = Policies.Data.Writer)]
public class OrganoidsController : SpecimensControllerBase
{
    public OrganoidsController(
        SpecimensDataWriter dataWriter,
        SpecimensDataRemover dataRemover,
        SpecimenIndexRemovalService indexRemover,
        SpecimenIndexingTasksService indexingTaskService,
        ILogger<OrganoidsController> logger) : base(dataWriter, dataRemover, indexRemover, indexingTaskService, logger)
    {
    }


    [HttpPost("tsv")]
    [Consumes("text/tab-separated-values")]
    public IActionResult PostTsv([ModelBinder(typeof(OrganoidTsvModelsBinder))]SpecimenDataModel[] models)
    {
        var dataModels = _converter.Convert(models);

        return PostData(dataModels);
    }
}
