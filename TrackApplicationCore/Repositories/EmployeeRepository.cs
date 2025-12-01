using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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


        /*----------CRUD from Interface----------*/
        public async Task<IEnumerable<Employee>> GetAllAsync() => await _context.Employees.ToListAsync();

        public async Task<Employee?> GetByIdAsync(int id) => await _context.Employees.FindAsync(id);

        public async Task AddAsync(Employee employee)
        {
            Debug.WriteLine("Context hash: " + _context.GetHashCode());

            await _context.Employees.AddAsync(employee);
            var changes = await _context.SaveChangesAsync();
            Debug.WriteLine("Changes saved = " + changes);
            /*
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            */
        }


        public async Task UpdateAsync(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }


        public async Task DeleteAsync(Employee employee)
        {
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }


        public async Task DeleteAllAsync()
        {
            var allEmployees = await _context.Employees.ToListAsync();
            _context.Employees.RemoveRange(allEmployees);
            await _context.SaveChangesAsync();
        }

    }
}
