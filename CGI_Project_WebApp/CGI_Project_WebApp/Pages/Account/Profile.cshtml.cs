using CGI_Project_WebApp_Core.classes;
using CGI_Project_WebApp_DAL.repositories;
using CGI_Project_WebApp_Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace acme.Pages;

public class ProfileModel : PageModel
{
    EmployeeService employeeService = new EmployeeService();
    public Employee employee = new();

    public string UserName { get; set; }
    public string UserEmailAddress { get; set; }
    public string UserProfileImage { get; set; }

    private string FormatName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return string.Empty;
        }

        // Capitalize the first letter of the username and make the rest lowercase
        return name.Substring(0, 1).ToUpper() + name.Substring(1).ToLower();
    }


    public void OnGet()
    {
        UserName = User.Identity.Name;
        UserEmailAddress = User.FindFirst(c => c.Type == ClaimTypes.Email)?.Value;
        UserProfileImage = User.FindFirst(c => c.Type == "picture")?.Value;

        UserName=FormatName(UserName);

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