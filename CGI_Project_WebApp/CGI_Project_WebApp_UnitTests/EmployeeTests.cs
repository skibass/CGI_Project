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
        PollSuggestionFactory pollSuggestionFactory = new PollSuggestionFactory();
        PollFactory pollFactory = new PollFactory();
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
                    pollSuggestionFactory.basic(1,"Efteling"),
                    pollSuggestionFactory.basic(1, "Bowlen"),
                    pollSuggestionFactory.basic(1, "Pannenkoeken huis"),
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

        [Fact]
        public void getEmployeeSuggestions_3_suggestions()
        {
            Employee emp = new Employee
            {
                Id = 1
            };
                                              //GetPollSuggestionsByEmployeeId
            A.CallTo(() => employeeRepository.GetPollSuggestionsByEmployeeId(emp.Id)).Returns(
                new List<PollSuggestion>
                {
                    pollSuggestionFactory.basic(1,"Efteling"),
                    pollSuggestionFactory.basic(1, "Bowlen"),
                    pollSuggestionFactory.basic(1, "Pannenkoeken huis"),
                }
                );

            Assert.True(employeeService.getEmployeeSuggestions(out List<Suggestion> suggestions, emp));
            Assert.Equal(3, suggestions.Count);
        }

        [Fact]
        public void TryGetWinningPolls_2_winningPolls()
        {
            Poll poll = new Poll();
            Employee emp = new Employee
            {
                Id = 1
            };
            List<PollSuggestion> psList = new List<PollSuggestion>
                {
                    pollSuggestionFactory.basic(1,"Efteling", pollFactory.BasicWinningPoll(1)),
                    pollSuggestionFactory.basic(1, "Bowlen" , pollFactory.BasicWinningPoll(1)),
                    pollSuggestionFactory.basic(1, "Pannenkoeken huis", pollFactory.BasicPoll(1)),
                };

            A.CallTo(() => employeeRepository.GetPollSuggestionsByEmployeeId(emp)).Returns(
                psList
                );
            A.CallTo(() => pollRepository.TryGetPoll(out poll, 1)).Returns(true).AssignsOutAndRefParameters(
                psList.Select(p => p.Poll).ToArray()[0]
                ) ;
            A.CallTo(() => pollRepository.TryGetPoll(out poll, 2)).Returns(true).AssignsOutAndRefParameters(
                psList.Select(p => p.Poll).ToArray()[1]
                );
            A.CallTo(() => pollRepository.TryGetPoll(out poll, 3)).Returns(true).AssignsOutAndRefParameters(
                psList.Select(p => p.Poll).ToArray()[2]
            );
                                        
            Assert.True(employeeService.TryGetAllPollesWithSuggestionFromEmployee(out List<Poll> polls, emp));

            Assert.True(employeeService.TryGetWinningPolls(out List<Poll> winningpolls, emp));
            Assert.Equal(2, winningpolls.Count);
        }

        [Fact]
        public void TryGetEmployeesWithMostWinningVotes_3_Max()
        {
            Poll poll = new Poll();
            List<Employee> emps = new List<Employee>
            {
                new Employee{
                Id = 1,
                FirstName = "First",
                LastName = "User",
                },
                new Employee{
                Id = 2,
                FirstName = "Second",
                LastName = "User",
                },
                new Employee{
                Id = 3,
                FirstName = "Third",
                LastName = "User",
                },
                new Employee{
                Id = 4,
                FirstName = "Fourth",
                LastName = "User",
                },
            };

            A.CallTo(() => employeeRepository.GetEmployees()).Returns(emps);
            List<PollSuggestion> psList = new List<PollSuggestion>
                {
                    pollSuggestionFactory.basic(1,          "Efteling", pollFactory.BasicWinningPoll(1)),//1
                    pollSuggestionFactory.basic(1,           "Bowlen" , pollFactory.BasicWinningPoll(1)),//2
                    pollSuggestionFactory.basic(1, "Pannenkoeken huis", pollFactory.BasicWinningPoll(1)),//3
                    pollSuggestionFactory.basic(2,          "Efteling", pollFactory.BasicWinningPoll(2)),//4
                    pollSuggestionFactory.basic(2,           "Bowlen" , pollFactory.BasicWinningPoll(2)),//5
                    pollSuggestionFactory.basic(2, "Pannenkoeken huis", pollFactory.BasicWinningPoll(2)),//6
                    pollSuggestionFactory.basic(3,          "Efteling", pollFactory.BasicWinningPoll(3)),//7
                    pollSuggestionFactory.basic(3,           "Bowlen" , pollFactory.BasicWinningPoll(3)),//8
                    pollSuggestionFactory.basic(1, "Pannenkoeken huis", pollFactory.BasicWinningPoll(1)),//9
                };

            A.CallTo(() => pollRepository.TryGetPoll(out poll, 1)).Returns(true).AssignsOutAndRefParameters(
                psList.Select(p => p.Poll).ToArray()[0]
                );
            A.CallTo(() => pollRepository.TryGetPoll(out poll, 2)).Returns(true).AssignsOutAndRefParameters(
                psList.Select(p => p.Poll).ToArray()[1]
                );
            A.CallTo(() => pollRepository.TryGetPoll(out poll, 3)).Returns(true).AssignsOutAndRefParameters(
                psList.Select(p => p.Poll).ToArray()[2]
            );
            A.CallTo(() => pollRepository.TryGetPoll(out poll, 4)).Returns(true).AssignsOutAndRefParameters(
                psList.Select(p => p.Poll).ToArray()[3]
                );
            A.CallTo(() => pollRepository.TryGetPoll(out poll, 5)).Returns(true).AssignsOutAndRefParameters(
                psList.Select(p => p.Poll).ToArray()[4]
                );
            A.CallTo(() => pollRepository.TryGetPoll(out poll, 6)).Returns(true).AssignsOutAndRefParameters(
                psList.Select(p => p.Poll).ToArray()[5]
            );
            A.CallTo(() => pollRepository.TryGetPoll(out poll, 7)).Returns(true).AssignsOutAndRefParameters(
                psList.Select(p => p.Poll).ToArray()[6]
                );
            A.CallTo(() => pollRepository.TryGetPoll(out poll, 8)).Returns(true).AssignsOutAndRefParameters(
                psList.Select(p => p.Poll).ToArray()[7]
                );
            A.CallTo(() => pollRepository.TryGetPoll(out poll, 9)).Returns(true).AssignsOutAndRefParameters(
                psList.Select(p => p.Poll).ToArray()[8]
            );

            Assert.True(employeeService.TryGetEmployeesWithMostWinningVotes(out List<EmployeeWinCount> empWinList, 3));
            Assert.Equal(3, empWinList.Count);
            Assert.Equal("First", empWinList[0].Employee.FirstName);
            Assert.Equal("Second", empWinList[1].Employee.FirstName);
            Assert.Equal("Third", empWinList[2].Employee.FirstName);
            Assert.DoesNotContain("Fourth", empWinList.Select(s => s.Employee.FirstName).ToList());
        }
    }
}
