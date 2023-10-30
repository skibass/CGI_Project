using CGI_Project_WebApp_DAL.repositories;
using CGI_Project_WebApp_Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace acme.Pages;

public class ProfileModel : PageModel
{
    EmployeeRepository employeeRepository = new EmployeeRepository();
    public Employee employee = new();

    public string UserName { get; set; }
    public string UserEmailAddress { get; set; }
    public string UserProfileImage { get; set; }
    public void OnGet()
    {
        UserName = User.Identity.Name;
        UserEmailAddress = User.FindFirst(c => c.Type == ClaimTypes.Email)?.Value;
        UserProfileImage = User.FindFirst(c => c.Type == "picture")?.Value;

        employee = employeeRepository.GetEmployeeByEmail(UserEmailAddress);
    }
}