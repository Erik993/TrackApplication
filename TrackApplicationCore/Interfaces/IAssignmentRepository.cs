using System;
using System.Collections.Generic;
using System.Text;
using TrackApplicationData.Models;

namespace TrackApplicationCore.Interfaces;
public interface IAssignmentRepository
{
    Task<IEnumerable<Assignment>> GetAllAsync();
    Task<Assignment> GetByIdAsync(int id);
    Task AddAsync(Assignment assignment);
    Task UpdateAsync(Assignment assignment);
    Task DeleteAsync(Assignment assignment);
    Task DeleteAllAsync();
}

