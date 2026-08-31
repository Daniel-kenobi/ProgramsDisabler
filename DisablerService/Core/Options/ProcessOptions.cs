namespace DisablerService.Core.Options;

public class ProcessOptions
{
    public string ProcessToWatch { get; set; } = null!;
    public string ProcessToClose { get; set; } = null!;
    public int IntervalInSeconds { get; set; }
}
