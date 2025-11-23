using System.Diagnostics;
using TrackApplicationCore.ViewModels;

namespace TrackApplication.Views.ItSupports;

public partial class ItSupportPage : ContentPage
{
	public ItSupportPage(ItSupportViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}


	public async void GoToAddItSupportButtonClicked(object sender, EventArgs e)
	{
        Debug.WriteLine("redirecting to add it support page");
		await Shell.Current.GoToAsync(nameof(AddItSupportPage));
    }

	public async void GoToShowItSupportsButtonClicked(object sender, EventArgs e)
	{
        Debug.WriteLine("redirecting to show it supports page");
        await Shell.Current.GoToAsync(nameof(ShowItSupportsPage));
    }

	public async void GoToMainPageButtonClicked(object sender, EventArgs e)
	{
        Debug.WriteLine("redirecting back to main menu");
        await Shell.Current.GoToAsync("..");
    }
}