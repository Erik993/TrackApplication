using System.Diagnostics;
using TrackApplicationCore.ViewModels;
using TrackApplicationData.Models;

namespace TrackApplication.Views.Employees;

public partial class ShowEmployeesPage : ContentPage
{
	public ShowEmployeesPage(EmployeeViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;

		vm.EmployeeDeleted += OnEmployeeDeleted;
	}

	private async void GoToEditEmployeePageButtonClicked(object sender, EventArgs e)
	{
        Debug.WriteLine("redirecting to update empl page");
        await Shell.Current.GoToAsync(nameof(EditEmployeePage));
    }

    private async void GoToBackToEmployeePageButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }


    private Task OnEmployeeDeleted(Employee employee)
    {
        return DisplayAlertAsync("Deleted",$"Employee {employee.UserName} was deleted","OK");
    }



    // for testing purpose
    private async void ClickedDelete(object sender, EventArgs e)
	{
		Debug.WriteLine("clicked delete");
	}



}