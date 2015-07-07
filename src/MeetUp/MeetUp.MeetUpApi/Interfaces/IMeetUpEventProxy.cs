using MeetUp.ApiProxy.Models;
using MeetUp.MeetUpApi.Models;

namespace MeetUp.MeetUpApi.Interfaces
{
    public interface IMeetUpEventProxy
    {
        Wrapper<MeetupRSVPObject> GetMeetupEvent(long meetUpid);
    }
}