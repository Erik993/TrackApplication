using System.Diagnostics;
using TrackApplicationCore.ViewModels;

namespace TrackApplication.Views.ItSupports;

public partial class EditItSupportPage : ContentPage
{
	public EditItSupportPage(ItSupportViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

    private async void GoBackToItSupportShowPageClicked(object sender, EventArgs e)
    {
		Debug.WriteLine("redirecting back to it sup show page");
        await Shell.Current.GoToAsync("..");
    }
}