namespace Netboot.Logging.Models
{
    public class LogConfiguration
    {
        public LogFileOption File { get; set; }
        public LogConsoleOption Console { get; set; }

        public LogOption()
        {
            File = new LogFileOption();
            Console = new LogConsoleOption();
        }
    }
}
