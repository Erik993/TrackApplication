using TrackApplication.Views.Employees;

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
            Routing.RegisterRoute(nameof(AddEmployeePage), typeof(AddEmployeePage));
            Routing.RegisterRoute(nameof(ShowEmployeesPage), typeof(ShowEmployeesPage));
            Routing.RegisterRoute(nameof(EditEmployeePage), typeof(EditEmployeePage));


            //Routing.RegisterRoute("ShowEmployeesPage/EditEmployeePage", typeof(EditEmployeePage));
            //Routing.RegisterRoute("ShowEmployeePage/EditEmployeePage", typeof(EditEmployeePage));

        }
    }
}
