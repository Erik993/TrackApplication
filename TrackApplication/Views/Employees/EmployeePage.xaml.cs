using System.Diagnostics;
using TrackApplicationCore.ViewModels;
using TrackApplicationCore.States;

namespace TrackApplication.Views.Employees;

public partial class EmployeePage : ContentPage
{
	private readonly EmployeeState _state;
	public EmployeePage(EmployeeViewModel vm, EmployeeState state)
	{
		InitializeComponent();
		BindingContext = vm;
		_state = state;
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
		if (_state == null || _state.Employees == null || !_state.Employees.Any())
		{
			await Application.Current.MainPage.DisplayAlertAsync(
                "No Employees",
				"There are no employees. Please add at least one before continuing.",
				"OK");
		}
		else
		{
            Debug.WriteLine("redirecting to show employee page");
            await Shell.Current.GoToAsync(nameof(ShowEmployeesPage));
        }

	}
}