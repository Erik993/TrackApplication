using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using TrackApplicationCore.Interfaces;
using TrackApplicationCore.Repositories;
using TrackApplicationCore.States;
using TrackApplicationData.DbContextData;
using TrackApplicationData.Models;

namespace TrackApplicationCore.ViewModels;


//if class inherits from ObservalbeObject, it should be partial, or there an error
public partial class EmployeeViewModel : ObservableObject
{
    private readonly IEmployeeRepository _repository;
    private readonly EmployeeState _state;

    //to show DB alerts
    private readonly IAlertService _alertService;


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
    private Employee? selectedEmployee = null;

    [ObservableProperty]
    private string editUserName = string.Empty;

    [ObservableProperty]
    private string editEmail = string.Empty;

    [ObservableProperty]
    private bool editIsActive;
    /*--------------------*/



    //Events to show alert windows
    public event Func<string, Task>? EmployeeAdded;
    public event Func<Employee, Task>? EmployeeDeleted;
    public event Func<Employee, Task>? EmployeeUpdated;

    public EmployeeViewModel(IEmployeeRepository repository, EmployeeState state, 
        INavigationService navigation, IAlertService alertService)
    {
        _repository = repository;
        _state = state;
        _navigation = navigation;
        _alertService = alertService;
        LoadEmployees();
    }


    [RelayCommand]
    public async Task LoadEmployees()
    {
        try
        {
            var list = await _repository.GetAllAsync();
            _state.Employees.Clear();
            foreach (var e in list)
            {
                _state.Employees.Add(e);
            }
        }
        catch(ApplicationException ex)
        {
            await _alertService.ShowAsync("Error", ex.Message, "ОК");
        }
    }


    [RelayCommand]
    private async Task AddEmployee()
    {
        if (string.IsNullOrEmpty(NewUserName) || string.IsNullOrEmpty(NewEmail))
            return;

        var employee = new Employee(NewUserName, NewEmail, NewIsActive);

        try
        {
            await _repository.AddAsync(employee);
            await LoadEmployees();

            if (EmployeeAdded != null)
                await EmployeeAdded.Invoke(NewUserName);
        }
        catch (ApplicationException ex)
        {
            // show message
            await _alertService.ShowAsync("Error", ex.Message, "ОК");
        }

        //clear inputs
        NewUserName = string.Empty;
        NewEmail = string.Empty;
    }



    //--------------manually throw exception with button. just to check it works-------------------------------
    [RelayCommand]
    private async Task TestErrorAlert()
    {
        try
        {
            throw new ApplicationException("eror in conncetion to DB");
        }
        catch (ApplicationException ex)
        {
            await _alertService.ShowAsync("Error", ex.Message, "ОК");
        }
    }


    /*-------------------------------------------*/



    [RelayCommand]
    private async Task DeleteEmployee(Employee employee)
    {
        try
        {
            Debug.WriteLine($"deleting {employee.UserName}");
            await _repository.DeleteAsync(employee);
            await LoadEmployees();
            //if object was deleted, invoke event to show display alert window
            if (EmployeeDeleted != null)
            {
                await EmployeeDeleted.Invoke(employee);
            }
        }
        catch(ApplicationException ex)
        {
            await _alertService.ShowAsync("Error", ex.Message, "ОК");
        }

    }


    [RelayCommand]
    public async Task UpdateEmployee()
    {
        try
        {
            if (SelectedEmployee == null)
                return;

            if (!string.IsNullOrWhiteSpace(EditUserName))
                SelectedEmployee.UserName = EditUserName;

            if (!string.IsNullOrWhiteSpace(EditEmail))
                SelectedEmployee.Email = EditEmail;

            SelectedEmployee.IsActive = EditIsActive;

            //update record in db
            await _repository.UpdateAsync(SelectedEmployee);

            //reload collection for UI
            await LoadEmployees();

            //run event if the employee was updated
            if (EmployeeUpdated != null)
                await EmployeeUpdated.Invoke(SelectedEmployee);
        }
        catch(ApplicationException ex)
        {
            await _alertService.ShowAsync("Error", ex.Message, "ОК");
        }


        //reset edited fields
        EditUserName = string.Empty;
        EditEmail = string.Empty;

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
