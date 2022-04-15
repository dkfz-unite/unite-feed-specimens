using System.Linq;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Unite.Data.Extensions;
using Unite.Specimens.Feed.Data.Exceptions;
using Unite.Specimens.Feed.Data.Specimens;
using Unite.Specimens.Feed.Web.Services;
using Unite.Specimens.Feed.Web.Services.Specimens;
using Unite.Specimens.Feed.Web.Services.Specimens.Converters;

namespace Unite.Specimens.Feed.Web.Controllers
{
    [Route("api/[controller]")]
    public class SpecimensController : Controller
    {
        private readonly SpecimenDataWriter _dataWriter;
        private readonly SpecimenIndexingTasksService _indexingTaskService;
        private readonly ILogger _logger;

        private readonly SpecimenModelConverter _converter;


        public SpecimensController(
            SpecimenDataWriter dataWriter,
            SpecimenIndexingTasksService indexingTaskService,
            ILogger<SpecimensController> logger)
        {
            _dataWriter = dataWriter;
            _indexingTaskService = indexingTaskService;
            _logger = logger;

            _converter = new SpecimenModelConverter();
        }


        public IActionResult Post([FromBody] SpecimenModel[] models)
        {
            try
            {
                models.ForEach(model => model.Sanitise());

                var dataModels = models.Select(model => _converter.Convert(model));

                _dataWriter.SaveData(dataModels, out var audit);

                _logger.LogInformation(audit.ToString());

                _indexingTaskService.PopulateTasks(audit.Specimens);

                return Ok();
            }
            catch (NotFoundException exception)
            {
                _logger.LogError(exception.Message);

                return BadRequest(exception.Message);
            }
        }
    }
}
