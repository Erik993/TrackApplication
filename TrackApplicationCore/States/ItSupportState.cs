using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TrackApplicationData.Models;

namespace TrackApplicationCore.States;

public class ItSupportState
{
    public ObservableCollection<ITSupport> ItSupports { get; set; } = new();
}

