using CGI_Project_WebApp_DAL.Database_Models;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using CGI_Project_WebApp_Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;

namespace CGI_Project_WebApp_DAL.repositories
{
    public class EmployeeRepository
    {
        public List<Employee> GetEmployees(int companyId)
        {

            Dbi511119Context context= new Dbi511119Context();

            //Company company = context.Companies.Include(c => c.Employees).Where(c => c.Id == companyId).FirstOrDefault();



            List<Employee> employees = context.Employees.Include(e=>e.Company).Include(e=>e.Role).Where(e => e.CompanyId == companyId).ToList();//
            if (employees == null)
            {
                employees= new List<Employee>();
            }
            return employees;
        }
        public bool TryGetEmployeeByID(int id, out Employee employee)
        {
            Dbi511119Context context = new Dbi511119Context();
            employee = context.Employees.Include(e => e.Company).Include(e => e.Role).Where(e => e.Id == id).FirstOrDefault();
            if (employee != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool TryChangeEmployeeRoll(Role role, Employee employee)
        {
            return TryChangeEmployeeRoll(role.Id, employee.Id);
        }
        public bool TryChangeEmployeeRoll(int roleId, int employeeId)
        {
            Dbi511119Context context = new Dbi511119Context();

            Employee? employee = context.Employees.Include(e => e.Role).Where(emp => emp.Id == employeeId).FirstOrDefault();
            Role? role = context.Roles.Where(role => role.Id == roleId).FirstOrDefault();

            if (employee != null && role != null)
            {
                employee.Role = role;
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

            

        }

        public bool TryGetAllPollesWithSuggestionFromEmloyee(out List<Poll> polls, Employee employee)
        {
            Dbi511119Context context = new Dbi511119Context();

            polls = new List<Poll>();
            List<PollSuggestion> pollSuggestion = context.PollSuggestions.Include(e => e.Poll).Include(e => e.Suggestion).ThenInclude(s=>s.Votes).Where(suggestion => suggestion.Suggestion.EmployeeId == employee.Id).ToList();

            foreach (PollSuggestion suggestion in pollSuggestion)
            {
                if(suggestion.Poll != null)
                {
                polls.Add(suggestion.Poll);
                }
            }
            if (polls.IsNullOrEmpty()) {
                return false;
            }
            else
            {
                return true;
            }
        }
        
        public bool TryGetWinningPolls(out List<Poll> winningPolls, Employee employee)
        {
            PollRepository pollRepository = new PollRepository();
            winningPolls = new List<Poll>();

            try
            {
                if (TryGetAllPollesWithSuggestionFromEmloyee(out List<Poll> allPolls, employee))
                {
                    foreach (Poll poll in allPolls)
                    {
                        int count;
                        bool draw;
                   
                        if(pollRepository.TryGetMaxVoteCount(out count,out draw, poll.Id))
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

    }



        
        //moeten wij de werknemers kunnen maken of gaat dit via de 3de party tool?
        
    }

