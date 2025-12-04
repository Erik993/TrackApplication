using System;
using System.Collections.Generic;
using System.Text;

namespace TrackApplicationCore.Interfaces;
public interface ICreateTestAssignmentService
{
    Task CreateAssignmentAsync(int count);
}
