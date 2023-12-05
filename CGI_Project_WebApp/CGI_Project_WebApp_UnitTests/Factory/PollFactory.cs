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
        int indexVal = 0;

        int index()
        {
            indexVal++;
            return indexVal;

        }

        public Poll NoVotePoll()
        {
            return new Poll
            {
                Poll_name = "TestPoll1",
                Id = index(),
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
                Id = index(),
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
                Id = index(),
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
                                    EmployeeId = 90,
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
                                    EmployeeId = 7,
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
                                    EmployeeId = VariableId,
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

        public Poll BasicWinningPoll(int VariableId)
        {
            return new Poll
            {
                Poll_name = "TestPoll1",
                Id = index(),
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
                                    EmployeeId= VariableId,
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
                                               SuggestionId= 1,
                                          }
                                      }
                                }
                            },
                            new PollSuggestion
                            {
                                Suggestion = new Suggestion
                                {
                                    EmployeeId=298,
                                      Name="Efteling",
                                      Id = 2,
                                      Votes = new List<Vote>
                                      {
                                          new Vote
                                          {
                                              Id = 4,
                                              EmployeeId = 18,
                                               SuggestionId= 1,
                                          },
                                          new Vote
                                          {
                                              Id = 5,
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
                                     EmployeeId = 89,
                                      Name="Efteling",
                                      Id = 3,
                                      Votes = new List<Vote>
                                      {
                                          new Vote
                                          {
                                              Id =6,
                                              EmployeeId = 18,
                                               SuggestionId= 1,
                                          },
                                          new Vote
                                          {
                                              Id = 7,
                                              EmployeeId = VariableId,
                                               SuggestionId= 1,
                                          }
                                      }
                                }
                            }
                        }
            };
        }

        public Poll BasicWinningPollDatedAndNamed(int VariableId, DateTime EndDate, string winnerName)
        {
            Poll poll = BasicWinningPoll(VariableId);
            poll.EndTime = EndDate;
            poll.StartTime = EndDate.AddDays(-5);
            List<PollSuggestion> ps = poll.PollSuggestions.ToList();
            ps[0].Suggestion.Name = winnerName;
            poll.PollSuggestions = ps;

            return poll;
        }
        public Poll BasicDrawPollDatedAndNamed(int VariableId, DateTime EndDate, string FirstSuggestionName)
        {
            Poll poll = BasicDrawPoll();
            poll.EndTime = EndDate;
            poll.StartTime = EndDate.AddDays(-5);
            List<PollSuggestion> ps = poll.PollSuggestions.ToList();
            ps[0].Suggestion.Name = FirstSuggestionName;
            poll.PollSuggestions = ps;

            return poll;
        }
    }
}
