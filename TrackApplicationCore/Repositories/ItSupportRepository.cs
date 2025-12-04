using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using TrackApplicationCore.Interfaces;
using TrackApplicationData.DbContextData;
using TrackApplicationData.Models;

namespace TrackApplicationCore.Repositories;

public class ItSupportRepository : IItSupportRepository
{
    private readonly ApplicationContext _context;

    public ItSupportRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ITSupport>> GetAllAsync()
    {
        Debug.WriteLine(">>> REPOSITORY.GetAllAsync called");
        var list = await _context.ITSupports.ToListAsync();
        /*var list = await _context.ITSupports
            .AsNoTracking()
            .ToListAsync();*/


        Debug.WriteLine(">>> Loaded " + list.Count + " IT support items from DB");
        return list;
    }

    public async Task<ITSupport?> GetByIdAsync(int id) => await _context.ITSupports.FindAsync(id);


    public async Task AddAsync(ITSupport itSupport)
    {
        /*
        await _context.ITSupports.AddAsync(itSupport);
        await _context.SaveChangesAsync();
        */

        Debug.WriteLine("Adding ITSupport: " + itSupport.UserName);
        await _context.ITSupports.AddAsync(itSupport);
        var changes = await _context.SaveChangesAsync();
        Debug.WriteLine("SaveChangesAsync returned: " + changes);

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

    public async Task DeleteAllAsync()
    {
        var allITSupports = await _context.ITSupports.ToListAsync();
        _context.ITSupports.RemoveRange(allITSupports);
        await _context.SaveChangesAsync();
    }

}

