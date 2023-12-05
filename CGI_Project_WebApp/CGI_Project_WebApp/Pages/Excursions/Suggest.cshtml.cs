using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CGI_Project_WebApp_Core.classes;
using CGI_Project_WebApp_Models;
using CGI_Project_WebApp_DAL.repositories;
using System.Security.Claims;

namespace CGI_Project_WebApp.Pages.Excursions
{
    public class SuggestModel : PageModel
    {
        public SuggestionService SuggestionService = new();
        [BindProperty]
        public required Suggestion Suggestion { get; set; }

        public EmployeeService EmployeeService = new EmployeeService();

        public int EmployeeId { get; set; }
        public string EmployeeEmail { get; set; }

        public Error ErrorHandeling = new();

        public void OnGet()
        {
            ErrorHandeling.MessageBool = false;
        }

        public async void OnPost()
        {
            EmployeeEmail = User.FindFirst(c => c.Type == ClaimTypes.Email)?.Value;

            if (!ModelState.IsValid)
            {
                //Model state not valid
                ErrorHandeling.HandleError("Model state not correct");
                Console.WriteLine("Error");
                return;
            }

            if (EmployeeService.TryGetEmployeeByEmail(EmployeeEmail, out Employee emp)) {
                if (SuggestionService.TryAddSuggestion(Suggestion.Name, Suggestion.Description, Suggestion.Location,
                        Suggestion.Exception, emp))
                {
                    //Suggestion added successfully
                    ErrorHandeling.HandleSuccess("Suggestion added successfully");
                    return;
                }
                //Suggestion not successfully added
                ErrorHandeling.HandleError("Failed to add suggestion. Suggestion might exist already");
                return;
            }
            //E-mail doesn't exist and/or server error
            ErrorHandeling.HandleError("E-mail does not exist and/or server error");

            //Wait for half a second before resetting properties
            await Task.Delay(500);
            
            //Reset all properties
            ErrorHandeling.ResetErrorHandling();
        }
    }
}
