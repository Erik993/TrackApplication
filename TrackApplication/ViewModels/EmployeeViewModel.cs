using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TrackApplication.Interfaces;
using TrackApplicationData.Models;
using System.Diagnostics;
using TrackApplication.States;

namespace TrackApplication.ViewModels;
public class EmployeeViewModel
{
    private readonly IEmployeeRepository _repository;
    private readonly EmployeeState _state;

    private ObservableCollection<Employee> Employees { get; set; } = new();
    
    public EmployeeViewModel(IEmployeeRepository repository, EmployeeState state)
    {
        _repository = repository;
        _state = state;


        //if this is printed when the page loads - DI is injecting
        Debug.WriteLine("VM created with repo: " + repository.GetType().Name);

    }

    public async Task LoadEmployee()
    {
        var list = await _repository.GetAllAsync();

        Employees.Clear();

        foreach (var el in list)
        {
            Employees.Add(el);
        }
    }


}
