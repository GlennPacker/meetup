using System.Collections.Generic;
using System.Linq;
using MeetUp.Domain;

namespace MeetUp.Core
{
	public interface IRSVPRepository
	{
		IQueryable<RSVP> List(int? occasionId);
        IQueryable<RSVP> ListByMeetupId(IEnumerable<long> meetupIds);
		RSVP Find(int id);
        RSVP FindByMeetUpId(long id);
		bool Add(RSVP item);
		bool Delete(int id);
		bool Delete(RSVP item);
		bool Save();
        void Dispose();

        
	}
}