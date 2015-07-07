using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MeetUp.Core;
using MeetUp.Domain;
using MeetUp.MeetUpApi.Models;
using MeetUp.Services.Interfaces;

namespace MeetUp.Services.Factories
{
    public class RsvpFactory : IRsvpFactory
    {
        private readonly IRSVPRepository _rsvpRepository;
        private readonly IUserAccountRepository _userAccountRepository;
        private readonly IMeetUpMemberService _memberService;

        public RsvpFactory(IRSVPRepository rsvpRepository, IMeetUpMemberService memberService, IUserAccountRepository userAccountRepository)
        {
            _rsvpRepository = rsvpRepository;
            _memberService = memberService;
            _userAccountRepository = userAccountRepository;
        }

        public void MapRsvps(List<MeetupRSVP> data, Occasion occasion)
        {
            if (!data.Any()) return;
            
            var currentRsvps = occasion.RSVP;
            var currentRsvpIds = currentRsvps.Select(r => r.MeetUpId).ToList();
            
            occasion.MeetupLastUpdated = DateTime.Now;

            var yesdata = data.Where(r => r.response == "yes").ToList();
            foreach (var meetupRsvp in yesdata.Where(r => currentRsvpIds.Contains(r.rsvp_id)))
                UpdateRsvp(meetupRsvp, currentRsvps.FirstOrDefault(s => s.MeetUpId == meetupRsvp.rsvp_id));
            
            foreach (var meetupRsvp in yesdata.Where(r => !currentRsvpIds.Contains(r.rsvp_id)))
                CreateRsvp(meetupRsvp, occasion);
            
            foreach (var meetuprsvp in data.Where(r => r.response == "no"))
                DeleteRsvp(meetuprsvp.Id, occasion);

            
        }

        private void DeleteRsvp(long id, Occasion occasion)
        {
            var rsvp = _rsvpRepository.FindByMeetUpId(id);
            if(rsvp != null) _rsvpRepository.Delete(rsvp);
        }

        private void UpdateRsvp(MeetupRSVP meetupRSVP, RSVP currentRsvp)
        {
            if (currentRsvp.Guests == meetupRSVP.guests) return;
            currentRsvp.Guests = meetupRSVP.guests;
        }

        public void CreateRsvp(MeetupRSVP meetupRsvp, Occasion occasion)
        {
            var user = GetUser(meetupRsvp.member.member_id);
            if (user == null) return;   // should never happen but better safe.
            AddRSVP(meetupRsvp, user, occasion);         
        }


        public UserAccount GetUser(long memberId)
        {
            var user = _userAccountRepository.FindByMeetUpId(memberId);
            if (user == null)
            {
                // need to update members to include this one
                _memberService.GetMembersFromMeetUp(true);  // we know there are new members so force update.
                user = _userAccountRepository.FindByMeetUpId(memberId);
            }
            return user;
        }

        public void AddRSVP(MeetupRSVP meetupRsvp, UserAccount user, Occasion occasion)
        {
            var rsvp = new RSVP { Guests = meetupRsvp.guests, MeetUpId = meetupRsvp.rsvp_id, UserId = user.Id };
            occasion.RSVP.Add(rsvp);
            return;
        }


    }
}
