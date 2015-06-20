using System;
using System.Data.Entity;
using System.Linq;
using log4net;
using MeetUp.Core;
using MeetUp.Domain;

namespace MeetUp.Infrastructure
{
	public class EmailRepository : IEmailRepository
	{
		private readonly MeetUpContext _db;
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


		public EmailRepository(MeetUpContext db)
		{
			_db = db;
		}


		public IQueryable<Email> List(bool isDeleted = false)
		{
			var data = _db.Emails;
			return isDeleted ? data.Where(r => r.IsDeleted) : data.Where(r => !r.IsDeleted);
		}

		public Email Find(int id)
		{
			var data = _db.Emails.Find(id);
			return data;
		}
		
		public bool Add(Email item)
		{
			_db.Emails.Add(item);
			return Save();
		}

		public bool Update(Email item)
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

		public bool Delete(Email item)
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
                Log.Error("Database Error Entity Email", ex);
				return false;
			}
		}


        public void Dispose()
        {
            _db.Dispose();
        }

	}
}
