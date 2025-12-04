using System.Diagnostics;
using TrackApplicationCore.States;
using TrackApplicationCore.ViewModels;

namespace TrackApplication.Views.ItSupports;

public partial class ItSupportPage : ContentPage
{
	//TODO: check state, before redirect to show page
	private readonly ItSupportState _state;
	public ItSupportPage(ItSupportViewModel vm, ItSupportState state)
	{
		InitializeComponent();
		BindingContext = vm;

		_state = state;
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