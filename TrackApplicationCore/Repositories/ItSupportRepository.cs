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
        try
        {
            Debug.WriteLine(">>> REPOSITORY.GetAllAsync called");
            var list = await _context.ITSupports.ToListAsync();
            /*var list = await _context.ITSupports
                .AsNoTracking()
                .ToListAsync();*/


            Debug.WriteLine(">>> Loaded " + list.Count + " IT support items from DB");
            return list;
        }
        catch(Exception ex)
        {
            Debug.WriteLine($"Error loading it supports {ex.Message}");
            throw new ApplicationException($"Cant load it supports, check connection with database, {ex}");
        }


    }

    public async Task<ITSupport?> GetByIdAsync(int id)
    {
        try
        {
            return await _context.ITSupports.FindAsync(id);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"cant get itsupport with id{id}, {ex.Message}");
            throw new ApplicationException($"Cant get particular it support, check connection with database, {ex}");
        }
    }


    public async Task AddAsync(ITSupport itSupport)
    {
        try
        {
            Debug.WriteLine("Adding ITSupport: " + itSupport.UserName);
            await _context.ITSupports.AddAsync(itSupport);
            var changes = await _context.SaveChangesAsync();
            Debug.WriteLine("SaveChangesAsync returned: " + changes);
        }
        catch(Exception ex)
        {
            Debug.WriteLine($"Error when adding new it support {ex.Message}");
            throw new ApplicationException($"Cant add new it support, check connection with database, {ex}");
        }


    }

    public async Task UpdateAsync(ITSupport itSupport)
    {
        try
        {
            _context.ITSupports.Update(itSupport);
            await _context.SaveChangesAsync();
        }
        catch(Exception ex)
        {
            Debug.WriteLine($"Error when updating it support {ex.Message}");
            throw new ApplicationException($"Cant update it support, check connection with database, {ex}");
        }

    }

    public async Task DeleteAsync(ITSupport itSupport)
    {
        try
        {
            _context.ITSupports.Remove(itSupport);
            await _context.SaveChangesAsync();
        }
        catch(Exception ex)
        {
            Debug.WriteLine($"Error when deleting it support {ex.Message}");
            throw new ApplicationException($"Cant delete it support, check connection with database, {ex}");
        }

    }

    public async Task DeleteAllAsync()
    {
        try
        {
            var allITSupports = await _context.ITSupports.ToListAsync();
            _context.ITSupports.RemoveRange(allITSupports);
            await _context.SaveChangesAsync();
        }
        catch(Exception ex)
        {
            Debug.WriteLine($"Error when deleting all itsupport{ex.Message}");
            throw new ApplicationException($"Cant delete all it supports, check connection with database, {ex}");
        }

    }

}

