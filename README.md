# Introduction 
An offline capable survey tool for PM TR.   

# Getting Started
## Pre-requisites
1. Git [Download](https://git-scm.com/downloads)
2. Node and NPM [Download](https://nodejs.org/en/download/)
3. .NET Core 3.1 SDK [Download](https://dotnet.microsoft.com/download/dotnet-core/3.1)
4. Visual Studio Code [Download](https://code.visualstudio.com/Download)

## Installation process
1. Install all pre-requisites
2. Open PowerShell as an administrator
3. In PowerShell, run the following command: 'setx ASPNETCORE_ENVIRONMENT "Development" /M'
4. In PowerShell, navigate to the location where you want the source code to reside
5. In PowerShell, clone the survey tool repo  'git clone https://peoc3t@dev.azure.com/peoc3t/TacticalRadio/_git/TacticalRadio_FeedbackTool <folder_name>'
6. In PowerShell, Navigate to the folder where the repo was cloned
7. In PowerShell, Create a branch to work in  'git checkout -b <branch_name>'
8. Open Visual Studio Code (VSCode)
9. In VSCode, open the folder with the source code
10. IN VSCode, open a new terminal window
11.	In the VSCode terminal, navigate to the ClientApp folder 'cd .\ClientApp'
12. In the VSCode terminal, update the NPM dependencies 'npm install'
13. Once the NPM packages have finished installing, from the VSCode terminal, navigate back to the root of the app. 'cd ..'
14. In the VSCode terminal, restore nuget packages 'dotnet restore'
15. In the VSCode terminal, build the application 'dotnet build -c debug'
16. In the VSCode terminal, run the application 'dotnet run'

# Build and Test
## Generate offline USB folder (Windows 10)
1. In PowerShell, run the following command: 'setx ASPNETCORE_ENVIRONMENT "Production" /M'
2. Navigate to root project folder
3. Run the following command: 'dotnet publish -c Release -r win-x64 /p:PublishSingleFile=true /p:PublishTrimmed=true'
4. Navigate from the root folder to .\bin\Release\netcoreapp3.1\win-x64\publish
5. Copy contents of publish folder to USB drive
## Test offline USB folder (Windows 10)
1. Run the SurveyTool.exe from the drive
2. Open a browser to: https://localhost:5001
3. Operate various tests on the system
4. Kill the terminal running (CTRL + C)

# Database Management
## SQLite Tools
Upon running, this application generates a SQLite database file, according to the schema defined by the Entity Framework Core (EF Core) classes and OnModelCreating method in the Database Context file. At the current time, the best way we know of being able to query against the SQLite file is to use the [SQLTools extension](https://marketplace.visualstudio.com/items?itemName=mtxr.sqltools) for Visual Studio Code. 

The SQLTools extension has a dependency on the Node.js [SQLite3 package](https://github.com/mapbox/node-sqlite3). It may also be useful to install the [mssql extension](https://marketplace.visualstudio.com/items?itemName=ms-mssql.mssql), which will allow for editor recognition of SQL files. Dependencies of the SQLTools extension should be automatically recommended when a user first attempts to connect to a SQLite database file. When encountered, please just accept the dependencies.

**NOTE:** Currently, the SQLite database is only generated in the Development/Debug environment. This is automatic if you are using VS Code and press the "Run Debug" button, but may not be automatic if you run from the command line. In that case, it may be necessary to run a `dotnet publish` command after building, so that the "Production" version of the files will be updated. Automatic run of the app in Development/Debug mode from the command line will be fixed in a future work item.

## Entity Framework Core
### In Visual Studio
In Visual Studio, make sure you download the EF Core tools according to the instructions [here](https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/powershell).

### In VS Code
In VS Code, make sure you download the EF Core CLI tools, according to the instructions [here](https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dotnet). The version of the EF Core CLI tools and EF Core package used will need to be compatible with the .NET Core version already in use. Please use the following syntax to specify version:

`dotnet tool install --global dotnet-ef --version 3.1.1`

`dotnet add package Microsoft.EntityFrameworkCore.Design -v 3.1.1`

### Developing against a local database
To make sure that we don't actually run migrations against the live database is Azure, it is imperative that each developer setup his or her own local database for development purposes. To do this, perform the following steps:

#### Common Steps
1. Using SSMS (or SQL Server Data Tools if you are using Visual Studio and prefer), create a database on your localhost, giving it whatever name you see fit (e.g. SurveyTool-AppDev).

2. Update the connection string for the **SurveyToolOnlineDbContext** key, replacing the connection string's server name and initial catalog name with **localhost** and **<your database name>**, respectively and as applicable.

    `"ConnectionStrings": {`
    `"SurveyToolOnlineDbContext": "Data Source=**localhost**;Initial Catalog=**SurveyTool-AppDev**;Integrated Security=True"`
    `},`

3. Make sure the **appsettings.Development.json** **AppConnectionMode** key's value is set to **"Online"** so that EF Core will use the SQL Server database when identifying the DbContext.

#### In Visual Studio

1. Update your local database with the latest status of the Entity Framework Core migrations, as found in the Migrations folder, by running the following command in the Package Manager Console:

    `Update-Database`

#### In VS Code

**Sanity Check:** If you want to make sure your configuration is correct after the Common Steps, simply run `dotnet ef dbcontext info` in the Terminal to make sure the Provider name and Data source are correct.

1. Update your local database with the latest status of the Entity Framework Core migrations, as found in the Migrations folder, by running the following command in the Terminal:

    `dotnet ef database update`

### Migrations
**IMPORTANT:** Before performing a migration, make sure you first have the AppConnectionMode set correctly in the appsettings.Development.json file. The EF Core tools build the .NET Core project based off of these settings and the AppConnectionMode setting will determine against which database the migration is run (i.e. Azure SQL Server or local SQLite).

Full information on adding, removing, reverting, and applying migrations can be found [here](https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli)

#### Listing DbContexts in VS Code
To list migrations in VS Code, use:

`dotnet ef dbcontext list`

#### Listing Migrations in VS Code
To list migrations in VS Code, use:

`dotnet ef migrations list --context <ContextName>`

#### Adding a Migration in VS Code
To add a new migration in VS Code, use:

`dotnet ef migrations add --context <ContextName> -o "Migrations/<output folder>`

## Access to the Azure SQL Database
In order to gain access to the Azure SQL Database, you must first ask an administrator of the database to create a login for you. Once this login is created, you will be able to connect to the database using SSMS or Azure SQL database tools within Visual Studio.

The type of login selected should be a SQL Server login. Please find the connection string in the appsettings.json file for this .NET Core project.