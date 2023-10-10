using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CGI_Project_WebApp_Models
{
    public class SuggestionList
    {

        public List<Suggestion> suggestions { get; set; } = new List<Suggestion>();
    }
}
