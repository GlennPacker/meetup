using System;

namespace MeetUp.ApiProxy.Models
{
    public class Wrapper<T>
    {
        public T Data { get; set; }
        public string Error { get; set; }
        public Exception ErrorException { get; set; }
        public bool IsGood { get; set; }
        public string Url { get; set; }
    }
}
