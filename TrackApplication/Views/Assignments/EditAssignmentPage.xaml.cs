using System.Diagnostics;
using TrackApplicationCore.ViewModels;
using TrackApplicationData.Models;

namespace TrackApplication.Views.Assignments;

[QueryProperty(nameof(AssignmentId), "assignmentId")]
public partial class EditAssignmentPage : ContentPage
{
    private readonly AssignmentViewModel _vm;
	public int AssignmentId { get; set; }

    public EditAssignmentPage(AssignmentViewModel vm)
	{
		InitializeComponent();
		_vm = vm;
		BindingContext = vm;

		vm.AssignmentUpdated += OnAssignmentUpdated;
	}

    private async void GoBackToAssignmentsShowPageClicked(object sender, EventArgs e)
    {
        Debug.WriteLine("redirecting back to assignments show page");
        await Shell.Current.GoToAsync("..");
    }

    //method from AI - chatgpt
    protected override async void OnAppearing()
    {
        Debug.WriteLine($"Edit Page Appearing, ID = {AssignmentId}");
        base.OnAppearing();
        await _vm.LoadAssignmentForEdit(AssignmentId);
    }

    private async Task OnAssignmentUpdated(Assignment assignment)
    {
        await DisplayAlertAsync("Success", $"Assignment with ID: {assignment.AssignmentId} is updated!", "OK");
        await Shell.Current.GoToAsync("..");
    }


}
