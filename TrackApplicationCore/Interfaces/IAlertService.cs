using System;
using System.Collections.Generic;
using System.Text;

namespace TrackApplicationCore.Interfaces;
public interface IAlertService
{
    Task ShowAsync(string title, string message, string cancel);
}

