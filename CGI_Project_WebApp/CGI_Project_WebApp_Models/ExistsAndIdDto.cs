using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGI_Project_WebApp_Models
{
    public class ExistsAndIdDto
    {
        public ExistsAndIdDto(bool exist, int? id = null)
        {
            this.exists = exist;
            this.id = id;
        }
        public bool exists { get; set; }
        public int? id { get; set; }
    }
}
