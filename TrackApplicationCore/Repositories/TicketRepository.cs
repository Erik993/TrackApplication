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

    /*
    public async Task AddAsync(Ticket ticket)
    {
        await _context.Tickets.AddAsync(ticket);
        await _context.SaveChangesAsync();
    }*/

    public async Task AddAsync(Ticket ticket)
    {
        try
        {
            //if the employee exists, dont insert it. Line of code from AI
            //_context.Attach(ticket.CreatedBy);

            //find out an employee from the context with the same id
            var employee = await _context.Employees
                .FirstOrDefaultAsync(e => e.UserId == ticket.CreatedById);

            /*
            if (employee == null)
                throw new Exception($"Employee with ID {ticket.CreatedById} not found");
            */
            
            ticket.CreatedBy = employee;

            await _context.Tickets.AddAsync(ticket);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error adding ticket. Title: {ticket.Title}, CreatedById: {ticket.CreatedById}");
            Debug.WriteLine("EXCEPTION: " + ex);
            Debug.WriteLine("INNER: " + ex.InnerException?.Message);
            throw;
        }
    }


    public async Task DeleteAllAsync()
    {
        var allTickets = await _context.Tickets.ToListAsync();
        _context.Tickets.RemoveRange(allTickets);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Ticket ticket)
    {
        _context.Tickets.Remove(ticket);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Ticket>> GetAllAsync() => await _context.Tickets.ToListAsync();


    public async Task<Ticket?> GetByIdAsync(int id) => await _context.Tickets.FindAsync(id);

    public async Task UpdateAsync(Ticket ticket)
    {
        _context.Tickets.Update(ticket);
        await _context.SaveChangesAsync();
    }
}
