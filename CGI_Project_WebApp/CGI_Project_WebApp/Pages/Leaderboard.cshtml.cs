using CGI_Project_WebApp_Core.classes;
using CGI_Project_WebApp_Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace CGI_Project_WebApp.Pages
{
    public class LeaderboardModel : PageModel
    {
        public EmployeeService _employeeService = new EmployeeService();

        public List<(string Name, int Score)> LeaderboardData { get; private set; }
        public int MaxScore { get; private set; }
        public List<(string Name, int Score)> TopThree { get; private set; }
        public List<(string Name, int Score)> PlacesFourToTen { get; private set; }

        public void OnGet()
        {
            // Fetch the leaderboard data using the EmployeeService
            if (_employeeService.TryGetEmployeesWithMostWinningVotes(out List<EmployeeWinCount> empWinCounts) && empWinCounts != null)
            {
                LeaderboardData = empWinCounts
                    .Select(e => (e.Employee.FirstName, e.Count)) 
                    .ToList();

                
                MaxScore = LeaderboardData.Any() ? LeaderboardData.Max(e => e.Score) : 0;
            }
            else
            {
                // Handle the case where data couldn't be fetched
                LeaderboardData = new List<(string Name, int Score)>();
                MaxScore = 0;
            }

            // Top three for the chart
            TopThree = LeaderboardData.Take(3).ToList();

            // Remaining for the table
            PlacesFourToTen = LeaderboardData.Skip(3).Take(7)
                .Select((x, index) => (x.Name, x.Score)).ToList();

        }
    }
}
