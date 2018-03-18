using Autofac;

namespace Poker.Core.Abstraction
{
    public interface IDependencyModule
    {
        void Register(ContainerBuilder builder);
    }
}
