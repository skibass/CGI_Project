using System;
using System.Collections.Generic;

namespace CGI_Project_WebApp_Models;

public partial class Date
{
    public int Id { get; set; }

    public DateTime Date1 { get; set; }

    public virtual ICollection<PreferredDate> PreferredDates { get; set; } = new List<PreferredDate>();
}
