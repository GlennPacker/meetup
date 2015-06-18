using System.Collections.Generic;
using MeetUp.Domain;
using MeetUp.MeetUpApi.Models;

namespace MeetUp.Services.Interfaces
{
    public interface IUserAccountFactory
    {
        List<UserAccount> MapUsers(List<MeetupMember> members);
        UserAccount MapUser(MeetupMember member, List<UserAccount> userAccounts);
        UserAccount Update(MeetupMember member, UserAccount user);
        UserAccount Create(MeetupMember member);
        void Dispose();
    }
}