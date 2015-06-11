using MeetUp.ApiProxy;
using MeetUp.ApiProxy.Models;

namespace MeetUp.MeetUpApi
{
    public class BaseProxy
    {
        public readonly string BaseUrl;
        private readonly IApiServices _services;
        private readonly string _key;
        public readonly string GroupUrl;

        public BaseProxy(IApiServices services, string key, string groupurl)
        {
            _services = services;
            BaseUrl = "https://api.meetup.com/";      // api url if this was to change nothing would work from meetup and meetup site defunct so... hard coded
            _key = "&key=" + key;
            GroupUrl = groupurl;
        }

        public Wrapper<T> Get<T>(string url) where T : new()
        {
            return _services.Get<T>(url + _key);
        }

        
    }
}