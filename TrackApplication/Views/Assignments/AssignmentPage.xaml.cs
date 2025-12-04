using System.Diagnostics;
using TrackApplication.Views.Tickets;
using TrackApplicationCore.ViewModels;

namespace TrackApplication.Views.Assignments;

public partial class AssignmentPage : ContentPage
{
	public AssignmentPage(AssignmentViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}


    public async void GoToAddAssignmentButtonClicked(object sender, EventArgs e)
    {
        Debug.WriteLine("redirecting to add assignment page");
        await Shell.Current.GoToAsync(nameof(AddAssignmentPage));
    }

    public async void GoToMainPageButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }

    public async void GoToShowAssignemetnsButtonClicked(object sender, EventArgs e)
    {
        Debug.WriteLine("redirecting to show assignments page");
        await Shell.Current.GoToAsync(nameof(ShowAssignmentsPage));
    }
}