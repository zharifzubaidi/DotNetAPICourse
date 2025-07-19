--Run button is to run all of the scripts
--Highlight the script and then run will run only the highlighted script--

--Create new database--
CREATE DATABASE DotNetCourseDatabase
GO --Everything after this is a new query--

--Use database--
USE DotNetCourseDatabase
GO

--Create schema--
CREATE SCHEMA TutorialAppSchema
GO

--Remove table--
DROP TABLE TutorialAppSchema.Computer
GO

--Create table--
CREATE TABLE TutorialAppSchema.Computer
(
    --Field creation--
    --TableId INT IDENTITY(Starting, Increment By)--
    ComputerId INT IDENTITY(1,1) PRIMARY KEY --The ID must be unique--
    --, Motherboard CHAR(10) 'x', 'x' --
    --, Motherboard VARCHAR(10) 'x' 'x' | Unicode. Save space but cannot receive symbol. 8 bits / 1 byte--
    , Motherboard NVARCHAR(50)         --Non unicode. String type. recommended. 16 bits / 2 byte. 255 is recommended--
    , CPUCores INT
    , HasWifi BIT                       --Boolean type--
    , HasLTE BIT
    , ReleaseDate DATETIME              --Date compatible with C#. DATETIME2 is take slightly more space but more accurate--
    , Price DECIMAL(18,4)               --For decimal. 18 whole num & 4 digits after decimal. Alternative: Float, Double--
    , VideoCard NVARCHAR(50)
)
GO

--Select all of the table value--
SELECT * FROM TutorialAppSchema.Computer
GO