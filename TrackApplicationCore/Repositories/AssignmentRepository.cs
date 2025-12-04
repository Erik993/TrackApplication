using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        try
        {
            //look for a ticket and it support from the context with the same id
            //context exception can occur wihout this check
            var itsupport = await _context.ITSupports
                .FirstOrDefaultAsync(i => i.UserId == assignment.ITSupportId);

            var ticket = await _context.Tickets
                .FirstOrDefaultAsync(t => t.TicketID == assignment.TicketId);

            assignment.ITSupport = itsupport;
            assignment.Ticket = ticket;

            await _context.Assignments.AddAsync(assignment);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error adding assignments {ex.Message}");
            throw new ApplicationException($"Cant add assignment, check connection with database, {ex}");
        }
    }

    public async Task DeleteAllAsync()
    {
        try
        {
            var allAssignments = await _context.Assignments.ToListAsync();
            _context.Assignments.RemoveRange(allAssignments);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error when deleting all assingments{ex.Message}");
            throw new ApplicationException($"Cant delete all assignments, check connection with database, {ex}");
        }
    }

    public async Task DeleteAsync(Assignment assignment)
    {
        try
        {
            _context.Assignments.Remove(assignment);
            await _context.SaveChangesAsync();
        }
        catch(Exception ex)
        {
            Debug.WriteLine($"Error when deleting assignment{ex.Message}");
            throw new ApplicationException($"Cant delete assignment, check connection with database, {ex}");
        }
    }

    public async Task<IEnumerable<Assignment>> GetAllAsync()
    {
        try
        {
            return await _context.Assignments.ToListAsync();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error loading assignments {ex.Message}");
            throw new ApplicationException($"Cant load assignments, check connection with database, {ex}");
        }
    }


    public async Task<Assignment?> GetByIdAsync(int id)
    {
        try
        {
            return await _context.Assignments.FindAsync(id);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"cant get assignment with id{id}, {ex.Message}");
            throw new ApplicationException($"Cant get particular assignment, check connection with database, {ex}");
        }
    }


    public async Task UpdateAsync(Assignment assignment)
    {
        try
        {
            _context.Assignments.Update(assignment);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error when assignment employee {ex.Message}");
            throw new ApplicationException($"Cant update assignment, check connection with database, {ex}");
        }

    }
}

