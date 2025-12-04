using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using TrackApplicationCore.Interfaces;
using TrackApplicationData.DbContextData;
using TrackApplicationData.Models;

namespace TrackApplicationCore.Repositories;

public class TicketRepository : ITicketRepository
{
    private readonly ApplicationContext _context;

    public TicketRepository(ApplicationContext context)
    {
        _context = context;
    }


    public async Task AddAsync(Ticket ticket)
    {
        try
        {
            //find out an employee from the context with the same id
            var employee = await _context.Employees
                .FirstOrDefaultAsync(e => e.UserId == ticket.CreatedById);
            
            ticket.CreatedBy = employee;
            await _context.Tickets.AddAsync(ticket);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error adding ticket. Title: {ticket.Title}, CreatedById: {ticket.CreatedById}");
            throw new ApplicationException($"Cant add new ticket, check connection with database, {ex}");
        }
    }


    public async Task DeleteAllAsync()
    {
        try
        {
            var allTickets = await _context.Tickets.ToListAsync();
            _context.Tickets.RemoveRange(allTickets);
            await _context.SaveChangesAsync();
        }
        catch(Exception ex)
        {
            Debug.WriteLine($"Error when deleting all tickets {ex.Message}");
            throw new ApplicationException($"Cant delete all tickets, check connection with database, {ex}");
        }

    }

    public async Task DeleteAsync(Ticket ticket)
    {
        try
        {
            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();
        }
        catch(Exception ex)
        {
            Debug.WriteLine($"Error when deleting all tickets: {ex.Message}");
            throw new ApplicationException($"Cant delete all tickets, check connection with database, {ex}");
        }

    }

    public async Task<IEnumerable<Ticket>> GetAllAsync()
    {
        try
        {
            return await _context.Tickets.ToListAsync();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error loading tickets {ex.Message}");
            throw new ApplicationException($"Cant load tickets, check connection with database, {ex}");
        }
    }


    public async Task<Ticket?> GetByIdAsync(int id)
    {
        try
        {
            return await _context.Tickets.FindAsync(id);
        }
        catch(Exception ex)
        {
            Debug.WriteLine($"Error loading particular ticket {ex.Message}");
            throw new ApplicationException($"Cant load parcticular ticket, check connection with database, {ex}");
        }
    }


    public async Task UpdateAsync(Ticket ticket)
    {
        try
        {
            _context.Tickets.Update(ticket);
            await _context.SaveChangesAsync();
        }
        catch(Exception ex)
        {
            Debug.WriteLine($"Error when updating ticket {ex.Message}");
            throw new ApplicationException($"Cant update ticket, check connection with database, {ex}");
        }

    }
}
