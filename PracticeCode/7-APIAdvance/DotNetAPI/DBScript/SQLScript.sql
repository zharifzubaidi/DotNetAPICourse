USE DotNetCourseDatabase
GO

-- Retrieve data query
SELECT [UserId],
[FirstName],
[LastName],
[Email],
[Gender],
[Active] FROM TutorialAppSchema.Users
ORDER BY UserId DESC
GO

SELECT [UserId],
[JobTitle],
[Department] FROM TutorialAppSchema.UserJobInfo
GO

SELECT [UserId],
[Salary],
[AvgSalary] FROM TutorialAppSchema.UserSalary
GO

-- Update / Edit data query
UPDATE TutorialAppSchema.Users
    SET [FirstName] = '',
        [LastName] = '',
        [Email] = '',
        [Gender] = '',
        [Active] = 1
    WHERE [UserId] = 1 -- primary key use to filter the record to update
GO

-- Insert data query without primary key UserId
INSERT INTO TutorialAppSchema.Users(
    [FirstName],
    [LastName],
    [Email],
    [Gender],
    [Active]
) VALUES(  -- replace with actual values
    [FirstName],
    [LastName],
    [Email],
    [Gender],
    [Active]
)


-- Troubleshoot #1: Need to ensure receive bit
UPDATE TutorialAppSchema.Users
    SET [FirstName] = 'Franky',
        [LastName] = 'Laidler',
        [Email] = 'flaidler1@over-blog.com',
        [Gender] = 'Bigender',
        [Active] = False
    WHERE [UserId] = 2

-- Troubleshoot #2: Just need value
INSERT INTO TutorialAppSchema.Users(
            [FirstName],
            [LastName],
            [Email],
            [Gender],
            [Active]
        ) VALUES(
            'Zharif',
            'Zubaidi',
            'zaref@gmail.com',
            'male',
            1
        )

-- Select all desc
SELECT * FROM TutorialAppSchema.Users
ORDER BY UserId DESC

-- Select all desc
SELECT * FROM TutorialAppSchema.Users
WHERE UserId BETWEEN 495 AND 505

-- To know how many records in the table
SELECT COUNT(*) FROM TutorialAppSchema.Users

-- Delete data query
DELETE FROM TutorialAppSchema.Users
WHERE UserId = 1001 -- primary key use to filter the record to delete

-- Select user by email
SELECT * FROM TutorialAppSchema.Users
WHERE FirstName = 'Test'

SELECT * FROM TutorialAppSchema.Users
WHERE UserId BETWEEN 1000 AND 1020