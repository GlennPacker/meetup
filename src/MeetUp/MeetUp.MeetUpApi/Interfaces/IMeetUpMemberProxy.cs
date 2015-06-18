using MeetUp.ApiProxy.Models;
using MeetUp.MeetUpApi.Models;

namespace MeetUp.MeetUpApi.Interfaces
{
    public interface IMeetUpMemberProxy
    {
        Wrapper<MeetUpMembers> GetMeetupMembers(int i);
    }
}