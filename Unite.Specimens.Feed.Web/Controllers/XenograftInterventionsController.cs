using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Unite.Specimens.Feed.Data.Specimens;
using Unite.Specimens.Feed.Web.Configuration.Constants;
using Unite.Specimens.Feed.Web.Models;
using Unite.Specimens.Feed.Web.Models.Binders;
using Unite.Specimens.Feed.Web.Models.Converters;
using Unite.Specimens.Feed.Web.Services;

namespace Unite.Specimens.Feed.Web.Controllers;

[Route("api/xenograft-interventions")]
[Authorize(Policy = Policies.Data.Writer)]
public class XenograftInterventionsController : SpecimensControllerBase
{
    private readonly XenograftInterventionsDataModelConverter _interventionModelConverter;
    private readonly XenograftInterventionDataTsvModelConverter _interventionTsvModelConverter;
    

    public XenograftInterventionsController(
        SpecimenDataWriter dataWriter, 
        SpecimenIndexingTasksService indexingTaskService, 
        ILogger<SpecimensControllerBase> logger) : base(dataWriter, indexingTaskService, logger)
    {
        _interventionModelConverter = new XenograftInterventionsDataModelConverter();
        _interventionTsvModelConverter = new XenograftInterventionDataTsvModelConverter();
    }


    [HttpPost("")]
    [Consumes("application/json")]
    public IActionResult Post([FromBody]XenograftInterventionsDataModel[] models)
    {
        var specimenModels = _interventionModelConverter.Convert(models);
        return PostData(specimenModels);
    }

    [HttpPost("tsv")]
    [Consumes("text/tab-separated-values")]
    public IActionResult PostTsv([ModelBinder(typeof(XenograftsInterventionsTsvModelBinder))]XenograftInterventionDataTsvModel[] models)
    {
        var specimenModels = _interventionTsvModelConverter.Convert(models);
        return PostData(specimenModels);
    }
}
