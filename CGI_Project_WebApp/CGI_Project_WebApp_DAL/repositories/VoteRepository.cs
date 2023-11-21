using CGI_Project_WebApp_DAL.Database_Models;
using CGI_Project_WebApp_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGI_Project_WebApp_DAL.Database_Models;
using CGI_Project_WebApp_Core.classes;

namespace CGI_Project_WebApp_DAL.repositories
{
    public class VoteRepository
    {
        public Result<EmptyResult> TryAddVote(Vote vote)
        {
            Dbi511119Context DBContext = new Dbi511119Context();

            try
            {
                DBContext.Votes.Add(vote);
                DBContext.SaveChanges();
                return new Result<EmptyResult>();
            }
            catch (Exception e)
            {
                return new Result<EmptyResult>("VoteRepository->TryAddVote: " + e.Message);
            }
        }
        public Result<EmptyResult> AddDateToVote(int voteId, int dateId)
        {
            Dbi511119Context DBContext = new Dbi511119Context();
            try
            {
                PreferredDate preferredDate = new PreferredDate();

                preferredDate.DateId = dateId;
                preferredDate.VoteId = voteId;

                DBContext.Add(preferredDate);
                DBContext.SaveChanges();

                return new Result<EmptyResult>();
            }
            catch (Exception e)
            {
                return new Result<EmptyResult>("VoteRepository->TryAddDateToVote: " + e.Message);
            }
        }
        
    }
}
