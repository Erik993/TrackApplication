

using TrackApplication.ViewModels;

namespace TrackApplication
{
    public partial class MainPage : ContentPage
    {

        //Injected:
        //EmployeeViewModel employeeVm
        public MainPage(EmployeeViewModel employeeVm)
        {
            InitializeComponent();
            
        }

    }
}
