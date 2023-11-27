
using CGI_Project_WebApp_Core.classes;
using CGI_Project_WebApp_Core.Interfaces;
using CGI_Project_WebApp_Models;
using FakeItEasy;
using System.Reflection.PortableExecutable;

namespace CGI_Project_WebApp_UnitTests
{
    public class PollTests
    {
        private PollService pollService;
        private readonly IPollRepository pollRepository;
        //private

        public PollTests()
        {
            pollRepository = A.Fake<IPollRepository>();

            pollService = new PollService(pollRepository);
        }

        [Fact]
        public void GetVotablePollsTest_3OpenPollsTwoVotable()
        {

            A.CallTo(() => pollRepository.GetOpenPolls()).Returns(
                new List<Poll>
                {
                    new Poll
                    {
                        Poll_name = "TestPoll1",
                        Id = 1,
                        ManagerId = 1,
                        StartTime = DateTime.Now.AddDays(-1),
                        EndTime = DateTime.Now.AddDays(1),
                        PeriodId= 1,
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
                                              EmployeeId = 81,
                                               SuggestionId= 1,
                                          }
                                      }
                                }
                            }
                        }
                    },
                    new Poll
                    {
                        Poll_name = "TestPoll2",
                        Id = 1,
                        ManagerId = 1,
                        StartTime = DateTime.Now.AddDays(-1),
                        EndTime = DateTime.Now.AddDays(1),
                        PeriodId= 1,
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
                                              EmployeeId = 81,
                                               SuggestionId= 1,
                                          }
                                      }
                                }
                            }
                        }
                    },
                    new Poll
                    {
                        Poll_name = "TestPoll3",
                        Id = 1,
                        ManagerId = 1,
                        StartTime = DateTime.Now.AddDays(-1),
                        EndTime = DateTime.Now.AddDays(1),
                        PeriodId= 1,
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
                                              EmployeeId = 1,
                                               SuggestionId= 1,
                                          },
                                          new Vote
                                          {
                                              Id = 2,
                                              EmployeeId = 18,
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
                                              EmployeeId = 8,
                                               SuggestionId= 1,
                                          },
                                          new Vote
                                          {
                                              Id = 2,
                                              EmployeeId = 1,
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
                            }
                        }
                    }
                }
                );

            pollService.TryGetValidAndVoteablePolls(out List<Poll> votablePolls, 1);

            Assert.Equal(2, votablePolls.Count);
        }

        [Fact]
        public void GetVotablePollsTest_3OpenPollsZeroVotable()
        {

            A.CallTo(() => pollRepository.GetOpenPolls()).Returns(
                new List<Poll>
                {
                    new Poll
                    {
                        Poll_name = "TestPoll1",
                        Id = 1,
                        ManagerId = 1,
                        StartTime = DateTime.Now.AddDays(-1),
                        EndTime = DateTime.Now.AddDays(1),
                        PeriodId= 1,
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
                                              EmployeeId = 1,
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
                                              EmployeeId = 1,
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
                                              EmployeeId = 1,
                                               SuggestionId= 1,
                                          }
                                      }
                                }
                            }
                        }
                    },
                    new Poll
                    {
                        Poll_name = "TestPoll2",
                        Id = 1,
                        ManagerId = 1,
                        StartTime = DateTime.Now.AddDays(-1),
                        EndTime = DateTime.Now.AddDays(1),
                        PeriodId= 1,
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
                                              EmployeeId = 1,
                                               SuggestionId= 1,
                                          },
                                          new Vote
                                          {
                                              Id = 2,
                                              EmployeeId = 1,
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
                                              EmployeeId = 1,
                                               SuggestionId= 1,
                                          }
                                      }
                                }
                            }
                        }
                    },
                    new Poll
                    {
                        Poll_name = "TestPoll3",
                        Id = 1,
                        ManagerId = 1,
                        StartTime = DateTime.Now.AddDays(-1),
                        EndTime = DateTime.Now.AddDays(1),
                        PeriodId= 1,
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
                                              EmployeeId = 1,
                                               SuggestionId= 1,
                                          },
                                          new Vote
                                          {
                                              Id = 2,
                                              EmployeeId = 1,
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
                                              EmployeeId = 8,
                                               SuggestionId= 1,
                                          },
                                          new Vote
                                          {
                                              Id = 2,
                                              EmployeeId = 1,
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
                            }
                        }
                    }
                }
                );

            pollService.TryGetValidAndVoteablePolls(out List<Poll> votablePolls, 1);

            Assert.Equal(0, votablePolls.Count);
        }

        [Fact]
        public void GetNonVotablePollsTest_3OpenPolls1NonVotable()
        {

            A.CallTo(() => pollRepository.GetOpenPolls()).Returns(
                new List<Poll>
                {
                    new Poll
                    {
                        Poll_name = "TestPoll1",
                        Id = 1,
                        ManagerId = 1,
                        StartTime = DateTime.Now.AddDays(-1),
                        EndTime = DateTime.Now.AddDays(1),
                        PeriodId= 1,
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
                                              EmployeeId = 81,
                                               SuggestionId= 1,
                                          }
                                      }
                                }
                            }
                        }
                    },
                    new Poll
                    {
                        Poll_name = "TestPoll2",
                        Id = 1,
                        ManagerId = 1,
                        StartTime = DateTime.Now.AddDays(-1),
                        EndTime = DateTime.Now.AddDays(1),
                        PeriodId= 1,
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
                                              EmployeeId = 81,
                                               SuggestionId= 1,
                                          }
                                      }
                                }
                            }
                        }
                    },
                    new Poll
                    {
                        Poll_name = "TestPoll3",
                        Id = 1,
                        ManagerId = 1,
                        StartTime = DateTime.Now.AddDays(-1),
                        EndTime = DateTime.Now.AddDays(1),
                        PeriodId= 1,
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
                                              EmployeeId = 1,
                                               SuggestionId= 1,
                                          },
                                          new Vote
                                          {
                                              Id = 2,
                                              EmployeeId = 18,
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
                                              EmployeeId = 8,
                                               SuggestionId= 1,
                                          },
                                          new Vote
                                          {
                                              Id = 2,
                                              EmployeeId = 1,
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
                            }
                        }
                    }
                }
                );

            pollService.TryGetValidButNonVoteablePolls(out List<Poll> NonVotablePolls, 1);

            Assert.Equal(1, NonVotablePolls.Count);
        }

        [Fact]
        public void TryGetMaxVoteCount_3VotesNoDraw()
        {
            Poll poll;
            A.CallTo(() => pollRepository.TryGetPoll(out poll, 1)).Returns(true).AssignsOutAndRefParameters(
                new Poll
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
                                              EmployeeId = 81,
                                               SuggestionId= 1,
                                          }
                                      }
                                }
                            }
                        }
                }
                  );

            pollService.TryGetMaxVoteCount(out int MaxCount, out bool Draw, 1);

            Assert.Equal(3, MaxCount);
            Assert.False(Draw);
        }
        [Fact]
        public void TryGetMaxVoteCount_3VotesAndDraw()
        {
            Poll poll;
            A.CallTo(() => pollRepository.TryGetPoll(out poll, 1)).Returns(true).AssignsOutAndRefParameters(
                new Poll
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
                            }
                        }
                }
                  );

            pollService.TryGetMaxVoteCount(out int MaxCount, out bool Draw, 1);

            Assert.Equal(3, MaxCount);
            Assert.True(Draw);
        }


    }
}