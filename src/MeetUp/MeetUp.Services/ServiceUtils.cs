using System;
using MeetUp.Core;
using MeetUp.Domain;
using MeetUp.Services.Interfaces;

namespace MeetUp.Services
{
    public class ServiceUtils : IServiceUtils
    {
        private readonly IRunnerRepository _runnerRepository;

        public ServiceUtils(IRunnerRepository runnerRepository)
        {
            _runnerRepository = runnerRepository;
        }

        #region Runner

        public bool ShouldUpdate(bool force, DateTime updateIfOlderThan, ApiType apiType, long? refId)
        {
            if (force)
            {
                _runnerRepository.StartUpdate(apiType, refId);
                return true;
            }

            // there is no point in updating the y every few seconds so lets see when last update was
            var lastrun = GetLastRun(apiType, refId);
            // if last update is in the last x hours don't get new y as it is unlikely there is new
            if (lastrun == null || Convert.ToDateTime(lastrun) <= updateIfOlderThan)
            {
                _runnerRepository.StartUpdate(apiType, refId);
                return true;
            }
            return false;
        }

        public DateTime? GetLastRun(ApiType apiType, long? refId)
        {
            var data = _runnerRepository.GetLastRun(apiType, refId);
            return data;
        }

        public void UpdateLastRun(ApiType meetUpEvents, long? refId)
        {
            _runnerRepository.Update(ApiType.MeetUpEvents, null);
        }

        #endregion

        public void Dispose()
        {
            _runnerRepository.Dispose();
        }


    }
}
