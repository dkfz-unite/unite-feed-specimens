using System.Diagnostics;
using Unite.Data.Context.Services.Tasks;
using Unite.Data.Entities.Tasks.Enums;
using Unite.Specimens.Feed.Data;
using Unite.Specimens.Feed.Web.Services;
using Unite.Specimens.Feed.Web.Submissions;

namespace Unite.Specimens.Feed.Web.Handlers.Submission;

public class XenograftsSubmissionHandler
{
    private readonly SpecimensWriter _dataWriter;
    private readonly SpecimenIndexingTasksService _tasksService;
    private readonly SpecimensSubmissionService _submissionService;
    private readonly TasksProcessingService _tasksProcessingService;

    private readonly Models.Specimens.Converters.XenograftModelConverter _modelConverter;

    private readonly ILogger _logger;

    public XenograftsSubmissionHandler(
        SpecimensWriter dataWriter,
        SpecimenIndexingTasksService tasksService,
        SpecimensSubmissionService submissionService,
        TasksProcessingService tasksProcessingService,
        ILogger<XenograftsSubmissionHandler> logger)
    {
        _dataWriter = dataWriter;
        _tasksService = tasksService;
        _submissionService = submissionService;
        _tasksProcessingService = tasksProcessingService;
        _logger = logger;

        _modelConverter = new Models.Specimens.Converters.XenograftModelConverter();
    }


    public void Handle()
    {
        ProcessSubmissionTasks();
    }

    private void ProcessSubmissionTasks()
    {
        var stopwatch = new Stopwatch();

        _tasksProcessingService.Process(SubmissionTaskType.XEN, TaskStatusType.Prepared, 1, (tasks) =>
        {
            stopwatch.Restart();

            ProcessSubmission(tasks[0].Target);

            stopwatch.Stop();

            _logger.LogInformation("Processed xenografts data submission in {time}s", Math.Round(stopwatch.Elapsed.TotalSeconds, 2));

            return true;
        });
    }

    private void ProcessSubmission(string submissionId)
    {
        var submittedData = _submissionService.FindXenograftsSubmission(submissionId);
        var convertedData = submittedData.Select(_modelConverter.Convert).ToArray();

        _dataWriter.SaveData(convertedData, out var audit);
        _tasksService.PopulateTasks(audit.Specimens);
        _submissionService.DeleteXenograftsSubmission(submissionId);

        _logger.LogInformation("{audit}", audit.ToString());
    }
}