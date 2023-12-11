using CGI_Project_WebApp_Core.classes;
using CGI_Project_WebApp_DAL.repositories;
using CGI_Project_WebApp_Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace CGI_Project_WebApp.Pages.Shared
{
    public class _NavbarModel : PageModel
    {
        EmployeeService employeeService = new(new EmployeeRepository(), new PollRepository());
        public Employee employee = new();
        public void OnGet()
        {
            string UserEmailAddress = User.FindFirst(c => c.Type == ClaimTypes.Email)?.Value;

            if (UserEmailAddress != null)
            {
                if (!employeeService.TryGetEmployeeByEmail(UserEmailAddress, out employee))
                {
                    //mail doesn't exist and/or server error
                }
            }
            else
            {
                //er is geen email/ kan de email niet ophalen
            }
        }
    }
}
