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

    public ObservableCollection<Employee> Employees { get; set; } = new();

    [ObservableProperty]
    private string newUserName = string.Empty;

    [ObservableProperty]
    private string newEmail = string.Empty;

    [ObservableProperty]
    private bool newIsActive = true;

    [ObservableProperty]
    private DateTime newContractDate;

    //Events to show alert windows
    public event Func<string, Task>? EmployeeAdded;
    public event Func<Employee, Task>? EmployeeDeleted;
    public event Func<Employee, Task>? EmployeeUpdated;

    public EmployeeViewModel(IEmployeeRepository repository, EmployeeState state)
    {
        _repository = repository;
        _state = state;
        LoadEmployees();

        //if this is printed when the page loads - DI is injecting
        Debug.WriteLine("ViewModel created with repo: " + repository.GetType().Name);
    }


    [RelayCommand]
    public async Task LoadEmployees()
    {
        Employees.Clear();

        var list = await _repository.GetAllAsync();
        foreach (var e in list)
        {
            Employees.Add(e);
        }

        _state.Employees = Employees;
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
    private async Task EditEmployee(Employee employee)
    {
        Debug.WriteLine("editing employee");

        if (!string.IsNullOrEmpty(NewUserName))
        {
            employee.UserName = NewUserName;
        }


        else if(!string.IsNullOrEmpty(NewEmail))
        {
            employee.Email = NewEmail;
        }

        await _repository.UpdateAsync(employee);
        await LoadEmployees();

        Debug.WriteLine($"new employee: {employee.UserName}, {employee.Email}");
    }

}

/*
////////
    [ObservableProperty]
    private int passwordLength = 8;

    [ObservableProperty]
    private string generatedPassword = string.Empty;

    [RelayCommand]
    private void GeneratePassword()
    {
        var chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        Random rand = new();
        GeneratedPassword = new string(Enumerable.Repeat(chars, PasswordLength)
                  .Select(s => s[rand.Next(s.Length)]).ToArray());
    }

    /// /////////////////////////////
*/