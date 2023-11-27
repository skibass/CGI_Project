
using CGI_Project_WebApp_Core.Interfaces;
using CGI_Project_WebApp_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGI_Project_WebApp_Core.classes
{
    public class SuggestionService
    {
        ISuggestionRepository suggestionRepository;
        IPollRepository pollRepository;
        IEmployeeRepository employeeRepository;
        public SuggestionService(ISuggestionRepository suggestionRepository, IPollRepository pollRepository, IEmployeeRepository employeeRepository)
        {
            this.suggestionRepository = suggestionRepository;
            this.pollRepository = pollRepository;
            this.employeeRepository = employeeRepository;
        }

        public bool TryGetLatestWinners(out List<SuggestionList> WinningSuggestionsList, int max)
        {
            WinningSuggestionsList = new List<SuggestionList>();

            try
            {

                WinningSuggestionsList = new List<SuggestionList>();

                List<Poll> polls = new List<Poll>();

                polls = pollRepository.GetPastPolls().OrderByDescending(x => x.EndTime).Take(max).ToList();

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
                PollService pollService = new PollService(pollRepository);

                foreach (PollSuggestion item in poll.PollSuggestions)
                {
                    pollService.TryGetMaxVoteCount(out int MaxCount, out bool draw, poll.Id);

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

        public bool TryAddSuggestion(string name, string description, string location, string exception, Employee employee)
        {
            
            if (employeeRepository.TryGetEmployeeByID(employee.Id, out Employee Emp))
            {

                Suggestion suggestion = new Suggestion();
                suggestion.Name = name;
                suggestion.Location = location;
                suggestion.Description = description;
                suggestion.Exception = exception;
                suggestion.EmployeeId = Emp.Id;

                if (suggestionRepository.TryAddSuggestionToDB(suggestion))
                {
                    return true;
                }
            }
            return false;



        }

        public bool TryGetSuggestions(out SuggestionList suggestions)
        {
            return suggestionRepository.TryGetUnusedSuggestions(out suggestions);
        }

    }
}
