USE DotNetCourseDatabase
GO

-- Topic 7 - Create a stored procedure for upsert operation
-- This stored procedure will insert a new user if UserId is NULL
-- or update the existing user if UserId is provided.
CREATE OR ALTER PROCEDURE TutorialAppSchema.spUser_Upsert
    /*EXEC TutorialAppSchema.spUser_Upsert @FirstName = 'Jenny', @LastName = 'Doe', 
        @Email = 'j.doe@example.com', @Gender = 'Male', @Active = 1, @UserId = NULL*/
	@FirstName NVARCHAR(50),
	@LastName NVARCHAR(50),
	@Email NVARCHAR(50),
	@Gender NVARCHAR(50),
	@Active BIT = 1,
    @UserId INT = NULL
AS
BEGIN
    IF NOT EXISTS (SELECT * FROM TutorialAppSchema.Users WHERE UserId = @UserId)
        BEGIN
        IF NOT EXISTS (SELECT * FROM TutorialAppSchema.Users WHERE Email = @Email)
            BEGIN
                INSERT INTO TutorialAppSchema.Users(
                    [FirstName],
                    [LastName],
                    [Email],
                    [Gender],
                    [Active]
                ) VALUES (
                    @FirstName,
                    @LastName,
                    @Email,
                    @Gender,
                    @Active
                )
            END
        END
    ELSE
        BEGIN
            UPDATE TutorialAppSchema.Users
            SET 
                FirstName = @FirstName,
                LastName = @LastName,
                Email = @Email,
                Gender = @Gender,
                Active = @Active
            WHERE UserId = @UserId
        END
END