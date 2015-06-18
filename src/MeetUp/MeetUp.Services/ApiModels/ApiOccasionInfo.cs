using System;
using MeetUp.Services.ApiModels;

namespace MeetUp.Domain
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
        public Venue Venue { get; set; }
	    public DateTime OccasionDateTime { get; set; }
    }
}