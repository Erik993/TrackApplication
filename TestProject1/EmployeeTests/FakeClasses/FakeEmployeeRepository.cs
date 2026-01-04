using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using TrackApplicationCore.Interfaces;
using TrackApplicationData.Models;


namespace TestProject1.EmployeeTests.FakeClasses;


public class FakeEmployeeRepository : IEmployeeRepository
{
    //list is used instead of context. context is in a real repository
    public List<Employee> Employees { get; } = new();

    public async Task<IEnumerable<Employee>> GetAllAsync()
    {
        return Employees.ToList();
    }


    public async Task<Employee?> GetByIdAsync(int id)
    {
        return Employees.FirstOrDefault(e => e.UserId == id);
    }


    public async Task AddAsync(Employee employee)
    {
        Employees.Add(employee);
    }


    public async Task UpdateAsync(Employee employee)
    {
        Console.WriteLine("update executed");
    }


    public async Task DeleteAsync(Employee employee)
    {
        Employees.Remove(employee);
    }


    public async Task DeleteAllAsync()
    {
        Employees.Clear();
    }

}



//-----Real Class-----
/*
public class EmployeeRepository : IEmployeeRepository
{
    private readonly ApplicationContext _context;

    //constructor accepts Database context and savve it in backing field
    public EmployeeRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Employee>> GetAllAsync()
    {
        try
        {
            return await _context.Employees.ToListAsync();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error loading employees {ex.Message}");
            throw new ApplicationException($"Cant load employees, check connection with database, {ex}");
        }
    }


    public async Task<Employee?> GetByIdAsync(int id)
    {
        try
        {
            return await _context.Employees.FindAsync(id);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"cant get employee with id{id}, {ex.Message}");
            throw new ApplicationException($"Cant get particular employee, check connection with database, {ex}");
        }
    }


    public async Task AddAsync(Employee employee)
    {
        try
        {
            await _context.Employees.AddAsync(employee);
            var changes = await _context.SaveChangesAsync();
            Debug.WriteLine("Changes saved = " + changes);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error when adding new employee {ex.Message}");
            throw new ApplicationException($"Cant add new employee, check connection with database, {ex}");
        }

    }


    public async Task UpdateAsync(Employee employee)
    {
        try
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error when updating employee {ex.Message}");
            throw new ApplicationException($"Cant update employee, check connection with database, {ex}");
        }
    }


    public async Task DeleteAsync(Employee employee)
    {
        try
        {
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error when deleting employee{ex.Message}");
            throw new ApplicationException($"Cant delete employee, check connection with database, {ex}");
        }
    }


    public async Task DeleteAllAsync()
    {
        try
        {
            var allEmployees = await _context.Employees.ToListAsync();
            _context.Employees.RemoveRange(allEmployees);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error when deleting all employees{ex.Message}");
            throw new ApplicationException($"Cant delete all employees, check connection with database, {ex}");
        }


    }
}
*/