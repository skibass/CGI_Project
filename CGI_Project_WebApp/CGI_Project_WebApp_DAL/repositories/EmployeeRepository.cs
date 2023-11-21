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
using System.ComponentModel.Design;
using System.Security.Claims;
using MySqlX.XDevAPI.Common;
using CGI_Project_WebApp_Core.classes;
using Google.Protobuf.WellKnownTypes;

namespace CGI_Project_WebApp_DAL.repositories
{
    public class EmployeeRepository
    {
        public Result<List<Employee>> GetEmployees()
        {
            try
            {
                Dbi511119Context context = new Dbi511119Context();
                List<Employee> employees = context.Employees.Include(e => e.Company).Include(e => e.Role).Include(e => e.Suggestions).Include(e => e.Votes).ToList();
                if (employees == null)
                {
                    employees = new List<Employee>();
                }
                return new Result<List<Employee>>(employees);
            }
            catch (Exception e )
            {
                return new Result<List<Employee>>("EmployeeRepository->TryGetEmployeeByEmail: "+e.Message);
            }

        }
        public Result<Employee> GetEmployeeByEmail(string email)
        {
            try
            {
                Dbi511119Context context = new Dbi511119Context();
                Employee employee = context.Employees.Include(e => e.Company).Include(e => e.Role).Include(e => e.Suggestions).Include(e => e.Votes).Where(e => e.Email == email).FirstOrDefault();//
                if (employee == null)
                {
                    return new Result<Employee>("gebruiker niet gevonden", "User not found");
                }
                return new Result<Employee>(employee);
            }
            catch (Exception e)
            {
                return new Result<Employee>("EmployeeRepository->TryGetEmployeeByEmail: " + e.Message, "er is iets fout gegaan", "something went wrong");
            }

            //return employee;
        }

        public Result<Employee> GetEmployeeByID(int id)
        {
            try
            {
                Dbi511119Context context = new Dbi511119Context();
                Employee employee = context.Employees.Include(e => e.Company).Include(e => e.Role).Where(e => e.Id == id).FirstOrDefault();
                if (employee == null)
                {
                    return new Result<Employee>("gebruiker niet gevonden", "User not found");
                }
                return new Result<Employee>(employee);
            }
            catch (Exception e)
            {
                return new Result<Employee>("EmployeeRepository->TryGetEmployeeByID: " + e.Message, "er is iets fout gegaan", "something went wrong");
            }
        }

        public Result<Employee> GetEmployeeByEmailAndFirstName(string Email, string FirstName)
        {
           try
            {
                Dbi511119Context context = new Dbi511119Context();
                Employee employee = context.Employees.Include(e => e.Company).Include(e => e.Role).Where(e => e.FirstName == FirstName && e.Email == Email).FirstOrDefault();
                if (employee == null)
                {
                    return new Result<Employee>("gebruiker niet gevonden", "User not found");
                }
                return new Result<Employee>(employee);
            }
            catch (Exception e)
            {
                return new Result<Employee>("EmployeeRepository->TryGetEmployeeByEmailAndFirstName: " + e.Message, "er is iets fout gegaan", "something went wrong");
            }
        }
        public Result<EmptyResult> ChangeEmployeeRoll(Role role, Employee employee)
        {
            Dbi511119Context context = new Dbi511119Context();
            try
            {
                context.Attach(employee);
                context.Attach(role);
                employee.Role = role;
                context.SaveChanges();
                return new Result<EmptyResult>();
            }
            catch (Exception e)
            {
                return new Result<EmptyResult>("EmployeeRepository->TryChangeEmployeeRoll: " + e.Message, "er is iets fout gegaan", "something went wrong");
                throw;
            }
        }
         public Result<List<PollSuggestion>> GetPollSuggestionsByEmployeeId(Employee employee)
        {
            return GetPollSuggestionsByEmployeeId(employee.Id);

            
        }
        public Result<List<PollSuggestion>> GetPollSuggestionsByEmployeeId(int employeeId)
        {
            try
            {
                Dbi511119Context context = new Dbi511119Context();
                List<PollSuggestion> pollSuggestion = context.PollSuggestions.Include(e => e.Poll).Include(e => e.Suggestion).ThenInclude(s => s.Votes).Where(suggestion => suggestion.Suggestion.EmployeeId == employeeId).ToList();
                return new Result<List<PollSuggestion>>(pollSuggestion);
            }
            catch (Exception e )
            {
                return new Result<List<PollSuggestion>>("EmployeeRepository->TryChangeEmployeeRoll: " + e.Message, "er is iets fout gegaan", "something went wrong");
            }



        }
        public Result<EmptyResult> AddEmployee(Employee emp)
        {

            Result <List<Employee>> employeesResult = GetEmployees();

            if (employeesResult.IsFailed)
            {
                return new Result<EmptyResult>(employeesResult.ErrorTransferObject);
            }
            if (employeesResult.Data.Select(e => e.Email).ToList().Contains(emp.Email))
            {
                return new Result<EmptyResult>("gebruiker bestaat al", "user already exists");
            }
            try
            {
                Dbi511119Context DB = new Dbi511119Context();
                DB.Employees.Add(emp);
                DB.SaveChanges();

                return new Result<EmptyResult>();
            }
            catch (Exception e)
            {
                return new Result<EmptyResult>("EmployeeRepository->TryChangeEmployeeRoll: " + e.Message, "er is iets fout gegaan met het maken van de gebruiker", "something with wrong with creating the user");
            }


        }
    
    }
}

