using CGI_Project_WebApp_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGI_Project_WebApp_Core.Interfaces
{
    public interface IVoteRepository
    {
        public bool TryAddVote(Vote vote);
        public bool TryAddDateToVote(int voteId, int dateId);

        public bool TryGetVotedSuggestions(out List<Vote> votes);
    }
}
