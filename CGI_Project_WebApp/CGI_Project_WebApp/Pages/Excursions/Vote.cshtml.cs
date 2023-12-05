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

        public VoteService VoteService = new VoteService(new VoteRepository(), new DateRepository(), new EmployeeRepository());
        private PollService pollService = new PollService(new PollRepository());
        private EmployeeService employeeService = new EmployeeService(new EmployeeRepository(), new PollRepository());

        public string EmployeeEmail;

        public List<Poll> Polls = new();

        public List<Vote> Votes = new();

        public Dictionary<int, int> VoteCounts = new();
        public Dictionary<int, double> VotePercentages = new();

        public int Progress { get; set; }
        public IActionResult OnGet()
        {
            if (User.Identity.IsAuthenticated == false)
            {
                return RedirectToPage("../Index");
            }
            else
            {
                EmployeeEmail = User.FindFirst(c => c.Type == ClaimTypes.Email)?.Value;

                if (employeeService.TryGetEmployeeByEmail(EmployeeEmail, out Employee emp))
                {
                    if (pollService.TryGetAllPolls(out List<Poll> polls))
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
            return null;
        }

        public IActionResult OnPostVote(int suggestionId)
        {
            Console.WriteLine("Arrived at vote function");
            EmployeeEmail = User.FindFirst(c => c.Type == ClaimTypes.Email)?.Value;

            if (employeeService.TryGetEmployeeByEmail(EmployeeEmail, out Employee emp))
            {
                Console.WriteLine("Found employee: " + emp.FirstName);

                if (VoteService.TryCreateVote(emp.Id, suggestionId))
                {
                    Console.WriteLine("Successfully created vote: " + suggestionId + " Submitted by " + emp.FirstName);
                    return RedirectToPage();
                }
                Console.WriteLine("Could not add vote");

            }
            Console.WriteLine("Idk bro");


            return RedirectToPage();
        }

        public (bool, Vote) PollContainsUserVote(int pollId)
        {
            if (employeeService.TryGetEmployeeByEmail(EmployeeEmail, out Employee emp))
            {
                if (pollService.PollContainsUserVote(pollId, emp.Id, out Vote vote))
                {
                    return (true, vote);
                }
            }
            return (false, null);


        }
    }
}
