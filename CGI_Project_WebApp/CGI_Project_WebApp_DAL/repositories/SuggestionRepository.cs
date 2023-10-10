using CGI_Project_WebApp_Models;
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
        public bool TryGetLatestWinners(out List<SuggestionList> WinningSuggestionsList, int max)
        {
            WinningSuggestionsList = new List<SuggestionList>();

            try
            {
                PollRepository pollRepository = new PollRepository();

                WinningSuggestionsList = new List<SuggestionList>();

                List<Poll> polls = new List<Poll>();

                polls = pollRepository.GetPastRepositories().OrderByDescending(x => x.EndTime).Take(max).ToList();

                foreach (var poll in polls)
                {
                    if (!TryGetWinningSuggestionOutOfPoll(poll, out SuggestionList sl))
                    {
                        return false;
                    }

                    WinningSuggestionsList.Add(sl);
                }
                return true;
            }
            catch (Exception)
            {
                return false;

            }


        }

        public bool TryGetWinningSuggestionOutOfPoll(Poll poll, out SuggestionList Winningsuggestions)
        {
            Winningsuggestions = new SuggestionList();

            try
            {
                PollRepository pollRepository = new PollRepository();

                foreach (PollSuggestion item in poll.PollSuggestions)
                {
                    pollRepository.TryGetMaxVoteCount(out int MaxCount, out bool draw, poll.Id);

                    if (item.Suggestion.Votes.Count == MaxCount)
                    {
                        Winningsuggestions.suggestions.Add(item.Suggestion);
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
