using CGI_Project_WebApp_DAL.Database_Models;
using CGI_Project_WebApp_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGI_Project_WebApp_DAL.repositories
{
    public class DateRepository
    {
        Dbi511119Context DbContext;

        public bool DoesDateExist(DateTime date)
        {
            date = date.Date;

            return DbContext.Dates.Select(e => e.Date1).Contains(date);
            
        }
        public bool TryAddDate(DateTime date)
        {
            try
            {
                date = date.Date;

                Date date1 = new Date();
                date1.Date1 = date;

                DbContext.Dates.Add(date1);
                DbContext.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
       
    }
}
