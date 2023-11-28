using CGI_Project_WebApp_Core.classes;
using CGI_Project_WebApp_Core.Interfaces;
using CGI_Project_WebApp_Core.Services;
using CGI_Project_WebApp_Models;
using CGI_Project_WebApp_UnitTests.Factory;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGI_Project_WebApp_UnitTests
{
    public class EmployeeTests
    {
        PollSuggestionFactory pollFactory = new PollSuggestionFactory();
        private EmployeeService employeeService;
        private readonly IPollRepository pollRepository;
        private readonly IEmployeeRepository employeeRepository;

        public EmployeeTests()
        {
            pollRepository = A.Fake<IPollRepository>();
            employeeRepository = A.Fake<IEmployeeRepository>();

            employeeService = new EmployeeService(employeeRepository ,pollRepository);
        }

        [Fact]
        public void TryGetAllPollesWithSuggestionFromEmployee_3_Polls_With_employee_suggestion()
        {
            Employee emp = new Employee
            {
                Id = 1
            };
            A.CallTo(() => employeeRepository.GetPollSuggestionsByEmployeeId(emp)).Returns(
                new List<PollSuggestion>
                {
                    pollFactory.basic(1,"Efteling"),
                    pollFactory.basic(1, "Bowlen"),
                    pollFactory.basic(1, "Pannenkoeken huis"),
                }
                ) ;


            Assert.True(employeeService.TryGetAllPollesWithSuggestionFromEmployee(out List<Poll> polls, emp));
            Assert.Equal(3, polls.Count);
        }

        [Fact]
        public void TryGetAllPollesWithSuggestionFromEmployee_0_Polls_With_employee_suggestion()
        {
            Employee emp = new Employee
            {
                Id = 1
            };
            A.CallTo(() => employeeRepository.GetPollSuggestionsByEmployeeId(emp)).Returns(
                new List<PollSuggestion>
                {
                }
                );


            Assert.True(employeeService.TryGetAllPollesWithSuggestionFromEmployee(out List<Poll> polls, emp));
            Assert.Empty(polls);
        }

        [Fact] void getEmployeeSuggestions_3_suggestions()
        {
            Employee emp = new Employee
            {
                Id = 1
            };
            A.CallTo(() => employeeRepository.GetPollSuggestionsByEmployeeId(emp)).Returns(
                new List<PollSuggestion>
                {
                    pollFactory.basic(1,"Efteling"),
                    pollFactory.basic(1, "Bowlen"),
                    pollFactory.basic(1, "Pannenkoeken huis"),
                }
                );

            Assert.True(employeeService.getEmployeeSuggestions(out List<Suggestion> suggestions, emp));
            Assert.Equal(3, suggestions.Count);
        }


    }
}
