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

        public SuggestModel(IStringLocalizer<SuggestModel> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
        }
        public IActionResult OnGet()
        {
            if (User.Identity.IsAuthenticated == false)
            {
               return RedirectToPage("../Index");
            }
            ErrorHandeling.ResetErrorHandling();
            return null;
        }

        public async void OnPost()
        {
            EmployeeEmail = User.FindFirst(c => c.Type == ClaimTypes.Email)?.Value;

            if (!ModelState.IsValid)
            {
                //Model state not valid
                ErrorHandeling.HandleError("ModelErrorKey");
                Console.WriteLine("Error");
                return;
            }

            if (EmployeeService.TryGetEmployeeByEmail(EmployeeEmail, out Employee emp)) {
                if (SuggestionService.TryAddSuggestion(Suggestion.Name, Suggestion.Description, Suggestion.Location,
                        Suggestion.Exception, emp))
                {
                    //Suggestion added successfully
                    ErrorHandeling.HandleSuccess("ModelSuggestKey");
                    return;
                }
                //Suggestion not successfully added
                ErrorHandeling.HandleError("ModelSuggestErrorKey");
                return;
            }
            //E-mail doesn't exist and/or server error
            ErrorHandeling.HandleError("ModelEmailErrorKey");

            //Wait for half a second before resetting properties
            await Task.Delay(500);
            
            //Reset all properties
            ErrorHandeling.ResetErrorHandling();
        }
    }
}
