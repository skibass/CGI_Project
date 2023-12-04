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

       
    

        public void OnGet()
        {

        }

        public void OnPost()
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
                      
                    
                    RedirectToPage();
                }
                else
                {
                    Console.WriteLine($"Not able to add" + Suggestion.Name);
                    //something went wrong with adding suggestion and/or server error
                }
            }
            else
            {
                //mail doesn't exist and/or server error
            }
            RedirectToPage();
        }
    }
}
