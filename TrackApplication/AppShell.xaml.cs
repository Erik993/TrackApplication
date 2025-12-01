using TrackApplication.Views.Employees;
using TrackApplication.Views.ItSupports;
using TrackApplication.Views.Tickets;

namespace TrackApplication
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            /*
            await Shell.Current.GoToAsync("RouteName"); - push to stack
            await Shell.Current.GoToAsync(".."); - pop from stack
             */

            //Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            //Routing.RegisterRoute(nameof(EmployeePage), typeof(EmployeePage));

            //Employee pages registered
            Routing.RegisterRoute(nameof(AddEmployeePage), typeof(AddEmployeePage));
            Routing.RegisterRoute(nameof(ShowEmployeesPage), typeof(ShowEmployeesPage));
            Routing.RegisterRoute(nameof(EditEmployeePage), typeof(EditEmployeePage));

            //It Support pages registered
            Routing.RegisterRoute(nameof(AddItSupportPage), typeof(AddItSupportPage));
            Routing.RegisterRoute(nameof(ShowItSupportsPage), typeof(ShowItSupportsPage));
            Routing.RegisterRoute(nameof(EditItSupportPage), typeof(EditItSupportPage));

            //Ticket pages registered
            Routing.RegisterRoute(nameof(AddTicketPage), typeof(AddTicketPage));
            Routing.RegisterRoute(nameof(ShowTicketsPage), typeof(ShowTicketsPage));
            Routing.RegisterRoute(nameof(EditTicketPage), typeof(EditTicketPage));



        }
    }
}
