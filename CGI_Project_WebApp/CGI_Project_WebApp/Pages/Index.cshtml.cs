using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CGI_Project_WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

        public IActionResult OnPostLanguage(string lang)
        {
            Response.Cookies.Append(".AspNetCore.Culture", $"c={lang}|uic={lang}", new CookieOptions { Expires = DateTime.Now.AddDays(1) });
            return RedirectToPage("/");
        }
    }
}