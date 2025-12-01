using TrackApplicationCore.ViewModels;
using TrackApplication.Views.Employees;

namespace TrackApplication
{
    public partial class MainPage : ContentPage
    {

        //Injected:
        //EmployeeViewModel employeeVm
        //Shell automatically sends ViewModel to constructor

        /*
        public MainPage(EmployeeViewModel employeeVm)
        {
            InitializeComponent();
            BindingContext = employeeVm;
        }*/

        public MainPage(MainViewModel mainVm)
        {
            InitializeComponent();
            BindingContext = mainVm;
        }


        public async void GoToEmployeePageButtonClicked(object sender, EventArgs e)
        {
            //await Shell.Current.GoToAsync(nameof(EmployeePage));
            await Shell.Current.GoToAsync("EmployeePage");
        }

    }
}
