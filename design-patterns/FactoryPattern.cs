// Exercise 2: Implementing the Factory Method Pattern
// TaskFactory creates different types of tasks based on input

namespace TaskFlow.DesignPatterns;

public interface ITaskFactory
{
    TaskItem CreateTask(string title, string description, int projectId);
}

public class BugTaskFactory : ITaskFactory
{
    public TaskItem CreateTask(string title, string description, int projectId)
    {
        return new TaskItem
        {
            Title = $"[BUG] {title}",
            Description = description,
            Status = "Pending",
            Priority = "High",
            ProjectId = projectId,
            CreatedAt = DateTime.UtcNow
        };
    }
}

public class FeatureTaskFactory : ITaskFactory
{
    public TaskItem CreateTask(string title, string description, int projectId)
    {
        return new TaskItem
        {
            Title = $"[FEATURE] {title}",
            Description = description,
            Status = "Pending",
            Priority = "Medium",
            ProjectId = projectId,
            CreatedAt = DateTime.UtcNow
        };
    }
}

public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Priority { get; set; } = string.Empty;
    public int ProjectId { get; set; }
    public DateTime CreatedAt { get; set; }
}
