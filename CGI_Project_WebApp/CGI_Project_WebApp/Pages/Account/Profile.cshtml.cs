using CGI_Project_WebApp;
using CGI_Project_WebApp_Core.classes;
using CGI_Project_WebApp_DAL.repositories;
using CGI_Project_WebApp_Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace acme.Pages;

public class ProfileModel : PageModel
{
    IWebHostEnvironment env;

    public IFormFile Upload { get; set; }

    EmployeeService employeeService = new EmployeeService(new EmployeeRepository(), new PollRepository());
    public Employee employee;
    SuggestionService suggestionService = new SuggestionService(new SuggestionRepository(), new PollRepository(), new EmployeeRepository());
    PhotoAlbumService photoAlbumService = new PhotoAlbumService(new PhotoAlbumRepository());
    public List<SuggestionWithVoteCount> SuggestionsWithVoteCount { get; set; } = new List<SuggestionWithVoteCount>();
    public string UserName { get; set; }
    public string UserEmailAddress { get; set; }
    public string UserProfileImage { get; set; }

    public string CurrentLanguage { get; private set; }
    public string CountryCode { get; private set; }


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
        CurrentLanguage = LanguageHelper.GetCurrentLanguage(HttpContext);
        CountryCode = CurrentLanguage;
        ViewData["CurrentLanguage"] = CurrentLanguage;
        ViewData["CountryCode"] = CountryCode;

        if (User.Identity.IsAuthenticated == false)
        {
            RedirectToPage("../Index");
        }
        else
        {
            UserEmailAddress = User.FindFirst(c => c.Type == ClaimTypes.Email)?.Value;

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
                                    VoteCount = 4
                                    //VoteCount = suggestion.Votes?.Count ?? 0 ----tmp
                                }) ;
                            }
                        }
                    }
                }
                else
                {
                    employee = null;
                }
            }
        }
    }
    public async Task<IActionResult> OnPostTryAddPhoto()
    {
        UserEmailAddress = User.FindFirst(c => c.Type == ClaimTypes.Email)?.Value;
        if (employeeService.TryGetEmployeeByEmail(UserEmailAddress, out Employee emp))
        {
            employee = emp;

            var path = Path.Combine(env.WebRootPath, "UserPictures");

            await photoAlbumService.TryAddUserPicture(emp, path, Upload);

            return RedirectToPage();
        }
        return Page();
    }
}