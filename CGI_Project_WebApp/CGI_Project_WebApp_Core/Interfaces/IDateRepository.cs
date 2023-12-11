using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGI_Project_WebApp_Core.Interfaces
{
    public interface IDateRepository
    {
        public bool DoesDateExist(DateTime date, out int? DateId);
        public bool TryAddDate(DateTime date, out int? DateId);
    }
}
