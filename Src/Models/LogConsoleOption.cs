namespace Netboot.Utility.Logging.Models
{
    public class LogConsoleOption
    {
        public bool Status { get; set; }
        public string OutputTemplate { get; set; }
        public int BufferSize { get; set; }
        public bool BlockWhenFull { get; set; }

        public LogConsoleOption()
        {
            Status = true;
            OutputTemplate = "[{Timestamp:HH:mm:ss.fff}][{Level:u4}][{ThreadId}][{SourceContext}] {Message}{NewLine}{Exception}";
            BufferSize = 10000;
            BlockWhenFull = true;
        }
    }
}