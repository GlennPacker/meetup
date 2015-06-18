using System;
using System.Linq;
using log4net;
using MeetUp.MeetUpApi.Interfaces;
using MeetUp.Services.Interfaces;

namespace MeetUp.Services
{
    public class MeetUpMemberService : IMeetUpMemberService
    {
        private readonly IMeetUpMemberProxy _memberProxy;
        private readonly IUserAccountFactory _userFactory;
        
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public MeetUpMemberService(IMeetUpMemberProxy memberProxy, IUserAccountFactory userFactory)
        {
            _memberProxy = memberProxy;
            _userFactory = userFactory;
        }


        public void GetMembersFromMeetUp()
        {
            var getmore = true;
            var i = 0;
            while (getmore)
            {
                try
                {
                    var wrapper = _memberProxy.GetMeetupMembers(i);
                    i++;
                    if (wrapper.IsGood)
                    {
                        var members = wrapper.Data.results.ToList();
                        _userFactory.MapUsers(members);
                        if (members.Count != 200) getmore = false;
                    }
                    else
                    {
                        Log.Error(wrapper.Error, wrapper.ErrorException);
                        getmore = false;
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message, ex);
                }
            }
        }

        public void Dispose()
        {
            _userFactory.Dispose();
        }
    }
}
