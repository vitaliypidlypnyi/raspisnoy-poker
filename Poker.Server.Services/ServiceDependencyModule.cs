using Autofac;
using Poker.Core.Abstraction;
using Poker.Core.Services;

namespace Poker.Server.Services
{
    public class ServiceDependencyModule : IDependencyModule
    {
        public void Register(ContainerBuilder builder)
        {
            builder.RegisterType<UserService>().As<IUserService>().InstancePerRequest();
        }
    }
}
