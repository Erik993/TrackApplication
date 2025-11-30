using System.Diagnostics;
using TrackApplicationCore.ViewModels;
using TrackApplicationData.Models;

namespace TrackApplication.Views.Employees;


//QueryProperty - used to provide a parameter for Shell navagation. 
[QueryProperty(nameof(EmployeeId), "employeeId")]
public partial class EditEmployeePage : ContentPage
{
    //new
    private readonly EmployeeViewModel _vm;

    //parameter that is provided for Shell navigation. this id will be updated
    public int EmployeeId { get; set; }




    public EditEmployeePage(EmployeeViewModel vm)
	{
        InitializeComponent();
        _vm = vm;
        BindingContext = vm;
        _vm.EmployeeUpdated += OnEmployeeUpdated;
    }

    private async void GoBackToEmployeeShowPageClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
        //await Shell.Current.GoToAsync("ShowEmployeesPage");
    }


    ///new
    protected override async void OnAppearing()
    {
        Debug.WriteLine($"Edit Page Appearing, ID = {EmployeeId}");
        base.OnAppearing();
        await _vm.LoadEmployeeForEdit(EmployeeId);
    }


    private async Task OnEmployeeUpdated(Employee employee)
    {
        await DisplayAlertAsync("Success", $"Employee {employee.UserName} is updated!", "OK");
        await Shell.Current.GoToAsync("..");
    }

}