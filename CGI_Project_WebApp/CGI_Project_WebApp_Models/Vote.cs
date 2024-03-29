﻿using System;
using System.Collections.Generic;

namespace CGI_Project_WebApp_Models;

public partial class Vote
{
    public int Id { get; set; }

    public int? EmployeeId { get; set; }

    public int? PollSuggestionId { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual ICollection<PreferredDate> PreferredDates { get; set; } = new List<PreferredDate>();

    public virtual PollSuggestion? PollSuggestion { get; set; }
}
