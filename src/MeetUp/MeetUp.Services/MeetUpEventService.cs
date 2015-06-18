using System.Linq;
using log4net;
using MeetUp.Core;
using MeetUp.Domain;
using MeetUp.MeetUpApi;
using MeetUp.MeetUpApi.Interfaces;
using MeetUp.Services.Interfaces;

namespace MeetUp.Services
{
    public class MeetUpEventService : IMeetUpEventService
    {
        private readonly IMeetUpEventsProxy _eventsProxy;
        private readonly IOccasionFactory _occasionFactory;
        private readonly IOccasionRepository _occasionRepository;
        
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        public MeetUpEventService(MeetUpEventsProxy eventsProxy, IOccasionRepository occasionRepository, IOccasionFactory occasionFactory)
        {
            _eventsProxy = eventsProxy;
            _occasionRepository = occasionRepository;
            _occasionFactory = occasionFactory;
        }

        public void GetEventsFromMeetUp()
        {
            var wrapper = _eventsProxy.GetMeetupEvents();

            if (wrapper.IsGood)
            {
                var meetupEvents = wrapper.Data.results.ToList();
                _occasionFactory.MapOccasions(meetupEvents);
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
        }
    }
}
