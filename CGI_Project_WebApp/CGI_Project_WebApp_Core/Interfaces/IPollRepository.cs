using CGI_Project_WebApp_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGI_Project_WebApp_Core.Interfaces
{
    public interface IPollRepository
    {
        public List<Poll> GetOpenPolls();
        public List<Poll> GetPastPolls();
        public List<Poll> GetFuturePolls();
        public bool TryAddPoll(NewPollDto NewPoll, out Poll poll);
        public bool TryRemovePoll(Poll poll);
        public bool TryRemovePoll(int pollId);
        public bool TryGetPoll(out Poll? poll, int pollId);
        public bool TryAddSuggestionToPoll(int pollId, int SuggestionId);


    }
}
