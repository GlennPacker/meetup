using System;
using System.Linq;
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
        private readonly IOccasionFactory _occasionFactory;
        private readonly IOccasionRepository _occasionRepository;
        private readonly IServiceUtils _serviceUtils;
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        public MeetUpEventService(IMeetUpEventsProxy eventsProxy, IOccasionRepository occasionRepository, IOccasionFactory occasionFactory, IServiceUtils serviceUtils)
        {
            _eventsProxy = eventsProxy;
            _occasionRepository = occasionRepository;
            _occasionFactory = occasionFactory;
            _serviceUtils = serviceUtils;
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
        
        public IQueryable<Occasion> GetOccasions()
        {
            var data = _occasionRepository.List();
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
