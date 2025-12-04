using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using TrackApplicationCore.Interfaces;
using TrackApplicationCore.States;
using TrackApplicationData.Models;

namespace TrackApplicationCore.ViewModels;



public partial class TicketViewModel : ObservableObject
{
    private readonly ITicketRepository _repository;
    private readonly TicketState _state;

    //to show DB alerts
    private readonly IAlertService _alertService;

    //property is used in GoToEditTicket method. without it shell navigation does not works. 
    private readonly INavigationService _navigation;

    public ObservableCollection<Ticket> Tickets => _state.Tickets;

    //observabble collection stores all EmployeeState elements - all existing employees
    public ObservableCollection<Employee> Employees { get; }


    //collection for Statuses
    public ObservableCollection<StatusEnum> Statuses { get; } = new();

    //collection for Priorities
    public ObservableCollection<PriorityEnum> Priorities { get; } = new();

    //elements from employee state is loaded in observable collection
    //Statuses and priorities are populated in constructor
    public TicketViewModel(EmployeeState employeeState, ITicketRepository repository,
        TicketState state, INavigationService navigation, IAlertService alertService)
    {
        Employees = employeeState.Employees;
        _repository = repository;
        _state = state;
        _navigation = navigation;
        _alertService = alertService;

        foreach(var status in Enum.GetValues(typeof(StatusEnum)).Cast<StatusEnum>())
        {
            Statuses.Add(status);
        }

        foreach (var priority in Enum.GetValues(typeof(PriorityEnum)).Cast<PriorityEnum>())
        {
            Priorities.Add(priority);
        }

        LoadTickets();
    }



    [ObservableProperty]
    private string newTitle = string.Empty;

    //only description can be empty in a ticket
    [ObservableProperty]
    private string newDescription = string.Empty;

    [ObservableProperty]
    private StatusEnum? newStatus = null;

    [ObservableProperty]
    private PriorityEnum? newPriority = null;

    [ObservableProperty]
    private Employee? newCreatedBy = null;

    [ObservableProperty]
    private bool newIsResolved = false;



    /*Fields for Editing the element*/
    [ObservableProperty]
    private Ticket? selectedTicket;

    [ObservableProperty]
    private string editTitle = string.Empty;

    [ObservableProperty]
    private string editDescription = string.Empty;

    [ObservableProperty]
    private StatusEnum? editStatus = null;

    [ObservableProperty]
    private PriorityEnum? editPriority = null;

    [ObservableProperty]
    private Employee? selectedCreatedBy = null;

    [ObservableProperty]
    private bool editIsResolved = false;
    /*--------------------*/


    //to able show displayAlerts
    public event Func<string, Task>? TicketAdded;
    public event Func<Ticket, Task>? TicketDeleted;
    public event Func<Ticket, Task>? TicketUpdated;



    [RelayCommand]
    public async Task LoadTickets()
    {
        try
        {
            var list = await _repository.GetAllAsync();
            _state.Tickets.Clear();
            foreach (var t in list)
            {
                _state.Tickets.Add(t);
            }
        }
        catch (ApplicationException ex)
        {
            await _alertService.ShowAsync("Error", ex.Message, "ОК");
        }

    }


    [RelayCommand]
    private async Task AddTicket()
    {
        try
        {
            Debug.WriteLine("AddTicketCommand EXECUTED");
            Debug.WriteLine($"Check values: title={NewTitle}, description={NewDescription}, status={NewStatus}, priority={NewPriority}");

            //only description can be empty in a ticket
            //is resolved ticket by default is false
            if (!string.IsNullOrEmpty(NewTitle) && NewStatus != null && NewPriority
                != null && NewCreatedBy != null)
            {
                var ticket = new Ticket(NewTitle, NewDescription, NewPriority.Value, NewCreatedBy, NewStatus.Value);

                await _repository.AddAsync(ticket);
                await LoadTickets();
            }
            if (TicketAdded != null)
                await TicketAdded.Invoke(NewTitle);
        }
        catch (ApplicationException ex)
        {
            // show message
            await _alertService.ShowAsync("Error", ex.Message, "ОК");
        }

        //clear inputs
        NewTitle = string.Empty;
        NewDescription = string.Empty;
        NewCreatedBy = null;
        NewIsResolved = false;
        NewPriority = null;
        NewStatus = null;
    }


    [RelayCommand]
    private async Task DeleteTicket(Ticket ticket)
    {
        try
        {
            Debug.WriteLine($"deleting {ticket.Title}");
            await _repository.DeleteAsync(ticket);
            await LoadTickets();
            if (TicketDeleted != null)
            {
                await TicketDeleted.Invoke(ticket);
            }
        }
        catch (ApplicationException ex)
        {
            await _alertService.ShowAsync("Error", ex.Message, "ОК");
        }

    }


    [RelayCommand]
    private async Task UpdateTicket()
    {
        try
        {
            Debug.Write("update ticket command is executed");

            if (SelectedTicket == null) return;

            if (!string.IsNullOrWhiteSpace(EditTitle))
                SelectedTicket.Title = EditTitle;

            if (!string.IsNullOrWhiteSpace(EditDescription))
                SelectedTicket.Description = EditDescription;

            if (EditStatus != null)
                SelectedTicket.Status = EditStatus.Value;

            if (EditPriority != null)
                SelectedTicket.Priority = EditPriority.Value;

            SelectedTicket.IsResolved = EditIsResolved;

            await _repository.UpdateAsync(SelectedTicket);

            await LoadTickets();

            if (TicketUpdated != null)
                await TicketUpdated.Invoke(SelectedTicket);
        }
        catch (ApplicationException ex)
        {
            await _alertService.ShowAsync("Error", ex.Message, "ОК");
        }

        //clear inputs
        NewTitle = string.Empty;
        NewDescription = string.Empty;
        NewCreatedBy = null;
        NewIsResolved = false;
        NewPriority = null;
        NewStatus = null;
    }


    //load elements data to display in edit page existing values
    public async Task LoadTicketForEdit(int ticketId)
    {
        SelectedTicket = await _repository.GetByIdAsync(ticketId);

        if(SelectedTicket != null)
        {
            EditTitle = SelectedTicket.Title;
            EditDescription = SelectedTicket.Description;
            SelectedCreatedBy = SelectedTicket.CreatedBy;
            EditIsResolved = SelectedTicket.IsResolved;
            EditPriority = SelectedTicket.Priority;
            EditStatus = SelectedTicket.Status;
        }
    }

    [RelayCommand]
    public async Task GoToEditTicket(int ticketId)
    {
        await _navigation.GoToAsync($"EditTicketPage?ticketId={ticketId}");
    }

}
