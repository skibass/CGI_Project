
using CGI_Project_WebApp_Core.Interfaces;
using CGI_Project_WebApp_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGI_Project_WebApp_Core.classes
{
    public class VoteService {
        IVoteRepository repository;
        IDateRepository dateRepository;
        DateService dateService;

        public VoteService(IVoteRepository repository, IDateRepository dateRepository)
        {
            this.repository = repository;
            this.dateRepository = dateRepository;
            this.dateService = new DateService(this.dateRepository);
        }
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
    }
}
