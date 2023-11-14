using CGI_Project_WebApp_DAL.Database_Models;
using CGI_Project_WebApp_Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGI_Project_WebApp_DAL.repositories
{
    public class SuggestionRepository
    {
       public bool TryAddSuggestionToDB(Suggestion newSuggestion)
       {
            Dbi511119Context DBContext = new Dbi511119Context();
            try
            {
                DBContext.Suggestions.Add(newSuggestion);
                DBContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
       }
    }
}
