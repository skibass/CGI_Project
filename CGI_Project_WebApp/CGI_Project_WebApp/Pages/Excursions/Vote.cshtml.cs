using System.Security.Claims;
using CGI_Project_WebApp_Core.classes;
using CGI_Project_WebApp_DAL.repositories;
using CGI_Project_WebApp_Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Org.BouncyCastle.Utilities;

namespace CGI_Project_WebApp.Pages.Excursions
{
    public class VoteModel : PageModel
    {

        public VoteService VoteService = new(new VoteRepository(), new DateRepository(), new EmployeeRepository());
        private PollService pollService = new(new PollRepository());
        private EmployeeService employeeService = new(new EmployeeRepository(), new PollRepository());
        public Error ErrorHandeling = new();

        public string EmployeeEmail;

        public Poll poll = new();

        public List<Poll> Polls = new();

        public List<Vote> Votes = new();

        public Dictionary<int, int> VoteCounts = new();
        public Dictionary<int, double> VotePercentages = new();

        public int Progress { get; set; }
        public string CurrentLanguage { get; private set; }
        public string CountryCode { get; private set; }

        public IActionResult OnGet()
        {
            CurrentLanguage = LanguageHelper.GetCurrentLanguage(HttpContext);
            CountryCode = CurrentLanguage;
            ViewData["CurrentLanguage"] = CurrentLanguage;
            ViewData["CountryCode"] = CountryCode;

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
                        DateTime now = DateTime.Now;

                        polls = polls.Where(p => p.StartTime < now && p.EndTime > now).ToList();

                        if(polls.Count > 1) { 
                        //error more then 1 active poll
                        }
                        poll = polls[0];
                        Votes = poll.PollSuggestions.SelectMany(PS=>PS.Votes).ToList();

                        int i = 1;

                        //Polls = polls;

                        /*if (VoteService.TryGetVotedSuggestions(emp.Id, out List<Vote> votes))
                        {
                            
                            Votes = votes;

                            foreach (var poll in Polls)
                            {
                                foreach (var suggestion in poll.PollSuggestions)
                                {
                                    int suggestionId = (int)suggestion.SuggestionId;
                                    int count = Votes.Count(v => v.PollSuggestion.SuggestionId == suggestionId);
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
                            ErrorHandeling.ResetErrorHandling();
                        }*/
                    }
                }
            }
            ErrorHandeling.ResetErrorHandling();
            return Page();
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
                    OnGet();
                    ErrorHandeling.HandleSuccess("VoteSuccess" + " " + emp.FirstName);
                    return Page();
                }
                ErrorHandeling.HandleError("VoteError");
                OnGet();
                return Page();
            }
            ErrorHandeling.HandleError("ModelEmailErrorKey");
            OnGet();
            return Page();
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
