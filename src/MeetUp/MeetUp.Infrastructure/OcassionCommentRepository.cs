using System;
using System.Data.Entity;
using System.Linq;
using MeetUp.Core;
using MeetUp.Domain;

namespace MeetUp.Infrastructure
{
	public class OcassionCommentRepository : IOcassionCommentRepository
	{
		private readonly MeetUpContext _db;

		public OcassionCommentRepository(MeetUpContext db)
		{
			_db = db;
		}


		public IQueryable<OcassionComment> List(int? occasionId)
		{
			var data = _db.OcassionComments;
			return occasionId == null ? data : data.Where(r => r.OcassionId == occasionId);
		}

		public OcassionComment Find(int id)
		{
			var data = _db.OcassionComments.Find(id);
			return data;
		}

		public bool Add(OcassionComment item)
		{
			_db.OcassionComments.Add(item);
			return Save();
		}

		public bool Update(OcassionComment item)
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

		public bool Delete(OcassionComment item)
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
				// logger needs adding here
				return false;
			}
		}

	}
}
