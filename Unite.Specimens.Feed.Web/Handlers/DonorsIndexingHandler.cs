using System.Linq;
using Microsoft.Extensions.Logging;
using Unite.Data.Entities.Tasks.Enums;
using Unite.Indices.Entities.Specimens;
using Unite.Indices.Services;
using Unite.Specimens.Feed.Web.Services;

namespace Unite.Specimens.Feed.Web.Handlers
{
    public class DonorsIndexingHandler
    {
        private readonly TasksProcessingService _taskProcessingService;
        private readonly IIndexCreationService<SpecimenIndex> _indexCreationService;
        private readonly IIndexingService<SpecimenIndex> _indexingService;
        private readonly ILogger _logger;


        public DonorsIndexingHandler(
            TasksProcessingService taskProcessingService,
            IIndexCreationService<SpecimenIndex> indexCreationService,
            IIndexingService<SpecimenIndex> indexingService,
            ILogger<DonorsIndexingHandler> logger)
        {
            _taskProcessingService = taskProcessingService;
            _indexCreationService = indexCreationService;
            _indexingService = indexingService;
            _logger = logger;
        }


        public void Handle(int bucketSize)
        {
            ProcessSpecimenIndexingTasks(bucketSize);
        }


        private void ProcessSpecimenIndexingTasks(int bucketSize)
        {
            _taskProcessingService.Process(TaskType.Indexing, TaskTargetType.Specimen, bucketSize, (tasks) =>
            {
                _logger.LogInformation($"Indexing {tasks.Length} specimens");

                var indices = tasks.Select(task =>
                {
                    var id = int.Parse(task.Target);

                    var index = _indexCreationService.CreateIndex(id);

                    return index;

                }).ToArray();

                _indexingService.IndexMany(indices);


                _logger.LogInformation($"Indexing of {tasks.Length} specimens completed");
            });
        }
    }
}

