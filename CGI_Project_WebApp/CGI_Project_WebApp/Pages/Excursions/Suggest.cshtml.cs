using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CGI_Project_WebApp_Core.classes;
using CGI_Project_WebApp_Models;

namespace CGI_Project_WebApp.Pages.Excursions
{
    public class SuggestModel : PageModel
    {
        public SuggestionService SuggestionService = new();
        [BindProperty]
        public required Suggestion Suggestion { get; set; }
        [BindProperty]
		public int EmployeeId { get; set; }
        public void OnGet()
        {
        }

        public void OnPost()
        {
	        if (!ModelState.IsValid)
	        {
		        TempData["fail"] = "Could not add suggestion";
	        }

            if (SuggestionService.TryAddSuggestion(Suggestion.Name, Suggestion.Description, Suggestion.Location,
                    Suggestion.Exception, EmployeeId))
            {
                TempData["success"] = "Suggestion has been added";
                RedirectToPage();
            }
            else
            {
                TempData["fail"] = "Could not add suggestion";
            }
        }
    }
}
