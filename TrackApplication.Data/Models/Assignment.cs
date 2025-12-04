using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TrackApplicationData.Models;

public class Assignment
{
    [Key]
    public int AssignmentId {get; set;} 
    public DateTime AssignedAt { get; set; } = DateTime.Now;


    public int ITSupportId { get; set; }

    [ForeignKey(nameof(ITSupportId))]
    public ITSupport ITSupport { get; set; } = null!;


    public int TicketId { get; set; }

    [ForeignKey(nameof(TicketId))]
    public Ticket Ticket { get; set; } = null!;


    [MaxLength(200)]
    public string Comment { get; set; } = string.Empty;


    public Assignment() { }

    public Assignment(ITSupport support, Ticket ticket, string comment = "")
    {
        //AssignedAt = DateTime.Now;
        ITSupport = support;
        ITSupportId = support.UserId;
        Ticket = ticket;
        TicketId = ticket.TicketID;
        Comment = comment;
    }
}
