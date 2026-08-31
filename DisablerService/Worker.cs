using DisablerService.Application;
using DisablerService.Core.Options;
using Microsoft.Extensions.Options;

namespace DisablerService;

public class Worker : BackgroundService
{
    private readonly TimeSpan _intervalToSearchByProcess;
    private readonly ProcessOptions _processOptions;


    public Worker(IOptions<ProcessOptions> processOptions)
    {
        _intervalToSearchByProcess = TimeSpan.FromSeconds(processOptions.Value.IntervalInSeconds);
        _processOptions = processOptions.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var timer = new PeriodicTimer(_intervalToSearchByProcess);

            while (await timer.WaitForNextTickAsync(stoppingToken))
                WorkerApplication.StartOperation(_processOptions);
        }
    }
}
