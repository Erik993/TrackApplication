/*
 Console project only to run migrations wiht .net maui
There are 3 projects:
1) .net MAUI - TrackApplication
2) TrackApplication.MigrationsRunner - to run migrations
3) TrackApplicationsData - DBContext and Models

To make the MAUI wor with entity framework:
1.Create console application
2.Create ClassLibraty with context and models
3.Console application -> add reference -> classlibrary
4.For console app - Install-Package Microsoft.EntityFrameworkCore.Design
5. Add code to console app
6. In package manager: 
    --as default project set projects with context and models
    -- as startup project set migration runner (console app)
7. now migrations work


 */


using Microsoft.EntityFrameworkCore;
using TrackApplicationData.DbContextData;

var db = new ApplicationContext();
db.Database.Migrate();

Console.WriteLine("Done.");


