﻿using System.Collections.Generic;
using System.Linq;
using MeetUp.Domain;

namespace MeetUp.Core
{
	public interface IOccasionRepository
	{
		IQueryable<Occasion> List(bool includeDeleted = false);
        IQueryable<Occasion> List(List<long> meetupEventIds);
		Occasion Find(int id);
		Occasion FindByMeetUp(double meetUpEventId);
		bool Add(Occasion item);
		bool Update(Occasion item);
		bool Delete(int id);
		bool Delete(Occasion item);
		bool Save();
	    void Dispose();
	}
}