using System.Collections.Generic;
using MeetUp.Domain;
using MeetUp.MeetUpApi.Models;

namespace MeetUp.Services.Interfaces
{
    public interface IOccasionFactory
    {
        List<Occasion> MapOccasions(List<MeetUpEvent> meetupEvents);
        Occasion UpdateOccasion(MeetUpEvent meetupEvent, Occasion occasion);
        Occasion CreateOccasion(MeetUpEvent meetupEvent);
        void Dispose();
    }
}