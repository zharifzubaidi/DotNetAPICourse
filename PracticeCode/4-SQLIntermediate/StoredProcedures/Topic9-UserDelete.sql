USE DotNetCourseDatabase
GO

-- Topic 9 - Create a stored procedure for user deletion
CREATE OR ALTER PROCEDURE TutorialAppSchema.spUser_Delete
-- EXEC TutorialAppSchema.spUser_Delete @UserId = 1011
    @UserId INT
AS
BEGIN

    -- Delete from Users
    DELETE FROM TutorialAppSchema.Users
        WHERE UserId = @UserId;
END

