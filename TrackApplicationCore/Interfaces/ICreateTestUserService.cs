using System;
using System.Collections.Generic;
using System.Text;

namespace TrackApplicationCore.Interfaces;
public interface ICreateTestUserService
{
    Task CreateEmployeeAsync(int count);
    Task CreateITSupportAsync(int count);
}

