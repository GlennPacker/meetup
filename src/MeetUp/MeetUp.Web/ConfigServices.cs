using System.Configuration;
using System.Web.Configuration;

namespace MeetUp.Web
{
    public static class ConfigServices
    {
        public static readonly string MeetUpKey = WebConfigurationManager.AppSettings["MeetUpKey"];
        public static readonly string GroupUrl = WebConfigurationManager.AppSettings["GroupUrl"];
        public static readonly string GroupSiteName = WebConfigurationManager.AppSettings["GroupSiteName"];
    }
}