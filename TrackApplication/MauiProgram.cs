using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using TrackApplication.Services;
using TrackApplication.Views.Assignments;
using TrackApplication.Views.Employees;
using TrackApplication.Views.ItSupports;
using TrackApplication.Views.Tickets;
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


            //connection string loading

            //need to manually load appsettings.json, otherwise i can't extract connection string
            //code from AI, chatpgt
            builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            Debug.WriteLine($"Connection string: {connectionString}");

            builder.Services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(connectionString));

            //------------View page registration-------------------

            builder.Services.AddTransient<MainPage>();
            

            //for navigation funtionality, is used in update functionality
            builder.Services.AddSingleton<INavigationService, NavigationService>();

            //service for dummy data creation
            builder.Services.AddSingleton<ICreateTestUserService, TestUserService>();
            builder.Services.AddSingleton<ICreateTestTicketService, TestTicketService>();
            builder.Services.AddSingleton<ICreateTestAssignmentService, TestAssignmentService>();


            //Employee pages
            builder.Services.AddTransient<EmployeePage>();
            builder.Services.AddTransient<AddEmployeePage>();
            builder.Services.AddTransient<ShowEmployeesPage>();
            builder.Services.AddTransient<EditEmployeePage>();

            //ItSupport pages
            builder.Services.AddTransient<ItSupportPage>();
            builder.Services.AddTransient<AddItSupportPage>();
            builder.Services.AddTransient<ShowItSupportsPage>();
            builder.Services.AddTransient<EditItSupportPage>();

            //Ticket pages
            builder.Services.AddTransient<TicketPage>();
            builder.Services.AddTransient<AddTicketPage>();
            builder.Services.AddTransient<ShowTicketsPage>();
            builder.Services.AddTransient<EditTicketPage>();

            //Assignment pages
            builder.Services.AddTransient<AssignmentPage>();
            builder.Services.AddTransient<AddAssignmentPage>();
            builder.Services.AddTransient<ShowAssignmentsPage>();
            builder.Services.AddTransient<EditAssignmentPage>();


            //------------ViewModel, State, Repo registration-----------

            //Main
            builder.Services.AddTransient<MainViewModel>();

            //Employee
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddTransient<EmployeeViewModel>();
            builder.Services.AddSingleton<EmployeeState>();
            
            //ITSupport
            builder.Services.AddScoped<IItSupportRepository, ItSupportRepository>();
            builder.Services.AddTransient<ItSupportViewModel>();
            builder.Services.AddSingleton<ItSupportState>();
            
            //Ticket
            builder.Services.AddScoped<ITicketRepository, TicketRepository>();
            builder.Services.AddTransient<TicketViewModel>();
            builder.Services.AddSingleton<TicketState>();

            //Assignment
            builder.Services.AddScoped<IAssignmentRepository, AssignmentRepository>();
            builder.Services.AddTransient<AssignmentViewModel>();
            builder.Services.AddSingleton<AssignmentState>();
            

            return builder.Build();
        }
    }
}
