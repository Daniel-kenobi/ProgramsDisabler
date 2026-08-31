using DisablerService.Core.Options;
using System.Diagnostics;

namespace DisablerService.Application
{
    public static class WorkerApplication
    {
        public static void StartOperation(ProcessOptions processOptions)
        {
            if (IsProcessOpen(processOptions.ProcessToWatch) && IsProcessOpen(processOptions.ProcessToClose))
                CloseProcess(processOptions.ProcessToClose);
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
}
