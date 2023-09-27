﻿using System;
using System.Collections.Generic;

namespace CGI_Models;

public partial class Right
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<RoleRight> RoleRights { get; set; } = new List<RoleRight>();
}
