USE DotNetCourseDatabase
GO

-- Topic 9 - Create a stored procedure for user deletion
CREATE OR ALTER PROCEDURE TutorialAppSchema.spUser_Delete
-- EXEC TutorialAppSchema.spUser_Delete @UserId = 1011
    @UserId INT
AS
BEGIN
    -- Delete from Users
    DECLARE @Email NVARCHAR(50);

    SELECT  @Email = Users.Email
      FROM  TutorialAppSchema.Users
     WHERE  Users.UserId = @UserId;

    DELETE  FROM TutorialAppSchema.UserSalary
     WHERE  UserSalary.UserId = @UserId;

    DELETE  FROM TutorialAppSchema.UserJobInfo
     WHERE  UserJobInfo.UserId = @UserId;

    DELETE  FROM TutorialAppSchema.Users
     WHERE  Users.UserId = @UserId;

    DELETE  FROM TutorialAppSchema.Auth
     WHERE  Auth.Email = @Email;
END

