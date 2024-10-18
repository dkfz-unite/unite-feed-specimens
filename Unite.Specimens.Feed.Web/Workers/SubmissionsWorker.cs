using Unite.Essentials.Extensions;
using Unite.Specimens.Feed.Web.Handlers.Submission;

namespace Unite.Specimens.Feed.Web.Workers;

public class SubmissionsWorker : BackgroundService
{
    private readonly MaterialsSubmissionHandler _materialsSubmissionHandler;
    private readonly LinesSubmissionHandler _linesSubmissionHandler;
    private readonly OrganoidsSubmissionHandler _organoidsSubmissionHandler;
    private readonly XenograftsSubmissionHandler _xenograftsSubmissionHandler;
    private readonly IntervensionsSubmissionHandler _intervensionsSubmissionHandler;
    private readonly DrugsSubmissionHandler _drugsSubmissionHandler;

    private readonly ILogger _logger;

    public SubmissionsWorker(MaterialsSubmissionHandler materialsSubmissionHandler,
            LinesSubmissionHandler linesSubmissionHandler,
            OrganoidsSubmissionHandler organoidsSubmissionHandler,
            XenograftsSubmissionHandler xenograftsSubmissionHandler,
            IntervensionsSubmissionHandler intervensionsSubmissionHandler,
            DrugsSubmissionHandler drugsSubmissionHandler,
            ILogger<SubmissionsWorker> logger)
    {
            _materialsSubmissionHandler = materialsSubmissionHandler;
            _linesSubmissionHandler = linesSubmissionHandler;
            _organoidsSubmissionHandler = organoidsSubmissionHandler;
            _xenograftsSubmissionHandler = xenograftsSubmissionHandler;
            _intervensionsSubmissionHandler = intervensionsSubmissionHandler;
            _drugsSubmissionHandler = drugsSubmissionHandler;
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
              _materialsSubmissionHandler.Handle();
              _linesSubmissionHandler.Handle();
              _organoidsSubmissionHandler.Handle();
              _xenograftsSubmissionHandler.Handle();
              _intervensionsSubmissionHandler.Handle();
              _drugsSubmissionHandler.Handle();
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