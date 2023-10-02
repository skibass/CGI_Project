using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGI_DAL.Database_Models;
using CGI_Models;

namespace CGI_DAL.repositories
{
    internal class PollRepository
    {
        public List<Poll> GetOpenRepositories()
        {
            List<Poll> result = new List<Poll>();
            Dbi511119Context DBContext = new Dbi511119Context();
            result = DBContext.Polls.Where(poll => poll.StartTime > DateTime.Now && DateTime.Now < poll.EndTime).ToList();
            return result;
        }
        public List<Poll> GetPastRepositories()
        {
            List<Poll> result = new List<Poll>();
            Dbi511119Context DBContext = new Dbi511119Context();
            result = DBContext.Polls.Where(poll => DateTime.Now > poll.EndTime).ToList();
            return result;
        }
        public List<Poll> GetFutureRepositories()
        {
            List<Poll> result = new List<Poll>();
            Dbi511119Context DBContext = new Dbi511119Context();
            result = DBContext.Polls.Where(poll => DateTime.Now < poll.StartTime).ToList();
            return result;
        }

        public bool TryAddPoll(Poll poll)
        {
            Dbi511119Context DBContext = new Dbi511119Context();
            try
            {
                DBContext.Polls.Add(poll);
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
