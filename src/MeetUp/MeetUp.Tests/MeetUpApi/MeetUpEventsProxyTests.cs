using MeetUp.ApiProxy;
using MeetUp.MeetUpApi;
using NUnit.Framework;

namespace MeetUp.Tests.MeetUpApi
{
    [TestFixture]
    public class MeetUpEventsProxyTests
    {
        private IApiServices _services;
        private TestModels _tm;
        private MeetUpEventsProxy _proxy;

        [SetUp]
        public void Basics()
        {
            _tm = new TestModels();
            _services = new ApiServices();
            _proxy = new MeetUpEventsProxy(_services, _tm.MeetUpKey, _tm.GroupUrl);
        }

        [Test]
        public void Can_Get_MeetUp_Events()
        {
            // Arrange
            // Act
            var result = _proxy.GetMeetupEvents();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsGood);
            Assert.IsNotNull(result.Data);
        }
    }
}
