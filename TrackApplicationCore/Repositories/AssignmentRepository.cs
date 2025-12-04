using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using TrackApplicationCore.Interfaces;
using TrackApplicationData.DbContextData;
using TrackApplicationData.Models;

namespace TrackApplicationCore.Repositories;

public class AssignmentRepository : IAssignmentRepository
{
    private readonly ApplicationContext _context;

    public AssignmentRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Assignment assignment)
    {
        await _context.Assignments.AddAsync(assignment);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAllAsync()
    {
        var allAssignments = await _context.Assignments.ToListAsync();
        _context.Assignments.RemoveRange(allAssignments);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Assignment assignment)
    {
        _context.Assignments.Remove(assignment);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Assignment>> GetAllAsync() => await _context.Assignments.ToListAsync();

    public async Task<Assignment?> GetByIdAsync(int id) => await _context.Assignments.FindAsync(id);

    public async Task UpdateAsync(Assignment assignment)
    {
        _context.Assignments.Update(assignment);
        await _context.SaveChangesAsync();
    }
}

