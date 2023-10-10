using CGI_Project_WebApp_DAL.Database_Models;
using CGI_Project_WebApp_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGI_Project_WebApp_DAL.Database_Models;

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
        
    }
}
