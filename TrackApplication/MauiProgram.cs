using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using TrackApplication.Views.Employees;
using TrackApplicationCore.Interfaces;
using TrackApplicationCore.Repositories;
using TrackApplicationCore.States;
using TrackApplicationCore.ViewModels;
using TrackApplicationData.DbContextData;

/*
MAUI is a startup file, so DI containers sould be registered there
I need to tell DI container what services exists
registration should be inside CreateMauiApp(), before builder.Build().

Once registered, MAUI can automatically:
-create ViewModels
-inject repositories
-inject DbContext
-create pages with ViewModels in constructor

 */

namespace TrackApplication
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            builder.Services.AddDbContext<ApplicationContext>();


            //To debug context
            /*
            builder.Services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TrackApplication;Trusted_Connection=True;MultipleActiveResultSets=true");
                options.LogTo(Console.WriteLine, LogLevel.Information)
                       .EnableSensitiveDataLogging();
            });*/



            //register pages
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<EmployeePage>();
            builder.Services.AddTransient<AddEmployeePage>();
            builder.Services.AddTransient<ShowEmployeesPage>();
            builder.Services.AddTransient<EditEmployeePage>();

            //Employee
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddTransient<EmployeeViewModel>();
            builder.Services.AddSingleton<EmployeeState>();
            

            //ITSupport
            /*
            builder.Services.AddScoped<IITSupportRepository, ITSupportRepository>();
            builder.Services.AddTransient<ITSupportViewModel>();
            builder.Services.AddSingleton<ITSupportState>();
            */

            //Ticket
            /*
            builder.Services.AddScoped<ITicketRepository, TicketRepository>();
            builder.Services.AddTransient<TicketViewModel>();
            builder.Services.AddSingleton<TicketState>();
            */



            //Assignment
            /*
            builder.Services.AddScoped<IAssignmentRepository, AssignmentRepository>();
            builder.Services.AddTransient<AssignmentViewModel>();
            builder.Services.AddSingleton<AssignmentState>();
            */

            return builder.Build();
        }
    }
}
