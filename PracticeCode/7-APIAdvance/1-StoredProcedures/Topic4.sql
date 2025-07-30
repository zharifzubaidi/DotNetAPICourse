-- Select the database to create the stored procedure
USE DotNetCourseDatabase
GO

-- Topic 4 - Join 
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
        [UserJobInfo].[Department]
    FROM TutorialAppSchema.Users AS Users
        LEFT JOIN TutorialAppSchema.UserSalary AS UserSalary
            ON UserSalary.UserId = Users.UserId
        LEFT JOIN TutorialAppSchema.UserJobInfo AS UserJobInfo
            ON UserJobInfo.UserId = Users.UserId
        WHERE Users.UserId = ISNULL(@UserId, Users.UserId) -- If @UserId is NULL, return all users
END


