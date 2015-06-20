using System.Collections.Generic;
using System.Linq;
using MeetUp.Core;
using MeetUp.Domain;
using MeetUp.MeetUpApi.Models;
using MeetUp.Services.Interfaces;

namespace MeetUp.Services.Factories
{
    public class UserAccountFactory : IUserAccountFactory
    {
        private readonly IUserAccountRepository _userAccountRepository;
        
        public UserAccountFactory(IUserAccountRepository userAccountRepository)
        {
            _userAccountRepository = userAccountRepository;
        }


        public List<UserAccount> MapUsers(List<MeetupMember> members)
        {
            if (members == null) return null;
            var meetupMemberIds = members.Select(r => r.id).ToList();
            var users = _userAccountRepository.List(meetupMemberIds).ToList();
            var userIds = users.Select(r => r.MeetupMemberId);

            // update all members in one db call.
            var ignored = members.Where(r => userIds.Contains(r.id)).Select(s => Update(s, users.FirstOrDefault(t => t.MeetupMemberId == s.id)));
            _userAccountRepository.Save();
            
            ignored = members.Where(r => !users.Select(u => u.MeetupMemberId).Contains(r.id)).Select(Create);
            var result = members.Select(r => MapUser(r, users)).ToList();
            return result;
        }

        public UserAccount MapUser(MeetupMember member, List<UserAccount> userAccounts)
        {
            var user = userAccounts.FirstOrDefault(r => r.MeetupMemberId == member.id);
            if (user == null) return Create(member);
            return Update(member, user);
        }

        public UserAccount Update(MeetupMember member, UserAccount user)
        {
            if (member.bio == user.Bio && member.name == user.Name && user.ProfilePic == member.Photo.photo_link) return user; // no need to check thb as it will be updated same time as full photo  
            user.Bio = member.bio;
            user.Name = member.name;
            if (member.Photo != null)
            {
                user.ProfileThmb = member.Photo.thumb_link;
                user.ProfilePic = member.Photo.photo_link;
            }
            //_userAccountRepository.Update(user);
            return user;
        }

        public UserAccount Create(MeetupMember member)
        {
            var user = new UserAccount
            {
                Bio = member.bio,
                IsAdmin = false,
                IsDeleted = false,
                Name = member.name,
                MeetupMemberId = member.id,
            };
            if (member.Photo != null)
            {
                user.ProfilePic = member.Photo.thumb_link;
                user.ProfileThmb = member.Photo.photo_link;
            }

            _userAccountRepository.Add(user);
            return user;
        }

        public void Dispose()
        {
            _userAccountRepository.Dispose();
        }
    }
}
