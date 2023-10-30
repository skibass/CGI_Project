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

        public EmployeeRepository EmployeeRepository = new();
        public int EmployeeId { get; set; }
        public string EmployeeEmail { get; set; }

        public void OnGet()
        {
        }

        public void OnPost()
        {
            EmployeeEmail = User.FindFirst(c => c.Type == ClaimTypes.Email)?.Value;

            //TODO: Finish Error
            if (!ModelState.IsValid)
	        {
		        
	        }

            if (SuggestionService.TryAddSuggestion(Suggestion.Name, Suggestion.Description, Suggestion.Location,
                    Suggestion.Exception, EmployeeRepository.GetEmployeeIdByEmail(EmployeeEmail)))
            {
                
                RedirectToPage();
            }
            else
            {
                
            }
        }
    }
}
