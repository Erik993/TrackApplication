using System.Diagnostics;
using TrackApplication.Views.Employees;
using TrackApplicationCore.ViewModels;

namespace TrackApplication.Views.Tickets;

public partial class TicketPage : ContentPage
{
	public TicketPage(TicketViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

    public async void GoToAddTicketButtonClicked(object sender, EventArgs e)
    {
        Debug.WriteLine("redirecting to add ticket page");
        await Shell.Current.GoToAsync(nameof(AddTicketPage));
    }

    public async void GoToMainPageButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }

    public async void GoToShowTicketsButtonClicked(object sender, EventArgs e)
    {
        Debug.WriteLine("redirecting to show ticket page");
        await Shell.Current.GoToAsync(nameof(ShowTicketsPage));
    }
}