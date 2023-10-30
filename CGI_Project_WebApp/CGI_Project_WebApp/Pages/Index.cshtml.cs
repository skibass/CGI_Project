using CGI_Project_WebApp_Core.Services;
using CGI_Project_WebApp_DAL.repositories;
using CGI_Project_WebApp_Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Runtime.InteropServices;
using System.Security.Claims;


namespace CGI_Project_WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly PollService _pollService = new();

        public string CurrentLanguage { get; private set; }
        public string CountryCode { get; private set; }

        public List<Suggestion> RecentExcursions { get; private set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
           UseCurrentLanguage();
            RecentExcursions = _pollService.GetRecentExcursions();

            AddUserToDbIfExist();
        }

        public void UseCurrentLanguage()
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

       

        public async Task<IActionResult> OnGetSetLanguage(string lang)
        {
            Response.Cookies.Append(".AspNetCore.Culture", $"c={lang}|uic={lang}", new CookieOptions { Expires = DateTime.Now.AddDays(1) });

            return new JsonResult("successfully changed language");
        }

        private void AddUserToDbIfExist()
        {
            if (User.Identity.Name != null)
            {
                EmployeeRepository emp = new EmployeeRepository();

                Employee employee = new();
                employee.FirstName = User.Identity.Name;
                employee.Email = User.FindFirst(c => c.Type == ClaimTypes.Email)?.Value;

                emp.TryAddEmployee(employee);
            }
        }
    }
}