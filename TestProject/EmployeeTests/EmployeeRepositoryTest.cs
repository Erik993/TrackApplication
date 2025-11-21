using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrackApplicationCore.Repositories;
using TrackApplicationData.DbContextData;
using TrackApplicationData.Models;
using Xunit;

//dotnet test


namespace TestProject.EmployeeTests;

public class EmployeeRepositoryTest
{
    private ApplicationContext GetInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new ApplicationContext(options);
    }

    [Fact]
    public async Task CanGetAllAsync()
    {
        var context = GetInMemoryContext();
        context.Employees.Add(new Employee{ UserName = "Mark", Email = "mark@gmail.com", IsActive = true });
        context.Employees.Add(new Employee { UserName = "M", Email = "ma@gmail.com", IsActive = true });
        await context.SaveChangesAsync();

        var repo = new EmployeeRepository(context);

        int expected = 2;
        var actual = await repo.GetAllAsync();
        Assert.Equal(expected, actual.Count());
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenNotFound()
    {
        // Arrange
        var repo = new EmployeeRepository(GetInMemoryContext());

        // Act
        var result = await repo.GetByIdAsync(999);

        // Assert
        Assert.Null(result);
    }



    [Fact]
    public async Task AddAsync_AddsNewEmployee()
    {
        // Arrange
        var context = GetInMemoryContext();
        var repo = new EmployeeRepository(context);

        var employee = new Employee { UserName = "Mark", Email = "mark@gmail.com", IsActive = true };

        // Act
        await repo.AddAsync(employee);

        // Assert
        Assert.Single(context.Employees);
        Assert.Equal("New", context.Employees.First().UserName);
    }


    [Fact]
    public async Task UpdateAsync_UpdatesEmployee()
    {
        // Arrange
        var context = GetInMemoryContext();
        var employee = new Employee { UserName = "Mark", Email = "mark@gmail.com", IsActive = true };
        context.Employees.Add(employee);
        await context.SaveChangesAsync();

        var repo = new EmployeeRepository(context);

        // Act
        employee.UserName = "Updated";
        await repo.UpdateAsync(employee);

        // Assert
        var updated = context.Employees.First();
        Assert.Equal("Updated", updated.UserName);
    }


    [Fact]
    public async Task DeleteAsync_RemovesEmployee()
    {
        // Arrange
        var context = GetInMemoryContext();
        var employee = new Employee { UserName = "Mark", Email = "mark@gmail.com", IsActive = true };
        context.Employees.Add(employee);
        await context.SaveChangesAsync();

        var repo = new EmployeeRepository(context);

        // Act
        await repo.DeleteAsync(employee);

        // Assert
        Assert.Empty(context.Employees);
    }
}



