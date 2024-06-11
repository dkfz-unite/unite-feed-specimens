using System.Diagnostics;
using Unite.Data.Context.Services.Tasks;
using Unite.Data.Entities.Tasks.Enums;
using Unite.Essentials.Extensions;
using Unite.Indices.Context;
using Unite.Indices.Entities.Specimens;
using Unite.Specimens.Indices.Services;

namespace Unite.Specimens.Feed.Web.Handlers;

public class SpecimensIndexingHandler
{
    private readonly TasksProcessingService _taskProcessingService;
    private readonly SpecimenIndexCreator _indexCreator;
    private readonly IIndexService<SpecimenIndex> _indexingService;
    private readonly ILogger _logger;


    public SpecimensIndexingHandler(
        TasksProcessingService taskProcessingService,
        SpecimenIndexCreator indexCreator,
        IIndexService<SpecimenIndex> indexingService,
        ILogger<SpecimensIndexingHandler> logger)
    {
        _taskProcessingService = taskProcessingService;
        _indexCreator = indexCreator;
        _indexingService = indexingService;
        _logger = logger;
    }

    public async Task Prepare()
    {
        await _indexingService.UpdateIndex();
    }

    public async Task Handle(int bucketSize)
    {
        await ProcessSpecimenIndexingTasks(bucketSize);
    }


    private async Task ProcessSpecimenIndexingTasks(int bucketSize)
    {
        var stopwatch = new Stopwatch();

        await _taskProcessingService.Process(IndexingTaskType.Specimen, bucketSize, async (tasks) =>
        {
            if (_taskProcessingService.HasTasks(WorkerType.Submission) || _taskProcessingService.HasTasks(WorkerType.Annotation))
                return false;

            stopwatch.Restart();

            var indicesToDelete = new List<string>();
            var indicesToCreate = new List<SpecimenIndex>();

            tasks.ForEach(task =>
            {
                var id = int.Parse(task.Target);

                var index = _indexCreator.CreateIndex(id);

                if (index == null)
                    indicesToDelete.Add($"{id}");
                else
                    indicesToCreate.Add(index);

            });

            if (indicesToDelete.Any())
                await _indexingService.DeleteRange(indicesToDelete);

            if (indicesToCreate.Any())
                await _indexingService.AddRange(indicesToCreate);

            stopwatch.Stop();

            _logger.LogInformation("Indexed {number} specimens in {time}s", tasks.Length, Math.Round(stopwatch.Elapsed.TotalSeconds, 2));

            return true;
        });
    }
}
