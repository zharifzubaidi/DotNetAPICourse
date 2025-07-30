USE DotNetCourseDatabase
GO

-- Topic 8 - Create a stored procedure for upsert operation with user peripherals
CREATE OR ALTER PROCEDURE TutorialAppSchema.spUser_Upsert
    /*EXEC TutorialAppSchema.spUser_Upsert @FirstName = 'Lisa', @LastName = 'La', 
        @Email = 'lalisa@example.com', @Gender = 'Female', @JobTitle = 'Software Engineer',
        @Department = 'Engineering', @Salary = 5000.00,
        @Active = 1, @UserId = NULL*/
	@FirstName NVARCHAR(50),
	@LastName NVARCHAR(50),
	@Email NVARCHAR(50),
	@Gender NVARCHAR(50),
    @JobTitle NVARCHAR(50),
    @Department NVARCHAR(50),
    @Salary DECIMAL(18, 4),
	@Active BIT = 1,
    @UserId INT = NULL
AS
BEGIN
    IF NOT EXISTS (SELECT * FROM TutorialAppSchema.Users WHERE UserId = @UserId)
        BEGIN
        IF NOT EXISTS (SELECT * FROM TutorialAppSchema.Users WHERE Email = @Email)
            BEGIN
                DECLARE @OutputUserId INT

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

                SET @OutputUserId = @@IDENTITY

                -- Insert into UserSalary
                INSERT INTO TutorialAppSchema.UserSalary(
                    UserId,
                    Salary
                ) VALUES (
                    @OutputUserId,
                    @Salary
                )

                -- Insert into UserJobInfo
                INSERT INTO TutorialAppSchema.UserJobInfo(
                    UserId,
                    JobTitle,
                    Department
                ) VALUES (
                    @OutputUserId,
                    @JobTitle,
                    @Department
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

            UPDATE TutorialAppSchema.UserSalary
            SET 
                Salary = @Salary
            WHERE UserId = @UserId

            UPDATE TutorialAppSchema.UserJobInfo
            SET 
                JobTitle = @JobTitle,
                Department = @Department
            WHERE UserId = @UserId
        END
END