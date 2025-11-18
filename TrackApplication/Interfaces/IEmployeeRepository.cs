using System;
using System.Collections.Generic;
using System.Text;
using TrackApplicationData.Models;

namespace TrackApplication.Interfaces;

public interface IEmployeeRepository
{
    Task <IEnumerable<Employee>> GetAllAsync();
    Task<Employee> GetByIdAsync(int id);
    Task AddAsync(Employee employee);
    Task UpdateAsync(Employee employee);
    Task DeleteAsync(Employee employee);
}

