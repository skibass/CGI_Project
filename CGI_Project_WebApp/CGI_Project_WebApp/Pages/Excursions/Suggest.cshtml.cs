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
            //TODO: Finish Error
	        if (!ModelState.IsValid)
	        {
		        
	        }

            if (SuggestionService.TryAddSuggestion(Suggestion.Name, Suggestion.Description, Suggestion.Location,
                    Suggestion.Exception, EmployeeId))
            {
                
                RedirectToPage();
            }
            else
            {
                
            }
        }
    }
}
