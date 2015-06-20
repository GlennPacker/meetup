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

        public bool ShouldUpdate(bool force, DateTime updateIfOlderThan, ApiType apiType, int? refId)
        {
            if (force)
            {
                _runnerRepository.StartUpdate(apiType, refId);
                return true;
            }

            // there is no point in updating the y every few seconds so lets see when last update was
            var lastrun = _runnerRepository.GetLastRun(apiType, refId);
            // if last update is in the last x hours don't get new y as it is unlikely there is new
            if (lastrun != null && Convert.ToDateTime(lastrun) >= updateIfOlderThan)
            {
                _runnerRepository.StartUpdate(apiType, refId);
                return true;
            }
            return false;
        }

        public void UpdateLastRun(ApiType meetUpEvents, int? refId)
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
