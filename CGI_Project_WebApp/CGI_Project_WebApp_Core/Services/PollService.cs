using CGI_Project_WebApp_DAL.repositories;
using CGI_Project_WebApp_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGI_Project_WebApp_Core.Services
{
    public class PollService
    {
        private readonly PollRepository _pollRepository = new();

        public List<Suggestion> GetRecentExcursions()
        {
            List<Poll> polls = _pollRepository.GetPastPolls();
            List<Suggestion> suggestions = new();

            foreach (var poll in polls)
            {
            
                var mostVotedSuggestion = poll.PollSuggestions
                    .Select(ps => ps.Suggestion)
                    .OrderByDescending(s => s.Votes.Count)
                    .FirstOrDefault();

                suggestions.Add(mostVotedSuggestion);
            }

            return suggestions;
        }
    }

}
