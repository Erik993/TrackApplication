using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using TrackApplication.Services;
using TrackApplication.Views.Employees;
using TrackApplication.Views.ItSupports;
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




            //To debug context
            /*
            builder.Services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TrackApplication;Trusted_Connection=True;MultipleActiveResultSets=true");
                options.LogTo(Console.WriteLine, LogLevel.Information)
                       .EnableSensitiveDataLogging();
            });*/


            builder.Services.AddDbContext<ApplicationContext>();

            //------------View page registration-------------------

            builder.Services.AddTransient<MainPage>();
            

            //for navigation funtionality, is used in update functionality
            builder.Services.AddSingleton<INavigationService, NavigationService>();

            //service for dummy data creation
            builder.Services.AddSingleton<ICreateTestUserService, TestUserService>();



            //Employee
            builder.Services.AddTransient<EmployeePage>();
            builder.Services.AddTransient<AddEmployeePage>();
            builder.Services.AddTransient<ShowEmployeesPage>();
            builder.Services.AddTransient<EditEmployeePage>();

            //ItSupport
            builder.Services.AddTransient<ItSupportPage>();
            builder.Services.AddTransient<AddItSupportPage>();
            builder.Services.AddTransient<ShowItSupportsPage>();
            builder.Services.AddTransient<EditItSupportPage>();





            //------------ViewModel, State, Repo registration-----------

            //Main
            builder.Services.AddTransient<MainViewModel>();


            //Employee
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<EmployeeViewModel>();
            builder.Services.AddSingleton<EmployeeState>();
            

            //ITSupport
            builder.Services.AddScoped<IItSupportRepository, ItSupportRepository>();
            builder.Services.AddScoped<ItSupportViewModel>();
            builder.Services.AddSingleton<ItSupportState>();
            

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
