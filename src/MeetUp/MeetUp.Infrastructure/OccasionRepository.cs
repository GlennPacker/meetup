using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using log4net;
using MeetUp.Core;
using MeetUp.Domain;

namespace MeetUp.Infrastructure
{
	public class OccasionRepository : IOccasionRepository
	{
		private readonly MeetUpContext _db;
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


		public OccasionRepository(MeetUpContext db)
		{
			_db = db;
		}


		public IQueryable<Occasion> List(bool includeDeleted = false)
		{
			var data = _db.Occasions;
			return includeDeleted ? data : data.Where(r => !r.IsDeleted);
		}

	    public IQueryable<Occasion> List(List<long> meetupEventIds)
	    {
            var data = List().Include(h => h.Host).Include(v => v.Venue).Where(r => meetupEventIds.Contains(r.MeetupEventId ?? 0));
	        return data;
	    }

	    public Occasion Find(int id)
		{
			var data = _db.Occasions.Find(id);
			return data;
		}

		public Occasion FindByMeetUp(double meetUpEventId)
		{
			var data = _db.Occasions.FirstOrDefault(r => r.MeetupEventId == meetUpEventId);
			return data;
		}

		public bool Add(Occasion item)
		{
			_db.Occasions.Add(item);
			return Save();
		}

		public bool Update(Occasion item)
		{
			_db.Entry(item).State = EntityState.Modified;
			return Save();
		}

		public bool Delete(int id)
		{
			var data = Find(id);
			if (data == null) return true; // if it can't be found then it is not there and therefore we could say it already has a state of deleted so just return true;
			return Delete(data);
		}

		public bool Delete(Occasion item)
		{
			item.IsDeleted = true;
			return Save();
		}

		public bool Save()
		{
			try
			{
				_db.SaveChanges();
				return true;
			}
			catch(Exception ex)
			{
                Log.Error("Database Error Entity Occasion", ex);
				return false;
			}
		}

	    public void Dispose()
	    {
	        _db.Dispose();
	    }
	}
}
