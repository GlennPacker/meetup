using MeetUp.ApiProxy;
using MeetUp.ApiProxy.Models;
using MeetUp.MeetUpApi.Interfaces;
using MeetUp.MeetUpApi.Models;

namespace MeetUp.MeetUpApi
{
    public class MeetUpEventsProxy: BaseProxy, IMeetUpEventsProxy
    {
        public MeetUpEventsProxy(IApiServices services, string key, string groupurl) : base(services, key, groupurl)
        {
        }

        public Wrapper<MeetupEvents> GetMeetupEvents()
        {
            var url = BaseUrl + "2/events?&sign=true&photo-host=public&group_urlname=" + GroupUrl + 
                "&fields = event_hosts & page = 20 & omit = fee, maybe_rsvp_count, visibility, utc_offset, announced, " +
                "waitlist_count, yes_rsvp_count, how_to_find_us, event_url, created, group, headcount, status";     // add in the rest of the url
            var result = Get<MeetupEvents>(url); 
            return result;
        }


        //public Wrapper<ApiFoo> PostApiObject(ApiFoo item)
        //{
        //    var results = new List<ValidationResult>();
        //    var isValid = Validator.TryValidateObject(item, new ValidationContext(item), results);
        //    if (!isValid)
        //    {
        //        string error = null;
        //        foreach (var modelError in results) error += modelError.ErrorMessage;

        //        var apiErrorResult = new Wrapper<ApiFoo> { Data = item, Error = error, IsGood = false };
        //        return apiErrorResult;
        //    }

        //    var url = _baseUrl + "controller";
        //    var postData = JsonConvert.SerializeObject(item);
        //    var result = _services.Post<ApiFoo>(url, postData);
        //    return result;
        //}

        //public Wrapper<string> DeleteApiObject(int id)
        //{
        //    var url = _baseUrl + "controller/" + id;
        //    return _services.Delete(url);
        //}



    }
}
