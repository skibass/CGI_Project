using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Runtime.InteropServices;

namespace CGI_Project_WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public string CurrentLanguage { get; private set; }
        public string CountryCode { get; private set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
             CurrentLanguage = Request.Cookies[".AspNetCore.Culture"] ?? "EN";
            var parts = CurrentLanguage.Split('|');
            if (parts.Length >= 2)
            {
                CountryCode = parts[0].Substring(2); 
            }

            ViewData["CurrentLanguage"] = CurrentLanguage;
            ViewData["CountryCode"] = CountryCode;

        }

        public IActionResult OnGetTest()
        {
            return new JsonResult("test response");
        }

        public async Task<IActionResult> OnGetSetLanguage(string lang)
        {
            Response.Cookies.Append(".AspNetCore.Culture", $"c={lang}|uic={lang}", new CookieOptions { Expires = DateTime.Now.AddDays(1) });

            return new JsonResult("successfully changed language");
        }

       
    }
}