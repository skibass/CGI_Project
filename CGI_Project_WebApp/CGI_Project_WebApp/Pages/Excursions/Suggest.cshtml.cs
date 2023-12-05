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

            //TODO: Finish Error
            if (!ModelState.IsValid)
            {
                ErrorHandeling.ErrorMessage = "Model state not correct";
		        Console.WriteLine("Error");
	        }

            if (EmployeeService.TryGetEmployeeByEmail(EmployeeEmail, out Employee emp)) {
                if (SuggestionService.TryAddSuggestion(Suggestion.Name, Suggestion.Description, Suggestion.Location,
                        Suggestion.Exception, emp))
                {
                    ErrorHandeling.SuccessMessage = "Suggestion added successfully";
                }
                else
                {
                    ErrorHandeling.ErrorMessage = "Failed to add suggestion. Suggestion might exist already";
                }
            }
            else
            {
                //mail doesn't exist and/or server error
            }

            ErrorHandeling.MessageBool = true;

            await Task.Delay(500);
            ErrorHandeling.MessageBool = false;
            ErrorHandeling.SuccessMessage = "";
            ErrorHandeling.ErrorMessage = "";
        }
    }
}
