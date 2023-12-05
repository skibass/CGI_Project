using CGI_Project_WebApp_Core.classes;
using CGI_Project_WebApp_DAL.repositories;
using CGI_Project_WebApp_Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace acme.Pages;

public class ProfileModel : PageModel
{
    EmployeeService employeeService = new EmployeeService(new EmployeeRepository(), new PollRepository());
    public Employee employee = new();
    SuggestionService suggestionService = new SuggestionService(new SuggestionRepository(), new PollRepository(), new EmployeeRepository());
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
        if (string.IsNullOrWhiteSpace(name))
        {
            return string.Empty;
        }

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

            UserName = FormatName(UserName);

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
            foreach (var empSuggestion in employee.Suggestions)
            {
                if (suggestionService.TryGetSuggestionById(out Suggestion suggestion, empSuggestion.Id))
                {
                    SuggestionsWithVoteCount.Add(new SuggestionWithVoteCount
                    {
                        Suggestion = suggestion,
                        VoteCount = suggestion.Votes.Count
                    });
                }
            }
        }
        return null;
    }
}