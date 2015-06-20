using System;
using FakeItEasy;
using MeetUp.Domain;
using MeetUp.Services;
using NUnit.Framework;

namespace MeetUp.Tests.Services
{
    [TestFixture]
    public class ServiceUtilsTests
    {
        private TestModels _tm;
        private ServiceUtils _service;

        [SetUp]
        public void Basics()
        {
            _tm = new TestModels();
            _service = new ServiceUtils(_tm.RunnerRepositoryFake);
        }        
        
        [Test]
        public void ServiceUtils_ShouldUpdate_forced()
        {
            // Arrange
            // Act
            var result = _service.ShouldUpdate(true, DateTime.Now.AddHours(-20), ApiType.MeetUpEvents, null);

            // Assert
            A.CallTo(() => _tm.RunnerRepositoryFake.StartUpdate(A<ApiType>.Ignored, null)).MustHaveHappened();
            A.CallTo(() => _tm.RunnerRepositoryFake.GetLastRun(A<ApiType>.Ignored, null)).MustNotHaveHappened();
            Assert.IsTrue(result);
        }

        [Test]
        public void ServiceUtils_ShouldUpdate()
        {
            // Arrange
            // Act
            var result = _service.ShouldUpdate(true, DateTime.Now.AddHours(-20), ApiType.MeetUpEvents, null);
            A.CallTo(() => _tm.RunnerRepositoryFake.GetLastRun(A<ApiType>.Ignored, null)).Returns(DateTime.Now);

            // Assert
            A.CallTo(() => _tm.RunnerRepositoryFake.StartUpdate(A<ApiType>.Ignored, null)).MustHaveHappened();
            
            Assert.IsTrue(result);
        }

        [Test]
        public void ServiceUtils_ShouldNotUpdate()
        {
            // Arrange
            // Act
            var result = _service.ShouldUpdate(true, DateTime.Now, ApiType.MeetUpEvents, null);
            A.CallTo(() => _tm.RunnerRepositoryFake.GetLastRun(A<ApiType>.Ignored, null)).Returns(DateTime.Now.AddMinutes(-5));

            // Assert
            A.CallTo(() => _tm.RunnerRepositoryFake.StartUpdate(A<ApiType>.Ignored, null)).MustHaveHappened();

            Assert.IsTrue(result);
        }
    }
}
