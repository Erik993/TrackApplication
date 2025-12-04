using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using TrackApplicationCore.Interfaces;

namespace TrackApplicationCore.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly IEmployeeRepository _employeeRepo;
    private readonly IItSupportRepository _itSupportRepo;
    private readonly ITicketRepository _ticketRepo;
    private readonly IAssignmentRepository _assignmentRepo;

    //Fields for test data creation
    private readonly ICreateTestUserService _testUserDataService;
    private readonly ICreateTestTicketService _testTicketDataService;
    private readonly ICreateTestAssignmentService _testAssignmentService;
    

    private readonly EmployeeViewModel _empVM;
    private readonly ItSupportViewModel _itVM;
    private readonly TicketViewModel _tcktVM;
    private readonly AssignmentViewModel _assignmVM;

    public MainViewModel(IEmployeeRepository employeeRepo,IItSupportRepository itSupportRepo,
        ICreateTestUserService testUserDataService, EmployeeViewModel empVM, ItSupportViewModel itVM,
        TicketViewModel tcktVM, ITicketRepository ticketRepo, ICreateTestTicketService testTicketService,
        IAssignmentRepository assignmentRepo, ICreateTestAssignmentService testAssignmentsService, AssignmentViewModel assgmtVM)
    {
        _employeeRepo = employeeRepo;
        _itSupportRepo = itSupportRepo;
        _assignmentRepo = assignmentRepo;
        _testUserDataService = testUserDataService;
        _testTicketDataService = testTicketService;
        _testAssignmentService = testAssignmentsService;
        _empVM = empVM;
        _itVM = itVM;
        _ticketRepo = ticketRepo;
        _tcktVM = tcktVM;
        _assignmVM = assgmtVM;
    }


    /*Create Test users - employee and it support*/
    [RelayCommand]
    public async Task GenerateTestUserData()
    {
        //5 - number of test objects
        await _testUserDataService.CreateEmployeeAsync(5);
        await _testUserDataService.CreateITSupportAsync(5);

        // Reload UI
        await _empVM.LoadEmployees();
        await _itVM.LoadItSupports();
    }
    

    /*Create test tickets*/
    [RelayCommand]
    public async Task GenerateTestTickets()
    {
        Debug.WriteLine("generate test tickets executed");

        //5 - number of test objects
        await _testTicketDataService.CreateTicketAsync(5);

        // Reload UI
        await _tcktVM.LoadTickets();
    }


    /* Delete ALL users from DB*/
    [RelayCommand]
    public async Task DeleteAllEmployeesAndItSupports()
    {
        // Delete from DB
        await _employeeRepo.DeleteAllAsync();
        await _itSupportRepo.DeleteAllAsync();

        // Reload UI
        await _empVM.LoadEmployees();
        await _itVM.LoadItSupports();
    }


    /*Delete ALL tickets from DB*/
    [RelayCommand]
    public async Task DeleteAllTickets()
    {
        // Delete from DB
        await _ticketRepo.DeleteAllAsync();

        // Reload UI
        await _tcktVM.LoadTickets();
    }


    /*Create Test Assignments*/
    [RelayCommand]
    public async Task GenerateTestAssignments()
    {
        Debug.WriteLine("generate test assignments executed");

        //5 - number of test objects
        await _testAssignmentService.CreateAssignmentAsync(5);

        // Reload UI
        await _assignmVM.LoadAssignments();
    }


    /*Delete ALL assignments from DB*/
    [RelayCommand]
    public async Task DeleteAllAssignments()
    {
        // Delete from DB
        await _assignmentRepo.DeleteAllAsync();

        // Reload UI
        await _assignmVM.LoadAssignments();
    }

}