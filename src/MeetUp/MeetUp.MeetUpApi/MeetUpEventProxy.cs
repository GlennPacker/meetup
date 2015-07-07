using System;
using log4net;
using MeetUp.ApiProxy;
using MeetUp.ApiProxy.Models;
using MeetUp.MeetUpApi.Interfaces;
using MeetUp.MeetUpApi.Models;

namespace MeetUp.MeetUpApi
{
    public class MeetUpEventProxy: BaseProxy, IMeetUpEventProxy
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public MeetUpEventProxy(IApiServices services, string key, string groupurl) : base(services, key, groupurl)
        {
        }

        public Wrapper<MeetupRSVPObject> GetMeetupEvent(long meetUpid)
        {
            var url = BaseUrl + "/2/rsvps?omit=event%2Cvenue%2Cgroup%2Cmtime&event_id=" + meetUpid +
                      "&order=event&desc=false&offset=0&photo-host=public&format=json&page=60&fields=member_bio";
           
            try
            {
                var result = Get<MeetupRSVPObject>(url);
                return result;
            }

            catch (Exception ex)
            {
              Log.Error("Unable to get event data from meetup", ex);
              return new Wrapper<MeetupRSVPObject>
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
