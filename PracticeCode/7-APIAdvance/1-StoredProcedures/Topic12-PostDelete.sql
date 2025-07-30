USE DotNetCourseDatabase
GO

-- Topic 12 - Post Delete
CREATE OR ALTER PROCEDURE TutorialAppSchema.spPosts_Delete
/*EXEC TutorialAppSchema.spPosts_Delete @PostId = 3, @UserId = 1009*/
    @PostId INT
    , @UserId INT
AS
BEGIN
    DELETE FROM TutorialAppSchema.Posts
        WHERE PostId = @PostId
            AND UserId = @UserId
END