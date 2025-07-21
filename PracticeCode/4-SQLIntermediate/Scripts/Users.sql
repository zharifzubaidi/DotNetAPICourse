USE DotNetCourseDatabase;
GO

CREATE TABLE TutorialAppSchema.Users
(
    UserId INT IDENTITY(1, 1) PRIMARY KEY
    , FirstName NVARCHAR(50)
    , LastName NVARCHAR(50)
    , Email NVARCHAR(50)
    , Gender NVARCHAR(50)
    , Active BIT
);

CREATE TABLE TutorialAppSchema.UserSalary
(
    UserId INT
    , Salary DECIMAL(18, 4)
);

CREATE TABLE TutorialAppSchema.UserJobInfo
(
    UserId INT
    , JobTitle NVARCHAR(50)
    , Department NVARCHAR(50),
);

-- Check Users table
SELECT  [UserId]
        , [FirstName]
        , [LastName]
        , [Email]
        , [Gender]
        , [Active]
  FROM  TutorialAppSchema.Users;

-- Check Users salary
SELECT  [UserId]
        , [Salary]
  FROM  TutorialAppSchema.UserSalary;

-- Check UserJobInfo table with qualified list of fields
SELECT  [UserId]
        , [JobTitle]
        , [Department]
  FROM  TutorialAppSchema.UserJobInfo;

-- Check all data in tables
SELECT * FROM TutorialAppSchema.Users;

SELECT * FROM TutorialAppSchema.UserSalary;

SELECT * FROM TutorialAppSchema.UserJobInfo;

-- Check all data in tables with qualified list of fields using alias for table name
-- Use Ctrl + space at the star to see the list of fields to ensure fields comes from the correct table
SELECT [Users].[UserId],
       [Users].[FirstName],
       [Users].[LastName],
       [Users].[Email],
       [Users].[Gender],
       [Users].[Active] FROM TutorialAppSchema.Users AS Users

SELECT [UserSalary].[UserId],
       [UserSalary].[Salary] FROM TutorialAppSchema.UserSalary AS UserSalary

SELECT [UserJobInfo].[UserId],
       [UserJobInfo].[JobTitle],
       [UserJobInfo].[Department] FROM TutorialAppSchema.UserJobInfo AS UserJobInfo

-- Manipulate data using aliases AS clause
SELECT [Users].[UserId],
       [Users].[FirstName] + ' ' + [Users].[LastName] AS FullName, -- Field/Value alias: Concatenate FirstName and LastName to create a new field called FullName
       [Users].[Email],
       [Users].[Gender],
       [Users].[Active]
    FROM TutorialAppSchema.Users AS Users -- Table alias: Use AS to create an alias for the table name

-- Sort data using ORDER BY clause
SELECT [Users].[UserId],
       [Users].[FirstName] + ' ' + [Users].[LastName] AS FullName,
       [Users].[Email],
       [Users].[Gender],
       [Users].[Active]
    FROM TutorialAppSchema.Users AS Users
    ORDER BY Users.UserId DESC; -- Sort by UserId in descending order

-- Sort data using ORDER BY and WHERE clauses
SELECT [Users].[UserId],
       [Users].[FirstName] + ' ' + [Users].[LastName] AS FullName,
       [Users].[Email],
       [Users].[Gender],
       [Users].[Active] 
    FROM TutorialAppSchema.Users AS Users
    WHERE Users.Active = 1      -- Filter to show only active users
    ORDER BY Users.UserId DESC; -- Sort by UserId in descending order

-- Inner join (JOIN) tables using JOIN clause
SELECT [Users].[UserId],
       [Users].[FirstName] + ' ' + [Users].[LastName] AS FullName,
       [Users].[Email],
       [Users].[Gender],
       [Users].[Active],
       [UserJobInfo].[JobTitle],
       [UserJobInfo].[Department]
    FROM TutorialAppSchema.Users AS Users
        JOIN TutorialAppSchema.UserJobInfo AS UserJobInfo   --Inner join UserJobInfo table
            ON Users.UserId = UserJobInfo.UserId -- Join condition: Match UserId in Users with UserId in UserJobInfo
    WHERE Users.Active = 1
    ORDER BY Users.UserId DESC;

-- Left inner join (LEFT JOIN) tables using LEFT JOIN clause
-- Query from all 3 tables using JOIN and LEFT JOIN
-- This query will return all users, their job titles, departments, and salaries, including those without job info or salary
-- If a user does not have a job title or salary, the corresponding fields will be NULL
SELECT [Users].[UserId],
       [Users].[FirstName] + ' ' + [Users].[LastName] AS FullName,
       [UserJobInfo].[JobTitle],
       [UserJobInfo].[Department],
       [UserSalary].[Salary],
       [Users].[Email],
       [Users].[Gender],
       [Users].[Active]
    FROM TutorialAppSchema.Users AS Users
        JOIN TutorialAppSchema.UserSalary AS UserSalary             -- Will find matching ID and just filter out missing columns
            ON Users.UserId = UserSalary.UserId 
        LEFT JOIN TutorialAppSchema.UserJobInfo AS UserJobInfo      -- Will find matching ID and just show NULL value in the missing columns
            ON Users.UserId = UserJobInfo.UserId                    -- Join condition: Match UserId in Users with UserId in UserJobInfo
    WHERE Users.Active = 1
    ORDER BY Users.UserId DESC;

-- Delete certain data in a column using DELETE clause
DELETE FROM TutorialAppSchema.UserJobInfo
    WHERE UserId > 500; -- Delete job info for users with UserId > 500
DELETE FROM TutorialAppSchema.UserSalary
    WHERE UserId BETWEEN 250 AND 750; -- Delete salary info for users with UserId between 250 and 750 -- 501 ROWS
    -- When we use between, we also include the lower and upper bounds, so 250 and 750 are also included

-- Check if only all have a record in UserJobInfo table
-- WHERE EXISTS clause is similar to INNER JOIN, but it checks if a record exists in the subquery
SELECT [UserSalary].[UserId],
    [UserSalary].[Salary] 
FROM TutorialAppSchema.UserSalary AS UserSalary
    WHERE EXISTS(
        SELECT * FROM TutorialAppSchema.UserJobInfo AS UserJobInfo
            WHERE UserJobInfo.UserId = UserSalary.UserId); -- Check if UserSalary has UserId that exists in UserJobInfo

-- Conditional logic using NOT EQUALS clause to exclude 7th UserId
SELECT [UserSalary].[UserId],
    [UserSalary].[Salary] 
FROM TutorialAppSchema.UserSalary AS UserSalary
    WHERE EXISTS(
        SELECT * FROM TutorialAppSchema.UserJobInfo AS UserJobInfo
            WHERE UserJobInfo.UserId = UserSalary.UserId)
        AND UserId <> 7; -- Check if UserSalary has UserId that exists in UserJobInfo and UserId is not equal to 7

-- Take data with the same header from multiple tables using UNION clause
SELECT [UserId],
[Salary] FROM TutorialAppSchema.UserSalary
-- UNION -- Combine distinct rows from both queries. If same UserId exists in both tables, it will only show once
UNION ALL -- Combine all rows from both queries, including duplicates
SELECT * FROM TutorialAppSchema.UserSalary

-- Index complicated topic: Clustered vs Non-Clustered Index
-- Clustered index: Sorts and stores the data rows in the table based on the index
-- Non-clustered index: Creates a separate structure that points to the data rows in the table
-- Making a faster query using clustered index. Create a clustered index on UserId column in UserSalary table
-- Store in the userId column in the UserSalary table
CREATE CLUSTERED INDEX cix_UserSalary_UserId ON TutorialAppSchema.UserSalary (UserId);
-- CREATE INDEX <index_name> ON <table_name> (<column_name>); -> non clustered index
CREATE NONCLUSTERED INDEX ix_UserSalary_Salary ON TutorialAppSchema.UserSalary (Salary);
CREATE NONCLUSTERED INDEX ix_UserJobInfo_JobTitle ON TutorialAppSchema.UserJobInfo (JobTitle) INCLUDE (Department); -- Include Department column in the index

-- Filtered index using non-clustered index
-- Filtered index: Index that only includes rows that meet a specific condition (Active)
CREATE NONCLUSTERED INDEX fix_UserJobInfo_Active ON TutorialAppSchema.Users (Active)
    INCLUDE ([Email],[FirstName], [LastName]) -- Also include userId(primary key) because it is our clustered index
        WHERE Active = 1; -- Only include rows where Active is 1

-- Querying data from multiple tables using JOIN and LEFT JOIN
SELECT [Users].[UserId],
       [Users].[FirstName] + ' ' + [Users].[LastName] AS FullName,
       [UserJobInfo].[JobTitle],
       [UserJobInfo].[Department],
       [UserSalary].[Salary],
       [Users].[Email],
       [Users].[Gender],
       [Users].[Active]
    FROM TutorialAppSchema.Users AS Users
        -- INNER JOIN
        JOIN TutorialAppSchema.UserSalary AS UserSalary             -- Will find matching ID and just filter out missing columns
            ON Users.UserId = UserSalary.UserId 
        LEFT JOIN TutorialAppSchema.UserJobInfo AS UserJobInfo      -- Will find matching ID and just show NULL value in the missing columns
            ON Users.UserId = UserJobInfo.UserId                    -- Join condition: Match UserId in Users with UserId in UserJobInfo
    WHERE Users.Active = 1
    ORDER BY Users.UserId DESC;
    
    -- Aggregate functions: COUNT, SUM, AVG, MIN, MAX, STRING_AGG
    -- Aggregate functions are used to perform calculations on a set of values and return a single value
    SELECT ISNULL([UserJobInfo].[Department], 'No Department Listed') AS Department,
        SUM([UserSalary].[Salary]) AS TotalSalary,   -- SUM: Calculate total salary for each department
        MIN([UserSalary].[Salary]) AS MinSalary,     -- MIN: Find minimum salary in each department
        MAX([UserSalary].[Salary]) AS MaxSalary,     -- MAX: Find maximum salary in each department
        AVG([UserSalary].[Salary]) AS AvgSalary,     -- AVG: Calculate average salary in each department
        COUNT(*) AS PeopleInDepartment,              -- COUNT: Count number of users in each department
        STRING_AGG(Users.UserId, ', ') AS UserIds    -- STRING_AGG: Concatenate UserIds in each department into a single string
    FROM TutorialAppSchema.Users AS Users
        -- INNER JOIN
        JOIN TutorialAppSchema.UserSalary AS UserSalary             -- Will find matching ID and just filter out missing columns
            ON Users.UserId = UserSalary.UserId 
        LEFT JOIN TutorialAppSchema.UserJobInfo AS UserJobInfo      -- Will find matching ID and just show NULL value in the missing columns
                ON Users.UserId = UserJobInfo.UserId                    -- Join condition: Match UserId in Users with UserId in UserJobInfo
        WHERE Users.Active = 1
        GROUP BY [UserJobInfo].[Department] -- GROUP BY: Group by department to calculate total salary for each department
        ORDER BY ISNULL([UserJobInfo].[Department], 'No Department Listed') DESC
        --ORDER BY UserJobInfo.Department DESC
        -- Order is where, group and order by are executed in this order

-- OUTER APPLY is used to join a table with a subquery that returns multiple rows
SELECT [Users].[UserId],
       [Users].[FirstName] + ' ' + [Users].[LastName] AS FullName,
       [UserJobInfo].[JobTitle],
       [UserJobInfo].[Department],
       DepartmentAverage.AvgSalary,
       [UserSalary].[Salary],
       [Users].[Email],
       [Users].[Gender],
       [Users].[Active]
    FROM TutorialAppSchema.Users AS Users
        JOIN TutorialAppSchema.UserSalary AS UserSalary
            ON Users.UserId = UserSalary.UserId 
        LEFT JOIN TutorialAppSchema.UserJobInfo AS UserJobInfo  
            ON Users.UserId = UserJobInfo.UserId               
        OUTER APPLY (   -- Similar to LEFT JOIN if remove ISNULL at WHERE clause
            -- Pull record where the department matches the outer query
            -- SELECT TOP 1
            SELECT ISNULL([UserJobInfo2].[Department], 'No Department Listed') AS Department,   
                AVG([UserSalary2].[Salary]) AS AvgSalary         
                FROM TutorialAppSchema.UserSalary AS UserSalary2
                    LEFT JOIN TutorialAppSchema.UserJobInfo AS UserJobInfo2      
                            ON UserSalary2.UserId = UserJobInfo2.UserId   
                 -- Match department with the outer query                
                WHERE ISNULL([UserJobInfo2].[Department], 'No Department Listed') = ISNULL([UserJobInfo].[Department], 'No Department Listed')
                GROUP BY [UserJobInfo2].[Department]
        ) AS DepartmentAverage -- OUTER APPLY: Join with a subquery that returns multiple rows     
    WHERE Users.Active = 1
    ORDER BY Users.UserId DESC;

SELECT [Users].[UserId],
       [Users].[FirstName] + ' ' + [Users].[LastName] AS FullName,
       [UserJobInfo].[JobTitle],
       [UserJobInfo].[Department],
       DepartmentAverage.AvgSalary,
       [UserSalary].[Salary],
       [Users].[Email],
       [Users].[Gender],
       [Users].[Active]
    FROM TutorialAppSchema.Users AS Users
        JOIN TutorialAppSchema.UserSalary AS UserSalary
            ON Users.UserId = UserSalary.UserId 
        LEFT JOIN TutorialAppSchema.UserJobInfo AS UserJobInfo  
            ON Users.UserId = UserJobInfo.UserId               
        OUTER APPLY (   -- Similar to INNER JOIN if remove ISNULL at WHERE clause
            SELECT ISNULL([UserJobInfo2].[Department], 'No Department Listed') AS Department,   
                AVG([UserSalary2].[Salary]) AS AvgSalary         
                FROM TutorialAppSchema.UserSalary AS UserSalary2
                    LEFT JOIN TutorialAppSchema.UserJobInfo AS UserJobInfo2      
                            ON UserSalary2.UserId = UserJobInfo2.UserId                  
                WHERE [UserJobInfo2].[Department] = [UserJobInfo].[Department]
                GROUP BY [UserJobInfo2].[Department]
        ) AS DepartmentAverage  
    WHERE Users.Active = 1
    ORDER BY Users.UserId DESC;

-- CROSS APPLY is similar to OUTER APPLY, but it returns all rows from the outer query and the subquery
-- exlcuding NULL values. Similar to INNER JOIN / JOIN
SELECT [Users].[UserId],
       [Users].[FirstName] + ' ' + [Users].[LastName] AS FullName,
       [UserJobInfo].[JobTitle],
       [UserJobInfo].[Department],
       DepartmentAverage.AvgSalary,
       [UserSalary].[Salary],
       [Users].[Email],
       [Users].[Gender],
       [Users].[Active]
    FROM TutorialAppSchema.Users AS Users
        JOIN TutorialAppSchema.UserSalary AS UserSalary
            ON Users.UserId = UserSalary.UserId 
        LEFT JOIN TutorialAppSchema.UserJobInfo AS UserJobInfo  
            ON Users.UserId = UserJobInfo.UserId               
        CROSS APPLY (   -- Similar to INNER JOIN if remove ISNULL at WHERE clause
            SELECT ISNULL([UserJobInfo2].[Department], 'No Department Listed') AS Department,   
                AVG([UserSalary2].[Salary]) AS AvgSalary         
                FROM TutorialAppSchema.UserSalary AS UserSalary2
                    LEFT JOIN TutorialAppSchema.UserJobInfo AS UserJobInfo2      
                            ON UserSalary2.UserId = UserJobInfo2.UserId                  
                WHERE [UserJobInfo2].[Department] = [UserJobInfo].[Department]
                GROUP BY [UserJobInfo2].[Department]
        ) AS DepartmentAverage  
    WHERE Users.Active = 1
    ORDER BY Users.UserId DESC;

-- Date and time functions
-- Get current date and time based on SQL server being hosted
SELECT GETDATE()
-- Minus 5 years from current date and time
SELECT DATEADD(YEAR, -5, GETDATE())
-- Get difference between two dates
SELECT DATEDIFF(MONTH, '2023-01-01', GETDATE())             -- Returns positive value
SELECT DATEDIFF(DAY,GETDATE(),DATEADD(YEAR, -5, GETDATE())) -- Returns negative value

-- Alter table to add a new column
-- Add a new column/field 
ALTER TABLE TutorialAppSchema.UserSalary ADD AvgSalary DECIMAL(18,4)
-- Check if the new column is added      
SELECT * FROM TutorialAppSchema.UserSalary;               
-- Dynamically update the new column with the average salary for each user
UPDATE UserSalary   -- Update UserSalary table with the new column AvgSalary
    SET UserSalary.AvgSalary = DepartmentAverage.AvgSalary
FROM TutorialAppSchema.UserSalary AS UserSalary
    LEFT JOIN TutorialAppSchema.UserJobInfo AS UserJobInfo
        ON UserJobInfo.UserId = UserSalary.UserId
    -- Use CROSS APPLY to calculate the average salary for each department
    -- Use ISNULL to handle NULL values in the Department column
    -- Add to the new column AvgSalary
    CROSS APPLY ( 
        SELECT ISNULL([UserJobInfo2].[Department], 'No Department Listed') AS Department,   
            AVG([UserSalary2].[Salary]) AS AvgSalary         
            FROM TutorialAppSchema.UserSalary AS UserSalary2
                LEFT JOIN TutorialAppSchema.UserJobInfo AS UserJobInfo2      
                        ON UserSalary2.UserId = UserJobInfo2.UserId                  
            WHERE ISNULL([UserJobInfo2].[Department], 'No Department Listed') = ISNULL([UserJobInfo].[Department], 'No Department Listed')
            GROUP BY [UserJobInfo2].[Department]
    ) AS DepartmentAverage      

-- After create a new column we can use it in our query
SELECT [Users].[UserId],
       [Users].[FirstName] + ' ' + [Users].[LastName] AS FullName,
       [UserJobInfo].[JobTitle],
       [UserJobInfo].[Department],
       [UserSalary].[Salary],
       [UserSalary].[AvgSalary], -- Use the new column AvgSalary
       [Users].[Email],
       [Users].[Gender],
       [Users].[Active]
    FROM TutorialAppSchema.Users AS Users
        JOIN TutorialAppSchema.UserSalary AS UserSalary
            ON Users.UserId = UserSalary.UserId 
        LEFT JOIN TutorialAppSchema.UserJobInfo AS UserJobInfo  
            ON Users.UserId = UserJobInfo.UserId                
    WHERE Users.Active = 1
    ORDER BY Users.UserId DESC;       

SELECT * FROM TutorialAppSchema.UserSalary
