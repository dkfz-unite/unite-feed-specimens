using Microsoft.AspNetCore.Mvc;
using Unite.Specimens.Feed.Web.Services;

namespace Unite.Specimens.Feed.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    public class IndexingController : Controller
    {
        private readonly SpecimenIndexingTasksService _indexingTaskService;


        public IndexingController(SpecimenIndexingTasksService indexingTaskService)
        {
            _indexingTaskService = indexingTaskService;
        }

        [HttpPost]
        public IActionResult Specimens()
        {
            _indexingTaskService.CreateTasks();

            return Ok();
        }
    }
}
