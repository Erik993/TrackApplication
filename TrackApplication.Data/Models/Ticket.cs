using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TrackApplicationData.Models;

public class Ticket
{
    [Key]
    public int TicketID { get; set; }

    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(2000)]
    public string Description { get; set; } = string.Empty;

    
    public int CreatedById { get; set; }

    [ForeignKey(nameof(CreatedById))]
    public Employee CreatedBy { get; set; } = null!;

    public StatusEnum Status { get; set; }
    public PriorityEnum Priority { get; set; }

    public bool IsResolved { get; set; }

    public Ticket() { }

    public Ticket(string title, string description, PriorityEnum priority, Employee createdBy, StatusEnum status, bool isResolved)
    {
        Title = title;
        Description = description;
        Priority = priority;
        CreatedBy = createdBy;
        Status = status;
        IsResolved = isResolved;
    }
}
public enum StatusEnum
{
    Open,
    InProgress,
    Resolved,
    Closed
}

public enum PriorityEnum
{
    Low,
    Medium,
    High
}