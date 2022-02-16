namespace MicroAutomation.Log.Models;

public class LogOption
{
    public LogFileOption File { get; set; }
    public LogConsoleOption Console { get; set; }

    public LogOption()
    {
        File = new LogFileOption();
        Console = new LogConsoleOption();
    }
}