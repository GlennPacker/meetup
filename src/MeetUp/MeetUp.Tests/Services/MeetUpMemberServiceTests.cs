using System;
using System.Collections.Generic;
using FakeItEasy;
using MeetUp.ApiProxy.Models;
using MeetUp.Domain;
using MeetUp.MeetUpApi.Models;
using MeetUp.Services;
using NUnit.Framework;

namespace MeetUp.Tests.Services
{
    [TestFixture]
    public class MeetUpMemberServiceTests
    {
        private TestModels _tm;
        private MeetUpMemberService _service;
        private Wrapper<MeetUpMembers> _goodwrapper;

        [SetUp]
        public void Basics()
        {
            _tm = new TestModels();
            _service = new MeetUpMemberService(_tm.MeetUpMemberProxyFake, _tm.UserAccountFactoryFake, _tm.ServiceUtilsFake);

            var members = new List<MeetupMember>();
            var wrapperdata = new MeetUpMembers { results = members };
            _goodwrapper = new Wrapper<MeetUpMembers> { IsGood = true, Data = wrapperdata };
        }

        [Test]
        public void GetMembersFromMeetUp_DoesNotUpdateMembers()
        {
            // Arrange
            var dt = DateTime.Now;
            A.CallTo(() => _tm.ServiceUtilsFake.ShouldUpdate(false, dt.AddHours(-4), ApiType.MeetupMembers, null)).Returns(false);
            A.CallTo(() => _tm.MeetUpMemberProxyFake.GetMeetupMembers(0)).Returns(_goodwrapper);

            // Act
            _service.GetMembersFromMeetUp(false, dt);

            // Assert
            A.CallTo(() => _tm.UserAccountFactoryFake.MapUsers(A<List<MeetupMember>>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => _tm.ServiceUtilsFake.UpdateLastRun(ApiType.MeetupMembers, null)).MustNotHaveHappened();
        }


        [Test]
        public void GetMembersFromMeetUp_UpdatesMembers()
        {
            // Arrange
            var dt = DateTime.Now;
            A.CallTo(() => _tm.ServiceUtilsFake.ShouldUpdate(true, dt.AddHours(-4), ApiType.MeetupMembers, null)).Returns(true);
            A.CallTo(() => _tm.MeetUpMemberProxyFake.GetMeetupMembers(0)).Returns(_goodwrapper);

            // Act
            _service.GetMembersFromMeetUp(true, dt);

            // Assert
            A.CallTo(() => _tm.UserAccountFactoryFake.MapUsers(A<List<MeetupMember>>.Ignored)).MustHaveHappened();
            A.CallTo(() => _tm.ServiceUtilsFake.UpdateLastRun(ApiType.MeetupMembers, null)).MustHaveHappened();
        }


        [Test]
        public void GetMembersFromMeetUp_WillLoopUpdatesMembers()
        {
            // Arrange
            var dt = DateTime.Now;
            A.CallTo(() => _tm.ServiceUtilsFake.ShouldUpdate(true, dt.AddHours(-4), ApiType.MeetupMembers, null)).Returns(true);
            var members = new List<MeetupMember>();
            for (int i = 0; i < 200; i++)
            {
                members.Add(new MeetupMember());
            }
            var wrapperdata = new MeetUpMembers { results = members };
            var wrapperloop = new Wrapper<MeetUpMembers> { IsGood = true, Data = wrapperdata };

            A.CallTo(() => _tm.MeetUpMemberProxyFake.GetMeetupMembers(0)).Returns(wrapperloop);
            A.CallTo(() => _tm.MeetUpMemberProxyFake.GetMeetupMembers(1)).Returns(_goodwrapper);

            // Act
            _service.GetMembersFromMeetUp(true, dt);

            // Assert
            A.CallTo(() => _tm.UserAccountFactoryFake.MapUsers(A<List<MeetupMember>>.Ignored)).MustHaveHappened();
            A.CallTo(() => _tm.ServiceUtilsFake.UpdateLastRun(ApiType.MeetupMembers, null)).MustHaveHappened();
            A.CallTo(() => _tm.MeetUpMemberProxyFake.GetMeetupMembers(0)).MustHaveHappened();
            A.CallTo(() => _tm.MeetUpMemberProxyFake.GetMeetupMembers(1)).MustHaveHappened();
        }

        [Test]
        public void GetMembersFromMeetUp_WillBombOutNicely()
        {
            // Arrange
            var dt = DateTime.Now;
            A.CallTo(() => _tm.ServiceUtilsFake.ShouldUpdate(true, dt.AddHours(-4), ApiType.MeetupMembers, null)).Returns(true);
            _goodwrapper.IsGood = false;
            A.CallTo(() => _tm.MeetUpMemberProxyFake.GetMeetupMembers(1)).Returns(_goodwrapper);

            // Act
            _service.GetMembersFromMeetUp(true, dt);

            // Assert
            A.CallTo(() => _tm.UserAccountFactoryFake.MapUsers(A<List<MeetupMember>>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => _tm.ServiceUtilsFake.UpdateLastRun(ApiType.MeetupMembers, null)).MustHaveHappened();
            A.CallTo(() => _tm.MeetUpMemberProxyFake.GetMeetupMembers(0)).MustHaveHappened();
        }

    }
}
