Comments:


There following projects in solution:
1) TrackApplication - MAUI application with UI (pages)
2) TrackApplication - console app just to run migrations
3) TrackApplicationData - stores Models and context
4) TrackApplicationCore - stores Viewmodels, Interfaces, Repositories, States
5) Testproject - xUnit poject to test viewmodels, repositories, states (not ui)
----


1) Interface - to define what operations exists,  to allow DI
2) Repository that implements interface - Database logic. Scoped lifetime
3) States - Holds data that multiple ViewModels shuld have access to. Singleton.
4) ViewModel - handle UI, repare data for UI, loads data into state. Transcient lifetime




Libraries installed:
1) CommunityToolkit.Mvvm -Version 8.4.0 to use it instead of manual INotifyPropertyChange
2) Microsoft.EntityFrameworkCore.InMemory - for test project
3) Bogus. https://github.com/bchavez/Bogus to populate DB with fake data
4) Microsoft.Extensions.Configuration.Json

How the update functionality works, data flow:
1) ShowPage has button that calls the command, as example:
<Button Text="Edit"
Command="{Binding BindingContext.GoToEditEmployeeCommand, Source={x:Reference ShowEmplPage}}"
CommandParameter="{Binding UserId}" />

2) ViewModel calls the method. it receivs id. then Shell naigates to the edit page, passing id through URL.
    [RelayCommand]
    public async Task GoToEditEmployee(int employeeId)
    {
        await _navigation.GoToAsync($"EditEmployeePage?employeeId={employeeId}");
    }

3) Edit Page receives the id, thansks to Query property.
QueryProperty(nameof(EmployeeId), "employeeId")]
+ property to save id: public int EmployeeId { get; set; }

4) Edit page appears and ViewModel loads particular entity with:
    protected override async void OnAppearing()
    {
        Debug.WriteLine($"Edit Page Appearing, ID = {EmployeeId}");
        base.OnAppearing();
        await _vm.LoadEmployeeForEdit(EmployeeId);
    }

    and

    public async Task LoadEmployeeForEdit(int employeeId)
    {
        SelectedEmployee = await _repository.GetByIdAsync(employeeId);

        if (SelectedEmployee != null)
        {
            EditUserName = SelectedEmployee.UserName;
            EditEmail = SelectedEmployee.Email;
            EditIsActive = SelectedEmployee.IsActive;
        }
    }


5) User changes values
6) user clicks Update button and calls - private async Task UpdateItSupport()