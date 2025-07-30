-- Select the database to create the stored procedure
USE DotNetCourseDatabase
GO

-- Topic 2 - Create a stored procedure with parameters
-- CREATE PROCEDURE TutorialAppSchema.spUsers_Get   -- Create new stored procedure
-- ALTER PROCEDURE TutorialAppSchema.spUsers_Get       -- Uncomment to update the stored procedure
-- EXEC TutorialAppSchema.spUsers_Get               -- To test the stored procedure
-- EXEC TutorialAppSchema.spUsers_Get @UserId=3     -- To test the stored procedure with parameters
    -- Add a parameter to the stored procedure
ALTER PROCEDURE TutorialAppSchema.spUsers_Get 
    @UserId INT 
AS
BEGIN
    -- Logic goes here
    SELECT [Users].[UserId],
        [Users].[FirstName],
        [Users].[LastName],
        [Users].[Email],
        [Users].[Gender],
        [Users].[Active] 
    FROM TutorialAppSchema.Users AS Users -- Create alias
    WHERE Users.UserId = @UserId -- Filter by UserId
END


