using CGI_Project_WebApp_DAL.repositories;
using CGI_Project_WebApp_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGI_Project_WebApp_Core.classes
{
    public class VoteService { 
        VoteRepository repository;
        public bool TryCreateVote(int employeeID,int suggestionID, DateTime preferredDate)
        {
            Vote vote = new Vote();
            vote.EmployeeId = employeeID;
            vote.SuggestionId = suggestionID;
            vote.Date = preferredDate;

            return repository.TryAddVote(vote);
        }
    }
}
