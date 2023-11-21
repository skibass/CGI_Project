using CGI_Project_WebApp_DAL.Database_Models;
using CGI_Project_WebApp_Models;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGI_Project_WebApp_Core.classes;
using Google.Protobuf.Collections;

namespace CGI_Project_WebApp_DAL.repositories
{
    public class DateRepository
    {
        Dbi511119Context DbContext= new Dbi511119Context();

        public Result<ExistsAndIdDto> DoesDateExist(DateTime date)
        {
            try
            {
                int? DateId = null;
                date = date.Date;
                bool exists = DbContext.Dates.Select(e => e.Date1).Contains(date);

                Date? tmp = DbContext.Dates.Where(e => e.Date1 == date).FirstOrDefault();

                if (tmp != null)
                {
                    DateId = tmp.Id;
                }

                return new Result<ExistsAndIdDto>(new ExistsAndIdDto(exists, DateId));
            }
            catch (Exception e)
            {

                return new Result<ExistsAndIdDto>("DateRepository->DoesDateExist: " + e.Message, "er is iets fout gegaan", "something went wrong");
            }
            
            
        }
        public Result<int> AddDate(DateTime date)
        {

            try
            {
                date = date.Date;

                Date date1 = new Date();
                date1.Date1 = date;

                DbContext.Dates.Add(date1);
                DbContext.SaveChanges();

                int DateId = DbContext.Dates.Where(e => e.Date1 == date).FirstOrDefault().Id;

                return new Result<int>(DateId);
            }
            catch (Exception e)
            {
                return new Result<int>("DateRepository->TryAddDate: " + e.Message, "er is iets fout gegaan", "something went wrong");
            }

        }
       
    }
}
