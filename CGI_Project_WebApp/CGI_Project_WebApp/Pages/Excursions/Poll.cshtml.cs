using CGI_Project_WebApp_Core.classes;
using CGI_Project_WebApp_DAL.repositories;
using CGI_Project_WebApp_Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.NetworkInformation;
using System.Security.Claims;


namespace CGI_Project_WebApp.Pages.Excursions
{
    public class PollModel : PageModel
    {
        public PollService PollService = new(new PollRepository());

        [BindProperty]
        public required NewPollDto Poll { get; set; }

        public SuggestionList unusedSuggestionsList;
        [BindProperty]
        public List<int> chosenSuggestions { get; set; }

        public SuggestionList UnusedSuggestionsList
        {
            get { return unusedSuggestionsList; }
            set { unusedSuggestionsList = value; }
        }

        public EmployeeService EmployeeService = new EmployeeService(new EmployeeRepository(), new PollRepository());

        public SuggestionService SuggestionsService = new SuggestionService(new SuggestionRepository(), new PollRepository(), new EmployeeRepository());

        public string EmployeeEmail { get; set; }
        public string CurrentLanguage { get; private set; }
        public string CountryCode { get; private set; }

        public string? ErrorMessage { get; private set; } = null;
        public void OnGet()
        {
            CurrentLanguage = LanguageHelper.GetCurrentLanguage(HttpContext);
            CountryCode = CurrentLanguage;
            ViewData["CurrentLanguage"] = CurrentLanguage;
            ViewData["CountryCode"] = CountryCode;

            chosenSuggestions = new List<int>();
            SuggestionsService.TryGetSuggestions(out unusedSuggestionsList);
        }

        public async Task<IActionResult> OnPost()
        {
            SuggestionsService.TryGetSuggestions(out unusedSuggestionsList);

            var l = chosenSuggestions;

            EmployeeEmail = User.FindFirst(c => c.Type == ClaimTypes.Email)?.Value;
            
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "ErrorInvalidFormSubmission";
                return RedirectToPage();
            }

            if (EmployeeService.TryGetEmployeeByEmail(EmployeeEmail, out Employee emp))
            {
                if (chosenSuggestions.Count < 2)
                {
                    TempData["Error"] = "You need at least 2 suggestions";
                    return RedirectToPage();
                }
                if (Poll.StartTime == null || Poll.EndTime == null || chosenSuggestions == null || Poll.Poll_name == null)
                {
                    TempData["Error"] = "ErrorAllFieldsRequired";
                    return RedirectToPage();
                }

                if (!PollService.TryCheckIsPollDateValid((DateTime)Poll.StartTime, (DateTime)Poll.EndTime, out bool timeAvailable) || !timeAvailable)
                {
                    TempData["Error"] = "ErrorPollPeriodConflict";
                    return RedirectToPage();
                }

                if (PollService.TryAddPoll(Poll.Poll_name, emp.Id, Poll.StartTime, Poll.EndTime, Poll.Period, emp, chosenSuggestions, unusedSuggestionsList.suggestions))
                {
                    TempData["Success"] = "SuccessPollCreated";
                    return RedirectToPage();
                }

                TempData["Error"] = "ErrorUserNotFoundOrServerError";
                return RedirectToPage();
            }
            return RedirectToPage("/Index");
        }
    }
}
