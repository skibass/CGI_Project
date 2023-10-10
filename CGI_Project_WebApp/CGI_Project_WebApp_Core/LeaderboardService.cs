using CGI_Project_WebApp_DAL;
using CGI_Project_WebApp_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CGI_Project_WebApp_Models.LeaderboardModels;

namespace CGI_Project_WebApp_Core
{
    public class LeaderboardService
    {
        private List<Poll> _dummyPolls;

        public LeaderboardService()
        {
            GenerateDummyData();
        }

        public List<(string Name, int Score)> CalculateLeaderboard(List<Employee> employees)
        {
            return employees.Select(e =>
            {
                int score = CalculateWinsForEmployee(e);
                return (Name: e.Name, Score: score);
            }).OrderByDescending(x => x.Score).ToList();
        }

        private int CalculateWinsForEmployee(Employee employee)
        {
            int winCount = 0;

            // Fetching polls directly from our dummy data
            var allPolls = _dummyPolls.Where(p => p.PollSuggestions.Any(ps => ps.EmployeeId == employee.Id)).ToList();

            foreach (Poll poll in allPolls)
            {
                int maxVotes = poll.PollSuggestions.Max(ps => ps.Votes.Count);
                var employeeSuggestion = poll.PollSuggestions.FirstOrDefault(ps => ps.EmployeeId == employee.Id);
                if (employeeSuggestion != null && employeeSuggestion.Votes.Count == maxVotes)
                {
                    winCount++;
                }
            }

            return winCount;
        }

        private void GenerateDummyData()
        {
            _dummyPolls = new List<LeaderboardModels.Poll>();

            // Using the GetDummyEmployees method to retrieve all employee IDs
            var employeeIds = Employee.GetDummyEmployees().Select(e => e.Id).ToList();

            Random rng = new Random();

            foreach (var empId in employeeIds)
            {
                for (int i = 0; i < 5; i++)
                {
                    var poll = new LeaderboardModels.Poll
                    {
                        Id = empId * 10 + i,
                        PollSuggestions = new List<LeaderboardModels.Suggestion>()
                    };

                    foreach (var emp in employeeIds)
                    {
                        var suggestion = new LeaderboardModels.Suggestion
                        {
                            Id = emp * 100 + i,
                            EmployeeId = emp,
                            Votes = new List<LeaderboardModels.Vote>()
                        };

                        int voteCount = rng.Next(1, 50);
                        for (int j = 0; j < voteCount; j++)
                        {
                            suggestion.Votes.Add(new LeaderboardModels.Vote { Id = j + 1 });
                        }

                        poll.PollSuggestions.Add(suggestion);
                    }
                    _dummyPolls.Add(poll);
                }
            }
        }


    }
}
