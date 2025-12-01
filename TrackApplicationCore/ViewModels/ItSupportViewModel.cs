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

public partial class ItSupportViewModel :ObservableObject
{
    private readonly IItSupportRepository _repository;
    private readonly ItSupportState _state;

    //property is used in GoToEditEmployee method. without it shell navigation does not works. 
    private readonly INavigationService _navigation;

    //public ObservableCollection<ITSupport> ItSupports { get; set; } = new();

    public ObservableCollection<ITSupport> ItSupports => _state.ItSupports;


    //collection with roles from enum
    public ObservableCollection<Role> Roles { get; } = new();

    [ObservableProperty]
    private string newUserName = string.Empty;

    [ObservableProperty]
    private string newEmail = string.Empty;

    [ObservableProperty]
    private bool newIsActive = true;

    [ObservableProperty]
    private Role? newSpecialization = null;


    /*Fields for Editing the element*/
    [ObservableProperty]
    private Employee? selectedItSupport;

    [ObservableProperty]
    private string editUserName;

    [ObservableProperty]
    private string editEmail;

    [ObservableProperty]
    private bool editIsActive;

    [ObservableProperty]
    private Role? editSpecialization = null;
    /*--------------------*/




    public event Func<string, Task>? ItSupportAdded;
    public event Func<ITSupport, Task>? ItSupportDeleted;
    public event Func<ITSupport, Task>? ItSupportUpdated;

    public ItSupportViewModel(IItSupportRepository repository, ItSupportState state, INavigationService navigation)
    {
        _repository = repository;
        _state = state;
        _navigation = navigation;

        //populate Roles collection with options from enum. enum is in model
        foreach(var role in Enum.GetValues(typeof(Role)).Cast<Role>())
        {
            Roles.Add(role);
        }


        LoadItSupports();
    }



    //TODO - check if func works, newSpecializtaion is enum
    [RelayCommand]
    public async Task LoadItSupports()
    {
        var list = await _repository.GetAllAsync();

        _state.ItSupports.Clear();
        foreach(var e in list)
        {
            _state.ItSupports.Add(e);
        }
    }


    [RelayCommand]
    private async Task AddItSupport()
    {
        Debug.WriteLine("AddItSupportCommand EXECUTED");
        Debug.WriteLine($"Check values: user={NewUserName}, email={NewEmail}, spec={newSpecialization}");
        if (!string.IsNullOrEmpty(NewUserName) && !string.IsNullOrEmpty(NewEmail) && newSpecialization != null)
        {
            var itSupport = new ITSupport(NewUserName, NewEmail, NewIsActive, newSpecialization.Value);
            Debug.WriteLine($"new employee: {itSupport.UserName}, {itSupport.Email}");

            await _repository.AddAsync(itSupport);

            await LoadItSupports();

        }

        if (ItSupportAdded != null)
            await ItSupportAdded.Invoke(NewUserName);

        //clear inputs
        NewUserName = string.Empty;
        NewEmail = string.Empty;
        newSpecialization = null;
    }

}

