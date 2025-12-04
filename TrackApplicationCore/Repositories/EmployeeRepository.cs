using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Text;
using TrackApplicationCore.Interfaces;
using TrackApplicationData.DbContextData;
using TrackApplicationData.Models;

namespace TrackApplicationCore.Repositories
{
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
            catch(Exception ex)
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
            catch(Exception ex)
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
            catch(Exception ex)
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
            catch(Exception ex)
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
            catch(Exception ex)
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
            catch(Exception ex)
            {
                Debug.WriteLine($"Error when deleting all employees{ex.Message}");
                throw new ApplicationException($"Cant delete all employees, check connection with database, {ex}");
            }


        }
    }
}
