using CGI_Project_WebApp_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGI_Project_WebApp_UnitTests.Factory
{
    public class VoteFactory
    {
        public Vote basic(int employeeId)
        {
            return new Vote
            {
                EmployeeId = employeeId,
                Id = 1,
            };
        }
    }
}
