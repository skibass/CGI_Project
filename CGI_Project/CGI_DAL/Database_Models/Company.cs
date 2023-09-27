﻿using System;
using System.Collections.Generic;

namespace CGI_DAL.Database_Models;

public partial class Company
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
