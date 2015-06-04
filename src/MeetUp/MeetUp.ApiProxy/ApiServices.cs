using System;
using System.Net;
using System.Text;
using MeetUp.ApiProxy.Models;
using Newtonsoft.Json;

namespace MeetUp.ApiProxy
{
    public class ApiServices : IApiServices
    {
        public Wrapper<T> Get<T>(string url) where T : new()
        {
            using (var webClient = new WebClient())
            {
                var wrapper = new Wrapper<T> { Url = url };
                try
                {
                    // attempt to download JSON data as a string
                    var jsonData = webClient.DownloadString(url);
                    // if string with JSON data is not empty, deserialize it to class and return its instance JsonConvert.DeserializeObject<T>
                    wrapper.Data = !string.IsNullOrEmpty(jsonData)
                        ? JsonConvert.DeserializeObject<T>(jsonData)
                        : new T();
                    wrapper.IsGood = true;
                    return wrapper;
                }
                catch (Exception ex)
                {
                    wrapper.IsGood = false;
                    wrapper.Error = ex.Message;
                    wrapper.Data = default(T);
                    return wrapper;
                }
            }
        }

        public Wrapper<T> Post<T>(string url, string data) where T : new()
        {
            using (var webClient = new WebClient())
            {
                var wrapper = new Wrapper<T>();
                try
                {
                    webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                    webClient.Encoding = Encoding.UTF8;
                    var jsonData = webClient.UploadString(url, data);

                    // if string with JSON data is not empty, deserialize it to class and return its instance JsonConvert.DeserializeObject<T>
                    wrapper.Data = !string.IsNullOrEmpty(jsonData)
                        ? JsonConvert.DeserializeObject<T>(jsonData)
                        : new T();
                    wrapper.IsGood = true;
                    return wrapper;
                }
                catch (Exception ex)
                {
                    wrapper.IsGood = false;
                    wrapper.Error = ex.Message;
                    wrapper.Data = default(T);
                    return wrapper;
                }

            }
        }

        public Wrapper<string> Delete(string url)
        {
            using (var webClient = new WebClient())
            {
                var wrapper = new Wrapper<string>();
                try
                {
                    webClient.UploadString(url, "DELETE", "");
                    wrapper.IsGood = true;
                    wrapper.Data = "Good";
                    return wrapper;
                }
                catch (Exception ex)
                {
                    wrapper.Error = ex.Message;
                    wrapper.Data = "Fail";
                    wrapper.IsGood = false;
                    return wrapper;
                }
            }
        }
    }
}