using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TrackApplicationData.Models;
using System.Diagnostics;
using TrackApplicationCore.States;
using TrackApplicationCore.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace TrackApplicationCore.ViewModels;


//if class inherits from ObservalbeObject, it should be partial, or there an error
public partial class EmployeeViewModel : ObservableObject
{
    private readonly IEmployeeRepository _repository;
    private readonly EmployeeState _state;


    //property is used in GoToEditEmployee method. without it shell navigation does not works. 
    private readonly INavigationService _navigation;




    //public ObservableCollection<Employee> Employees { get; set; } = new();
    public ObservableCollection<Employee> Employees => _state.Employees;





    [ObservableProperty]
    private string newUserName = string.Empty;

    [ObservableProperty]
    private string newEmail = string.Empty;

    [ObservableProperty]
    private bool newIsActive = true;

    [ObservableProperty]
    private DateTime newContractDate;



    /*Fields for Editing the element*/
    [ObservableProperty]
    private Employee? selectedEmployee;

    [ObservableProperty]
    private string editUserName;

    [ObservableProperty]
    private string editEmail;

    [ObservableProperty]
    private bool editIsActive;
    /*--------------------*/



    //Events to show alert windows
    public event Func<string, Task>? EmployeeAdded;
    public event Func<Employee, Task>? EmployeeDeleted;
    public event Func<Employee, Task>? EmployeeUpdated;

    public EmployeeViewModel(IEmployeeRepository repository, EmployeeState state, INavigationService navigation)
    {
        _repository = repository;
        _state = state;
        _navigation = navigation;
        LoadEmployees();

        //if this is printed when the page loads - DI is injecting
        Debug.WriteLine("ViewModel created with repo: " + repository.GetType().Name);
    }

    /*
    [RelayCommand]
    public async Task LoadEmployees()
    {
        Employees.Clear();

        var list = await _repository.GetAllAsync();
        foreach (var e in list)
        {
            Employees.Add(e);
        }

        _state.Employees.Clear();
        foreach (var e in Employees)
            _state.Employees.Add(e);
    }*/


    [RelayCommand]
    public async Task LoadEmployees()
    {
        var list = await _repository.GetAllAsync();

        _state.Employees.Clear();
        foreach (var e in list)
            _state.Employees.Add(e);
    }



    [RelayCommand]
    private async Task AddEmployee()
    {
        //to debug context
        Debug.WriteLine("Context type: " + _repository.GetType().FullName);


        if (!string.IsNullOrEmpty(NewUserName) && !string.IsNullOrEmpty(NewEmail))
        {
            var employee = new Employee(NewUserName, NewEmail, NewIsActive);
            Debug.WriteLine($"new employee: {employee.UserName}, {employee.Email}");

            await _repository.AddAsync(employee);
            
            await LoadEmployees();

        }

        if (EmployeeAdded != null)
            await EmployeeAdded.Invoke(NewUserName);

        //clear inputs
        NewUserName = string.Empty;
        NewEmail = string.Empty;
    }


    [RelayCommand]
    private async Task DeleteEmployee(Employee employee)
    {
        Debug.WriteLine($"deleting {employee.UserName}");
        await _repository.DeleteAsync(employee);
        
        await LoadEmployees();

        if(EmployeeDeleted != null)
        {
            await EmployeeDeleted.Invoke(employee);
        }
    }


    [RelayCommand]
    public async Task UpdateEmployee()
    {
        if (SelectedEmployee == null)
            return;

        if (!string.IsNullOrWhiteSpace(EditUserName))
            SelectedEmployee.UserName = EditUserName;

        if (!string.IsNullOrWhiteSpace(EditEmail))
            SelectedEmployee.Email = EditEmail;

        SelectedEmployee.IsActive = EditIsActive;

        //update record in database
        await _repository.UpdateAsync(SelectedEmployee);

        //reload collection for UI
        await LoadEmployees();


        //reset edited fields
        EditUserName = string.Empty;
        EditEmail = string.Empty;


        //run event if the employee was updated
        if (EmployeeUpdated != null)
            await EmployeeUpdated.Invoke(SelectedEmployee);

        //await _navigation.GoToAsync("..");

        // Update ObservableCollection immediately
        


    }



    //Load Employee by id, to edit it later
    public async Task LoadEmployeeForEdit(int employeeId)
    {
        SelectedEmployee = await _repository.GetByIdAsync(employeeId);

        if (SelectedEmployee != null)
        {
            EditUserName = SelectedEmployee.UserName;
            EditEmail = SelectedEmployee.Email;
            EditIsActive = SelectedEmployee.IsActive;
        }
    }

    
    [RelayCommand]
    public async Task GoToEditEmployee(int employeeId)
    {
        await _navigation.GoToAsync($"EditEmployeePage?employeeId={employeeId}");
    }


}
