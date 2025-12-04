using Bogus;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using TrackApplicationCore.Interfaces;
using TrackApplicationCore.States;
using TrackApplicationData.Models;

namespace TrackApplication.Services;

class TestTicketService : ICreateTestTicketService
{
    //TODO - test ticket creation logic

    private readonly ITicketRepository _repo;
    private readonly EmployeeState _emplState;

    public TestTicketService (ITicketRepository repo, EmployeeState emplState)
    {
        _repo = repo;
        _emplState = emplState;
    }


    public async Task CreateTicketAsync(int count)
    {
        if (_emplState.Employees == null || !_emplState.Employees.Any())
        {
            await Application.Current.MainPage.DisplayAlertAsync("No Employees", "Can't create test tickets, at least 1 employee should exist", "OK");

            Debug.WriteLine("No Employees in state, can't create test ticket");
            return;   
        }

        Debug.WriteLine($"Genetating {count} tickets");
        
        //new test Ticket with random data creation
        var faker = new Faker<Ticket>()
            .CustomInstantiator(f =>
            {
                //chose a random employee from existing employees
                //need to convert ObservableCollection to list, before pick a random element,
                //because Bogus doesnot work with ObservableCollection
                var employee = f.PickRandom(_emplState.Employees.ToList());

                //var employee = _emplState.Employees[0];

                return new Ticket(
                    title: f.Lorem.Sentence(3),
                    description: f.Lorem.Sentence(30),
                    priority: f.PickRandom<PriorityEnum>(),
                    createdBy: employee,
                    status: f.PickRandom<StatusEnum>(),
                    isResolved: false);
            });

        var items = faker.Generate(count);
        
        foreach(var t in items)
        {
            await _repo.AddAsync(t);
        } 
        
    }
}
