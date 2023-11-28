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
                if (!DBContext.Suggestions.Any(s =>
                        string.Equals(s.Name.Trim(), newSuggestion.Name.Trim(), StringComparison.OrdinalIgnoreCase)))
                {
                    DBContext.Suggestions.Add(newSuggestion);
                    DBContext.SaveChanges();
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
       }

        public bool TryGetUnusedSuggestions(out SuggestionList suggestionList)
        {
            Dbi511119Context DBContext = new Dbi511119Context();
            suggestionList = new SuggestionList();

            try
            {
                suggestionList.suggestions = DBContext.Suggestions.Where(s => DBContext.PollSuggestions.Select(ps => ps.SuggestionId).Contains(s.Id) == false).ToList();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool TryGetSuggestionWithId(out Suggestion suggestion, int id)
        {
            Dbi511119Context DBContext = new Dbi511119Context();
            suggestion = new Suggestion();

            try
            {
                suggestion = DBContext.Suggestions.Include(s => s.Votes).Include(ps=>ps.PollSuggestions).ThenInclude(p=>p.Poll).Where(p => p.Id == id).FirstOrDefault();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
