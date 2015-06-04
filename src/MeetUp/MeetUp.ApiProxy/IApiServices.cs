using MeetUp.ApiProxy.Models;

namespace MeetUp.ApiProxy
{
    public interface IApiServices
    {
        Wrapper<T> Get<T>(string url) where T : new();
        Wrapper<T> Post<T>(string url, string data) where T : new();
        Wrapper<string> Delete(string url);
    }
}