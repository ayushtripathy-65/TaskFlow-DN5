-- Exercise 1: Ranking and Window Functions
-- Get tasks ranked by due date within each project

SELECT 
    t.Id,
    t.Title,
    t.Status,
    t.Priority,
    t.DueDate,
    t.ProjectId,
    p.Name AS ProjectName,
    ROW_NUMBER() OVER (PARTITION BY t.ProjectId ORDER BY t.DueDate) AS TaskRankInProject,
    RANK() OVER (ORDER BY t.DueDate) AS OverallRank,
    DENSE_RANK() OVER (ORDER BY t.DueDate) AS DenseRank
FROM TaskItems t
JOIN Projects p ON t.ProjectId = p.Id;

-- Get overdue tasks with row number
SELECT 
    Id,
    Title,
    Status,
    DueDate,
    ROW_NUMBER() OVER (ORDER BY DueDate DESC) AS RowNum
FROM TaskItems
WHERE DueDate < DATE('now') AND Status != 'Done';
