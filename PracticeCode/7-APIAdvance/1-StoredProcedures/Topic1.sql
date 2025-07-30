-- Select the database to create the stored procedure in
USE DotNetCourseDatabase
GO


-- Topic 1 - Create a stored procedure
CREATE PROCEDURE TutorialAppSchema.spUsers_Get
-- ALTER PROCEDURE TutorialAppSchema.spUsers_Get    -- Uncomment to update the stored procedure
-- EXEC TutorialAppSchema.spUsers_Get               -- To test the stored procedure
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
END


