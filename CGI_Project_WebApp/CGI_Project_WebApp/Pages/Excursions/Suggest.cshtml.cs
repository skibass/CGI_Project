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

        public bool MessageBool { get; set; }

        [TempData]
        public string SuccessMessage { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public void OnGet()
        {
            MessageBool = false;
        }

        public async void OnPost()
        {

            TempData["ShowToast"] = true;



            EmployeeEmail = User.FindFirst(c => c.Type == ClaimTypes.Email)?.Value;

            //TODO: Finish Error
            if (!ModelState.IsValid)
	        {
		        Console.WriteLine("Error");
	        }

            if (EmployeeService.TryGetEmployeeByEmail(EmployeeEmail, out Employee emp)) {
                if (SuggestionService.TryAddSuggestion(Suggestion.Name, Suggestion.Description, Suggestion.Location,
                        Suggestion.Exception, emp))
                {
                    SuccessMessage = "Suggestion added successfully";
                }
                else
                {
                    ErrorMessage = "Failed to add suggestion. Suggestion might exist already";
                }
            }
            else
            {
                //mail doesn't exist and/or server error
            }

            MessageBool = true;

            await Task.Delay(500);
            MessageBool = false;
            SuccessMessage = "";
            ErrorMessage = "";
        }
    }
}
