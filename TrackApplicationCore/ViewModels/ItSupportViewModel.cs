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

        //clear inputs
        NewUserName = string.Empty;
        NewEmail = string.Empty;
        NewSpecialization = null;
    }


    [RelayCommand]
    private async Task DeleteItSuppport(ITSupport itsupport)
    {
        Debug.WriteLine($"deleting {itsupport.UserName}");
        await _repository.DeleteAsync(itsupport);

        await LoadItSupports();

        if(ItSupportDeleted != null)
        {
            await ItSupportDeleted.Invoke(itsupport);
        }
    }

    [RelayCommand]
    private async Task UpdateItSupport()
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

        //reset edited fields
        EditUserName = string.Empty;
        EditEmail = string.Empty;
        EditSpecialization = null;

        if (ItSupportUpdated != null)
            await ItSupportUpdated.Invoke(SelectedItSupport);
    }


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

