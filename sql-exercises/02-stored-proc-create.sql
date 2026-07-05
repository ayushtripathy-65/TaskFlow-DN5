-- Exercise 1: Create a Stored Procedure
-- Stored procedure to get tasks by project with status filter

CREATE PROCEDURE sp_GetTasksByProject
    @ProjectId INT,
    @Status NVARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    IF @Status IS NULL
        SELECT t.*, p.Name AS ProjectName 
        FROM TaskItems t
        JOIN Projects p ON t.ProjectId = p.Id
        WHERE t.ProjectId = @ProjectId;
    ELSE
        SELECT t.*, p.Name AS ProjectName 
        FROM TaskItems t
        JOIN Projects p ON t.ProjectId = p.Id
        WHERE t.ProjectId = @ProjectId AND t.Status = @Status;
END;
