﻿using System;
using System.Collections.Generic;
using System.Linq;
using log4net;
using MeetUp.Core;
using MeetUp.Domain;
using MeetUp.MeetUpApi.Helpers;
using MeetUp.MeetUpApi.Models;
using MeetUp.Services.Interfaces;

namespace MeetUp.Services.Factories
{
    public class OccasionFactory : IOccasionFactory
    {
        private readonly IOccasionRepository _occasionRepository;
        private readonly IVenueFactory _venueFactory;
        private readonly IUserAccountRepository _userAccountRepository;
        private readonly IMeetUpMemberService _memberService;
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public OccasionFactory(IOccasionRepository occasionRepository, IVenueFactory venueFactory, IUserAccountRepository userAccountRepository, IMeetUpMemberService memberService)
        {
            _occasionRepository = occasionRepository;
            _venueFactory = venueFactory;
            _userAccountRepository = userAccountRepository;
            _memberService = memberService;
        }

        public List<Occasion> MapOccasions(List<MeetUpEvent> meetupEvents)
        {
            if (meetupEvents == null) return null;
            var meetupEventIds = meetupEvents.Select(r => r.Id).ToList();
            var occasions = _occasionRepository.List(meetupEventIds).ToList(); // get all the occasions in one db hit
            var occasionIds = occasions.Select(r => r.MeetupEventId);

            var result = meetupEvents.Where(r => !occasionIds.Contains(r.Id)).Select(CreateOccasion).ToList();    // create new events
            result.AddRange(meetupEvents.Where(r => occasionIds.Contains(r.Id)).Select(s => UpdateOccasion(s, occasions.FirstOrDefault(o => o.MeetupEventId == s.Id))).ToList()); // update events

            // if we have updated anything save the changes. done here to stop repeated db hit.
            _occasionRepository.Save();
            return result;
        }

        public Occasion UpdateOccasion(MeetUpEvent meetupEvent, Occasion occasion)
        {
            var eventDateTime = meetupEvent.Time.FromUnixTime();
            if (occasion.IsDeleted) return null; // if it deleted no point updating it

            if (eventDateTime == occasion.OccasionDateTime && occasion.Title == meetupEvent.Name &&
                occasion.Description == meetupEvent.Description && meetupEvent.Venue.Name == occasion.Venue.Name &&
                meetupEvent.event_hosts.First().member_id == occasion.Host.MeetupMemberId) return occasion;

            // we do not have enough data from api call to create a user so get the members if we don't have the host
            var host = _userAccountRepository.FindByMeetUpId(meetupEvent.event_hosts.First().member_id);
            if (host == null)
            {
                // get members
                _memberService.GetMembersFromMeetUp(true);  // force update we know there are new ones
                host = _userAccountRepository.FindByMeetUpId(meetupEvent.event_hosts.First().member_id);
                
                if(host == null) {
                    // this rrings alarm bells as the host can not be found anywhere and the event can not be scheduled
                    Log.Error("unable to create event as host can not be found");
                    return null;   // if we still don't have the host don't update the event.
                }
            }
            
            occasion.Title = meetupEvent.Name;
            occasion.Date = eventDateTime.Date;
            occasion.Description = meetupEvent.Description;
            //occasion.MeetupLastUpdated = DateTime.Now;  is just for rsvps and not the event itself. comment so it won't be misused in future
            occasion.Hour = eventDateTime.Hour;
            occasion.Min = eventDateTime.Minute;
            occasion.Created = meetupEvent.created.FromUnixTime();
            if (occasion.Venue.MeetUpId != meetupEvent.Venue.Id)
                occasion.VenueId = _venueFactory.MapVenue(meetupEvent.Venue).Id;
            occasion.HostId = host.Id;
            return occasion;
        }

        public Occasion CreateOccasion(MeetUpEvent meetupEvent)
        {
            // we do not have enough data from api call to create a user so if the host doesn't exist don't go any further
            var host = _userAccountRepository.FindByMeetUpId(meetupEvent.event_hosts.First().member_id);
            if (host == null)
            {
                // get members from meetup api
                _memberService.GetMembersFromMeetUp();
                host = _userAccountRepository.FindByMeetUpId(meetupEvent.event_hosts.First().member_id);
            }
            if (host == null) return null;   // if we still don't have the host don't add the event.

            var eventDateTime = meetupEvent.Time.FromUnixTime();
            var result = new Occasion
            {
                Title = meetupEvent.Name,
                Date = eventDateTime.Date,
                Description = meetupEvent.Description,
                Created = DateTime.Now,
                MeetupEventId = meetupEvent.Id,
                //MeetupLastUpdated = DateTime.Now,  is just for rsvps and not for the event its self. comment so it won't be misused in future
                Hour = eventDateTime.Hour,
                Min = eventDateTime.Minute,
                IsDeleted = false,
                VenueId = _venueFactory.MapVenue(meetupEvent.Venue).Id,
                HostId = host.Id
            };
            _occasionRepository.Add(result);
            return result;
        }
        
        public void Dispose()
        {
            _occasionRepository.Dispose();
            _userAccountRepository.Dispose();
            _memberService.Dispose();
            _venueFactory.Dispose();
        }
    }
}
