using Unite.Essentials.Extensions;
using Unite.Specimens.Feed.Web.Handlers.Submission;

namespace Unite.Specimens.Feed.Web.Workers;

public class SubmissionsWorker : BackgroundService
{
    private readonly DrugsSubmissionHandler _drugsSubmissionHandler;
    private readonly IntervensionsSubmissionHandler _intervensionsSubmissionHandler;
    private readonly LinesSubmissionHandler _linesSubmissionHandler;
    private readonly MaterialsSubmissionHandler _materialsSubmissionHandler;
    private readonly OrganoidsSubmissionHandler _organoidsSubmissionHandler;
    private readonly XenograftsSubmissionHandler _xenograftsSubmissionHandler;

    private readonly ILogger _logger;

    public SubmissionsWorker(DrugsSubmissionHandler drugsSubmissionHandler,
            IntervensionsSubmissionHandler intervensionsSubmissionHandler,
            LinesSubmissionHandler linesSubmissionHandler,
            MaterialsSubmissionHandler materialsSubmissionHandler,
            OrganoidsSubmissionHandler organoidsSubmissionHandler,
            XenograftsSubmissionHandler xenograftsSubmissionHandler,
            ILogger<SubmissionsWorker> logger)
    {
            _drugsSubmissionHandler = drugsSubmissionHandler;
            _intervensionsSubmissionHandler = intervensionsSubmissionHandler;
            _linesSubmissionHandler = linesSubmissionHandler;
            _materialsSubmissionHandler = materialsSubmissionHandler;
            _organoidsSubmissionHandler = organoidsSubmissionHandler;
            _xenograftsSubmissionHandler = xenograftsSubmissionHandler;
            _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Submissions worker started");

        stoppingToken.Register(() => _logger.LogInformation("Submissions worker stopped"));

            // Delay 5 seconds to let the web api start working
        await Task.Delay(5000, stoppingToken);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
              _drugsSubmissionHandler.Handle();
              _intervensionsSubmissionHandler.Handle();
              _linesSubmissionHandler.Handle();
              _materialsSubmissionHandler.Handle();
              _organoidsSubmissionHandler.Handle();
              _xenograftsSubmissionHandler.Handle();
            }
            catch (Exception exception)
            {
                _logger.LogError("{error}", exception.GetShortMessage());
            }
            finally
            {
                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}