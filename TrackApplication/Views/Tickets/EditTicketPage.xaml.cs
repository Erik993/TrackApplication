using System.Diagnostics;
using TrackApplicationCore.ViewModels;
using TrackApplicationData.Models;

namespace TrackApplication.Views.Tickets;


[QueryProperty(nameof(TicketId), "ticketId")]
public partial class EditTicketPage : ContentPage
{
	private readonly TicketViewModel _vm;

	public int TicketId { get; set; }
	public EditTicketPage(TicketViewModel vm)
	{
		InitializeComponent();
        _vm = vm;
		BindingContext = vm;
	}

    private async void GoBackToTicketsShowPageClicked(object sender, EventArgs e)
    {
        Debug.WriteLine("redirecting back to ticket show page");
        await Shell.Current.GoToAsync("..");
    }

    //method from AI - chatgpt

    //TODO: uncomment when LoadTicketForEdit is ready
    /*
    protected override async void OnAppearing()
    {
        Debug.WriteLine($"Edit Page Appearing, ID = {TicketId}");
        base.OnAppearing();
        await _vm.LoadTicketForEdit(TicketId);
    }*/

    private async Task OnTicketUpdated(Ticket ticket)
    {
        await DisplayAlertAsync("Success", $"Ticket {ticket.Title} is updated!", "OK");
        await Shell.Current.GoToAsync("..");
    }


}