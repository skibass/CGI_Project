using CGI_Project_WebApp_Core.classes;
using CGI_Project_WebApp_Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.NetworkInformation;
using System.Security.Claims;


namespace CGI_Project_WebApp.Pages.Excursions
{
    public class PollModel : PageModel
    {
		public PollService PollService = new();
        
		[BindProperty]
		public required NewPollDto Poll { get; set; }

		private SuggestionList unusedSuggestionsList;

		public SuggestionList UnusedSuggestionsList
		{
			get { return unusedSuggestionsList; }
			set { unusedSuggestionsList = value; }
		}

		public EmployeeService EmployeeService = new EmployeeService();
		
		public SuggestionService SuggestionsService = new SuggestionService();

		public string EmployeeEmail { get; set; }

		public void OnGet()
        {
			SuggestionsService.TryGetSuggestions(out unusedSuggestionsList);
		}

		public void OnPost()
		{
			EmployeeEmail = User.FindFirst(c => c.Type == ClaimTypes.Email)?.Value;

			//TODO: Finish Error
			if (!ModelState.IsValid)
			{

			}

			if (EmployeeService.TryGetEmployeeByEmail(EmployeeEmail, out Employee emp))
			{
				if (PollService.TryAddPoll(Poll.Poll_name, Poll.ManagerId, Poll.StartTime, Poll.EndTime, Poll.Period, emp))
				{
					RedirectToPage();
				}
				else
				{
					//something went wrong with adding poll and/or server error
				}
			}
			
			//pollToSave.ManagerId
			//pollToSave.StartTime > moet gesplitst worden uit dateRange picker
			//pollToSave.EndTime > moet gesplitst worden uit dateRange picker
			//pollToSave.Period ?
		}
	}
}
