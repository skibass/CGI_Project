using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CGI_Project_WebApp_Models;

public partial class Company
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
