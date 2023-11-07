using CGI_Project_WebApp_Core.classes;
using CGI_Project_WebApp_Core.Services;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

VoteService voteService= new VoteService();

List<DateTime> dates = new List<DateTime>()
{
    new DateTime(2023,11,11),
    new DateTime(2023,11,12),
    new DateTime(2023,11,14),
};

voteService.TryCreateVote(39,31,dates);
