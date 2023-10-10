// See https://aka.ms/new-console-template for more information


using CGI_Project_WebApp_DAL.repositories;
using CGI_Project_WebApp_Models;

EmployeeRepository repository = new EmployeeRepository();

repository.TryGetValidAndVoteablePolls(out List<Poll> polls, 1);

int i = 0;
