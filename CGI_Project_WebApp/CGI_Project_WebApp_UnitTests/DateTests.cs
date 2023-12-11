using CGI_Project_WebApp_Core.classes;
using CGI_Project_WebApp_Core.Interfaces;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CGI_Project_WebApp_UnitTests
{
    public class DateTests
    {
        IDateRepository dateRepository;
        DateService dateService;

        public DateTests()
        {
            dateRepository = A.Fake<IDateRepository>();
            dateService = new DateService(dateRepository);
        }

        [Fact]
        public void TryAddDatesOrGetDates_Add()
        {
            int? i = 0;

            List<DateTime> currentDates = new List<DateTime> {
                new DateTime(2010,10,5),
                new DateTime(2010,11,5),
                new DateTime(2010,12,5),
            };
            List<DateTime> NewDates = new List<DateTime>
            {
                new DateTime(2011,10,5),
                new DateTime(2011,11,5),
                new DateTime(2011,12,5),
            };
            A.CallTo(() => dateRepository.TryAddDate(currentDates[0].Date,out i)).Returns(true).AssignsOutAndRefParameters(1);
            A.CallTo(() => dateRepository.TryAddDate(currentDates[1].Date, out i)).Returns(true).AssignsOutAndRefParameters(2);
            A.CallTo(() => dateRepository.TryAddDate(currentDates[2].Date, out i)).Returns(true).AssignsOutAndRefParameters(3);

            A.CallTo(() => dateRepository.TryAddDate(NewDates[0].Date, out i)).Returns(true).AssignsOutAndRefParameters(4);
            A.CallTo(() => dateRepository.TryAddDate(NewDates[1].Date, out i)).Returns(true).AssignsOutAndRefParameters(5);
            A.CallTo(() => dateRepository.TryAddDate(NewDates[2].Date, out i)).Returns(true).AssignsOutAndRefParameters(6);

            List<DateTime> alldates = currentDates;
            alldates.AddRange(NewDates);

            List<int> dates;
            Assert.True(dateService.TryAddDatesOrGetDates(alldates, out dates));

            Assert.Equal(6, dates.Count);
            Assert.Contains(1, dates);
            Assert.Contains(2, dates);
            Assert.Contains(3, dates);
            Assert.Contains(4, dates);
            Assert.Contains(5, dates);
            Assert.Contains(6, dates);
        }
    }
}
