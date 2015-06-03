using System;
using System.Linq;
using MeetUp.Core;
using MeetUp.Domain;

namespace MeetUp.Infrastructure
{
	public class RunnerRepository : IRunnerRepository
	{
		private readonly MeetUpContext _db;

		public RunnerRepository(MeetUpContext db)
		{
			_db = db;
		}


		public IQueryable<Runner> List()
		{
			var data = _db.Runners;
			return data;
		}

		public Runner Find(int id)
		{
			var data = _db.Runners.Find(id);
			return data;
		}

		public bool Add(Runner item)
		{
			_db.Runners.Add(item);
			return Save();
		}
		
		public bool Delete(int id)
		{
			var data = Find(id);
			if (data == null) return true; // if it can't be found then it is not there and therefore we could argue and say it already has a state of deleted so just return true;
			return Delete(data);
		}

		public bool Delete(Runner item)
		{
			_db.Runners.Remove(item);	// can't see a reason to keep to physical
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
