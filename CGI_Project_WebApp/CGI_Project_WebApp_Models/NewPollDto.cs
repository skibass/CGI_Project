using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGI_Project_WebApp_Models
{
    public class NewPollDto
    {
        public int? ManagerId { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public int? PeriodId { get; set; }
        public string? Poll_name { get; set; }
        public virtual Employee? Manager { get; set; }

        public virtual Period? Period { get; set; }

        public List<Suggestion> suggestions { get; set; }
    }
}
