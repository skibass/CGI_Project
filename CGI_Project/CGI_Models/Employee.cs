using System;
using System.Collections.Generic;

namespace CGI_Models;

public partial class Employee
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? ProfileImage { get; set; }

    public int? CompanyId { get; set; }

    public int? RoleId { get; set; }

    public virtual Company Company { get; set; }

    public virtual ICollection<Poll> Polls { get; set; } = new List<Poll>(); //Creators(mangers)only

    public virtual Role? Role { get; set; }

    public virtual ICollection<Suggestion> Suggestions { get; set; } = new List<Suggestion>();

    public virtual ICollection<Vote> Votes { get; set; } = new List<Vote>();
}
