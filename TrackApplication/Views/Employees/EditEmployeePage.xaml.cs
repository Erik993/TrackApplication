using TrackApplicationCore.ViewModels;

namespace TrackApplication.Views.Employees;

public partial class EditEmployeePage : ContentPage
{
	public EditEmployeePage(EmployeeViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
	}


    private async void GoBackToEmployeeShowPageClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
        //await Shell.Current.GoToAsync("ShowEmployeesPage");
    }
}