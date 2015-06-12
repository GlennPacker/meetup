using System;
using System.Data.Entity;
using System.Linq;
using log4net;
using MeetUp.Core;
using MeetUp.Domain;

namespace MeetUp.Infrastructure
{
	public class UserAccountRepository : IUserAccountRepository
	{
		private readonly MeetUpContext _db;
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		public UserAccountRepository(MeetUpContext db)
		{
			_db = db;
		}


		public UserAccount Find(int id)
		{
			var data = _db.UserAccounts.Find(id);
			return data;
		}

		public UserAccount FindByMeetUp(double meetUpEventId)
		{
			var data = _db.UserAccounts.FirstOrDefault(r => r.MeetupMemberId == meetUpEventId);
			return data;
		}

		public bool Add(UserAccount item)
		{
			_db.UserAccounts.Add(item);
			return Save();
		}

		public bool Update(UserAccount item)
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

		public bool Delete(UserAccount item)
		{
			item.IsDeleted = true;		// logical as could break db or end up deleting way to much data
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
                Log.Error("Database Error Entity UserAccount", ex);
				return false;
			}
		}

	}
}
