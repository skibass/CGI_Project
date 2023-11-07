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
        VoteRepository repository= new VoteRepository();
        DateRepository dateRepository= new DateRepository();
        DateService dateService= new DateService();
        public bool TryCreateVote(int employeeID,int suggestionID, List<DateTime> preferredDates)
        {
            Vote vote = new Vote();
            vote.EmployeeId = employeeID;
            vote.SuggestionId = suggestionID;
            //vote.PreferredDates
            if (repository.TryAddVote(vote))
            {
                if (dateService.TryAddDatesOrGetDates(preferredDates, out List<int> dateIds))
                {
                    foreach (int dateId in dateIds)
                    {
                        repository.TryAddDateToVote(vote.Id, dateId);
                    }
                }
            }
            return false;
        }

        public bool TryGetVotedSuggestions(out Vote vote)
        {
            return repository.TryGetVotedSuggestions(out vote);
        }
    }
}
