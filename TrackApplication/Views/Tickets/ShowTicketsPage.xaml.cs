using System.Diagnostics;
using TrackApplication.Views.ItSupports;
using TrackApplicationCore.ViewModels;
using TrackApplicationData.Models;

namespace TrackApplication.Views.Tickets;

public partial class ShowTicketsPage : ContentPage
{
	public ShowTicketsPage(TicketViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;

	}
    private async void GoToBackToTicketPageButtonClicked(object sender, EventArgs e)
    {
        Debug.WriteLine("redirecting back to it support page");
        await Shell.Current.GoToAsync("..");
    }

    private async void GoToEditTicketPageButtonClicked(object sender, EventArgs e)
    {
        Debug.WriteLine("redirecting to update ticket page");
        await Shell.Current.GoToAsync(nameof(EditTicketPage));
    }

    private Task OnTicketDeleted(Ticket ticket)
    {
        Debug.WriteLine("clicked delete ticket");
        return DisplayAlertAsync("Deleted", $"Ticket {ticket.Title} was deleted", "OK");
    }

}