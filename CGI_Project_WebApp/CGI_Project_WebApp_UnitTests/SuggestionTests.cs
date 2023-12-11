using CGI_Project_WebApp_Core.classes;
using CGI_Project_WebApp_Core.Interfaces;
using CGI_Project_WebApp_Models;
using CGI_Project_WebApp_UnitTests.Factory;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGI_Project_WebApp_UnitTests
{
    public class SuggestionTests
    {
        PollSuggestionFactory pollSuggestionFactory = new PollSuggestionFactory();
        PollFactory pollFactory = new PollFactory();
        private SuggestionService suggestionService;
        private readonly IPollRepository pollRepository;
        private readonly IEmployeeRepository employeeRepository;
        private readonly ISuggestionRepository suggestionRepository;

        public SuggestionTests()
        {
            pollRepository = A.Fake<IPollRepository>();
            employeeRepository = A.Fake<IEmployeeRepository>();

            suggestionService = new SuggestionService(suggestionRepository, pollRepository, employeeRepository);
        }

        [Fact]
        public void TryGetWinningSuggestionOutOfPoll_get_winner_Efteling()
        {
            Poll poll = new Poll();
            List<Poll> polls = new List<Poll>()
            {
                pollFactory.BasicWinningPoll(1),
                pollFactory.BasicPoll(2),
                pollFactory.BasicPoll(3),
            };

            A.CallTo(() => pollRepository.TryGetPoll(out poll, 1)).Returns(true).AssignsOutAndRefParameters(
               polls[0]
                  );

            Assert.True(suggestionService.TryGetWinningSuggestionOutOfPoll(polls[0], out SuggestionList winningsuggestions));
            Assert.Single(winningsuggestions.suggestions);
            Assert.Equal("Efteling", winningsuggestions.suggestions[0].Name);
        }
        [Fact]
        public void TryGetWinningSuggestionOutOfPoll_get_no_winner_Draw()
        {
            Poll poll = new Poll();
            List<Poll> polls = new List<Poll>()
            {
                pollFactory.BasicWinningPoll(1),
                pollFactory.BasicDrawPoll(),
                pollFactory.BasicPoll(3),
            };

            A.CallTo(() => pollRepository.TryGetPoll(out poll, 1)).Returns(true).AssignsOutAndRefParameters(
               polls[1]
                  );

            Assert.True(suggestionService.TryGetWinningSuggestionOutOfPoll(polls[0], out SuggestionList winningsuggestions));
            Assert.Empty(winningsuggestions.suggestions);
        }

        [Fact]
        public void TryGetLatestWinners_get_3_winners()
        {
            Poll poll = new Poll();

            List<Poll> polls = new List<Poll>()
            {
                pollFactory.BasicWinningPollDatedAndNamed(1,new DateTime(2022,7,20), "BBQ"),
                pollFactory.BasicWinningPollDatedAndNamed(1,new DateTime(2022,5,20), "Efteling"),
                pollFactory.BasicWinningPollDatedAndNamed(1,new DateTime(2022,4,20), "Frankrijk"),
                pollFactory.BasicWinningPollDatedAndNamed(1,new DateTime(2022,6,20), "Spanje"),
                pollFactory.BasicWinningPollDatedAndNamed(1,new DateTime(2022,1,20), "Lego"),
            };
            A.CallTo(() => pollRepository.TryGetPoll(out poll, 1)).Returns(true).AssignsOutAndRefParameters(
               polls[0]
                  );
            A.CallTo(() => pollRepository.TryGetPoll(out poll, 2)).Returns(true).AssignsOutAndRefParameters(
               polls[1]
                  );
            A.CallTo(() => pollRepository.TryGetPoll(out poll, 3)).Returns(true).AssignsOutAndRefParameters(
               polls[2]
                  );
            A.CallTo(() => pollRepository.TryGetPoll(out poll, 4)).Returns(true).AssignsOutAndRefParameters(
               polls[3]
                  );
            A.CallTo(() => pollRepository.TryGetPoll(out poll, 5)).Returns(true).AssignsOutAndRefParameters(
               polls[4]
                  );

            A.CallTo(() => pollRepository.GetPastPolls()).Returns(
                polls);

            suggestionService.TryGetLatestWinners(out List<SuggestionList> winningSuggestions, 3);

            foreach (SuggestionList item in winningSuggestions)
            {
                Assert.Single(item.suggestions);
            }
            Assert.Equal(3, winningSuggestions.Count);
            Assert.Equal("BBQ", winningSuggestions[0].suggestions[0].Name);
            Assert.Equal("Spanje", winningSuggestions[1].suggestions[0].Name);
            Assert.Equal("Efteling", winningSuggestions[2].suggestions[0].Name);
        }

        [Fact]
        public void TryGetLatestWinners_get_3_winners_one_draw()
        {
            Poll poll = new Poll();

            List<Poll> polls = new List<Poll>()
            {
                pollFactory.BasicWinningPollDatedAndNamed(1,new DateTime(2022,7,20), "BBQ"),
                pollFactory.BasicWinningPollDatedAndNamed(1,new DateTime(2022,5,20), "Efteling"),
                pollFactory.BasicWinningPollDatedAndNamed(1,new DateTime(2022,4,20), "Frankrijk"),
                pollFactory.BasicDrawPollDatedAndNamed(1,new DateTime(2022,6,20), "Spanje"),
                pollFactory.BasicWinningPollDatedAndNamed(1,new DateTime(2022,1,20), "Lego"),
            };
            A.CallTo(() => pollRepository.TryGetPoll(out poll, 1)).Returns(true).AssignsOutAndRefParameters(
               polls[0]
                  );
            A.CallTo(() => pollRepository.TryGetPoll(out poll, 2)).Returns(true).AssignsOutAndRefParameters(
               polls[1]
                  );
            A.CallTo(() => pollRepository.TryGetPoll(out poll, 3)).Returns(true).AssignsOutAndRefParameters(
               polls[2]
                  );
            A.CallTo(() => pollRepository.TryGetPoll(out poll, 4)).Returns(true).AssignsOutAndRefParameters(
               polls[3]
                  );
            A.CallTo(() => pollRepository.TryGetPoll(out poll, 5)).Returns(true).AssignsOutAndRefParameters(
               polls[4]
                  );

            A.CallTo(() => pollRepository.GetPastPolls()).Returns(
                polls);

            suggestionService.TryGetLatestWinners(out List<SuggestionList> winningSuggestions, 3);

            foreach (SuggestionList item in winningSuggestions)
            {
                Assert.Single(item.suggestions);
            }
            Assert.Equal(3, winningSuggestions.Count);
            Assert.Equal("BBQ", winningSuggestions[0].suggestions[0].Name);
            Assert.Equal("Efteling", winningSuggestions[1].suggestions[0].Name);
            Assert.Equal("Frankrijk", winningSuggestions[2].suggestions[0].Name);
        }
    }
}
