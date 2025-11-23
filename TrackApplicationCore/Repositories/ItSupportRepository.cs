using System;
using System.Collections.Generic;
using System.Text;
using TrackApplicationCore.Interfaces;
using TrackApplicationData.DbContextData;
using TrackApplicationData.Models;
using Microsoft.EntityFrameworkCore;

namespace TrackApplicationCore.Repositories;

public class ItSupportRepository : IItSupportRepository
{
    private readonly ApplicationContext _context;

    public ItSupportRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ITSupport>> GetAllAsync() => await _context.ITSupports.ToListAsync();

    public async Task<ITSupport?> GetByIdAsync(int id) => await _context.ITSupports.FindAsync(id);

    public async Task AddAsync(ITSupport itSupport)
    {
        await _context.ITSupports.AddAsync(itSupport);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ITSupport itSupport)
    {
        _context.ITSupports.Update(itSupport);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(ITSupport itSupport)
    {
        _context.ITSupports.Remove(itSupport);
        await _context.SaveChangesAsync();
    }

}

