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
public class MaterialsController : SpecimensControllerBase
{
    public MaterialsController(
        SpecimensDataWriter dataWriter,
        SpecimensDataRemover dataRemover,
        SpecimenIndexRemovalService indexRemover,
        SpecimenIndexingTasksService indexingTaskService,
        ILogger<MaterialsController> logger) : base(dataWriter, dataRemover, indexRemover, indexingTaskService, logger)
    {
    }


    [HttpPost("tsv")]
    public IActionResult PostTsv([ModelBinder(typeof(MaterialTsvModelsBinder))]SpecimenDataModel[] models)
    {
        var dataModels = _converter.Convert(models);

        return PostData(dataModels);
    }
}
