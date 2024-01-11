using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Unite.Specimens.Feed.Data.Specimens;
using Unite.Specimens.Feed.Web.Configuration.Constants;
using Unite.Specimens.Feed.Web.Models;
using Unite.Specimens.Feed.Web.Models.Binders;
using Unite.Specimens.Feed.Web.Models.Converters;
using Unite.Specimens.Feed.Web.Services;

namespace Unite.Specimens.Feed.Web.Controllers;

[Route("api/organoid-interventions")]
[Authorize(Policy = Policies.Data.Writer)]
public class OrganoidInterventionsController : SpecimensControllerBase
{
    private readonly OrganoidInterventionsDataModelConverter _interventionModelConverter;
    private readonly OrganoidInterventionDataTsvModelConverter _interventionTsvModelConverter;
    
    
    public OrganoidInterventionsController(
        SpecimenDataWriter dataWriter, 
        SpecimenIndexingTasksService indexingTaskService, 
        ILogger<SpecimensControllerBase> logger) : base(dataWriter, indexingTaskService, logger)
    {
        _interventionModelConverter = new OrganoidInterventionsDataModelConverter();
        _interventionTsvModelConverter = new OrganoidInterventionDataTsvModelConverter();
    }


    [HttpPost("")]
    [Consumes("application/json")]
    public IActionResult Post([FromBody]OrganoidInterventionsDataModel[] models)
    {
        var specimenModels = _interventionModelConverter.Convert(models);
        return PostData(specimenModels);
    }

    [HttpPost("tsv")]
    [Consumes("text/tab-separated-values")]
    public IActionResult PosTsv([ModelBinder(typeof(OrganoidsInterventionsTsvModelBinder))]OrganoidInterventionDataTsvModel[] models)
    {
        var specimenModels = _interventionTsvModelConverter.Convert(models);
        return PostData(specimenModels);
    }
}
