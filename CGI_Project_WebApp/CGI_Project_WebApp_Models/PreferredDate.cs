using System;
using System.Collections.Generic;

namespace CGI_Project_WebApp_Models;

public partial class PreferredDate
{
    public int Id { get; set; }

    public int VoteId { get; set; }

    public int DateId { get; set; }

    public virtual Date Date { get; set; } = null!;

    public virtual Vote Vote { get; set; } = null!;
}
