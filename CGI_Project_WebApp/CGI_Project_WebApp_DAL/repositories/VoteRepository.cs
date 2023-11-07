using CGI_Project_WebApp_DAL.Database_Models;
using CGI_Project_WebApp_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGI_Project_WebApp_DAL.Database_Models;
using Microsoft.EntityFrameworkCore;

namespace CGI_Project_WebApp_DAL.repositories
{
    public class VoteRepository
    {
        public bool TryAddVote(Vote vote)
        {
            Dbi511119Context DBContext = new Dbi511119Context();

            try
            {
                DBContext.Votes.Add(vote);
                DBContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool TryAddDateToVote(int voteId, int dateId)
        {
            Dbi511119Context DBContext = new Dbi511119Context();
            try
            {
                PreferredDate preferredDate = new PreferredDate();

                preferredDate.DateId = dateId;
                preferredDate.VoteId = voteId;

                DBContext.Add(preferredDate);
                DBContext.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        

        public bool TryGetVotedSuggestions(out Vote vote)
        {
            using Dbi511119Context dbContext = new();

            try
            {
                vote = dbContext.Votes.AsNoTracking()
                    .Include(v => v.EmployeeId)
                    .Include(v => v.SuggestionId)
                    .Include(v => v.Date)
                    .FirstOrDefault();

                if (vote == null)
                {
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                vote = null;
                return false;
            }
        }
    }
}
