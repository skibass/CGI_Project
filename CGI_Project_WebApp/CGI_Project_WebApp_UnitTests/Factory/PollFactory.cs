using CGI_Project_WebApp_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGI_Project_WebApp_UnitTests.Factory
{
    internal class PollFactory
    {
        public Poll NoVotePoll()
        {
            return new Poll
            {
                Poll_name = "TestPoll1",
                Id = 1,
                ManagerId = 1,
                StartTime = DateTime.Now.AddDays(-1),
                EndTime = DateTime.Now.AddDays(1),
                PeriodId = 1,
                PollSuggestions = new List<PollSuggestion>
                        {
                            new PollSuggestion
                            {
                                Suggestion = new Suggestion
                                {
                                      Name="Efteling",
                                      Id = 1,
                                      Votes = new List<Vote>
                                      {
                                      }
                                }
                            },
                            new PollSuggestion
                            {
                                Suggestion = new Suggestion
                                {
                                    Name = "Efteling",
                                    Id = 1,
                                    Votes = new List<Vote>
                                      {

                                      }
                                }
                            },
                            new PollSuggestion
                            {
                                Suggestion = new Suggestion
                                {
                                    Name = "Efteling",
                                    Id = 1,
                                    Votes = new List<Vote>
                                      {

                                      }
                                }
                            }
                        }
            };
        }
        public Poll BasicDrawPoll()
        {
            return new Poll
            {
                Poll_name = "TestPoll1",
                Id = 1,
                ManagerId = 1,
                StartTime = DateTime.Now.AddDays(-1),
                EndTime = DateTime.Now.AddDays(1),
                PeriodId = 1,
                PollSuggestions = new List<PollSuggestion>
                        {
                            new PollSuggestion
                            {
                                Suggestion = new Suggestion
                                {
                                      Name="Efteling",
                                      Id = 1,
                                      Votes = new List<Vote>
                                      {
                                          new Vote
                                          {
                                              Id = 1,
                                              EmployeeId = 18,
                                               SuggestionId= 1,
                                          },
                                                                                    new Vote
                                          {
                                              Id = 2,
                                              EmployeeId = 18,
                                               SuggestionId= 1,
                                          },
                                          new Vote
                                          {
                                              Id = 3,
                                              EmployeeId = 81,
                                              SuggestionId = 1,
                                          }
                                      }
                                }
                            },
                            new PollSuggestion
                            {
                                Suggestion = new Suggestion
                                {
                                    Name = "Efteling",
                                    Id = 1,
                                    Votes = new List<Vote>
                                      {
                                          new Vote
                                          {
                                              Id = 1,
                                              EmployeeId = 18,
                                               SuggestionId= 1,
                                          },
                                          new Vote
                                          {
                                              Id = 2,
                                              EmployeeId = 81,
                                               SuggestionId= 1,
                                          },
                                          new Vote
                                          {
                                              Id = 3,
                                              EmployeeId = 81,
                                               SuggestionId= 1,
                                          }
                                      }
                                }
                            },
                            new PollSuggestion
                            {
                                Suggestion = new Suggestion
                                {
                                    Name = "Efteling",
                                    Id = 1,
                                    Votes = new List<Vote>
                                      {
                                          new Vote
                                          {
                                              Id = 1,
                                              EmployeeId = 18,
                                               SuggestionId= 1,
                                          },
                                          new Vote
                                          {
                                              Id = 2,
                                              EmployeeId = 81,
                                               SuggestionId= 1,
                                          }
                                      }
                                }
                            }
                        }
            };
        }
        public Poll BasicPoll(int VariableId)
        {
            return new Poll
            {
                Poll_name = "TestPoll1",
                Id = 1,
                ManagerId = 1,
                StartTime = DateTime.Now.AddDays(-1),
                EndTime = DateTime.Now.AddDays(1),
                PeriodId = 1,
                PollSuggestions = new List<PollSuggestion>
                        {
                            new PollSuggestion
                            {
                                Suggestion = new Suggestion
                                {
                                      Name="Efteling",
                                      Id = 1,
                                      Votes = new List<Vote>
                                      {
                                          new Vote
                                          {
                                              Id = 1,
                                              EmployeeId = 18,
                                               SuggestionId= 1,
                                          },
                                                                                    new Vote
                                          {
                                              Id = 1,
                                              EmployeeId = 18,
                                               SuggestionId= 1,
                                          },
                                          new Vote
                                          {
                                              Id = 2,
                                              EmployeeId = 81,
                                               SuggestionId= 1,
                                          }
                                      }
                                }
                            },
                            new PollSuggestion
                            {
                                Suggestion = new Suggestion
                                {
                                      Name="Efteling",
                                      Id = 1,
                                      Votes = new List<Vote>
                                      {
                                          new Vote
                                          {
                                              Id = 1,
                                              EmployeeId = 18,
                                               SuggestionId= 1,
                                          },
                                          new Vote
                                          {
                                              Id = 2,
                                              EmployeeId = 81,
                                               SuggestionId= 1,
                                          }
                                      }
                                }
                            },
                            new PollSuggestion
                            {
                                Suggestion = new Suggestion
                                {
                                      Name="Efteling",
                                      Id = 1,
                                      Votes = new List<Vote>
                                      {
                                          new Vote
                                          {
                                              Id = 1,
                                              EmployeeId = 18,
                                               SuggestionId= 1,
                                          },
                                          new Vote
                                          {
                                              Id = 2,
                                              EmployeeId = VariableId,
                                               SuggestionId= 1,
                                          }
                                      }
                                }
                            }
                        }
            };
        }
    }
}
