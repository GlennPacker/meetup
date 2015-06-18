using System;

namespace MeetUp.Services.ApiModels
{
    public class ApiOccasionComment
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public int UserId { get; set; }
        public virtual ApiUserAccount User { get; set; }
        public DateTime Added { get; set; }
    }
}