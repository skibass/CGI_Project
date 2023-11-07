using CGI_Project_WebApp_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGI_Project_WebApp_DAL.repositories;

namespace CGI_Project_WebApp_Core.classes
{
    public class DateService
    {
        DateRepository dateRepository = new DateRepository();
       /* public bool DoesDateExistIfNotAddDate(DateTime date)
        {
            date = date.Date;
            if(dateRepository.DoesDateExist(date))
            {
                return true;
            }
            return dateRepository.TryAddDate(date);
            
        }*/

        public bool TryAddDatesOrGetDates(List<DateTime> dates, out List<int> DateIds)
        {
            dates.ForEach(date => { date = date.Date; });
            DateIds = new List<int>();


            foreach (DateTime date in dates) {
                if (dateRepository.DoesDateExist(date, out int? dateId))
                {
                    DateIds.Add((int)dateId);
                }
                else
                {
                    dateRepository.TryAddDate(date, out int? newDateId);
                    DateIds.Add((int)newDateId);
                }
            }
        return true;
        }
        /*public bool TryAddAndGetDates(List<DateTime> InputDates, out List<Date> outputDates)
        {
            
        }*/
    }
}
