using System.Diagnostics;
using TrackApplicationCore.ViewModels;
using TrackApplicationData.Models;

namespace TrackApplication.Views.ItSupports;

[QueryProperty(nameof(ItSupportId), "itsupportId")]
public partial class EditItSupportPage : ContentPage
{

	private readonly ItSupportViewModel _vm;

	public int ItSupportId { get; set; }

    public EditItSupportPage(ItSupportViewModel vm)
	{
		_vm = vm;
		InitializeComponent();
		BindingContext = vm;

        _vm.ItSupportUpdated += OnItSupportUpdated;
	}

    private async void GoBackToItSupportShowPageClicked(object sender, EventArgs e)
    {
		Debug.WriteLine("redirecting back to it sup show page");
        await Shell.Current.GoToAsync("..");
    }

    //method from AI - chatgpt
    protected override async void OnAppearing()
	{
        Debug.WriteLine($"Edit Page Appearing, ID = {ItSupportId}");
        base.OnAppearing();
        await _vm.LoadItSupportForEdit(ItSupportId);
    }

    private async Task OnItSupportUpdated(ITSupport itsupport)
    {
        await DisplayAlertAsync("Success", $"It Support {itsupport.UserName} is updated!", "OK");
        await Shell.Current.GoToAsync("..");
    }


}