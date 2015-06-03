using System;
using System.Data.Entity;
using MeetUp.Core;
using MeetUp.Domain;

namespace MeetUp.Infrastructure
{
	public class UserPicRepository : IUserPicRepository
	{
		private readonly MeetUpContext _db;

		public UserPicRepository(MeetUpContext db)
		{
			_db = db;
		}
		
		public UserPic Find(int id)
		{
			var data = _db.UserPics.Find(id);
			return data;
		}

		public bool Add(UserPic item)
		{
			_db.UserPics.Add(item);
			return Save();
		}

		public bool Update(UserPic item)
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

		public bool Delete(UserPic item)
		{
			_db.UserPics.Remove(item);		// physical this could end up with byte data and large object
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
