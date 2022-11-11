using System.Diagnostics;
using Unite.Data.Entities.Tasks.Enums;
using Unite.Data.Services.Tasks;
using Unite.Indices.Entities.Specimens;
using Unite.Indices.Services;

namespace Unite.Specimens.Feed.Web.Handlers;

public class SpecimensIndexingHandler
{
    private readonly TasksProcessingService _taskProcessingService;
    private readonly IIndexCreationService<SpecimenIndex> _indexCreationService;
    private readonly IIndexingService<SpecimenIndex> _indexingService;
    private readonly ILogger _logger;


    public SpecimensIndexingHandler(
        TasksProcessingService taskProcessingService,
        IIndexCreationService<SpecimenIndex> indexCreationService,
        IIndexingService<SpecimenIndex> indexingService,
        ILogger<SpecimensIndexingHandler> logger)
    {
        _taskProcessingService = taskProcessingService;
        _indexCreationService = indexCreationService;
        _indexingService = indexingService;
        _logger = logger;
    }

    public void Prepare()
    {
        _indexingService.UpdateMapping().GetAwaiter().GetResult();
    }

    public void Handle(int bucketSize)
    {
        ProcessSpecimenIndexingTasks(bucketSize);
    }


    private void ProcessSpecimenIndexingTasks(int bucketSize)
    {
        var stopwatch = new Stopwatch();

        var shouldWait = _taskProcessingService.HasAnnotationTasks();

        if (shouldWait)
        {
            return;
        }

        _taskProcessingService.Process(IndexingTaskType.Specimen, bucketSize, (tasks) =>
        {
            _logger.LogInformation($"Indexing {tasks.Length} specimens");

            stopwatch.Restart();

            var indices = tasks.Select(task =>
            {
                var id = int.Parse(task.Target);

                var index = _indexCreationService.CreateIndex(id);

                return index;

            }).ToArray();

            _indexingService.IndexMany(indices);

            stopwatch.Stop();

            _logger.LogInformation($"Indexing of {tasks.Length} specimens completed in {Math.Round(stopwatch.Elapsed.TotalSeconds, 2)}s");
        });
    }
}

