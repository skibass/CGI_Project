using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGI_Project_WebApp_Models
{
    public class CreatedAndIdDto
    {
        public CreatedAndIdDto(bool created, int? id = null)
        {
            this.created = created;
            this.id = id;
        }
        public bool created { get; set; }
        public int? id { get; set; }
    }
}
