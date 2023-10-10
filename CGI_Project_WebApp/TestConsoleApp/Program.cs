// See https://aka.ms/new-console-template for more information

using System.Drawing.Text;
using CGI_Project_WebApp_DAL.repositories;
using CGI_Project_WebApp_Models;

EmployeeRepository employeeRepository = new EmployeeRepository();

PollRepository pollRepository = new PollRepository();

List<Employee> employees = new List<Employee>();

employees = employeeRepository.GetEmployees(1);

foreach (Employee employee in employees)
{
    Console.WriteLine(employee.FirstName + " " + employee.LastName + " - " + employee.Company.Name);
}

List<Poll> polls = new List<Poll>();

Employee emp;
employeeRepository.TryGetEmployeeByID(2, out emp);

employeeRepository.TryChangeEmployeeRoll(2, 1);

employeeRepository.TryGetAllPollesWithSuggestionFromEmloyee(out polls, employees[0]);

employeeRepository.TryGetWinningPolls(out polls, employees[1]);

//repository.TryChangeEmployeeRoll()



List<Poll> openRepositories = pollRepository.GetOpenRepositories();

foreach (Poll poll in openRepositories)
{
    Console.WriteLine(poll.Id + " " + poll.StartTime + " " + poll.EndTime);
}

List<Poll> pastRepositories = pollRepository.GetPastRepositories();

foreach (Poll poll in pastRepositories)
{
    Console.WriteLine(poll.Id + " " + poll.StartTime + " " + poll.EndTime);
}

List<Poll> getFutureRepositories = pollRepository.GetFutureRepositories();

foreach (var poll in getFutureRepositories)
{
    Console.WriteLine(poll.Id + " " + poll.StartTime + " " + poll.EndTime);
}


Poll poll1 = new Poll
{
    StartTime = DateTime.Now,
    EndTime = DateTime.Now.AddDays(6)
};

//pollRepository.TryAddPoll(poll1);

//Console.WriteLine(poll1.Id + " " + poll1.StartTime + " " + poll1.EndTime);

//var removePoll = pollRepository.TryRemovePoll(7);
//if (removePoll)
//{
//    Console.WriteLine("Successfully deleted poll " + poll1.Id);
//}

List<Vote> getPollVotes = new List<Vote>();

pollRepository.TryGetPollVotes(out getPollVotes, 1);

foreach (var vote in getPollVotes)
{
    Console.WriteLine(vote.Id + " " + vote.Date);
}

pollRepository.TryGetPoll(out poll1, 1);

Console.WriteLine(poll1);

int maxCount;
bool draw;

pollRepository.TryGetMaxVoteCount(out maxCount, out draw, poll1.Id);

Console.WriteLine(poll1.Id + " " + poll1.StartTime + " " + poll1.EndTime + " Noice");

int i = 0;