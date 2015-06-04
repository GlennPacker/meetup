using System.Collections.Generic;
using MeetUp.ApiProxy;
using MeetUp.ApiProxy.Models;
using Newtonsoft.Json;

namespace MeetUp.MeetUpApi
{
    public class ExampleProxy
    {
        private readonly string _baseUrl;
        private readonly IApiServices _services;

		public ExampleProxy()
		{
            // empty constructor making unit testing easier
			_services = new ApiServices();
			_baseUrl = "";      // api url 
		}

        public ExampleProxy(string baseUrl)
        {
            _services = new ApiServices();
            _baseUrl = baseUrl;      // api url url from di
        }

        public ExampleProxy(string baseUrl, IApiServices services)
        {
            _services = services;
            _baseUrl = baseUrl;      // api url 
        }

        //public Wrapper<ApiFoo> GetApiObject(int id)     
        //{
        //    var url = _baseUrl + "controller/" + id;     // add in the rest of the url
        //    var result = _services.Get<ApiFoo>(url);
        //    return result;
        //}


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
