/*To use master database first*/
USE master
/*To stop database*/
DROP DATABASE DotNetCourseDatabase

/*Create database*/
CREATE DATABASE DotNetCourseDatabase
GO

/*Use this database*/
USE DotNetCourseDatabase
GO

/*Create a schema*/ 
/*Is a organisational unit for table*/
CREATE SCHEMA TutorialAppSchema
GO

/*Create a table on that schema*/ 
CREATE TABLE TutorialAppSchema.Computer(
    ComputerId INT IDENTITY(1,1) PRIMARY KEY,
    Motherboard NVARCHAR(50),
    CPUCores INT,
    HasWifi BIT,
    HasLTE BIT,
    ReleaseDate DATE,
    Price DECIMAL(18,4),
    VideoCard NVARCHAR(50)
);

/*SELECT * means select everything*/
SELECT * FROM TutorialAppSchema.Computer

