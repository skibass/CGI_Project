﻿using CGI_Project_WebApp_DAL.repositories;
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

        public bool TryRemovePoll(int pollId)
        {
            return pollsRepository.TryRemovePoll(pollId);
        }

        public bool TryAddPoll(Poll poll)
        {
            return pollsRepository.TryAddPoll(poll);
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
    }
}