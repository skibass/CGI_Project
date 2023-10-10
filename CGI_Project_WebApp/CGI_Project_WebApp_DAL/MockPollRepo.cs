using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CGI_Project_WebApp_Models.LeaderboardModels;

namespace CGI_Project_WebApp_DAL
{
    public interface IPollRepository
    {
        bool TryGetAllPollsWithSuggestionFromEmployee(out List<Poll> allPolls, Employee employee);
        bool TryGetMaxVoteCount(out int count, out bool draw, int pollId);
    }

    public class MockPollRepo : IPollRepository
    {
        public List<Poll> Polls { get; private set; } = new List<Poll>();

        public bool TryGetAllPollsWithSuggestionFromEmployee(out List<Poll> allPolls, Employee employee)
        {
            allPolls = Polls.Where(p => p.PollSuggestions.Any(s => s.EmployeeId == employee.Id)).ToList();
            return allPolls.Any();
        }

        public bool TryGetMaxVoteCount(out int count, out bool draw, int pollId)
        {
            var poll = Polls.FirstOrDefault(p => p.Id == pollId);
            if (poll == null)
            {
                count = 0;
                draw = false;
                return false;
            }

            count = poll.PollSuggestions.Max(s => s.Votes.Count); 
            int localCount = count;
            draw = poll.PollSuggestions.Count(s => s.Votes.Count == localCount) > 1;
            return true;
        }
    }
}
