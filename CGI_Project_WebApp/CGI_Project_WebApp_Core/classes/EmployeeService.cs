using CGI_Project_WebApp_Core.Interfaces;
using CGI_Project_WebApp_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGI_Project_WebApp_Core.classes
{
    public class EmployeeService
    {
        IEmployeeRepository employeeRepository;
        IPollRepository pollRepository;

        public EmployeeService(IEmployeeRepository employeeRepository, IPollRepository pollRepository)
        {
            this.employeeRepository = employeeRepository;
            this.pollRepository = pollRepository;
        }

        public bool TryGetAllPollesWithSuggestionFromEmployee(out List<Poll> polls, Employee employee)
        {
            List<PollSuggestion> pollSuggestion = employeeRepository.GetPollSuggestionsByEmployeeId(employee);
            polls = new List<Poll>();
            try
            {

                foreach (PollSuggestion suggestion in pollSuggestion)
                {
                    if (suggestion.Poll != null)
                    {
                        polls.Add(suggestion.Poll);

                    }
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool getEmployeeSuggestions(out List<Suggestion> suggestions, Employee employee)
        {
            try
            {
                List<PollSuggestion> pollSuggestion = employeeRepository.GetPollSuggestionsByEmployeeId(employee.Id);

                suggestions = pollSuggestion.Select(suggestion => suggestion.Suggestion).ToList();
                return true;
            }
            catch (Exception)
            {
                suggestions = null;
                return false;
            }

        }
        public bool TryAddEmployee(string Email)
        {
            Employee emp = new Employee();
            emp.Email = Email;
            emp.CompanyId = 1;

            if (DoesEmailExist(Email))
            {
                return false;
            }
            if (employeeRepository.TryAddEmployee(emp))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool TryAddEmployee(string Email, string FirstName)
        {
            Employee emp = new Employee();
            emp.Email = Email;
            emp.FirstName = FirstName;
            emp.CompanyId = 1;

            if (DoesEmailExist(Email))
            {
                return false;
            }
            if (employeeRepository.TryAddEmployee(emp))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool TryGetWinningPolls(out List<Poll> winningPolls, Employee employee)
        {
            PollService pollService = new PollService(pollRepository);
            winningPolls = new List<Poll>();

            try
            {
                if (TryGetAllPollesWithSuggestionFromEmployee(out List<Poll> allPolls, employee))
                {
                    foreach (Poll poll in allPolls)
                    {
                        bool winner = false;
                        if (pollService.TryGetMaxVoteCount(out int count, out bool draw, poll.Id))
                        {


                            if (!draw && poll.PollSuggestions.Where(ps => ps.Suggestion.EmployeeId == employee.Id).Select(s => s.Suggestion.Votes.Count).Contains(count) && count > 0)
                            {
                                winner = true; 
                            }
                            if(winner) winningPolls.Add(poll);
                        }

                    }
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
            return false;

        }
        public bool TryChangeEmployeeRoll(Role role, Employee employee)
        {
            return employeeRepository.TryChangeEmployeeRoll(role, employee);
        }
        public bool TryGetEmployeeByID(int id, out Employee employee)
        {
            return employeeRepository.TryGetEmployeeByID(id, out employee);
        }
        public bool TryGetEmployeeByEmailAndFirstName(string Email, string FirstName, out Employee employee)
        {
            return employeeRepository.TryGetEmployeeByEmailAndFirstName(Email, FirstName, out employee);
        }

        public bool TryGetEmployeeByEmail(string Email, out Employee employee)
        {
            employee = null;
            try
            {
                if (DoesEmailExist(Email))
                {
                    return employeeRepository.TryGetEmployeeByEmail(Email, out employee);
                }
            }
            catch (Exception e)
            {

            }
            return false;
        }

        public bool TryGetEmployeesWithMostWinningVotes(out List<EmployeeWinCount> EmpWincounts, int max = 10)
        {
            EmpWincounts = new List<EmployeeWinCount>();
            try
            {
                foreach (Employee employee in employeeRepository.GetEmployees())
                {
                    if (TryGetWinningPolls(out List<Poll> winningPolls, employee))
                    {
                        EmployeeWinCount employeesWinCount = new EmployeeWinCount
                        {
                            Employee = employee,
                            Count = winningPolls.Count
                        };
                        EmpWincounts.Add(employeesWinCount);
                    }
                }

                EmpWincounts = EmpWincounts.OrderByDescending(e => e.Count).Take(max).ToList();
                
                return true;
            }
            catch (Exception)
            {
                return false;

            }
        }
        public bool DoesEmailExist(string Email)
        {
            return employeeRepository.GetEmployees().Select(e => e.Email).Contains(Email);
        }
    }
}
