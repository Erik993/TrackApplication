using System;
using System.Collections.Generic;
using System.Text;

namespace TrackApplicationCore.Interfaces;

public interface INavigationService
{
    Task GoToAsync(string route);
}
