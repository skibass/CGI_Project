using CGI_Project_WebApp_DAL.Database_Models;
using CGI_Project_WebApp_DAL.repositories;
using CGI_Project_WebApp_Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGI_Project_WebApp_Core.classes
{
    public class EmployeeService
    {
        EmployeeRepository employeeRepository = new EmployeeRepository();
        public bool TryGetAllPollesWithSuggestionFromEmloyee(out List<Poll> polls, Employee employee)
        {
            
            List<PollSuggestion> pollSuggestion = employeeRepository.GetPollSuggestionsByEmployeeId(employee);
            polls = new List<Poll>();
            foreach (PollSuggestion suggestion in pollSuggestion)
            {
                if (suggestion.Poll != null)
                {
                    polls.Add(suggestion.Poll);
                }
            }
            if (polls.IsNullOrEmpty())
            {
                return false;
            }
            else
            {
                return true;
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
            PollService pollService = new PollService();
            winningPolls = new List<Poll>();

            try
            {
                if (TryGetAllPollesWithSuggestionFromEmloyee(out List<Poll> allPolls, employee))
                {
                    foreach (Poll poll in allPolls)
                    {
                        int count;
                        bool draw;

                        if (pollService.TryGetMaxVoteCount(out count, out draw, poll.Id))
                        {
                            if (poll.PollSuggestions.Where(ps => ps.Suggestion.EmployeeId == employee.Id).FirstOrDefault().Suggestion.Votes.Count == count)
                            {

                                winningPolls.Add(poll);
                                return true;
                            }
                        }

                    }
                }

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
    }
}
