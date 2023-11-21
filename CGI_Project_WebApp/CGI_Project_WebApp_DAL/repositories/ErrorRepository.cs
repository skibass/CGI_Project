using CGI_Project_WebApp_DAL.Database_Models;
using CGI_Project_WebApp_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGI_Project_WebApp_DAL.repositories
{
    public class ErrorRepository
    {

        //this funtion is an special case to keep as an try method to avoid endless error loops
        public bool TryAddErrorToDB(string message)
        {
            try
            {
                Error error = new Error();
                error.message = message;

                Dbi511119Context context = new Dbi511119Context();

                context.Add(error);
                context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }


        }
    }
}
