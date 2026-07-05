// Exercise 7: Financial Forecasting
// Applied to TaskFlow: Calculate task completion trends and predict delivery dates

namespace TaskFlow.DSA;

public class TaskForecasting
{
    // Calculate average completion time for done tasks
    public static double AverageCompletionTimeDays(List<TaskItem> tasks)
    {
        var doneTasks = tasks.Where(t => t.Status == "Done" && t.CompletedAt.HasValue).ToList();
        if (!doneTasks.Any()) return 0;
        
        var totalDays = doneTasks.Sum(t => (t.CompletedAt!.Value - t.CreatedAt).TotalDays);
        return totalDays / doneTasks.Count;
    }
    
    // Predict if project will be completed on time
    public static bool IsProjectOnTrack(List<TaskItem> tasks, DateTime projectDeadline)
    {
        var pendingTasks = tasks.Where(t => t.Status != "Done").ToList();
        if (!pendingTasks.Any()) return true;
        
        var avgCompletionTime = AverageCompletionTimeDays(tasks);
        var estimatedCompletion = DateTime.Now.AddDays(avgCompletionTime * pendingTasks.Count);
        
        return estimatedCompletion <= projectDeadline;
    }
    
    // Calculate completion percentage
    public static double CompletionPercentage(List<TaskItem> tasks)
    {
        if (!tasks.Any()) return 0;
        var doneCount = tasks.Count(t => t.Status == "Done");
        return (double)doneCount / tasks.Count * 100;
    }
}

public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
}
