using System;
using System.Collections.Generic;
using System.Text;
using TrackApplicationCore.Interfaces;

namespace TrackApplication.Services;

public class NavigationService : INavigationService
{
    public Task GoToAsync(string route)
    {
        return Shell.Current.GoToAsync(route);
    }
}

