using System.Security.Claims;
using CGI_Project_WebApp_Core.classes;
using CGI_Project_WebApp_DAL.repositories;
using CGI_Project_WebApp_Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CGI_Project_WebApp.Pages.Excursions
{
    public class VoteModel : PageModel
    {
        public VoteService VoteService = new();
        private PollService pollService = new();
        private EmployeeService employeeService = new();

        public string EmployeeEmail;

        public List<Poll> Polls = new();

        public List<Vote> Votes = new();

        public Dictionary<int, int> VoteCounts = new();
        public Dictionary<int, double> VotePercentages = new();

        public int Progress { get; set; }
        public void OnGet()
        {
            EmployeeEmail = User.FindFirst(c => c.Type == ClaimTypes.Email)?.Value;

            if (employeeService.TryGetEmployeeByEmail(EmployeeEmail, out Employee emp))
            {
                if (pollService.TryGetValidAndVoteablePolls(out List<Poll> polls, emp.Id))
                {
                    if (VoteService.TryGetVotedSuggestions(emp.Id, out List<Vote> votes))
                    {
                        Polls = polls;
                        Votes = votes;

                        foreach (var poll in Polls)
                        {
                            foreach (var suggestion in poll.PollSuggestions)
                            {
                                int suggestionId = (int)suggestion.SuggestionId;
                                int count = Votes.Count(v => v.SuggestionId == suggestionId);
                                VoteCounts[suggestionId] = count;
                            }
                        }

                        int totalVotes = VoteCounts.Values.Sum();

                        foreach (var suggestionId in VoteCounts.Keys)
                        {
                            double percentage = totalVotes == 0 ? 0 : (double)VoteCounts[suggestionId] / totalVotes * 100;
                            VotePercentages[suggestionId] = percentage;

                            //Console.WriteLine($"Suggestion ID: {suggestionId}, Percentage: {percentage}, Vote Count: {VoteCounts[suggestionId]}");
                        }

                        RedirectToPage();
                    }
                }
            }
        }

        public IActionResult OnPostSubmitVote([FromBody]int suggestionId)
        {
            Console.WriteLine("Arrived at function");
            return Content("success, " + suggestionId);
        }

        [HttpPost]
         public void TestAjax([FromBody]int testval)
         {
            Console.WriteLine("Arrived at function, received:" + testval);
        }

    }
}
