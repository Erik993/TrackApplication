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

    //Fields for test data creation
    private readonly ICreateTestUserService _testUserDataService;
    private readonly ICreateTestTicketService _testTicketDataService;
    

    private readonly EmployeeViewModel _empVM;
    private readonly ItSupportViewModel _itVM;
    private readonly TicketViewModel _tcktVM;

    public MainViewModel(IEmployeeRepository employeeRepo,IItSupportRepository itSupportRepo,
        ICreateTestUserService testUserDataService, EmployeeViewModel empVM, ItSupportViewModel itVM,
        TicketViewModel tcktVM, ITicketRepository ticketRepo, ICreateTestTicketService testTicketService)
    {
        _employeeRepo = employeeRepo;
        _itSupportRepo = itSupportRepo;
        _testUserDataService = testUserDataService;
        _testTicketDataService = testTicketService;
        _empVM = empVM;
        _itVM = itVM;
        _ticketRepo = ticketRepo;
        _tcktVM = tcktVM;
    }

    [RelayCommand]
    public async Task GenerateTestData()
    {
        await _testUserDataService.CreateEmployeeAsync(5);
        await _testUserDataService.CreateITSupportAsync(5);

        await _empVM.LoadEmployees();
        await _itVM.LoadItSupports();
    }
    

    [RelayCommand]
    public async Task GenerateTestTickets()
    {
        Debug.WriteLine("generate test tickets executed");
        await _testTicketDataService.CreateTicketAsync(5);

        await _tcktVM.LoadTickets();
    }


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

    [RelayCommand]
    public async Task DeleteAllTickets()
    {
        // Delete from DB
        await _ticketRepo.DeleteAllAsync();

        // Reload UI
        await _tcktVM.LoadTickets();
    }
}