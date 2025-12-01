using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using TrackApplicationCore.ViewModels;

namespace TrackApplication.Views.Employees;

public partial class AddEmployeePage : ContentPage
{
	public AddEmployeePage(EmployeeViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
        vm.EmployeeAdded += OnEmployeeAdded;
	}


    /*
    private async void GoToUpdateEmployeePageButtonClicked(object sender, EventArgs e)
    {
        Debug.WriteLine("redirecting to update empl page");
        await Shell.Current.GoToAsync(nameof(EditEmployeePage));
    }
    */


    private async void GoBackToEmployeePageClicked(object sender, EventArgs e)
	{
        Debug.WriteLine("back to empl page");
        await Shell.Current.GoToAsync("..");
    }


    //Event to show alert window if the employee is added to database successfully
    private async Task OnEmployeeAdded(string name)
    {
        await DisplayAlertAsync("Success", $"Employee {name} added!", "OK");
        await Shell.Current.GoToAsync("..");
    }

}