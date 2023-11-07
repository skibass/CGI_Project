using CGI_Project_WebApp_Core.classes;
using CGI_Project_WebApp_Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CGI_Project_WebApp.Pages.Excursions
{
    public class VoteModel : PageModel
    {
        public VoteService VoteService = new();

        public int Progress { get; set; }
        public void OnGet()
        {
            //if (VoteService.TryGetVotedSuggestions(out Vote vote))
            //{
            //    Progress = vote.Progress;
            //}
            //else
            //{
            //    Progress = 0;
            //}
        }
    }
}
