// Exercise 2: E-commerce Platform Search Function
// Applied to TaskFlow: Search tasks by title, status, or priority

namespace TaskFlow.DSA;

public class TaskSearch
{
    // Linear search - find tasks by title (case insensitive)
    public static List<TaskItem> SearchByTitle(List<TaskItem> tasks, string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword)) return tasks;
        
        var result = new List<TaskItem>();
        foreach (var task in tasks)
        {
            if (task.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                result.Add(task);
        }
        return result;
    }
    
    // Binary search - find task by ID (requires sorted list)
    public static TaskItem? BinarySearchById(List<TaskItem> tasks, int id)
    {
        var sorted = tasks.OrderBy(t => t.Id).ToList();
        int left = 0, right = sorted.Count - 1;
        
        while (left <= right)
        {
            int mid = left + (right - left) / 2;
            if (sorted[mid].Id == id) return sorted[mid];
            if (sorted[mid].Id < id) left = mid + 1;
            else right = mid - 1;
        }
        return null;
    }
    
    // Filter tasks by multiple criteria
    public static List<TaskItem> FilterTasks(
        List<TaskItem> tasks, 
        string? status = null, 
        string? priority = null,
        DateTime? dueBefore = null)
    {
        return tasks.Where(t => 
            (status == null || t.Status == status) &&
            (priority == null || t.Priority == priority) &&
            (dueBefore == null || t.DueDate <= dueBefore)
        ).ToList();
    }
}

public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Priority { get; set; } = string.Empty;
    public DateTime? DueDate { get; set; }
}
