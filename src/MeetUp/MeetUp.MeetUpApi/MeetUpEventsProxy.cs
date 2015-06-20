using System;
using log4net;
using MeetUp.ApiProxy;
using MeetUp.ApiProxy.Models;
using MeetUp.MeetUpApi.Interfaces;
using MeetUp.MeetUpApi.Models;

namespace MeetUp.MeetUpApi
{
    public class MeetUpEventsProxy: BaseProxy, IMeetUpEventsProxy
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public MeetUpEventsProxy(IApiServices services, string key, string groupurl) : base(services, key, groupurl)
        {
        }

        public Wrapper<MeetupEvents> GetMeetupEvents()
        {
            var url = BaseUrl + "2/events?&sign=true&photo-host=public&group_urlname=" + GroupUrl +
          "&fields=event_hosts&page=40&omit=fee,maybe_rsvp_count,visibility,utc_offset,announced," +
          "waitlist_count,yes_rsvp_count,how_to_find_us,event_url,created,group,headcount,status";
           
            try
            {
                var result = Get<MeetupEvents>(url);
                return result;
            }

            catch (Exception ex)
            {
              Log.Error("Unable to get event data from meetup", ex);
              return new Wrapper<MeetupEvents>
                {
                    Error = ex.Message,
                    IsGood = false,
                    Data = null,
                    ErrorException = ex,
                    Url = url
                };
            }
        }
    }
}
