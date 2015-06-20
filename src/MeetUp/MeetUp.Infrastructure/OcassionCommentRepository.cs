using System;
using System.Data.Entity;
using System.Linq;
using log4net;
using MeetUp.Core;
using MeetUp.Domain;

namespace MeetUp.Infrastructure
{
	public class OcassionCommentRepository : IOcassionCommentRepository
	{
		private readonly MeetUpContext _db;
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


		public OcassionCommentRepository(MeetUpContext db)
		{
			_db = db;
		}


		public IQueryable<OccasionComment> List(int? occasionId)
		{
			var data = _db.OcassionComments;
			return occasionId == null ? data : data.Where(r => r.OcassionId == occasionId);
		}

		public OccasionComment Find(int id)
		{
			var data = _db.OcassionComments.Find(id);
			return data;
		}

		public bool Add(OccasionComment item)
		{
			_db.OcassionComments.Add(item);
			return Save();
		}

		public bool Update(OccasionComment item)
		{
			_db.Entry(item).State = EntityState.Modified;
			return Save();
		}

		public bool Delete(int id)
		{
			var data = Find(id);
			if (data == null) return true; // if it can't be found then it is not there and therefore we could argue and say it already has a state of deleted so just return true;
			return Delete(data);
		}

		public bool Delete(OccasionComment item)
		{
			_db.OcassionComments.Remove(item);	// i don't see any reason to keep comments so physical delete
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
                Log.Error("Database Error Entity OccasionComment", ex);
				return false;
			}
		}


        public void Dispose()
        {
            _db.Dispose();
        }


	}
}
