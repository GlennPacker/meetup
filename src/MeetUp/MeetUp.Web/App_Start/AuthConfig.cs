using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web.Configuration;
using Microsoft.Web.WebPages.OAuth;
using MeetUp.Web.Models;

namespace MeetUp.Web
{
    public static class AuthConfig
    {
        public static void RegisterAuth()
        {
            // To let users of this site log in using their accounts from other sites such as Microsoft, Facebook, and Twitter,
            // you must update this site. For more information visit http://go.microsoft.com/fwlink/?LinkID=252166

            //OAuthWebSecurity.RegisterMicrosoftClient(
            //    clientId: "",
            //    clientSecret: "");

            //OAuthWebSecurity.RegisterTwitterClient(
            //    consumerKey: "",
            //    consumerSecret: "");

            OAuthWebSecurity.RegisterFacebookClient(
                appId: WebConfigurationManager.AppSettings["FaceBookAppId"],
                appSecret: WebConfigurationManager.AppSettings["FaceBookAppSecret"]);

            //OAuthWebSecurity.RegisterGoogleClient();
        }
    }
}
