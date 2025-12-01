using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Text;
using TrackApplicationCore.Interfaces;

namespace TrackApplicationCore.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly IEmployeeRepository _employeeRepo;
    private readonly IItSupportRepository _itSupportRepo;
    private readonly ICreateTestUserService _testDataService;

    private readonly EmployeeViewModel _empVM;
    private readonly ItSupportViewModel _itVM;

    public MainViewModel(IEmployeeRepository employeeRepo,IItSupportRepository itSupportRepo,
        ICreateTestUserService testDataService, EmployeeViewModel empVM, ItSupportViewModel itVM)
    {
        _employeeRepo = employeeRepo;
        _itSupportRepo = itSupportRepo;
        _testDataService = testDataService;
        _empVM = empVM;
        _itVM = itVM;
    }

    [RelayCommand]
    public async Task GenerateTestData()
    {
        await _testDataService.CreateEmployeeAsync(5);
        await _testDataService.CreateITSupportAsync(5);

        await _empVM.LoadEmployees();
        await _itVM.LoadItSupports();
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
}