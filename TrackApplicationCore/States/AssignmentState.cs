using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TrackApplicationData.Models;

namespace TrackApplicationCore.States;
public class AssignmentState
{
    public ObservableCollection<Assignment> Assignments { get; set; } = new();
}
