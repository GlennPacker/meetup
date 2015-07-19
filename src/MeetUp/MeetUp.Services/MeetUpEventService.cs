using System;
using System.Linq;
using Glimpse.AspNet.Tab;
using log4net;
using MeetUp.Core;
using MeetUp.Domain;
using MeetUp.MeetUpApi.Interfaces;
using MeetUp.Services.Interfaces;

namespace MeetUp.Services
{
    public class MeetUpEventService : IMeetUpEventService
    {
        private readonly IMeetUpEventsProxy _eventsProxy;
        private readonly IMeetUpEventProxy _eventProxy;
        private readonly IOccasionFactory _occasionFactory;
        private readonly IOccasionRepository _occasionRepository;
        private readonly IServiceUtils _serviceUtils;
        private readonly IRsvpFactory _rsvpFactory;

        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        public MeetUpEventService(IMeetUpEventsProxy eventsProxy, IOccasionRepository occasionRepository, IOccasionFactory occasionFactory, IServiceUtils serviceUtils, IMeetUpEventProxy eventProxy, IRsvpFactory rsvpFactory)
        {
            _eventsProxy = eventsProxy;
            _occasionRepository = occasionRepository;
            _occasionFactory = occasionFactory;
            _serviceUtils = serviceUtils;
            _eventProxy = eventProxy;
            _rsvpFactory = rsvpFactory;
        }

        public void GetEventsFromMeetUp(bool force = false)
        {
            if (! _serviceUtils.ShouldUpdate(force, DateTime.Now.AddHours(-3), ApiType.MeetUpEvents, null)) return;
            
            var wrapper = _eventsProxy.GetMeetupEvents();
            if (wrapper.IsGood)
            {
                var meetupEvents = wrapper.Data.results.ToList();
                _occasionFactory.MapOccasions(meetupEvents);
                // add runner and time
                _serviceUtils.UpdateLastRun(ApiType.MeetUpEvents, null);
            }
            else
            {
                Log.Error(wrapper.Error, wrapper.ErrorException);
            }
        }

        public void GetEventRsvpFromMeetUp()
        {
            // routines purpose is to try to update events/occasions as often as possible without hitting meetup api limits
            var now = DateTime.Now;
            var dt = now.AddDays(-2);
            var data = _occasionRepository.List().Where(r => r.Date > dt).ToList();

            data = data.Where(r => r.Title == "Beach BBQ").ToList();
            
            
            foreach (var occasion in data)
            {
                if (occasion.MeetupEventId == null) continue;

                if(occasion.Created > now.AddDays(-1))     // just been created so likely for heavy activity
                {
                    if(occasion.MeetupLastUpdated == null || occasion.MeetupLastUpdated < DateTime.Now.AddMinutes(-15))
                        GetEventFromMeetup((long)occasion.MeetupEventId, occasion);
                    continue;
                }

                if ((occasion.OccasionDateTime > now.AddDays(-1)  && occasion.OccasionDateTime < now.AddDays(1)))  // last day or day just gone everything changes so often just grab the data from api   
                {
                    if (occasion.MeetupLastUpdated < DateTime.Now.AddMinutes(-5)) // as long as it hasn't been done in the last 5 mins
                        GetEventFromMeetup((long)occasion.MeetupEventId, occasion);
                    continue;
                }
                
                if (occasion.OccasionDateTime < now.AddDays(7)) // event/occasion this week
                {
                    if(occasion.MeetupLastUpdated < DateTime.Now.AddHours(-3))
                        GetEventFromMeetup((long)occasion.MeetupEventId, occasion);
                    continue;
                }
                if (occasion.OccasionDateTime > now.AddDays(-7) && occasion.MeetupLastUpdated < DateTime.Now.AddHours(-8)) // anything else 
                        GetEventFromMeetup((long) occasion.MeetupEventId, occasion); // anything after next week just update three times a day little changes this far ahead
            }
            _occasionRepository.Save();
        }

        public void GetEventFromMeetup(long meetupId, Occasion occasion)
        {
            // check the update hasnt' just been called by somthing or someone else.
            var lastrunstillrunning = _serviceUtils.GetLastRun(ApiType.MeetUpEvent, meetupId);
            if (!(lastrunstillrunning == null || lastrunstillrunning < DateTime.Now.AddMinutes(-2))) return;
            var wrapper = _eventProxy.GetMeetupEvent(meetupId);
            if (wrapper.IsGood)
            {
                var meetupEvent = wrapper.Data.results.ToList();
                _rsvpFactory.MapRsvps(meetupEvent, occasion);
                
                // add runner and time
                _serviceUtils.UpdateLastRun(ApiType.MeetUpEvents, null);
            }
            else
            {
                Log.Error(wrapper.Error, wrapper.ErrorException);
            }

        }


        public IQueryable<Occasion> GetOccasionsFromDate(DateTime dateTime)
        {
            var data = _occasionRepository.List().Where(r => r.Date > dateTime);
            return data;
        }


        

        public Occasion Find(int id)
        {
            var data = _occasionRepository.Find(id);
            return data;
        }

        public void Dispose()
        {
            _occasionFactory.Dispose();
            _occasionRepository.Dispose();
            _serviceUtils.Dispose();
        }

        
    }
}
