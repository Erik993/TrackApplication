using System.Diagnostics;
using TrackApplicationCore.ViewModels;
using TrackApplicationData.Models;

namespace TrackApplication.Views.ItSupports;

public partial class ShowItSupportsPage : ContentPage
{
	public ShowItSupportsPage(ItSupportViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;

        vm.ItSupportDeleted += OnItSuppportDeleted;

    }

	private async void GoToBackToItSupportPageButtonClicked(object sender, EventArgs e)
    {
        Debug.WriteLine("redirecting back to it support page");
        await Shell.Current.GoToAsync("..");
    }

    private async void GoToEditItSupportPageButtonClicked(object sender, EventArgs e)
    {
        Debug.WriteLine("redirecting to update it support page");
        await Shell.Current.GoToAsync(nameof(EditItSupportPage));
    }

    private Task OnItSuppportDeleted(ITSupport itsupport)
    {
        Debug.WriteLine("clicked delete");
        return DisplayAlertAsync("Deleted", $"IT Support {itsupport.UserName} was deleted", "OK");
    }

}