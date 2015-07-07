using System;
using System.Linq;
using log4net;
using MeetUp.Core;
using MeetUp.Domain;

namespace MeetUp.Infrastructure
{
	public class RunnerRepository : IRunnerRepository
	{
		private readonly MeetUpContext _db;
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

		public RunnerRepository(MeetUpContext db)
		{
			_db = db;
		}

        public bool Update(ApiType apiType, long? refId)
	    {
            var data = Find(apiType, refId);           
            data.LastRun = DateTime.Now;
	        return Save();
	    }

        public DateTime? GetLastRun(ApiType apiType, long? refId)
	    {
            var data = Find(apiType, refId);
            if (data == null) {return null;}
            // concurrency check with failure
	        return data.Started > DateTime.Now.AddMinutes(-2) ? DateTime.Now : data.LastRun;
	    }

	    public void StartUpdate(ApiType apiType, long? refId)
	    {
	        var data = Find(apiType, refId);
	        if (data == null)
	        {
	            Add(new Runner {ApiType = apiType, LastRun = DateTime.Now, RefId = refId, Started = DateTime.Now});
	        }
	        else
	        {
	            data.Started = DateTime.Now;
	        }
	        Save();
	    }

        public Runner Find(ApiType apiType, long? refId)
        {
            var data = _db.Runners.Where(r => r.ApiType == apiType);
            return refId == null ? data.FirstOrDefault() : data.FirstOrDefault(r => r.RefId == refId);
        }

		public bool Add(Runner item)
		{
			_db.Runners.Add(item);
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
				Log.Error("Database Error Entity Runner", ex);
				return false;
			}
		}


        public void Dispose()
        {
            _db.Dispose();
        }


	}
}
