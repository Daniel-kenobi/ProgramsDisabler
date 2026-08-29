using DisablerService.Core.Constants;
using System.Diagnostics;

namespace DisablerService;

public class Worker(ILogger<Worker> logger) : BackgroundService
{
    private readonly TimeSpan _intervalToSearchByProcess = TimeSpan.FromMinutes(2);
    private readonly ILogger<Worker> _logger = logger;


    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var timer = new PeriodicTimer(_intervalToSearchByProcess);

            while (await timer.WaitForNextTickAsync(stoppingToken))
            {
                _logger.LogInformation("Worker executing task at: {time}", DateTimeOffset.Now);

                HandleOperation();
            }
        }
    }

    private static void HandleOperation()
    {
        if (IsProcessOpen(Constants.PROCESS_TO_WATCH_NAME) && IsProcessOpen(Constants.PROCESS_TO_CLOSE))
            CloseProcess(Constants.PROCESS_TO_CLOSE);
    }

    private static bool IsProcessOpen(string processName)
    {
        var allRunnningProcesses = Process.GetProcesses();

        if (allRunnningProcesses.Length <= 0)
            return false;

        return allRunnningProcesses.Any(
            x => string.Equals(
                x.ProcessName, processName, StringComparison.OrdinalIgnoreCase
            )
        );
    }

    private static Process[] GetProcessesByName(string processName) =>
        Process.GetProcessesByName(processName);

    private static void CloseProcess(string processName)
    {
        var processes = GetProcessesByName(processName);

        if (processes is not null && processes.Length > 0)
        {
            var process = processes.First();
            process.Kill();
        }
    }
}
