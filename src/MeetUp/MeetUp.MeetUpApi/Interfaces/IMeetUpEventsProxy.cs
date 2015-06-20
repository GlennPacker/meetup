using MeetUp.ApiProxy.Models;
using MeetUp.MeetUpApi.Models;

namespace MeetUp.MeetUpApi.Interfaces
{
    public interface IMeetUpEventsProxy
    {
        Wrapper<MeetupEvents> GetMeetupEvents();
    }
}