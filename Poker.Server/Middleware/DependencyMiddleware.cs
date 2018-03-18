using System.Collections.Generic;
using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Owin;
using Poker.Core.Abstraction;
using Poker.Server.DI;
using Poker.Server.Services;
using Autofac.Integration.WebApi.Owin;

namespace Poker.Server.Middleware
{
    internal static class DependencyMiddleware
    {
        public static IAppBuilder ConfigureDependencies(this IAppBuilder app, HttpConfiguration config)
        {
            var builder = new ContainerBuilder();

            var modulesToRegister = new List<IDependencyModule> { new DataAccessModule(), new ServiceDependencyModule() };

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            modulesToRegister.ForEach(m => m.Register(builder));

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(config);

            return app;
        }
    }
}