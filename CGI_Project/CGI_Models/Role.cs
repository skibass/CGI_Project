using System;
using System.Collections.Generic;

namespace CGI_Models;

public partial class Role
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<RoleRight> RoleRights { get; set; } = new List<RoleRight>();
}
