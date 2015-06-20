using System;
using FakeItEasy;
using MeetUp.ApiProxy.Models;
using MeetUp.Domain;
using MeetUp.MeetUpApi.Models;
using MeetUp.Services;
using NUnit.Framework;

namespace MeetUp.Tests.Services
{
    [TestFixture]
    public class MeetUpEventServiceTests
    {
        private TestModels _tm;
        private MeetUpEventService _service;
        private Wrapper<MeetupEvents> _goodWrapper;

        [SetUp]
        public void Basics()
        {
            _tm = new TestModels();
            // service inclueds utils class as the utils class was written after tests. Tests More complete if they test both classes although not good practice.
            _service = new MeetUpEventService(_tm.MeetUpEventsProxyFake, _tm.OccasionRepositoryFake, _tm.OccasionFactoryFake, new ServiceUtils(_tm.RunnerRepositoryFake));  
            var events = new MeetupEvents{results = new MeetUpEvent[1], meta = new Meta()};
            _goodWrapper = new Wrapper<MeetupEvents> {Data = events, IsGood = true};
        }

        [Test]
        public void GetEventsFromMeetUp_CanGetUpdates_forced()
        {
            // Arrange
            A.CallTo(() => _tm.MeetUpEventsProxyFake.GetMeetupEvents()).Returns(_goodWrapper);

            // Act
            _service.GetEventsFromMeetUp(true);

            // Assert
            A.CallTo(() => _tm.RunnerRepositoryFake.GetLastRun(ApiType.MeetUpEvents, null)).MustNotHaveHappened();  // forced update shouldn't check anything just make call.
            A.CallTo(() => _tm.MeetUpEventsProxyFake.GetMeetupEvents()).MustHaveHappened();
            A.CallTo(() => _tm.RunnerRepositoryFake.Update(ApiType.MeetUpEvents, null)).MustHaveHappened();
        }


        [Test]
        public void GetEventsFromMeetUp_Update_RequiresUpdate()
        {
            // Arrange
            A.CallTo(() => _tm.MeetUpEventsProxyFake.GetMeetupEvents()).Returns(_goodWrapper);
            A.CallTo(() => _tm.RunnerRepositoryFake.GetLastRun(ApiType.MeetUpEvents, null)).Returns(DateTime.Now.AddDays(-1));

            // Act
            _service.GetEventsFromMeetUp();

            // Assert
            A.CallTo(() => _tm.MeetUpEventsProxyFake.GetMeetupEvents()).MustHaveHappened();
        }

        [Test]
        public void GetEventsFromMeetUp_Update_RequiresUpdate_FirstRun()
        {
            // Arrange
            A.CallTo(() => _tm.MeetUpEventsProxyFake.GetMeetupEvents()).Returns(_goodWrapper);
            A.CallTo(() => _tm.RunnerRepositoryFake.GetLastRun(ApiType.MeetUpEvents, null)).Returns(null);

            // Act
            _service.GetEventsFromMeetUp();

            // Assert
            A.CallTo(() => _tm.MeetUpEventsProxyFake.GetMeetupEvents()).MustHaveHappened();
        }

        [Test]
        public void GetEventsFromMeetUp_Update_DoesNotRequireUpdate()
        {
            // Arrange
            A.CallTo(() => _tm.MeetUpEventsProxyFake.GetMeetupEvents()).Returns(_goodWrapper);
            A.CallTo(() => _tm.RunnerRepositoryFake.GetLastRun(ApiType.MeetUpEvents, null)).Returns(DateTime.Now);

            // Act
            _service.GetEventsFromMeetUp();

            // Assert
            A.CallTo(() => _tm.RunnerRepositoryFake.Update(ApiType.MeetUpEvents, null)).MustNotHaveHappened();
        }

        [Test]
        public void GetEventsFromMeetUp_ProxyError()
        {
            // Arrange
            _goodWrapper.IsGood = false;
            A.CallTo(() => _tm.MeetUpEventsProxyFake.GetMeetupEvents()).Returns(_goodWrapper);
            
            // Act
            _service.GetEventsFromMeetUp(true);

            // Assert
            A.CallTo(() => _tm.RunnerRepositoryFake.Update(ApiType.MeetUpEvents, null)).MustNotHaveHappened();
        }

    }
}
