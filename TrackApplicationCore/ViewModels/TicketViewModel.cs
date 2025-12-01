using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TrackApplicationCore.States;
using TrackApplicationData.Models;

namespace TrackApplicationCore.ViewModels;

public class TicketViewModel
{
    public ObservableCollection<Employee> Employees { get; }

    public TicketViewModel(EmployeeState employeeState)
    {
        Employees = employeeState.Employees;
    }
}
