using System;
using System.Linq;
using MeetUp.Domain;

namespace MeetUp.Core
{
    public interface IMeetUpEventService
    {
        void GetEventsFromMeetUp(bool force = false);
        void GetEventRsvpFromMeetUp();
        IQueryable<Occasion> GetOccasionsByStartDate();
        IQueryable<Occasion> GetOccasionsFromDate(DateTime dateTime);
        void Dispose();

        Occasion Find(int id);
    }
}