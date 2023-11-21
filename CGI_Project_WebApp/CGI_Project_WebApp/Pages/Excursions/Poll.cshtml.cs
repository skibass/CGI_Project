using CGI_Project_WebApp_Core.classes;
using CGI_Project_WebApp_Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.NetworkInformation;


namespace CGI_Project_WebApp.Pages.Excursions
{
    public class PollModel : PageModel
    {
		public PollService PollService = new();
        
		[BindProperty]
		public required Poll Poll { get; set; }

		private SuggestionList unusedSuggestionsList;

		public SuggestionList UnusedSuggestionsList
		{
			get { return unusedSuggestionsList; }
			set { unusedSuggestionsList = value; }
		}

		public EmployeeService EmployeeService = new EmployeeService();
		
		public SuggestionService SuggestionsService = new SuggestionService();

		public void OnGet()
        {
			SuggestionsService.TryGetSuggestions(out unusedSuggestionsList);
		}

		public void OnPost()
		{
			Poll pollToSave = new Poll();
			pollToSave.Poll_name = Poll.Poll_name;
			
			//TODO: Finish Error
			if (!ModelState.IsValid)
			{
				
			}

			if (PollService.TryAddPoll(pollToSave))
			{
				RedirectToPage();
			}
			else
			{
					
			}
	    }
	}
}
