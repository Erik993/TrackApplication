using Microsoft.Extensions.Logging;
using System;
using TrackApplication.Interfaces;
using TrackApplication.Repositories;
using TrackApplication.States;
using TrackApplication.ViewModels;
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
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<EmployeeViewModel>();
            builder.Services.AddSingleton<EmployeeState>();

            return builder.Build();
        }
    }
}
