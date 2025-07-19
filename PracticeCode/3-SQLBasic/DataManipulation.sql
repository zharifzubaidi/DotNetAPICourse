--Select all of the table value--
SELECT * FROM TutorialAppSchema.Computer
GO

--Select the certain column--
SELECT [ComputerId],
[Motherboard],
[CPUCores],
[HasWifi],
[HasLTE],
[ReleaseDate],
[Price],
[VideoCard] FROM TutorialAppSchema.Computer --WHERE 1 = 0 -->To show only column and return zero rows

--Set identity insert--
SET IDENTITY_INSERT TutorialAppSchema.Computer ON
SET IDENTITY_INSERT TutorialAppSchema.Computer OFF
GO

--Insert the data--
INSERT INTO TutorialAppSchema.Computer(
    [Motherboard],
    [CPUCores],
    [HasWifi],
    [HasLTE],
    [ReleaseDate],
    [Price],
    [VideoCard]
) VALUES(
    'Sample-Motherboard',
    4,
    1,
    0,
    '2022-01-01',
    1000,
    'Sample-VideoCard'
)
GO

--Delete row. Row is define by identity field or other indicator--
DELETE FROM TutorialAppSchema.Computer WHERE ComputerId = 1
GO

--Reset identity after delete all row
DBCC CHECKIDENT ('TutorialAppSchema.Computer', RESEED, 0);
GO

--Update value to all rows--
UPDATE TutorialAppSchema.Computer SET CPUCores = 4
GO

--Update value to certain rows--
UPDATE TutorialAppSchema.Computer SET CPUCores = 2 WHERE ComputerId = 1
GO

--Update value to certain range of rows but not all--
UPDATE TutorialAppSchema.Computer SET ReleaseDate = '2025-07-19' WHERE ComputerId > 1
GO

--Update value to null
UPDATE TutorialAppSchema.Computer SET CPUCores = NULL WHERE ReleaseDate < '2025-07-19'
GO

--Select all and filter out the null values
SELECT [ComputerId],
[Motherboard],
ISNULL([CPUCores],0) AS CPUCores,   --Check if the field is null, change to a default value
[HasWifi],
[HasLTE],
[ReleaseDate],
[Price],
[VideoCard] FROM TutorialAppSchema.Computer
GO

--Select all by ascending order
SELECT [ComputerId],
[Motherboard],
ISNULL([CPUCores],0) AS CPUCores,   
[HasWifi],
[HasLTE],
[ReleaseDate],
[Price],
[VideoCard] FROM TutorialAppSchema.Computer
    ORDER BY ReleaseDate --Small to big value
GO

--Select all by descending order
SELECT [ComputerId],
[Motherboard],
ISNULL([CPUCores],0) AS CPUCores,   
[HasWifi],
[HasLTE],
[ReleaseDate],
[Price],
[VideoCard] FROM TutorialAppSchema.Computer
    ORDER BY HasWifi DESC, ReleaseDate DESC --Big to small value. Prioritise the first and the second order
GO


