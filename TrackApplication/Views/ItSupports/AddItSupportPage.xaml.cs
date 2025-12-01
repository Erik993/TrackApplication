using System.Diagnostics;
using TrackApplicationCore.ViewModels;

namespace TrackApplication.Views.ItSupports;

public partial class AddItSupportPage : ContentPage
{
	public AddItSupportPage(ItSupportViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
        vm.ItSupportAdded += OnItSupportAdded;

        //for debugging
        Debug.WriteLine("BindingContext = " + BindingContext?.GetType().Name);
    }

	private async void GoBackToItSupportPageClicked(object sender, EventArgs e)
	{
        Debug.WriteLine("back to it support page");
		await Shell.Current.GoToAsync("..");
    }

    private async Task OnItSupportAdded(string name)
    {
        await DisplayAlertAsync("Success", $"ItSupport {name} added!", "OK");
        await Shell.Current.GoToAsync("..");
    }

}