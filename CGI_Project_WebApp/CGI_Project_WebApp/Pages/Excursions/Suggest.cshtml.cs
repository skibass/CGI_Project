using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CGI_Project_WebApp_Core.classes;
using CGI_Project_WebApp_Models;
using CGI_Project_WebApp_DAL.repositories;
using System.Security.Claims;
using Microsoft.Extensions.Localization;

namespace CGI_Project_WebApp.Pages.Excursions
{
    public class SuggestModel : PageModel
    {
        public SuggestionService SuggestionService = new SuggestionService(new SuggestionRepository(), new PollRepository(), new EmployeeRepository());
        [BindProperty]
        public required Suggestion Suggestion { get; set; }

        public EmployeeService EmployeeService = new EmployeeService(new EmployeeRepository(), new PollRepository());

        public int EmployeeId { get; set; }
        public string EmployeeEmail { get; set; }

        public Error ErrorHandeling = new();


        private readonly IStringLocalizer<SuggestModel> _stringLocalizer;
        public string CurrentLanguage { get; private set; }
        public string CountryCode { get; private set; }

        public SuggestModel(IStringLocalizer<SuggestModel> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
        }

        public async Task<IActionResult> OnGet()
        {
            CurrentLanguage = LanguageHelper.GetCurrentLanguage(HttpContext);
            CountryCode = CurrentLanguage;
            ViewData["CurrentLanguage"] = CurrentLanguage;
            ViewData["CountryCode"] = CountryCode;

            if (User.Identity.IsAuthenticated == false)
            {
                return RedirectToPage("../Index");
            }

            if (ErrorHandeling.MessageBool)
            {
                //Wait for half a second before resetting properties
                await Task.Delay(10000);
                ErrorHandeling.ResetErrorHandling();
            }
            return null;
        }

        public async Task<IActionResult> OnPost()
        {
            EmployeeEmail = User.FindFirst(c => c.Type == ClaimTypes.Email)?.Value;

            if (!ModelState.IsValid)
            {
                TempData["Error"] = "ModelState not valid";
                return RedirectToPage();
            }

            if (!ErrorHandeling.MessageBool)
            {
                if (EmployeeService.TryGetEmployeeByEmail(EmployeeEmail, out Employee emp))
                {
                    if (SuggestionService.TryAddSuggestion(Suggestion.Name, Suggestion.Description, Suggestion.Location,
                            Suggestion.Exception, emp))
                    {
                        //Suggestion added successfully
                        TempData["Success"] = "Suggestion added successfully";
                        return RedirectToPage();
                    }
                    //Suggestion not successfully added
                    TempData["Error"] = "Could not add suggestion";
                    return RedirectToPage();
                }
                //E-mail doesn't exist and/or server error
                TempData["Error"] = "User not found and/or server error";
                return RedirectToPage();
            }

            return RedirectToPage();
        }
    }
}
