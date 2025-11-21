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




Librarys installed:
1) CommunityToolkit.Mvvm -Version 8.4.0 to use it instead of manual INotifyPropertyChange
2) Microsoft.EntityFrameworkCore.InMemory - for test project h