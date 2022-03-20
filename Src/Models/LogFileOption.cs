using Serilog;

namespace Netboot.Logging.Models;

public class LogFileOption
{
    public bool Status { get; set; }
    public string Path { get; set; }
    public long FileSizeLimitBytes { get; set; }
    public RollingInterval RollingInterval { get; set; }
    public bool RollOnFileSizeLimit { get; set; }
    public int RetainedFileCountLimit { get; set; }

    public LogFileOption()
    {
        Status = true;
        Path = "Logs\\log-.txt";
        FileSizeLimitBytes = 5242880;
        RollingInterval = RollingInterval.Day;
        RollOnFileSizeLimit = true;
        RetainedFileCountLimit = 30;
    }
}