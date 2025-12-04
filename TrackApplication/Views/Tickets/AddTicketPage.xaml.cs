using System.Diagnostics;
using TrackApplicationCore.ViewModels;

namespace TrackApplication.Views.Tickets;

using System.Collections.ObjectModel;
using TrackApplicationCore.States;
using TrackApplicationData.Models;

public partial class AddTicketPage : ContentPage
{
    //public ObservableCollection<Employee> Employees { get; }
    public AddTicketPage(TicketViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
        vm.TicketAdded += OnTicketAdded;

    }


    private async void GoBackToTicketPageClicked(object sender, EventArgs e)
    {
        Debug.WriteLine("back to ticket page");
        await Shell.Current.GoToAsync("..");
    }

    private async Task OnTicketAdded(string ticketTitle)
    {
        await DisplayAlertAsync("Success", $"Ticket {ticketTitle} added!", "OK");
        await Shell.Current.GoToAsync("..");
    }

}