using CGI_Project_WebApp_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGI_Project_WebApp_UnitTests.Factory
{
    internal class PollSuggestionFactory
    {
        SuggestionFactory suggestionFactory = new SuggestionFactory();
        PollFactory pollFactory = new PollFactory();
        int index = 0;
        public PollSuggestion basic(int VariableUserId, string name)
        {
            index++;
            return new PollSuggestion
            {
                Id = 0,
                SuggestionId = 1,
                Suggestion = suggestionFactory.Basic(VariableUserId, index, name),
                Poll = pollFactory.BasicPoll(1),
                
            };

        }
    }
}
