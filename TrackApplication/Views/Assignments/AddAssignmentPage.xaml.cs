using System.Diagnostics;
using TrackApplicationCore.ViewModels;

namespace TrackApplication.Views.Assignments;

public partial class AddAssignmentPage : ContentPage
{
	public AddAssignmentPage(AssignmentViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
		vm.AssignmentAdded += OnAssignmentAdded;
	}


    private async void GoBackToAssignmentPageClicked(object sender, EventArgs e)
    {
        Debug.WriteLine("back to assignment page");
        await Shell.Current.GoToAsync("..");
    }

    private async Task OnAssignmentAdded()
    {
        await DisplayAlertAsync("Success", $"New assignment created!", "OK");
        await Shell.Current.GoToAsync("..");
    }

}