using CGI_Project_WebApp_Core.classes;
using CGI_Project_WebApp_DAL.repositories;
using CGI_Project_WebApp_Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace acme.Pages;

public class ProfileModel : PageModel
{
    private EmployeeService employeeService = new EmployeeService();
    private SuggestionService suggestionService = new SuggestionService();
    public Employee employee; // Removed 'new' to avoid creating a default empty object
    public List<SuggestionWithVoteCount> SuggestionsWithVoteCount { get; set; } = new List<SuggestionWithVoteCount>();
    public string UserName { get; set; }
    public string UserEmailAddress { get; set; }
    public string UserProfileImage { get; set; }

    public class SuggestionWithVoteCount
    {
        public Suggestion Suggestion { get; set; }
        public int VoteCount { get; set; }
    }

    private string FormatName(string name)
    {
        return string.IsNullOrWhiteSpace(name) ? string.Empty : char.ToUpper(name[0]) + name.Substring(1).ToLower();
    }

    public void OnGet()
    {
        UserName = FormatName(User.Identity.Name);
        UserEmailAddress = User.FindFirst(c => c.Type == ClaimTypes.Email)?.Value;
        UserProfileImage = User.FindFirst(c => c.Type == "picture")?.Value;
        // Capitalize the first letter of the username and make the rest lowercase
        return name.Substring(0, 1).ToUpper() + name.Substring(1).ToLower();
    }


    public IActionResult OnGet()
    {
        if (User.Identity.IsAuthenticated == false)
        {
            return RedirectToPage("../Index");
        }
        else
        {
            UserName = User.Identity.Name;
            UserEmailAddress = User.FindFirst(c => c.Type == ClaimTypes.Email)?.Value;
            UserProfileImage = User.FindFirst(c => c.Type == "picture")?.Value;

        if (!string.IsNullOrWhiteSpace(UserEmailAddress))
        {
            if (employeeService.TryGetEmployeeByEmail(UserEmailAddress, out Employee emp))
            {
                employee = emp;
                if (employee?.Suggestions != null)
                {
                    foreach (var empSuggestion in employee.Suggestions)
                    {
                        if (suggestionService.TryGetSuggestionById(out Suggestion suggestion, empSuggestion.Id) && suggestion != null)
                        {
                            SuggestionsWithVoteCount.Add(new SuggestionWithVoteCount
                            {
                                Suggestion = suggestion,
                                VoteCount = suggestion.Votes?.Count ?? 0
                            });
                        }
                    }
                }
            }
            else
            {
                employee = null; 
            }
        }
        // Else block for handling cases where the email can't be retrieved 
    }
}