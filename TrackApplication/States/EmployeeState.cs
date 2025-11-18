using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TrackApplicationData.Models;

namespace TrackApplication.States;

/*
 States - global storage for elements, singleton
 
 */
public class EmployeeState
{
    public ObservableCollection<Employee> Employees { get; set; } = new();
}

