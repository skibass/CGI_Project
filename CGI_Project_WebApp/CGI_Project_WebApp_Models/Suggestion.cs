using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CGI_Project_WebApp_Models;

public partial class Suggestion
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public string? Location { get; set; }

    public string? Description { get; set; }

    public string? Exception { get; set; }

    public int? EmployeeId { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual ICollection<PollSuggestion> PollSuggestions { get; set; } = new List<PollSuggestion>();

    public virtual ICollection<Vote> Votes { get; set; } = new List<Vote>();
}
