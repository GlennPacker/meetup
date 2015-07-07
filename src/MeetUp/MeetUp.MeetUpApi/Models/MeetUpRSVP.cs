using System;

namespace MeetUp.MeetUpApi.Models
{
    public class MeetupRSVP
    {
        public Int64 Id { get; set; }
        public long Time { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public MeetupVenue Venue { get; set; }
        public event_hosts[] event_hosts { get; set; }
        public string response { get; set; }
        public Member member { get; set; }
        public Member_Photo member_photo { get; set; }
        public int guests { get; set; }
        public Int64 created { get; set; }
        public long rsvp_id { get; set; }
    }
}