using System;
using log4net;
using MeetUp.ApiProxy;
using MeetUp.ApiProxy.Models;
using MeetUp.MeetUpApi.Interfaces;
using MeetUp.MeetUpApi.Models;

namespace MeetUp.MeetUpApi
{
    public class MeetUpMemberProxy: BaseProxy, IMeetUpMemberProxy
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public MeetUpMemberProxy(IApiServices services, string key, string groupurl) : base(services, key, groupurl)
        {
        }

        public Wrapper<MeetUpMembers> GetMeetupMembers(int i)
        {
            var url = BaseUrl + "/2/members?&sign=true&group_urlname=" + GroupUrl + "&page=200&offset=" + i.ToString();
            
            try
            {
                var result = Get<MeetUpMembers>(url);
                return result;
            }

            catch (Exception ex)
            {
              Log.Error("Unable to get event data from meetup", ex);
              return new Wrapper<MeetUpMembers>
                {
                    Error = ex.Message,
                    IsGood = false,
                    Data = null,
                    ErrorException = ex,
                    Url = url
                };
            }
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
