using System;
using System.Collections.Generic;
using System.Text;
using TrackApplicationCore.Interfaces;
using TrackApplicationData.Models;
using TrackApplicationData.DbContextData;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

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
        await _context.Tickets.AddAsync(ticket);
        await _context.SaveChangesAsync();
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
