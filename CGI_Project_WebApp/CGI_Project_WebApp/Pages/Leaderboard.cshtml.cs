using CGI_Project_WebApp_Core;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using static CGI_Project_WebApp_Models.LeaderboardModels;

namespace CGI_Project_WebApp.Pages
{
    public class LeaderboardModel : PageModel
    {
        private readonly LeaderboardService _leaderboardService;

        public LeaderboardModel(LeaderboardService leaderboardService)
        {
            _leaderboardService = leaderboardService;
        }

        public List<(string Name, int Score)> LeaderboardData { get; private set; }
        public int MaxScore { get; private set; }
        public List<(string Name, int Score)> TopThree { get; private set; }
        public List<(string Name, int Score, int Position)> PlacesFourToTen { get; private set; }

        public void OnGet()
        {
            // Retrieve leaderboard data
            LeaderboardData = _leaderboardService.CalculateLeaderboard(Employee.GetDummyEmployees());

            // Extract the required sections of the leaderboard for display
            MaxScore = LeaderboardData.Max(x => x.Score);
            TopThree = LeaderboardData.Take(3).ToList();
            PlacesFourToTen = LeaderboardData.Skip(3).Take(7)
                .Select((x, index) => (x.Name, x.Score, Position: index + 4)).ToList();
        }
    }
}
