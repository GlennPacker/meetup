using System;
using System.Collections.Generic;
using MeetUp.MeetUpApi.Helpers;

namespace MeetUp.Services.ApiModels
{
    public class ApiOccasionInfo
    {
        public ApiOccasionInfo()
        {
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int HostId { get; set; }
        public ApiUserAccount Host { get; set; }
        public int VenueId { get; set; }
        public DateTime OccasionDateTime { get; set; }
        public List<ApiRSVP> Rsvp { get; set; }
        public ApiVenue Venue { get; set; }
        public DateTime? MeetupLastUpdated { get; set; }

        public double JavascriptOccasionDateTime
        {
            get { return OccasionDateTime.ToJavascriptDateTime(); }
        }

        public double JavascriptLastUpdated
        {
            get
            {
                return MeetupLastUpdated == null? 0  : Convert.ToDateTime(MeetupLastUpdated).ToJavascriptDateTime();
            }
        }

        public bool forceUpdate
        {
            get
            {
                var lastdayOrJustgone = OccasionDateTime.AddDays(1) > DateTime.Now && DateTime.Now > OccasionDateTime.AddDays(-1);
                var hadRecentUpdate = MeetupLastUpdated != null && MeetupLastUpdated > DateTime.Now.AddMinutes(-5);
                return lastdayOrJustgone && !hadRecentUpdate;
            }
        }
    }
}