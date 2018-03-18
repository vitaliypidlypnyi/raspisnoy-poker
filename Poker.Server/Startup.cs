using Microsoft.Owin;
using Microsoft.AspNet.SignalR;
using Owin;
using Poker.Server.Pipelines;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Poker.Server.Identity;
using Microsoft.AspNet.Identity;
using System;
using Poker.Server.Middleware;
using System.Web.Http;

[assembly: OwinStartup(typeof(Poker.Server.Startup))]

namespace Poker.Server
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            config.Routes.MapHttpRoute(
                "Default",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional });

            GlobalHost.HubPipeline.AddModule(new ErrorHandlingPipelineModule());
            GlobalHost.HubPipeline.AddModule(new LoggingPipelineModule());

            app.MapSignalR("/hubs", new HubConfiguration { EnableDetailedErrors = true});

            app.ConfigureDependencies(config)
               .ConfigureAuth();

            app.UseWebApi(config);
        }
    }
}
