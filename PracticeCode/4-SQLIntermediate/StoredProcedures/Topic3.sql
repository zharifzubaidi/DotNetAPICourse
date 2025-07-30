-- Select the database to create the stored procedure
USE DotNetCourseDatabase
GO

-- Topic 3 - How to handle null parameters in stored procedures
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
        [Users].[Active] 
    FROM TutorialAppSchema.Users AS Users
        WHERE Users.UserId = ISNULL(@UserId, Users.UserId) -- If @UserId is NULL, return all users
END


