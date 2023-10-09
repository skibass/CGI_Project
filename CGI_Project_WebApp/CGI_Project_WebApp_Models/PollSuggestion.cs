using System;
using System.Collections.Generic;

namespace CGI_Models;

public partial class PollSuggestion
{
    public int Id { get; set; }

    public int? PollId { get; set; }

    public int? SuggestionId { get; set; }

    public virtual Poll? Poll { get; set; }

    public virtual Suggestion? Suggestion { get; set; }
}
