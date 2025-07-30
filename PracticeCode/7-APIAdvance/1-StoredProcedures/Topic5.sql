-- Select the database to create the stored procedure
USE DotNetCourseDatabase
GO

-- Topic 5 - Outer apply to append a new column with average salary by department
-- do averaging for every row in the main query
ALTER PROCEDURE TutorialAppSchema.spUsers_Get
-- EXEC TutorialAppSchema.spUsers_Get @UserId=3     -- To test the stored procedure with parameters
    @UserId INT = NULL  -- Default parameter value
AS
BEGIN
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
            OUTER APPLY (
                SELECT UserJobInfo2.Department
                    , AVG(UserSalary2.Salary) AvgSalary
                FROM TutorialAppSchema.Users AS Users
                    LEFT JOIN TutorialAppSchema.UserSalary AS UserSalary2
                        ON UserSalary2.UserId = Users.UserId
                    LEFT JOIN TutorialAppSchema.UserJobInfo AS UserJobInfo2
                        ON UserJobInfo2.UserId = Users.UserId
                    WHERE UserJobInfo2.Department = UserJobInfo.Department
                    GROUP BY UserJobInfo2.Department
                ) AS AvgSalary
        WHERE Users.UserId = ISNULL(@UserId, Users.UserId) -- If @UserId is NULL, return all users
END
