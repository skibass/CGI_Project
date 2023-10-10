using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGI_Project_WebApp_Models
{
    public class LeaderboardModels
    {
        public class Employee
        {
            public int Id { get; set; }
            public string Name { get; set; }
            // Other properties...

            public static List<Employee> GetDummyEmployees()
            {
                return new List<Employee>
    {
        new Employee { Id = 1, Name = "Alice" },
        new Employee { Id = 2, Name = "Bob" },
        new Employee { Id = 3, Name = "Charlie" },
        new Employee { Id = 4, Name = "David" },
        new Employee { Id = 5, Name = "Eva" },
        new Employee { Id = 6, Name = "Frank" },
        new Employee { Id = 7, Name = "Grace" },
        new Employee { Id = 8, Name = "Hannah" },
        new Employee { Id = 9, Name = "Isaac" },
        new Employee { Id = 10, Name = "Jack" }
    };
            }

    }

        public class Vote
        {
            public int Id { get; set; }
            public int SuggestionId { get; set; }
        }

        public class Suggestion
        {
            public int Id { get; set; }
            public int EmployeeId { get; set; }
            public List<Vote> Votes { get; set; }
        }

        public class Poll
        {
            public int Id { get; set; }
            public List<Suggestion> PollSuggestions { get; set; }
        }
        }
    

}
