using System;
using System.Collections.Generic;

namespace CGI_Project_WebApp_Models;

public partial class Poll
{
    public int Id { get; set; }

    public int? ManagerId { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public int? PeriodId { get; set; }

    public virtual Employee? Manager { get; set; }

    public virtual Period? Period { get; set; }

    public virtual ICollection<PollSuggestion> PollSuggestions { get; set; } = new List<PollSuggestion>();
}
