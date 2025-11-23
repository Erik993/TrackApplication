using System.Diagnostics;
using TrackApplicationCore.ViewModels;

namespace TrackApplication.Views.Employees;

public partial class EmployeePage : ContentPage
{
	public EmployeePage(EmployeeViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

	public async void GoToAddEmployeeButtonClicked(object sender, EventArgs e)
	{
		Debug.WriteLine("redirecting to add employee page");

		await Shell.Current.GoToAsync(nameof(AddEmployeePage));
        //await Shell.Current.GoToAsync("");
    }

	public async void GoToMainPageButtonClicked(object sender, EventArgs e)
	{
        //await Shell.Current.GoToAsync("//MainPage");
        //await Shell.Current.GoToAsync(nameof(MainPage));
        await Shell.Current.GoToAsync("..");
    }

	public async void GoToShowEmployeesButtonClicked(object sender, EventArgs e)
	{
        Debug.WriteLine("redirecting to show employee page");
        await Shell.Current.GoToAsync(nameof(ShowEmployeesPage));
	}
}