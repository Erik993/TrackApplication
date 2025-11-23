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

    public ObservableCollection<ITSupport> ItSupports { get; set; } = new();

    [ObservableProperty]
    private string newUserName = string.Empty;

    [ObservableProperty]
    private string newEmail = string.Empty;

    [ObservableProperty]
    private bool newIsActive = true;

    [ObservableProperty]
    private Role? newSpecialization = null;

    public event Func<string, Task>? ItSupportAdded;
    public event Func<ITSupport, Task>? ItSupportDeleted;
    public event Func<ITSupport, Task>? ItSupportUpdated;

    public ItSupportViewModel(IItSupportRepository repository, ItSupportState state)
    {
        _repository = repository;
        _state = state;

        LoadItSupports();
    }



    //TODO - check if func works, newSpecializtaion is enum
    [RelayCommand]
    public async Task LoadItSupports()
    {
        ItSupports.Clear();

        var list = await _repository.GetAllAsync();
        foreach(var e in list)
        {
            ItSupports.Add(e);
        }

        _state.ItSupports = ItSupports;
    }


    [RelayCommand]
    private async Task AddItSupport()
    {
        if (!string.IsNullOrEmpty(NewUserName) && !string.IsNullOrEmpty(NewEmail) && newSpecialization!=null)
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

