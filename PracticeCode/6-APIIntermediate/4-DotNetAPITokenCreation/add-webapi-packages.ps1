# Run this script from the folder containing your .csproj file
# Add dapper and entity framework core to the project 
# by the following command for database connection
# This commands will be recorded in the .csproj file
# To run: powershell -ExecutionPolicy Bypass -File .\add-webapi-packages.ps1

# Run this script from the folder containing your .csproj file
# Add dapper and entity framework core to the project 
# by the following command for database connection
# This commands will be recorded in the .csproj file
# To run: powershell -ExecutionPolicy Bypass -File .\add-db-packages.ps1

Write-Host "Adding Dapper..."
dotnet add package Dapper
if ($LASTEXITCODE -eq 0) { Write-Host "Dapper added successfully." } else { Write-Host "Failed to add Dapper." }

Write-Host "Adding AutoMapper..."
dotnet add package AutoMapper
if ($LASTEXITCODE -eq 0) { Write-Host "AutoMapper added successfully." } else { Write-Host "Failed to add AutoMapper." }

Write-Host "Adding Microsoft.Data.SqlClient..."
dotnet ad package Microsoft.Data.SqlClient
if ($LASTEXITCODE -eq 0) { Write-Host "Microsoft.Data.SqlClient added successfully." } else { Write-Host "Failed to add Microsoft.Data.SqlClient." }

Write-Host "Adding Swashbuckle.AspNetCore..."
dotnet add package Swashbuckle.AspNetCore
if ($LASTEXITCODE -eq 0) { Write-Host "Swashbuckle.AspNetCore added successfully." } else { Write-Host "Failed to add Swashbuckle.AspNetCore." }

Write-Host "Adding Microsoft.EntityFrameworkCore..."
dotnet add package Microsoft.entityframeworkcore
if ($LASTEXITCODE -eq 0) { Write-Host "Microsoft.EntityFrameworkCore added successfully." } else { Write-Host "Failed to add Microsoft.EntityFrameworkCore." }

Write-Host "Adding Microsoft.EntityFrameworkCore.Relational..."
dotnet add package Microsoft.EntityFrameworkCore.Relational
if ($LASTEXITCODE -eq 0) { Write-Host "Microsoft.EntityFrameworkCore.Relational added successfully." } else { Write-Host "Failed to add Microsoft.EntityFrameworkCore.Relational." }

Write-Host "Adding Microsoft.EntityFrameworkCore.SqlServer..."
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
if ($LASTEXITCODE -eq 0) { Write-Host "Microsoft.EntityFrameworkCore.SqlServer added successfully." } else { Write-Host "Failed to add Microsoft.EntityFrameworkCore.SqlServer." }

Write-Host "All package installation commands completed."


