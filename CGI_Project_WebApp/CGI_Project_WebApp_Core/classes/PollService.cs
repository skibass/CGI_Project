using CGI_Project_WebApp_DAL.repositories;
using CGI_Project_WebApp_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGI_Project_WebApp_Core.classes
{
    public class PollService
    {
        PollRepository pollsRepository = new PollRepository();
        public bool TryGetValidAndVoteablePolls(out List<Poll> VotablePolls, int employeeId)
        {
            VotablePolls = new List<Poll>();
            try
            {
                List<Poll> polls = new List<Poll>();
                
                polls = pollsRepository.GetOpenPolls();

                if (polls != null)
                {
                    foreach (Poll poll in polls)
                    {
                        bool validToVote = true;

                        foreach (Suggestion suggestion in poll.PollSuggestions.Select(ps => ps.Suggestion).ToList())
                        {
                            List<int> tmp = suggestion.Votes.Select(vote => vote.Employee.Id).ToList();

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

        public bool PollContainsUserVote(int pollId, int employeeId, out Vote vote)
        {
            List<Vote> votes = new List<Vote>();
            vote = null;

            if (pollsRepository.TryGetPollByPollID(out Poll poll, pollId))
            {
                List<PollSuggestion> suggestions = poll.PollSuggestions.ToList();
                votes = suggestions
                    .SelectMany(suggestion => suggestion.Suggestion.Votes)
                    .Where(v => v.EmployeeId == employeeId)
                    .ToList();
            }

            if (votes.Any())
            {
                vote = votes.First();
                return true;
            }

            return false;
        }




        public bool TryGetAllPolls(out List<Poll> allPolls)
        {
            allPolls = new List<Poll>();
            try
            {
                List<Poll> polls = pollsRepository.GetOpenPolls();

                if (polls != null)
                {
                    allPolls.AddRange(polls);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }



        

        }    

        //zo kan de mederwerker nogsteeds de huidige stand van de polls zien zelfs als die all heeft gevote
        public bool TryGetValidButNonVoteablePolls(out List<Poll> nonVotablePolls, int employeeId)
        {
            nonVotablePolls = new List<Poll>();
            try
            {
                List<Poll> polls = new List<Poll>();

                polls = pollsRepository.GetOpenPolls();

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
                        if (!validToVote) { nonVotablePolls.Add(poll); }
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
            try
            {
                if (pollsRepository.TryGetPoll(out Poll poll, pollId))
                {

                    List<PollSuggestion>? Suggestions = poll.PollSuggestions.ToList();
                    if (Suggestions == null)
                    {
                        return false;
                    }
                    foreach (PollSuggestion suggestion in Suggestions)
                    {
                        if (MaxCount < suggestion.Suggestion.Votes.Count)
                        {
                            MaxCount = suggestion.Suggestion.Votes.Count;
                            Draw = false;
                        }
                        else if (MaxCount == suggestion.Suggestion.Votes.Count)
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
        public bool TryRemovePoll(int pollId)
        {
            return pollsRepository.TryRemovePoll(pollId);
        }
        public bool TryAddPoll(NewPollDto newpoll)
        {
            try
            {
                pollsRepository.TryAddPoll(newpoll, out Poll poll);
                //add suggestions
                foreach (Suggestion item in newpoll.suggestions)
                {
                   if(!TryAddSuggestionToPoll(poll, item))
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public bool TryGetPollVotes(out List<Vote> votes, int pollId)
        {
            votes = new List<Vote>();
            try
            {
                if (pollsRepository.TryGetPollByPollID(out Poll poll, pollId))
                {
                    List<PollSuggestion> Suggestions = poll.PollSuggestions.ToList();
                    votes = new List<Vote>();
                    foreach (PollSuggestion suggestion in Suggestions)
                    {
                        votes.AddRange(suggestion.Suggestion.Votes);
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

        public bool TryAddSuggestionToPoll(Poll poll, Suggestion suggestion)
        {
            return TryAddSuggestionToPoll(poll.Id, suggestion.Id);
        }
        public bool TryAddSuggestionToPoll(int pollId, int suggestionId)
        {
            return pollsRepository.TryAddSuggestionToPoll(pollId, suggestionId);
        }
        //public bool 
    }
}
