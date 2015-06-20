using System;

namespace MeetUp.Services.Interfaces
{
    public interface IMeetUpMemberService
    {
        void GetMembersFromMeetUp(bool force = false);
        void Dispose();
    }
}