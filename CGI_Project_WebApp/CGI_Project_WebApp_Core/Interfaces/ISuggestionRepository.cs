﻿using CGI_Project_WebApp_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGI_Project_WebApp_Core.Interfaces
{
    public interface ISuggestionRepository
    {
        public bool TryAddSuggestionToDB(Suggestion newSuggestion);
        public bool TryGetAllSuggestions(out SuggestionList suggestionList);

        public bool TryGetSuggestionWithId(out Suggestion suggestion, int id);
    }
}
