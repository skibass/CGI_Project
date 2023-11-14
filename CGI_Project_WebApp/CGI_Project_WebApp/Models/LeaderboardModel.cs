using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CGI_Project_WebApp.Models
{
    public class LeaderboardModel : PageModel
    {
        public List<LeaderboardEntry> TopThree { get; set; }
        public List<LeaderboardEntry> PlacesFourToTen { get; set; }

        public class LeaderboardEntry
        {
            public int Position { get; set; }
            public string Name { get; set; }
            public int Score { get; set; }
            public int Percentage { get; set; } // for bar chart representation
        }

        public void OnGet()
        {
            // Dummy data
            TopThree = new List<LeaderboardEntry>
        {
            new LeaderboardEntry { Position = 1, Name = "test1", Score = 100, Percentage = 100 },
            new LeaderboardEntry { Position = 2, Name = "test2", Score = 77, Percentage = 77 },
            new LeaderboardEntry { Position = 3, Name = "test3", Score = 54, Percentage = 54},
        };

            PlacesFourToTen = new List<LeaderboardEntry>
        {
            new LeaderboardEntry { Position = 4, Name = "test4", Score = 50 },
            new LeaderboardEntry { Position = 5, Name = "test5", Score = 47 },
            new LeaderboardEntry { Position = 6, Name = "test6", Score = 45 },

        };
        }
    }
}
