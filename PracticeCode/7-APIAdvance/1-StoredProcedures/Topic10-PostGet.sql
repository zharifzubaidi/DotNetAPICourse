USE DotNetCourseDatabase
GO

-- Topic 10 - Get post by user ID (optional) and search value. Two parameters
CREATE OR ALTER PROCEDURE TutorialAppSchema.spPosts_Get
/* EXEC TutorialAppSchema.spPosts_Get @UserId = 1008, @SearchValue = 'Edited' */
/* EXEC TutorialAppSchema.spPosts_Get @PostId = 1 */
    @UserId INT = NULL
    , @SearchValue NVARCHAR(MAX) = NULL
    , @PostId INT = NULL
AS
BEGIN
    SELECT [Posts].[PostId],
        [Posts].[UserId],
        [Posts].[PostTitle],
        [Posts].[PostContent],
        [Posts].[PostCreated],
        [Posts].[PostUpdated] 
    FROM TutorialAppSchema.Posts AS Posts
        WHERE Posts.UserId = ISNULL(@UserId, Posts.UserId)
            AND Posts.PostId = ISNULL(@PostId, Posts.PostId)
            AND (@SearchValue IS NULL
                OR Posts.PostContent LIKE '%' + @SearchValue + '%'
                OR Posts.PostTitle LIKE '%' + @SearchValue + '%')
END