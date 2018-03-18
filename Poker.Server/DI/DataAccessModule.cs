using Autofac;
using Autofac.Core;
using Poker.Core.Abstraction;
using Poker.Core.DAL;
using Poker.Server.DataAccess;

namespace Poker.Server.DI
{
    public class DataAccessModule : IDependencyModule
    {
        public void Register(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>));
            var conString = string.Empty;
#if DEBUG
            conString = System.Configuration.ConfigurationManager.ConnectionStrings["PokerLocalConnection"].ConnectionString;
#else
            conString = System.Configuration.ConfigurationManager.ConnectionStrings["PokerHostConnection"].ConnectionString;
#endif


            builder.RegisterType<MssqlConnectionFactory>().As<IDbConnectionFactory>().WithParameter(new ResolvedParameter(
                                                                                        (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "connectionString",
                                                                                        (pi, ctx) => conString));
        }
    }
}