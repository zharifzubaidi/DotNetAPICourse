USE DotNetCourseDatabase
GO

-- Topic 11 - Post Upsert
CREATE PROCEDURE TutorialAppSchema.spPosts_Upsert
/*EXEC TutorialAppSchema.spPosts_Upsert @PostId = NULL, @UserId = 1009, @PostTitle = 'New Post', @PostContent = 'This is a new 2nd post.'*/
    @UserId INT
    , @PostTitle NVARCHAR(255)
    , @PostContent NVARCHAR(MAX)
    , @PostId INT = NULL

AS
BEGIN
    IF NOT EXISTS (SELECT * FROM TutorialAppSchema.Posts WHERE PostId = @PostId)
        BEGIN
            INSERT INTO TutorialAppSchema.Posts (
                UserId,
                PostTitle,
                PostContent,
                PostCreated,
                PostUpdated
            ) VALUES (
                @UserId,
                @PostTitle,
                @PostContent,
                GETDATE(),
                GETDATE()
            );
        END
    ELSE
        BEGIN
            UPDATE TutorialAppSchema.Posts
            SET
                PostTitle = @PostTitle,
                PostContent = @PostContent,
                PostUpdated = GETDATE()
            WHERE PostId = @PostId
        END
END