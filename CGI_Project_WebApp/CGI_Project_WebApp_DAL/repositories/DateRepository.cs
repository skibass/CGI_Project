using CGI_Project_WebApp_Core.Interfaces;
using CGI_Project_WebApp_DAL.Database_Models;
using CGI_Project_WebApp_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGI_Project_WebApp_DAL.repositories
{
    public class DateRepository : IDateRepository
    {
        Dbi511119Context DbContext= new Dbi511119Context();

        public bool DoesDateExist(DateTime date, out int? DateId)
        {
            DateId = null;
            date = date.Date;
            bool exists = DbContext.Dates.Select(e => e.Date1).Contains(date);

            Date? tmp = DbContext.Dates.Where(e => e.Date1 == date).FirstOrDefault();

            if (tmp!=null)
            {
                DateId = tmp.Id;
            }

            return exists;
            
        }
        public bool TryAddDate(DateTime date, out int? DateId)
        {
            DateId = null;
            try
            {
                date = date.Date;

                Date date1 = new Date();
                date1.Date1 = date;

                DbContext.Dates.Add(date1);
                DbContext.SaveChanges();

                DateId = DbContext.Dates.Where(e => e.Date1 == date).FirstOrDefault().Id;

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
       
    }
}
