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

    //property is used in GoToEditItSupport method. without it shell navigation does not works. 
    private readonly INavigationService _navigation;

    //to show DB alerts
    private readonly IAlertService _alertService;

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
    private ITSupport? selectedItSupport;

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

    public ItSupportViewModel(IItSupportRepository repository, ItSupportState state, 
        INavigationService navigation, IAlertService alertService)
    {
        _repository = repository;
        _state = state;
        _navigation = navigation;
        _alertService = alertService;

        //populate Roles collection with options from enum. enum is in model
        foreach (var role in Enum.GetValues(typeof(Role)).Cast<Role>())
        {
            Roles.Add(role);
        }

        LoadItSupports();
    }

    
    public async Task LoadItSupports()
    {
        try
        {
            Debug.WriteLine(">>> LoadItSupports START");
            _state.ItSupports.Clear();
            var list = await _repository.GetAllAsync();
            foreach (var e in list)
            {
                _state.ItSupports.Add(e);
            }
            Debug.WriteLine($">>> ItSupportViewModel saved {ItSupports.Count} items into state");
        }
        catch(ApplicationException ex)
        {
            await _alertService.ShowAsync("Error", ex.Message, "ОК");
        }


    }


    [RelayCommand]
    private async Task AddItSupport()
    {
        try
        {
            Debug.WriteLine("AddItSupportCommand EXECUTED");
            Debug.WriteLine($"Check values: user={NewUserName}, email={NewEmail}, spec={NewSpecialization}");
            if (!string.IsNullOrEmpty(NewUserName) && !string.IsNullOrEmpty(NewEmail) && NewSpecialization != null)
            {
                var itSupport = new ITSupport(NewUserName, NewEmail, NewIsActive, NewSpecialization.Value);
                Debug.WriteLine($"new employee: {itSupport.UserName}, {itSupport.Email}");

                await _repository.AddAsync(itSupport);

                await LoadItSupports();

            }

            if (ItSupportAdded != null)
                await ItSupportAdded.Invoke(NewUserName);
        }
        catch(ApplicationException ex)
        {
            await _alertService.ShowAsync("Error", ex.Message, "ОК");
        }

        //clear inputs
        NewUserName = string.Empty;
        NewEmail = string.Empty;
        NewSpecialization = null;
    }


    [RelayCommand]
    private async Task DeleteItSuppport(ITSupport itsupport)
    {
        try
        {
            Debug.WriteLine($"deleting {itsupport.UserName}");
            await _repository.DeleteAsync(itsupport);

            await LoadItSupports();

            if (ItSupportDeleted != null)
            {
                await ItSupportDeleted.Invoke(itsupport);
            }
        }
        catch(ApplicationException ex)
        {
            await _alertService.ShowAsync("Error", ex.Message, "ОК");
        }

    }

    [RelayCommand]
    private async Task UpdateItSupport()
    {
        try
        {
            Debug.Write("update it support command is executed");

            if (SelectedItSupport == null)
                return;

            if (!string.IsNullOrWhiteSpace(EditUserName))
                SelectedItSupport.UserName = EditUserName;

            if (!string.IsNullOrWhiteSpace(EditEmail))
                SelectedItSupport.Email = EditEmail;

            if (EditSpecialization != null)
                SelectedItSupport.Specialization = EditSpecialization.Value;

            SelectedItSupport.IsActive = EditIsActive;

            await _repository.UpdateAsync(SelectedItSupport);

            await LoadItSupports();


            if (ItSupportUpdated != null)
                await ItSupportUpdated.Invoke(SelectedItSupport);
        }
        catch(ApplicationException ex)
        {
            await _alertService.ShowAsync("Error", ex.Message, "ОК");
        }

        //reset edited fields
        EditUserName = string.Empty;
        EditEmail = string.Empty;
        EditSpecialization = null;

    }

    //load elements data to display in edit page existing values
    public async Task LoadItSupportForEdit(int itsupportId)
    {
        SelectedItSupport = await _repository.GetByIdAsync(itsupportId);

        if(SelectedItSupport != null)
        {
            EditUserName = SelectedItSupport.UserName;
            EditEmail = SelectedItSupport.Email;
            EditIsActive = SelectedItSupport.IsActive;
            EditSpecialization = SelectedItSupport.Specialization;
        }
    }

    [RelayCommand]
    public async Task GoToEditItSupport(int itsupportId)
    {
        await _navigation.GoToAsync($"EditItSupportPage?itsupportId={itsupportId}");
    }


}

