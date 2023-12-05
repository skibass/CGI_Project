using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGI_Project_WebApp_DAL.Database_Models;
using CGI_Project_WebApp_Models;
using Google.Protobuf.WellKnownTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Org.BouncyCastle.Utilities;

namespace CGI_Project_WebApp_DAL.repositories
{
    public class PollRepository
    {

        //this had recently been renamed
        public List<Poll> GetOpenPolls()
        {
            List<Poll> result = new List<Poll>();
            Dbi511119Context DBContext = new Dbi511119Context();
            result = DBContext.Polls.Include(e => e.Manager).Include(e => e.PollSuggestions).ThenInclude(e => e.Suggestion).ThenInclude(s => s.Votes).ThenInclude(v=>v.Employee).Where(poll => poll.StartTime < DateTime.Now && DateTime.Now < poll.EndTime).ToList();
            return result;
        }
        public List<Poll> GetPastPolls()
        {
            List<Poll> result = new List<Poll>();
            Dbi511119Context DBContext = new Dbi511119Context();
            result = DBContext.Polls.Include(e => e.Manager).Include(e => e.PollSuggestions).ThenInclude(e => e.Suggestion).ThenInclude(s => s.Votes).ThenInclude(p=>p.PreferredDates).ThenInclude(d=>d.Date).Where(poll => DateTime.Now > poll.EndTime).ToList();

            return result;
        }
        public List<Poll> GetFuturePolls()
        {
            List<Poll> result = new List<Poll>();
            Dbi511119Context DBContext = new Dbi511119Context();
            result = DBContext.Polls.Include(e => e.Manager).Include(e => e.PollSuggestions).ThenInclude(e => e.Suggestion).Where(poll => DateTime.Now < poll.StartTime).ToList();
            return result;
        }
        public bool TryAddPoll(NewPollDto NewPoll, out Poll poll)
        {
            Dbi511119Context DBContext = new Dbi511119Context();
            poll = new Poll();
            try
            {

                poll.Poll_name = NewPoll.Poll_name;
                poll.StartTime = NewPoll.StartTime;
                poll.EndTime = NewPoll.EndTime;
                poll.ManagerId = NewPoll.ManagerId;
                poll.PeriodId = NewPoll.PeriodId;

                DBContext.Polls.Add(poll);
                DBContext.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool TryRemovePoll(Poll poll)
        {
            return TryRemovePoll(poll.Id);
        }
        public bool TryRemovePoll(int pollId)
        {
            Dbi511119Context DBContext = new Dbi511119Context();
            try
            {
                Poll? poll = DBContext.Polls.Include(e => e.Manager).Include(e => e.PollSuggestions).ThenInclude(e => e.Suggestion).Where(poll => poll.Id == pollId).FirstOrDefault();
                if (poll != null)
                {
                    DBContext.Polls.Remove(poll);
                    DBContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool TryGetPollByPollID(out Poll? poll, int pollId)
        {
            Dbi511119Context DBContext = new Dbi511119Context();
            poll = DBContext.Polls.Include(e => e.Manager).Include(e => e.PollSuggestions).ThenInclude(e => e.Suggestion).ThenInclude(s => s.Votes).Where(p => p.Id == pollId).FirstOrDefault();

            if (poll == null)
            {
                return false;
            }

            return true;
        }
        public bool TryGetPoll(out Poll? poll, int pollId)
        {
            Dbi511119Context DBContext = new Dbi511119Context();
            poll = DBContext.Polls.Include(e => e.Manager).Include(e => e.PollSuggestions).ThenInclude(e => e.Suggestion).ThenInclude(s => s.Votes).Where(p => p.Id == pollId).FirstOrDefault();

            if (poll == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool TryAddSuggestionToPoll(int pollId, int SuggestionId)
        {
            Dbi511119Context DBContext = new Dbi511119Context();
            try
            {
                Poll poll = DBContext.Polls.Where(p => p.Id == pollId).First();
                Suggestion suggestion = DBContext.Suggestions.Where(s => s.Id == SuggestionId).First();
                if(poll != null && suggestion != null)
                {
                    PollSuggestion pollSuggestion = new PollSuggestion
                    {
                        PollId = poll.Id,
                        SuggestionId = suggestion.Id
                    };
                    DBContext.Add(pollSuggestion);
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
    }
}
