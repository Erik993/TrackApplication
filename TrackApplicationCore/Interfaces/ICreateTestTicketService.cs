using System;
using System.Collections.Generic;
using System.Text;

namespace TrackApplicationCore.Interfaces;
public interface ICreateTestTicketService
{
    Task CreateTicketAsync(int count);
}
