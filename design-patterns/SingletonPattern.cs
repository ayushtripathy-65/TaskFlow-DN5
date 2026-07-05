// Exercise 1: Implementing the Singleton Pattern
// TaskLogger ensures only one logger instance exists across the application

namespace TaskFlow.DesignPatterns;

public sealed class TaskLogger
{
    private static readonly TaskLogger _instance = new TaskLogger();
    private readonly List<string> _logs = new List<string>();
    
    private TaskLogger() { }
    
    public static TaskLogger Instance => _instance;
    
    public void Log(string message)
    {
        _logs.Add($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}");
    }
    
    public IEnumerable<string> GetLogs() => _logs.AsReadOnly();
    
    public void ClearLogs() => _logs.Clear();
}
