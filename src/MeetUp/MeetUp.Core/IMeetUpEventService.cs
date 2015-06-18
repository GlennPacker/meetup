using System.Linq;
using MeetUp.Domain;

namespace MeetUp.Core
{
    public interface IMeetUpEventService
    {
        void GetEventsFromMeetUp();
        IQueryable<Occasion> GetOccasions();
        void Dispose();
    }
}