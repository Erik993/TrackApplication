using System;
using System.Collections.Generic;
using System.Text;
using TrackApplicationData.Models;

namespace TrackApplicationCore.Interfaces;
public interface IItSupportRepository
{

    Task<IEnumerable<ITSupport>> GetAllAsync();
    Task<ITSupport> GetByIdAsync(int id);
    Task AddAsync(ITSupport itSupport);
    Task UpdateAsync(ITSupport itSupport);
    Task DeleteAsync(ITSupport itSupport);
    Task DeleteAllAsync();
}



