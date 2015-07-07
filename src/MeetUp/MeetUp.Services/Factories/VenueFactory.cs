using MeetUp.Core;
using MeetUp.Domain;
using MeetUp.MeetUpApi.Models;
using MeetUp.Services.Interfaces;

namespace MeetUp.Services.Factories
{
    public class VenueFactory : IVenueFactory
    {
        private readonly IVenueRepository _venueRepository;

        public VenueFactory(IVenueRepository venueRepository)
        {
            _venueRepository = venueRepository;
        }

        public Venue MapVenue(MeetupVenue meetupVenue)
        {
            var venue = _venueRepository.FindByMeetUpId(meetupVenue.Id);
            return venue == null ? CreateVenue(meetupVenue) : UpdateVenue(venue, meetupVenue);
        }

        public Venue UpdateVenue(Venue venue, MeetupVenue meetupVenue)
        {
            if (venue.Address1 == meetupVenue.Address_1 && venue.City == meetupVenue.City && venue.IsDeleted == false &&
                venue.Name == meetupVenue.Name) return venue;

            venue.Address1 = meetupVenue.Address_1;
            venue.City = meetupVenue.City;
            venue.IsDeleted = false;
            venue.Name = meetupVenue.Name;
            _venueRepository.Update(venue);
            return venue;
        }

        public Venue CreateVenue(MeetupVenue meetupVenue)
        {
            var venue = new Venue
            {
                Address1 = meetupVenue.Address_1,
                City = meetupVenue.City,
                IsDeleted = false,
                MeetUpId = meetupVenue.Id,
                Name = meetupVenue.Name
            };
            _venueRepository.Add(venue);
            return venue;
        }


        public void Dispose()
        {
            _venueRepository.Dispose();
        }
    }
}
