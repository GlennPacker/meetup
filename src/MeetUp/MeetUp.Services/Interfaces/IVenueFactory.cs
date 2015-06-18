using MeetUp.Domain;
using MeetUp.MeetUpApi.Models;

namespace MeetUp.Services.Interfaces
{
    public interface IVenueFactory
    {
        Venue MapVenue(MeetupVenue meetupVenue);
        Venue UpdateVenue(Venue venue, MeetupVenue meetupVenue);
        Venue CreateVenue(MeetupVenue meetupVenue);
        void Dispose();
    }
}