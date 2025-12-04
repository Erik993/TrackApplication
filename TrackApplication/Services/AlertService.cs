using System;
using System.Collections.Generic;
using System.Text;
using TrackApplicationCore.Interfaces;

namespace TrackApplication.Services;

public class AlertService : IAlertService
{
    public async Task ShowAsync(string title, string message, string cancel)
    {
        await Application.Current.MainPage.DisplayAlertAsync(title, message, cancel);
    }
}