
using CGI_Project_WebApp_Core.classes;
using CGI_Project_WebApp_Core.Interfaces;
using CGI_Project_WebApp_Models;
using CGI_Project_WebApp_UnitTests.Factory;
using FakeItEasy;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using System.Reflection.PortableExecutable;

namespace CGI_Project_WebApp_UnitTests
{
    public class PollTests
    {
        private PollFactory pollFactory = new PollFactory();

        private PollService pollService;
        private readonly IPollRepository pollRepository;
        //private

        public PollTests()
        {
            pollRepository = A.Fake<IPollRepository>();

            pollService = new PollService(pollRepository);
        }
		#region happy_flow

		[Fact]
        public void GetVotablePollsTest_3_Open_polls_two_votable()
        {

            A.CallTo(() => pollRepository.GetOpenPolls()).Returns(
                new List<Poll>
                {
                    pollFactory.BasicPoll(23),
                    pollFactory.BasicPoll(24),
                    pollFactory.BasicPoll(1),
                }
                );

            Assert.True(pollService.TryGetValidAndVoteablePolls(out List<Poll> votablePolls, 1));

            Assert.Equal(2, votablePolls.Count);
        }

        [Fact]
        public void GetVotablePollsTest_3_Open_polls_zero_votable()
        {

            A.CallTo(() => pollRepository.GetOpenPolls()).Returns(
                new List<Poll>
                {
                    pollFactory.BasicPoll(1),
                    pollFactory.BasicPoll(1),
                    pollFactory.BasicPoll(1)
                }
                );

            pollService.TryGetValidAndVoteablePolls(out List<Poll> votablePolls, 1);

            Assert.Equal(0, votablePolls.Count);
        }

        [Fact]
        public void GetNonVotablePollsTest_3_Open_polls_1_non_votable()
        {

            A.CallTo(() => pollRepository.GetOpenPolls()).Returns(
                new List<Poll>
                {
                    pollFactory.BasicPoll(1),
                    pollFactory.BasicPoll(988),
                    pollFactory.BasicPoll(987)
                }
                ) ;

            pollService.TryGetValidButNonVoteablePolls(out List<Poll> NonVotablePolls, 1);

            Assert.Equal(1, NonVotablePolls.Count);
        }

        [Fact]
        public void TryGetMaxVoteCount_3_Votes_no_draw()
        {
            Poll poll;
            A.CallTo(() => pollRepository.TryGetPoll(out poll, 1)).Returns(true).AssignsOutAndRefParameters(
                pollFactory.BasicPoll(1)
                  ) ;

            pollService.TryGetMaxVoteCount(out int MaxCount, out bool Draw, 1);

            Assert.Equal(3, MaxCount);
            Assert.False(Draw);
        }
        [Fact]
        public void TryGetMaxVoteCount_3_Votes_and_draw()
        {
            Poll poll;
            A.CallTo(() => pollRepository.TryGetPoll(out poll, 1)).Returns(true).AssignsOutAndRefParameters(
                pollFactory.BasicDrawPoll()
                  );

            pollService.TryGetMaxVoteCount(out int MaxCount, out bool Draw, 1);

            Assert.Equal(3, MaxCount);
            Assert.True(Draw);
        }

        [Fact]
        public void TryGetPollVotes_8_Votes()
        {

            Poll poll;
            A.CallTo(() => pollRepository.TryGetPoll(out poll, 1)).Returns(true).AssignsOutAndRefParameters(
               pollFactory.BasicDrawPoll()
                  );

            pollService.TryGetPollVotes(out List<Vote> votes, 1);

            Assert.Equal(8, votes.Count);
        }
        [Fact]
        public void TryGetPollVotes_0_Votes()
        {
            Poll poll;
            A.CallTo(() => pollRepository.TryGetPoll(out poll, 1)).Returns(true).AssignsOutAndRefParameters(
                pollFactory.NoVotePoll()
                  );

            pollService.TryGetPollVotes(out List<Vote> votes, 1);

            Assert.Empty(votes);
        }
		#endregion
		#region bad_flow
		[Fact]
		public void GetVotablePollsTest_Null()
		{

			A.CallTo(() => pollRepository.GetOpenPolls()).Returns(
				null
				);

			Assert.False(pollService.TryGetValidAndVoteablePolls(out List<Poll> votablePolls, 1));
		}
        [Fact]
        public void GetNonVotablePollsTest_Null ()
		{

			A.CallTo(() => pollRepository.GetOpenPolls()).Returns(
				null
				);

			Assert.False(pollService.TryGetValidButNonVoteablePolls(out List<Poll> NonVotablePolls, 1));
		}
        [Fact]
        public void GetNonVotablePollsTest_0()
        {

            A.CallTo(() => pollRepository.GetOpenPolls()).Returns(
                new List<Poll>
                {
                }
                );

            Assert.True(pollService.TryGetValidButNonVoteablePolls(out List<Poll> NonVotablePolls, 1));

            Assert.Equal(0, NonVotablePolls.Count);
        }
        [Fact]
        public void GetNonVotablePollsTest_null()
        {

            A.CallTo(() => pollRepository.GetOpenPolls()).Returns(
                null
                );

            Assert.False(pollService.TryGetValidButNonVoteablePolls(out List<Poll> NonVotablePolls, 1));
        }

        #endregion


    }
}