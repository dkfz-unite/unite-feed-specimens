using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Unite.Specimens.Feed.Data.Specimens;
using Unite.Specimens.Feed.Web.Configuration.Constants;
using Unite.Specimens.Feed.Web.Models;
using Unite.Specimens.Feed.Web.Models.Binders;
using Unite.Specimens.Feed.Web.Services;

namespace Unite.Specimens.Feed.Web.Controllers;

[Route("api/organoids")]
[Authorize(Policy = Policies.Data.Writer)]
public class OrganoidsController : SpecimensControllerBase
{
    public OrganoidsController(
        SpecimensDataWriter dataWriter,
        SpecimenIndexingTasksService indexingTaskService,
        ILogger<OrganoidsController> logger) : base(dataWriter, indexingTaskService, logger)
    {
    }


    [HttpPost("tsv")]
    [Consumes("text/tab-separated-values")]
    public IActionResult PostTsv([ModelBinder(typeof(OrganoidFlatModelsBinder))]SpecimenDataModel[] models)
    {
        return PostData(models);
    }
}
