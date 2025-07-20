
SELECT * FROM DotNetCourseDatabase.TutorialAppSchema.Computer
GO

SELECT * FROM DotNetCourseDatabase.TutorialAppSchema.Computer WHERE VideoCard = 'Robel-O''Hara'
GO

-- Reset the identity column to start from 1
TRUNCATE TABLE DotNetCourseDatabase.TutorialAppSchema.Computer
GO