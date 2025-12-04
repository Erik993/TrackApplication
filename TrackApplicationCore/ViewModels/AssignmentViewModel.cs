using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using TrackApplicationCore.Interfaces;
using TrackApplicationCore.States;
using TrackApplicationData.Models;

namespace TrackApplicationCore.ViewModels;
public partial class AssignmentViewModel : ObservableObject
{
    private readonly IAssignmentRepository _repository;
    private readonly AssignmentState _state;

    //property is used in GoToEditTicket method. without it shell navigation does not works. 
    private readonly INavigationService _navigation;

    public ObservableCollection<Assignment> Assignments => _state.Assignments;

    public ObservableCollection<ITSupport> ITSupports { get; }
    public ObservableCollection<Ticket> Tickets { get; }


    public AssignmentViewModel(IAssignmentRepository repository, AssignmentState state,
        INavigationService navigation, ItSupportState itSupState, TicketState ticketState)
    {
        _repository = repository;
        _state = state;
        _navigation = navigation;
        ITSupports = itSupState.ItSupports;
        Tickets = ticketState.Tickets;

        LoadAssignments();
    }


    /*fields for adding new element*/
    [ObservableProperty]
    private Assignment? selectedAssignment;

    [ObservableProperty]
    private ITSupport newITSupResponsibleFor = null;

    [ObservableProperty]
    private Ticket newTicket = null;

    [ObservableProperty]
    private string newComment = string.Empty;
    /*----------------------*/

    /*fields for updating element*/
    [ObservableProperty]
    private ITSupport editITSupResponsibleFor = null;

    [ObservableProperty]
    private Ticket editTicket = null;

    [ObservableProperty]
    private string editComment = string.Empty;
    /*--------------------------*/

    public event Func<Task>? AssignmentAdded;
    public event Func<Assignment, Task>? AssignmentDeleted;
    public event Func<Assignment, Task>? AssignmentUpdated;


    [RelayCommand]
    public async Task LoadAssignments()
    {
        var list = await _repository.GetAllAsync();

        _state.Assignments.Clear();
        foreach(var a in list)
        {
            _state.Assignments.Add(a);
        }
    }

    [RelayCommand]
    private async Task AddAssignment()
    {
        Debug.WriteLine("AddAssignmentCommand executed");

        if(NewITSupResponsibleFor != null && NewTicket != null)
        {
            var assignment = new Assignment(NewITSupResponsibleFor, NewTicket, NewComment);
            await _repository.AddAsync(assignment);
            await LoadAssignments();
        }

        if (AssignmentAdded != null) await AssignmentAdded.Invoke();

        NewITSupResponsibleFor = null;
        NewTicket = null;
        NewComment = string.Empty;
    }


    [RelayCommand]
    private async Task DeleteAssignment(Assignment assignment)
    {
        Debug.WriteLine($"deleting assignment with id: {assignment.AssignmentId}");
        await _repository.DeleteAsync(assignment);
        await LoadAssignments();

        if (AssignmentDeleted != null) await AssignmentDeleted.Invoke(assignment);
    }


    [RelayCommand]
    private async Task UpdateAssignment()
    {
        Debug.Write("update assignment command is executed");

        if (SelectedAssignment == null) return;

        if (EditITSupResponsibleFor != null)
            SelectedAssignment.ITSupport = EditITSupResponsibleFor;

        if (EditTicket != null)
            SelectedAssignment.Ticket = EditTicket;

        if (!string.IsNullOrWhiteSpace(EditComment))
            SelectedAssignment.Comment = EditComment;

        await _repository.UpdateAsync(SelectedAssignment);

        await LoadAssignments();


        //clear inputs
        EditITSupResponsibleFor = null;
        EditTicket = null;
        EditComment = string.Empty;

        if (AssignmentUpdated != null)
            await AssignmentUpdated.Invoke(SelectedAssignment);
           
    }

    //load elements data to display in edit page existing values
    public async Task LoadAssignmentForEdit(int assignmentId)
    {
        SelectedAssignment = await _repository.GetByIdAsync(assignmentId);

        if(SelectedAssignment != null)
        {
            EditITSupResponsibleFor = SelectedAssignment.ITSupport;
            EditTicket = SelectedAssignment.Ticket;
            EditComment = SelectedAssignment.Comment;
        }
    }


    [RelayCommand]
    public async Task GoToEditAssignment(int assignmentId)
    {
        await _navigation.GoToAsync($"EditAssignmentPage?assignmentId={assignmentId}");
    }



}

