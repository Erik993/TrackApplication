using System.Diagnostics;
using TrackApplication.Views.Tickets;
using TrackApplicationCore.ViewModels;
using TrackApplicationData.Models;

namespace TrackApplication.Views.Assignments;

public partial class ShowAssignmentsPage : ContentPage
{
	public ShowAssignmentsPage(AssignmentViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;

		vm.AssignmentDeleted += OnAssignmentDeleted;
	}

    private async void GoToBackToAssignmentPageButtonClicked(object sender, EventArgs e)
    {
        Debug.WriteLine("redirecting back to assignment page");
        await Shell.Current.GoToAsync("..");
    }

    private async void GoToEditAssignmentPageButtonClicked(object sender, EventArgs e)
    {
        Debug.WriteLine("redirecting to update assignment page");
        await Shell.Current.GoToAsync(nameof(EditAssignmentPage));
    }

    private Task OnAssignmentDeleted(Assignment assignment)
    {
        Debug.WriteLine("clicked delete assignment");
        return DisplayAlertAsync("Deleted", $"Assignment with ID: {assignment.AssignmentId} was deleted", "OK");
    }

}
