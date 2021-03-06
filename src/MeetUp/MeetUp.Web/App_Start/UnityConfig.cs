using System;
using MeetUp.ApiProxy;
using MeetUp.Core;
using MeetUp.Infrastructure;
using MeetUp.MeetUpApi;
using MeetUp.MeetUpApi.Interfaces;
using MeetUp.Services;
using MeetUp.Services.Factories;
using MeetUp.Services.Interfaces;
using Microsoft.Practices.Unity;

namespace MeetUp.Web
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            container.RegisterType<IApiServices, ApiServices>();
            container.RegisterType<IMeetUpEventsProxy, MeetUpEventsProxy>(new InjectionConstructor(new ApiServices(), ConfigServices.MeetUpKey,ConfigServices.GroupUrl));
            container.RegisterType<IMeetUpMemberProxy, MeetUpMemberProxy>(new InjectionConstructor(new ApiServices(), ConfigServices.MeetUpKey, ConfigServices.GroupUrl));
            container.RegisterType<IMeetUpEventProxy, MeetUpEventProxy>(new InjectionConstructor(new ApiServices(), ConfigServices.MeetUpKey, ConfigServices.GroupUrl));

            // Factories
            container.RegisterType<IOccasionFactory, OccasionFactory>();
            container.RegisterType<IUserAccountFactory, UserAccountFactory>();
            container.RegisterType<IVenueFactory, VenueFactory>();
            container.RegisterType<IRsvpFactory, RsvpFactory>();

            // Services
            container.RegisterType<IMeetUpEventService, MeetUpEventService>();
            container.RegisterType<IMeetUpMemberService, MeetUpMemberService>();
            
            container.RegisterType<IServiceUtils, ServiceUtils>();

            // Repos
            container.RegisterType<IOccasionRepository, OccasionRepository>();
            container.RegisterType<IVenueRepository, VenueRepository>();
            container.RegisterType<IUserAccountRepository, UserAccountRepository>();
            container.RegisterType<IRunnerRepository, RunnerRepository>();
            container.RegisterType<IRSVPRepository, RSVPRepository>();
        }
    }
}
