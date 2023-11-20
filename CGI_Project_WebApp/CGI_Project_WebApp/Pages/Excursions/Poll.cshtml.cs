using CGI_Project_WebApp_Core.classes;
using CGI_Project_WebApp_Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.NetworkInformation;


namespace CGI_Project_WebApp.Pages.Excursions
{
    public class PollModel : PageModel
    {
        [BindProperty]
		public required Poll Poll { get; set; }

		public void OnGet()
        {
        }
    }
}
