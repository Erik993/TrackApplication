using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TrackApplicationData.Models;

public class ITSupport : User
{
    [Required]
    public Role Specialization { get; set; }

    public ITSupport() { }

    public ITSupport(string name, string email, bool isactive, Role spec) : base(name, email, isactive)
    {
        Specialization = spec;
    }
}

public enum Role
{
    Network,
    Software,
    Hardware,
    Security,
    HelpDesk
}


