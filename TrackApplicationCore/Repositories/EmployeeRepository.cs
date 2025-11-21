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


        /// <summary>
        /// Asynchronously retrieves all employee records from the data store.
        /// </summary>
        /// <remarks>This method executes a database query to fetch all employees. The returned collection
        /// is materialized in memory and is not tracked for changes. This method is thread-safe and can be
        /// awaited.</remarks>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of <see
        /// cref="Employee"/> objects representing all employees. If no employees exist, the collection will be empty.</returns>
        public async Task<IEnumerable<Employee>> GetAllAsync() => await _context.Employees.ToListAsync();


        /// <summary>
        /// Asynchronously retrieves the employee with the specified identifier from the data store.
        /// </summary>
        /// <param name="id">The unique identifier of the employee to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="Employee"/> if
        /// found; otherwise, <see langword="null"/>.</returns>
        public async Task<Employee?> GetByIdAsync(int id) => await _context.Employees.FindAsync(id);


        /// <summary>
        /// Asynchronously adds a new employee to the data store.
        /// </summary>
        /// <remarks>The employee is persisted to the database when the operation completes. This method
        /// does not validate the employee entity; ensure that all required properties are set before calling.</remarks>
        /// <param name="employee">The employee entity to add. Cannot be null.</param>
        /// <returns>A task that represents the asynchronous add operation.</returns>
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
        /*------------------------------------*/

    }
}
