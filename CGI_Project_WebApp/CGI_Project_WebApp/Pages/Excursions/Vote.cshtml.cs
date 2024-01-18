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

        public Poll poll;

        public List<Poll> Polls = new();

        public List<Vote> Votes = new();

        public Dictionary<int, int> VoteCounts = new();
        public Dictionary<int, double> VotePercentages = new();
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
            EmployeeEmail = User.FindFirst(c => c.Type == ClaimTypes.Email)?.Value;

            if (employeeService.TryGetEmployeeByEmail(EmployeeEmail, out Employee emp))
            {
                if (pollService.TryGetAllPolls(out List<Poll> polls) && polls.Count > 0)
                {
                    DateTime now = DateTime.Now;

                    polls = polls.Where(p => p.StartTime < now && p.EndTime > now).ToList();

                    poll = polls[0];
                    Votes = poll.PollSuggestions.SelectMany(PS => PS.Votes).ToList();
                }
            }
            return Page();
        }

        public async Task<IActionResult> OnPostVote(int suggestionId)
        {
            EmployeeEmail = User.FindFirst(c => c.Type == ClaimTypes.Email)?.Value;

            if (employeeService.TryGetEmployeeByEmail(EmployeeEmail, out Employee emp))
            {
                if (VoteService.TryCreateVote(emp.Id, suggestionId))
                {
                    TempData["Success"] = "VoteSuccess";
                    return RedirectToPage();
                }
                TempData["Error"] = "VoteError";
                return RedirectToPage();
            }
            TempData["Error"] = "VoteNoEmployee";
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
