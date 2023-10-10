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
            result = DBContext.Polls.Include(e => e.Manager).Include(e => e.PollSuggestions).ThenInclude(e => e.Suggestion).ThenInclude(s => s.Votes).Where(poll => DateTime.Now > poll.EndTime).ToList();
            return result;
        }
        public List<Poll> GetFuturePolls()
        {
            List<Poll> result = new List<Poll>();
            Dbi511119Context DBContext = new Dbi511119Context();
            result = DBContext.Polls.Include(e => e.Manager).Include(e => e.PollSuggestions).ThenInclude(e => e.Suggestion).Where(poll => DateTime.Now < poll.StartTime).ToList();
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

        public bool TryGetPollVotes(out List<Vote> votes, Poll poll)
        {
            return TryGetPollVotes(out votes, poll.Id);


        }
        public bool TryGetPollVotes(out List<Vote> votes, int pollId)
        {

            votes = new List<Vote>();

            Dbi511119Context DBContext = new Dbi511119Context();
            try
            {
                Poll? poll = DBContext.Polls.Include(e => e.Manager).Include(e => e.PollSuggestions).ThenInclude(e => e.Suggestion).ThenInclude(s => s.Votes).Where(p => p.Id == pollId).FirstOrDefault();

                if (poll == null)
                {
                    return false;
                }
                List<PollSuggestion> Suggestions = poll.PollSuggestions.ToList();
                votes = new List<Vote>();
                foreach (PollSuggestion suggestion in Suggestions)
                {
                    votes.AddRange(suggestion.Suggestion.Votes);
                }
                return true;

            }
            catch (Exception)
            {
                return false;
            }
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
        public bool TryGetValidAndVoteablePolls(out List<Poll> VotablePolls, int employeeId)
        {
            VotablePolls = new List<Poll>();
            try
            {
                PollRepository pollRepository = new PollRepository();

                List<Poll> polls = new List<Poll>();
                polls = pollRepository.GetOpenPolls();

                if (polls != null)
                {
                    foreach (Poll poll in polls)
                    {
                        bool validToVote = true;

                        foreach (Suggestion suggestion in poll.PollSuggestions.Select(ps => ps.Suggestion).ToList())
                        {
                            if (suggestion.Votes.Select(vote => vote.Employee.Id).ToList().Contains(employeeId))
                            {
                                validToVote = false;
                            }
                        }
                        if (validToVote) { VotablePolls.Add(poll); }
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }

        }
        public bool TryGetMaxVoteCount(out int MaxCount, out bool Draw, int pollId)
        {
            MaxCount = -1;
            Draw = false;
            Dbi511119Context DBContext = new Dbi511119Context();
            try
            {
                if (TryGetPoll(out Poll poll, pollId))
                {

                    List<PollSuggestion>? Suggestions = poll.PollSuggestions.ToList();
                    if (Suggestions == null)
                    {
                        return false;
                    }
                    foreach (PollSuggestion suggestion in Suggestions)
                    {
                        if (MaxCount < suggestion.Suggestion.Votes.Count())
                        {
                            MaxCount = suggestion.Suggestion.Votes.Count();
                            Draw = false;
                        }
                        else if (MaxCount == suggestion.Suggestion.Votes.Count())
                        {
                            Draw = true;
                        }
                    }
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
    }
}
