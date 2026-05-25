public class AppLogger
{
    private static readonly Lazy<AppLogger> _lazy = new Lazy<AppLogger>(() => new AppLogger());

    private readonly List<string> _logBuffer = new();
    private readonly object _bufferLock = new object();

    private AppLogger() => Console.WriteLine("Logger Initialized");

    public static AppLogger Instance => _lazy.Value;

    public void Log(string message)
    {
        var entry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}";
        lock (_bufferLock)
        {
            _logBuffer.Add(entry);
        }
        Console.WriteLine(entry);
    }

    public IReadOnlyList<string> GetAllLogs()
    {
        lock (_bufferLock)
        {
            return _logBuffer.AsReadOnly();
        }
    }
}
