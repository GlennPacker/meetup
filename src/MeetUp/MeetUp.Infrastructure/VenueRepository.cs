using System;
using System.Data.Entity;
using System.Linq;
using log4net;
using MeetUp.Core;
using MeetUp.Domain;

namespace MeetUp.Infrastructure
{
	public class VenueRepository : IVenueRepository
	{
		private readonly MeetUpContext _db;
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		public VenueRepository(MeetUpContext db)
		{
			_db = db;
		}

		public IQueryable<Venue> List()
		{
			var data = _db.Venues.Where(r => !r.IsDeleted);	// Can't see a need for showing deleted. 
			return data;
		}


		public Venue Find(int id)
		{
			var data = _db.Venues.Find(id);
			return data;
		}

	    public Venue FindByMeetUpId(long id)
	    {
	        var data = _db.Venues.FirstOrDefault(r => r.MeetUpId == id);
            return data;
	    }

	    public bool Add(Venue item)
		{
			_db.Venues.Add(item);
			return Save();
		}

		public bool Update(Venue item)
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

		public bool Delete(Venue item)
		{
			item.IsDeleted = true;		// logical as tied data
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
                Log.Error("Database Error Entity Venue", ex);
				return false;
			}
		}


        public void Dispose()
        {
            _db.Dispose();
        }


	}
}
