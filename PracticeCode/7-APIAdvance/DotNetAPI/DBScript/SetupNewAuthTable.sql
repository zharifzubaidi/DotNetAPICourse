USE DotNetCourseDatabAse
GO

-- To link password to an email
-- Password salt to create a password hash that is stored in the database
CREATE TABLE TutorialAppSchema.Auth (
    Email VARCHAR(50)
    , PasswordHash VARBINARY(MAX)
    , PasswordSalt VARBINARY(MAX)
)
GO

-- Boilerplate code to insert a user
SELECT * FROM TutorialAppSchema.Auth WHERE Email = ''

-- To insert a user
INSERT INTO TutorialAppSchema.Auth (Email, PasswordHash, PasswordSalt)
VALUES ()

-- Find user ID by email
SELECT UserId FROM TutorialAppSchema.Users WHERE FirstName = 'Test'