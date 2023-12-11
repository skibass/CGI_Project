using System;
using System.Collections.Generic;

namespace CGI_Project_WebApp_Models;

public partial class Period
{
    public int Id { get; set; }

    public DateTime? BeginDate { get; set; }

    public DateTime? EndDate { get; set; }

    public virtual ICollection<Poll>? Polls { get; set; } = new List<Poll>();
}
