# Run this script from the folder containing your .csproj file
# Add dapper and entity framework core to the project 
# by the following command for database connection
# This commands will be recorded in the .csproj file
# To run: powershell -ExecutionPolicy Bypass -File .\add-db-packages.ps1

Write-Host "Adding Dapper..."
dotnet add package Dapper
if ($LASTEXITCODE -eq 0) { Write-Host "Dapper added successfully." } else { Write-Host "Failed to add Dapper." }

Write-Host "Adding Microsoft.Data.SqlClient..."
dotnet add package Microsoft.Data.SqlClient
if ($LASTEXITCODE -eq 0) { Write-Host "Microsoft.Data.SqlClient added successfully." } else { Write-Host "Failed to add Microsoft.Data.SqlClient." }

Write-Host "Adding Microsoft.EntityFrameworkCore..."
dotnet add package Microsoft.EntityFrameworkCore
if ($LASTEXITCODE -eq 0) { Write-Host "Microsoft.EntityFrameworkCore added successfully." } else { Write-Host "Failed to add Microsoft.EntityFrameworkCore." }

Write-Host "Adding Microsoft.EntityFrameworkCore.SqlServer..."
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
if ($LASTEXITCODE -eq 0) { Write-Host "Microsoft.EntityFrameworkCore.SqlServer added successfully." } else { Write-Host "Failed to add Microsoft.EntityFrameworkCore.SqlServer." }

Write-Host "Adding Microsoft.Extensions.Configuration..."
dotnet add package Microsoft.Extensions.Configuration
if ($LASTEXITCODE -eq 0) { Write-Host "Microsoft.Extensions.Configuration added successfully." } else { Write-Host "Failed to add Microsoft.Extensions.Configuration." }

Write-Host "Adding Microsoft.Extensions.Configuration.Json..."
dotnet add package Microsoft.Extensions.Configuration.Json
if ($LASTEXITCODE -eq 0) { Write-Host "Microsoft.Extensions.Configuration.Json added successfully." } else { Write-Host "Failed to add Microsoft.Extensions.Configuration.Json." }

Write-Host "Adding Newtonsoft.Json..."
dotnet add package Newtonsoft.Json
if ($LASTEXITCODE -eq 0) { Write-Host "Newtonsoft.Json added successfully." } else { Write-Host "Failed to add Newtonsoft.Json." }

Write-Host "Adding AutoMapper..."
dotnet add package AutoMapper
if ($LASTEXITCODE -eq 0) { Write-Host "AutoMapper added successfully." } else { Write-Host "Failed to add AutoMapper." }

Write-Host "Adding Microsoft.Extensions.Logging..."
dotnet add package Microsoft.Extensions.Logging.Console
if ($LASTEXITCODE -eq 0) { Write-Host "Microsoft.Extensions.Logging.Console added successfully." } else { Write-Host "Failed to add Microsoft.Extensions.Logging.Console." }    

Write-Host "Adding Swashbuckle.AspNetCore..."
dotnet add package Swashbuckle.AspNetCore
if ($LASTEXITCODE -eq 0) { Write-Host "Swashbuckle.AspNetCore added successfully." } else { Write-Host "Failed to add Swashbuckle.AspNetCore." }    

Write-Host "All package installation commands completed."

