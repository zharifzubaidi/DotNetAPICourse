USE DotNetCourseDatabase
GO

CREATE TABLE TutorialAppSchema.Posts(
    PostId INT IDENTITY(1,1),
    UserId INT,
    PostTitle NVARCHAR(255),
    PostContent NVARCHAR(MAX),
    PostCreated DATETIME,
    PostUpdated DATETIME
)
GO

-- Cluster index to inform the order that the rows is physically stored in the table
-- To search for posts by UserId and the result is sort by PostId
CREATE CLUSTERED INDEX cix_Posts_UserId_PostId ON TutorialAppSchema.Posts(UserId, PostId)
GO

SELECT [PostId],
[UserId],
[PostTitle],
[PostContent],
[PostCreated],
[PostUpdated] FROM TutorialAppSchema.Posts
GO

-- Add statement
INSERT INTO TutorialAppSchema.Posts([PostId],
[UserId],
[PostTitle],
[PostContent],
[PostCreated],
[PostUpdated]) VALUES()

-- Edit statement
UPDATE TutorialAppSchema.Posts
    SET [PostTitle] = '',
    [PostContent] = '',
    [PostCreated] = GETDATE(),
    [PostUpdated] = GETDATE()
    WHERE [PostId] = 1

UPDATE TutorialAppSchema.Posts
    SET UserId = 1008
    WHERE [PostId] = 1

SELECT [PostId],
[UserId],
[PostTitle],
[PostContent],
[PostCreated],
[PostUpdated] FROM TutorialAppSchema.Posts
    WHERE PostTitle LIKE '%search%'
        OR PostContent LIKE '%search%'
GO