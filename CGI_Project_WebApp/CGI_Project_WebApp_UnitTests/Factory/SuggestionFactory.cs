using CGI_Project_WebApp_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGI_Project_WebApp_UnitTests.Factory
{
    public class SuggestionFactory
    {
        public Suggestion Basic(int VariableUserId, int id, string Name, List<Vote> votes = null)
        {
            if (votes == null)
            {
                votes = new List<Vote>();
            }

            return new Suggestion
            {
                Id = id,
                Name = Name,
                EmployeeId = VariableUserId,
                Description = "Test",
               // Votes = votes,
                
                
            };
        }
    }
}
