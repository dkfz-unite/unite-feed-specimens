using System.Diagnostics;
using Unite.Data.Context.Services.Tasks;
using Unite.Data.Entities.Tasks.Enums;
using Unite.Specimens.Feed.Data;
using Unite.Specimens.Feed.Web.Services;
using Unite.Specimens.Feed.Web.Submissions;

namespace Unite.Specimens.Feed.Web.Handlers.Submission;

public class DrugsSubmissionHandler
{
    private readonly AnalysisWriter _dataWriter;
    private readonly SpecimenIndexingTasksService _tasksService;
    private readonly SpecimensSubmissionService _submissionService;
    private readonly TasksProcessingService _tasksProcessingService;

    private readonly Models.Drugs.Converters.AnalysisModelConverter _modelConverter;

    private readonly ILogger _logger;

    public DrugsSubmissionHandler(
        AnalysisWriter dataWriter,
        SpecimenIndexingTasksService tasksService,
        SpecimensSubmissionService submissionService,
        TasksProcessingService tasksProcessingService,
        ILogger<DrugsSubmissionHandler> logger)
    {
        _dataWriter = dataWriter;
        _tasksService = tasksService;
        _submissionService = submissionService;
        _tasksProcessingService = tasksProcessingService;
        _logger = logger;

        _modelConverter = new Models.Drugs.Converters.AnalysisModelConverter();
    }


    public void Handle()
    {
        ProcessSubmissionTasks();
    }

    private void ProcessSubmissionTasks()
    {
        var stopwatch = new Stopwatch();

        _tasksProcessingService.Process(SubmissionTaskType.SPE_DRG, TaskStatusType.Prepared, 1, (tasks) =>
        {
            stopwatch.Restart();

            ProcessSubmission(tasks[0].Target);

            stopwatch.Stop();

            _logger.LogInformation("Processed drugs data submission in {time}s", Math.Round(stopwatch.Elapsed.TotalSeconds, 2));

            return true;
        });
    }

    private void ProcessSubmission(string submissionId)
    {
        var submittedData = _submissionService.FindDrugsSubmission(submissionId);
        var convertedData = _modelConverter.Convert(submittedData);

        _dataWriter.SaveData(convertedData, out var audit);
        _tasksService.PopulateTasks(audit.Samples);
        _submissionService.DeleteDrugsSubmission(submissionId);

        _logger.LogInformation("{audit}", audit.ToString());
    }
}