-- Exercise 5: Return Data from a Stored Procedure
-- Stored procedure that returns task statistics

CREATE PROCEDURE sp_GetTaskStatistics
    @ProjectId INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        COUNT(*) AS TotalTasks,
        SUM(CASE WHEN Status = 'Done' THEN 1 ELSE 0 END) AS CompletedTasks,
        SUM(CASE WHEN Status = 'Pending' THEN 1 ELSE 0 END) AS PendingTasks,
        SUM(CASE WHEN Status = 'InProgress' THEN 1 ELSE 0 END) AS InProgressTasks,
        SUM(CASE WHEN DueDate < DATE('now') AND Status != 'Done' THEN 1 ELSE 0 END) AS OverdueTasks
    FROM TaskItems
    WHERE @ProjectId IS NULL OR ProjectId = @ProjectId;
END;
