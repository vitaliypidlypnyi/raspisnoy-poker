using Microsoft.Owin;
using Microsoft.AspNet.SignalR;
using Owin;
using Poker.Server.Pipelines;

[assembly: OwinStartup(typeof(Poker.Server.Startup))]

namespace Poker.Server
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalHost.HubPipeline.AddModule(new ErrorHandlingPipelineModule());
            GlobalHost.HubPipeline.AddModule(new LoggingPipelineModule());

            app.MapSignalR("/hubs", new HubConfiguration { EnableDetailedErrors = true});
        }
    }
}
