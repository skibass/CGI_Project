using System;
using System.Collections.Generic;

namespace CGI_Models;

public partial class Vote
{
    public int Id { get; set; }

    public int? EmployeeId { get; set; }

    public int? SuggestionId { get; set; }

    public DateTime? Date { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual Suggestion? Suggestion { get; set; }
}
