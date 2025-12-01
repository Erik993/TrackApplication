using System;
using System.Collections.Generic;
using System.Text;
using TrackApplicationCore.Interfaces;
using TrackApplicationData.Models;
using Bogus;
using System.Diagnostics;


namespace TrackApplication.Services;


//Bogus library is used to create fake dumy data, instead of manual creation
public class TestUserService : ICreateTestUserService
{

    private readonly IEmployeeRepository _employeeRepo;
    private readonly IItSupportRepository _itsupportRepo;

    public TestUserService(IEmployeeRepository emplrepo, IItSupportRepository itsuprepo)
    {
        _employeeRepo = emplrepo;
        _itsupportRepo = itsuprepo;
    }


    public async Task CreateEmployeeAsync(int count)
    {
        Debug.WriteLine($"Genetating {count} employees");

        var faker = new Faker<Employee>()
            .CustomInstantiator(f => new Employee(
                f.Name.FullName(),
                f.Internet.Email(),
                f.Random.Bool()
            ));

        var items = faker.Generate(count);

        foreach (var e in items)
            await _employeeRepo.AddAsync(e);
    }


    public async Task CreateITSupportAsync(int count)
    {
        Debug.WriteLine($"Genetating {count} it supports");

        var faker = new Faker<ITSupport>()
            .CustomInstantiator(f => new ITSupport(
                f.Name.FullName(),
                f.Internet.Email(),
                f.Random.Bool(),
                f.PickRandom<Role>()
            ));

        var items = faker.Generate(count);

        foreach (var s in items)
            await _itsupportRepo.AddAsync(s);
    }
}

