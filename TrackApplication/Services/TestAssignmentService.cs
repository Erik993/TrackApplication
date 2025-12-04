using Bogus;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using TrackApplication.Views.Assignments;
using TrackApplicationCore.Interfaces;
using TrackApplicationCore.States;
using TrackApplicationData.Models;

namespace TrackApplication.Services;

public class TestAssignmentService : ICreateTestAssignmentService
{
    private readonly IAssignmentRepository _repo;
    private readonly ItSupportState _itSupState;
    private readonly TicketState _ticketState;

    public TestAssignmentService(IAssignmentRepository repo, ItSupportState itsupstate, TicketState ticketstate)
    {
        _repo = repo;
        _itSupState = itsupstate;
        _ticketState = ticketstate;
    }

    public async Task CreateAssignmentAsync(int count)
    {
        if(_itSupState.ItSupports == null || !_itSupState.ItSupports.Any() ||
            _ticketState.Tickets == null || !_ticketState.Tickets.Any())
        {
            await Application.Current.MainPage.DisplayAlertAsync("No It Support or tickets", "Can't create test assignemnt, at least 1 it support and 1 ticket should exist", "OK");

            Debug.WriteLine("No It supports or tickets in state, can't create test assignment");
            return;
        }

        Debug.WriteLine($"Genetating {count} assignments");

        var faker = new Faker<Assignment>()
            .CustomInstantiator(f =>
            {
                var itsupport = f.PickRandom(_itSupState.ItSupports.ToList());
                var tckt = f.PickRandom(_ticketState.Tickets.ToList());

                return new Assignment(
                    support: itsupport,
                    ticket: tckt,
                    comment: f.Lorem.Sentence(2));
            });

        var items = faker.Generate(count);
        foreach(var a in items)
        {
            await _repo.AddAsync(a);
        }
    }
}