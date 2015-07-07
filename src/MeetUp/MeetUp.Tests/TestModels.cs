using FakeItEasy;
using MeetUp.Core;
using MeetUp.MeetUpApi.Interfaces;
using MeetUp.Services.Interfaces;

namespace MeetUp.Tests
{
    public class TestModels: TestModelsHiddenBase
    {
        public IMeetUpEventsProxy MeetUpEventsProxyFake { get; set; }
        public IOccasionRepository OccasionRepositoryFake { get; set; }
        public IOccasionFactory OccasionFactoryFake { get; set; }
        public IRunnerRepository RunnerRepositoryFake { get; set; }
        public IMeetUpMemberProxy MeetUpMemberProxyFake { get; set; }
        public IUserAccountFactory UserAccountFactoryFake { get; set; }
        public IServiceUtils ServiceUtilsFake { get; set; }
        public IMeetUpEventProxy MeetUpEventProxyFake { get; set; }
        public IRsvpFactory RsvpFactoryFake { get; set; }

        public TestModels()
        {
            MeetUpEventsProxyFake = A.Fake<IMeetUpEventsProxy>();
            OccasionRepositoryFake = A.Fake<IOccasionRepository>();
            OccasionFactoryFake = A.Fake<IOccasionFactory>();
            RunnerRepositoryFake = A.Fake<IRunnerRepository>();
            MeetUpMemberProxyFake = A.Fake<IMeetUpMemberProxy>();
            UserAccountFactoryFake = A.Fake<IUserAccountFactory>();
            ServiceUtilsFake = A.Fake<IServiceUtils>();
            MeetUpEventProxyFake = A.Fake<IMeetUpEventProxy>();
            RsvpFactoryFake = A.Fake<IRsvpFactory>();
        }
    }
}