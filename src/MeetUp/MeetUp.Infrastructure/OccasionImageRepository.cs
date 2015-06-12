using System;
using System.Data.Entity;
using System.Linq;
using log4net;
using MeetUp.Core;
using MeetUp.Domain;

namespace MeetUp.Infrastructure
{
	public class OccasionImageRepository : IOccasionImageRepository
	{
		private readonly MeetUpContext _db;
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		public OccasionImageRepository(MeetUpContext db)
		{
			_db = db;
		}


		public IQueryable<OccasionImage> List(int? occasionId)
		{
			var data = _db.OccasionImages;
			return occasionId == null ? data : data.Where(r => r.OccasionId == occasionId);
		}

		public OccasionImage Find(int id)
		{
			var data = _db.OccasionImages.Find(id);
			return data;
		}

		public bool Add(OccasionImage item)
		{
			_db.OccasionImages.Add(item);
			return Save();
		}

		public bool Update(OccasionImage item)
		{
			// not sure if this is required maybe a comment edit??
			_db.Entry(item).State = EntityState.Modified;
			return Save();
		}

		public bool Delete(int id)
		{
			var data = Find(id);
			if (data == null) return true; // if it can't be found then it is not there and therefore we could argue and say it already has a state of deleted so just return true;
			return Delete(data);
		}

		public bool Delete(OccasionImage item)
		{
			_db.OccasionImages.Remove(item);	// if we end up taking images to bytes then this could be large object so physical
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
                Log.Error("Database Error Entity OccasionImage", ex);
				return false;
			}
		}

	}
}
