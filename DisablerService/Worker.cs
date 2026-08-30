using DisablerService.Core.Constants;
using System.Diagnostics;

namespace DisablerService;

public class Worker : BackgroundService
{
    private readonly TimeSpan _intervalToSearchByProcess = TimeSpan.FromSeconds(30);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var timer = new PeriodicTimer(_intervalToSearchByProcess);

            while (await timer.WaitForNextTickAsync(stoppingToken))
                HandleOperation();
        }
    }

    private static void HandleOperation()
    {
        if (IsProcessOpen(Constants.PROCESS_TO_WATCH_NAME) && IsProcessOpen(Constants.PROCESS_TO_CLOSE))
            CloseProcess(Constants.PROCESS_TO_CLOSE);
    }

    private static bool IsProcessOpen(string processName)
    {
        var processesByName = Process.GetProcessesByName(processName);

        if (processesByName.Length <= 0)
            return false;

        return processesByName.Length > 0;
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
