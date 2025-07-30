-- Select the database to create the stored procedure
USE DotNetCourseDatabase
GO

-- Topic 6 - Temporary table to store average salary by department and join it with the main query
ALTER PROCEDURE TutorialAppSchema.spUsers_Get
-- EXEC TutorialAppSchema.spUsers_Get @UserId=3 @Active=1    -- To test the stored procedure with parameters
    @UserId INT = NULL
    , @Active BIT = NULL
AS
BEGIN
    -- Old SQL server command
    -- IF OBJECT_ID('tempdb..#AverageDeptSalary', 'U') IS NOT NULL
    --     BEGIN
    --         DROP TABLE #AverageDeptSalary
    --     END
    -- New SQL server command
    DROP TABLE IF EXISTS #AverageDeptSalary
    -- Create a temporary table to store average salary by department
    SELECT UserJobInfo.Department
        , AVG(UserSalary.Salary) AvgSalary
        INTO #AverageDeptSalary -- Create a temporary table
    FROM TutorialAppSchema.Users AS Users
        LEFT JOIN TutorialAppSchema.UserSalary AS UserSalary
            ON UserSalary.UserId = Users.UserId
        LEFT JOIN TutorialAppSchema.UserJobInfo AS UserJobInfo
            ON UserJobInfo.UserId = Users.UserId
        GROUP BY UserJobInfo.Department

    -- Basically sort by category group such as department
    CREATE CLUSTERED INDEX cix_AverageDeptSalary_Department ON #AverageDeptSalary(Department)

    -- Select the user details along with salary, job title, department, and average salary by department
    SELECT [Users].[UserId],
        [Users].[FirstName],
        [Users].[LastName],
        [Users].[Email],
        [Users].[Gender],
        [Users].[Active],
        [UserSalary].[Salary],
        [UserJobInfo].[JobTitle],
        [UserJobInfo].[Department],
        AvgSalary.AvgSalary
    FROM TutorialAppSchema.Users AS Users
        LEFT JOIN TutorialAppSchema.UserSalary AS UserSalary
            ON UserSalary.UserId = Users.UserId
        LEFT JOIN TutorialAppSchema.UserJobInfo AS UserJobInfo
            ON UserJobInfo.UserId = Users.UserId
        LEFT JOIN #AverageDeptSalary AS AvgSalary
            ON AvgSalary.Department = UserJobInfo.Department -- Join the temporary table with the main query
        WHERE Users.UserId = ISNULL(@UserId, Users.UserId)
            AND Users.Active = ISNULL(@Active, Users.Active)
END

