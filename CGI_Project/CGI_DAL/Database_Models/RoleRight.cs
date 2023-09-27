using System;
using System.Collections.Generic;

namespace CGI_DAL.Database_Models;

public partial class RoleRight
{
    public int Id { get; set; }

    public int? RoleId { get; set; }

    public int? RightId { get; set; }

    public virtual Right? Role { get; set; }

    public virtual Role? RoleNavigation { get; set; }
}
